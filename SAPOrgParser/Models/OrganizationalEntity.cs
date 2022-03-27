using System;

namespace SAPOrgParser.Models
{
    public abstract class OrganizationalEntity
    {
        /// <summary>
        /// Identifier
        /// </summary>
        public Guid Id { get; private set; }
        /// <summary>
        /// The name of the Component
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// The internal Organization Id of the Component.
        /// </summary>
        public string OrganizationId { get; private set; }
        public int NestedLevel { get; private set;}

        public OrganizationalEntity(Guid id, string name, string organizationId, int nestedLevel)
        {
            Id = id;
            Name = name;
            OrganizationId = organizationId;
            NestedLevel = nestedLevel;
        }
        public OrganizationalEntity(ParsedEntity entity)
        {
            Id = entity.uniqueId;
            Name = entity.Assignment;
            OrganizationId = entity.ID;
            NestedLevel = entity.nestLevel;
        }
    }
}
