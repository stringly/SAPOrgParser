using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class that represents an Organizational Component
    /// </summary>
    public class Component : OrganizationalEntity
    {   
        /// <summary>
        /// The Guid Identifier of the Component's parent Component.
        /// </summary>
        /// <remarks>
        /// This will be null for the top-level component in the org.
        /// </remarks>
        public Guid? ParentComponentId { get; set; }
        public int LineupPosition { get; set; } = 0;

        public List<Component> ChildComponents { get; set;}
        public List<Position> Positions { get; set;}
        private Component() : base() { }
        /// <summary>
        /// Creates a new instance of the Class.
        /// </summary>
        public Component(Guid id, string name, string organizationId, int nestedLevel) : base(id, name, organizationId, nestedLevel)
        {
            ChildComponents = new List<Component>();
            Positions = new List<Position>();
        }
        public Component(ParsedEntity entity) : base(entity)
        {
            ChildComponents = new List<Component>();
            Positions = new List<Position>();
        }
        /// <summary>
        /// Updates the Parent Component Id of the Component.
        /// </summary>
        /// <param name="parentComponentId">The GUID Id of the Parent Component</param>
        /// <remarks>The topmost component will have a ParentComponentId equal to a null Guid.</remarks>
        public void UpdateParentComponentId(Guid? parentComponentId)
        {
            if (parentComponentId == Guid.Empty)
            {
                throw new InvalidOperationException("Cannot assign a Component's Parent Component Id to an Empty GUID.");
            }
            ParentComponentId = parentComponentId;
        }
        public void AddChildComponent(Component toAdd)
        {
            if (ChildComponents.Any(x => x.Id == toAdd.Id))
            {
                // component already exists in child collection
                throw new InvalidOperationException($"Component {toAdd.Name} is already a Child of Component {Name}.");
            }
            toAdd.LineupPosition = ChildComponents.Count();
            toAdd.ParentComponentId = Id;
            ChildComponents.Add(toAdd);
        }
        public void RemoveChildComponent(Component toRemove)
        {
            if (!ChildComponents.Any(x => x.Id == toRemove.Id))
            {
                // component does not exist in child collection
                throw new InvalidOperationException($"Component {toRemove.Name} is not a Child of Component {Name}.");
            }
            toRemove.ParentComponentId = null;
            toRemove.LineupPosition = 0;
            ChildComponents.Remove(toRemove);
        }
        /// <summary>
        /// Adds a position to the Component's Positions collection.
        /// </summary>
        /// <param name="toAdd">The <see cref="Position"/> to add.</param>
        /// <remarks>
        /// A Position cannot be "removed" from a Component; it must be re-assigned by invoking the .AddPosition method on the Component to which the Position will be moved.
        /// </remarks>
        public void AddPosition(Position toAdd)
        {
            if (Positions.Any(x => x.Id == toAdd.Id))
            {
                // position already exists in child collection
                throw new InvalidOperationException($"Positions {toAdd.Name} is already a member of Component {Name}'s Position Collection.");
            }
            toAdd.UpdateParentComponentId(Id);
            Positions.Add(toAdd);
        }
    }
}
