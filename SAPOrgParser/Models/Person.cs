using System;

namespace SAPOrgParser.Models
{
    public class Person : OrganizationalEntity
    {
        /// <summary>
        /// Creates a new instance of the Class.
        /// </summary>
        /// <param name="id">The Guid Id of the Person.</param>
        /// <param name="name">A string containing the name of the Person.</param>
        /// <param name="organizationId">A string containing the Organization Id of the person.</param>
        public Person(Guid id, string name, string organizationId, int nestedLevel) : base(id, name, organizationId, nestedLevel)
        {
        }
        /// <summary>
        /// Creates a new instance of the Class.
        /// </summary>
        /// <param name="entity">A <see cref="ParsedEntity"/> containing details for a person.</param>
        public Person(ParsedEntity entity) : base(entity)
        {
        }
    }
}