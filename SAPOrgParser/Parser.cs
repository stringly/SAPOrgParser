using SAPOrgParser.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAPOrgParser
{
    /// <summary>
    /// Class that parses the string array into objects and collections.
    /// </summary>
    public class Parser
    {        
        private string[] _lines;
        /// <summary>
        /// Returns the String Array of lines to be parsed as a Readonly Collection
        /// </summary>
        public List<string> Lines => _lines.ToList();
        private readonly List<ParsedEntity> _parsedEntities;
        /// <summary>
        /// Returns the collection of Parsed Entities as a readonly Collection.
        /// </summary>
        public List<ParsedEntity> ParsedEntities => _parsedEntities;
        private readonly List<OrganizationalEntity> _organizationalEntities;
        /// <summary>
        /// Returns the collection of Organizational Entities as a readonly Collection.
        /// </summary>
        public List<OrganizationalEntity> OrganizationalEntities => _organizationalEntities;
        private List<Component> _nonNestedComponentList;
        /// <summary>
        /// Returns the list of Parsed Components prior to any attempt to establsh parent/child relationships.
        /// </summary>
        public List<Component> NonNestedComponentList => _nonNestedComponentList;
        private readonly List<SimpleComponent> _componentFlatList;
        /// <summary>
        /// Returns a flattened list of Components with their Parent Components, Positions, and Persons populated.
        /// </summary>
        /// <returns></returns>
        public List<SimpleComponent> ComponentsFlatList => _componentFlatList;
        private readonly List<SimplePosition> _positionsFlatList;
        /// <summary>
        /// Returns a flat list of <see cref="SimplePosition"/> objects.
        /// </summary>
        public List<SimplePosition> PositionsFlatList => _positionsFlatList;

        private readonly List<SimplePerson> _personsFlatList;
        /// <summary>
        /// Returns a flat list of <see cref="SimplePerson"/> objects.
        /// </summary>
        public List<SimplePerson> PersonsFlatList => _personsFlatList;

        private Component _department;
        /// <summary>
        /// Returns the final, top-level component.
        /// </summary>
        public Component Department => _department;
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        public Parser() 
        {   
            _parsedEntities = new List<ParsedEntity>();
            _organizationalEntities = new List<OrganizationalEntity>();
            _nonNestedComponentList = new List<Component>();
            _componentFlatList = new List<SimpleComponent>();
            _positionsFlatList = new List<SimplePosition>();
            _personsFlatList = new List<SimplePerson>();

        }
        /// <summary>
        /// Parses the string array.
        /// </summary>
        /// <param name="lines">A string array containing the lines to parse.</param>
        /// <param name="linesToSkip">An optional parameter indicating how many initial lines to skip.</param>
        /// <remarks>
        /// This method assumes that the first 2 lines of the provided input are header rows to be skipped.
        /// </remarks>
        public void Parse(string[] lines, int linesToSkip = 2)
        {
            // ensure the lines parameter is not empty.
            if (lines == null) { throw new ArgumentException("Parse String Array failed: lines parameter cannot be null."); }
            _lines = lines;
            // create a new entity for each line and add it to the collection.
            // use explicit loop to skip the first 2 lines, which are likely to be the header line and a spacer line
            for( var i = linesToSkip; i < _lines.Length; i++)
            {
                _parsedEntities.Add(new ParsedEntity(_lines[i]));
            }
            try
            {
                MapToNonNestedComponentList();
                MapParentAndChildComponents();
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        private void MapToNonNestedComponentList()
        {
            if (_parsedEntities.Count == 0) { throw new InvalidOperationException("Map to Organizational Entities Failed: There are no Parsed Entities to output."); }
            List<Component> components = new List<Component>();
            for (int i = 0; i < _parsedEntities.Count; i++)
            {
                if (_parsedEntities[i].EntityTypeCode == "O")
                {
                    Component c = new Component(_parsedEntities[i]);
                    List<Position> positions = new List<Position>();
                    for (int j = i+1; j < ParsedEntities.Count; j++)
                    {
                        Position p;
                        if (_parsedEntities[j].EntityTypeCode == "S")
                        {
                            p = new Position(_parsedEntities[j]);
                            for (int k = j+1; k < ParsedEntities.Count; k++)
                            {
                                if (_parsedEntities[k].EntityTypeCode == "P")
                                {
                                    Person person = new Person(_parsedEntities[k]);
                                    p.AssignPerson(person);
                                    break;
                                }
                            }
                            c.AddPosition(p);
                        } 
                        else if (_parsedEntities[j].EntityTypeCode == "O")
                        {
                            i = j-1;
                            break;
                        }
                    }
                    _nonNestedComponentList.Add(c);
                }
            }
        }
        private void MapParentAndChildComponents()
        {
            if (_nonNestedComponentList.Count == 0) { throw new InvalidOperationException("Map Parent/Child Components failed: There are no Non-Nested Components to output."); }
            // we need a copy of the nonNested List, as the processing will transform the list as it works through it.
            List<Component> workingList = new List<Component>(_nonNestedComponentList);
            while (workingList.Count > 1) 
            {
                // continue processing until the list is left with only 1 component, which should be the top-level master component with all children nested.
                ProcessComponentList(workingList);
            }
            // set the top level "Department" component to the single component left on the list.
            _department = workingList.FirstOrDefault();
        }
        /// <summary>
        /// This method iterates backwards through a list, removes the last item, attempts to find that item's parent, and returns the transformed list.
        /// </summary>
        /// <param name="workingList">A list of <see cref="Component"/> objects.</param>
        private void ProcessComponentList(List<Component> workingList)
        {
            // retrieve the last item   
            Component current = workingList.Last();
            // remove from list
            workingList.Remove(current);
            // attempt to find parent.
            Component parent = FindParent(current.NestedLevel, workingList);
            // if parent found, assign current component as a child of the parent.
            if (parent != null)
            {
                parent.AddChildComponent(current);
            }
            // TODO: Each Component, Position, and Person needs to be in a flat list with Id/ParentId.
            // add the component, which should now have it's parentComponentId property set, to the flat file list.
            _componentFlatList.Add(new SimpleComponent(current));
            int positionCounter = 0;
            foreach(Position p in current.Positions)
            {
                _positionsFlatList.Add(new SimplePosition(p, positionCounter));
                positionCounter++;
                if(p.PersonAssigned != null)
                {
                    SimplePerson toAdd = new SimplePerson(p.PersonAssigned);
                    toAdd.PositionId = p.Id;
                    _personsFlatList.Add(toAdd);
                }
            }
        }
        private Component FindParent(int currentLevel, List<Component> workingList)
        {
            // work through the list in reverse to find the first component with a "Nested Level" one less than the target
            // because the way the list is ordered, the first component found should be the parent.
            for (var i = workingList.Count - 1; i > -1; i--)
            {
                if (workingList[i].NestedLevel == (currentLevel - 1))
                {
                    return workingList[i];
                }
            }
            throw new InvalidOperationException("Find Parent failed: Cannot locate a parent for Component.");
        }
    }
}
