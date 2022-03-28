using System;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class that represents a Position Entity.
    /// </summary>
    public class Position : OrganizationalEntity
    {   
        /// <summary>
        /// The Guid Identifier of the Component's parent Component.
        /// </summary>
        /// <remarks>
        /// All positions MUST have a parent component.
        /// </remarks>
        public Guid ParentComponentId { get; set; }
        public Person PersonAssigned { get; set;}
        private Position() : base() { }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="id">The Guid identifier of the Position.</param>
        /// <param name="name">A string containing the name of the Position.</param>
        /// <param name="organizationId">A string containing the Organization ID of the Position.</param>
        public Position(Guid id, string name, string organizationId, int nestedLevel) : base(id, name, organizationId, nestedLevel)
        {  
        }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="entity">A <see cref="ParsedEntity"/></param>
        public Position(ParsedEntity entity) : base(entity)
        {   
        }
        /// <summary>
        /// Updates the Parent Component Id of the Position.
        /// </summary>
        /// <param name="parentComponentId"></param>
        public void UpdateParentComponentId(Guid parentComponentId)
        {
            if (parentComponentId == Guid.Empty)
            {
                throw new InvalidOperationException("Cannot assign a Position's Parent Component Id to an Empty GUID.");
            }
            ParentComponentId = parentComponentId;
        }
        public void AssignPerson(Person toAssign)
        {
            PersonAssigned = toAssign;
        }
        public void RemoveAssignee()
        {
            PersonAssigned = null;
        }
    }
}
