using System;
using System.Collections.Generic;
using System.Text;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class used to store Simple persons info.
    /// </summary>
    public class SimplePerson
    {
        /// <summary>
        /// The Id of the Person.
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the person.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Code for the person.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The Id of the Person's Position.
        /// </summary>
        public Guid PositionId { get; set; }
        public SimplePerson() { }
        /// <summary>
        /// Creates a new instance of the Class.
        /// </summary>
        /// <param name="id">The Person's Id.</param>
        /// <param name="name">The person's name.</param>
        /// <param name="code">The Code of the person.</param>
        public SimplePerson(Guid id, string name, string code)
        {
            Id = id;
            Name = name;
            Code = code;
        }
        /// <summary>
        /// Creates a new instance of the Class.
        /// </summary>
        /// <param name="p">A <see cref="Person"/> object.</param>
        public SimplePerson(Person p)
        {
            Id = p.Id;
            Name = p.Name;
            Code = p.OrganizationId;
        }
    }
}
