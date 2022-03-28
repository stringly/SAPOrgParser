using System;
using System.Collections.Generic;
using System.Text;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class used to store simple Position Info
    /// </summary>
    public class SimplePosition
    {
        /// <summary>
        /// The Id of the Position
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The Name of the Position
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Id of the Position's Parent Component.
        /// </summary>
        public Guid ParentComponentId { get; set; }
        /// <summary>
        /// The Position's code.
        /// </summary>
        public string Code { get; set; }
        public SimplePosition() { }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="id">The Id of the Position.</param>
        /// <param name="name">The name of the Component.</param>
        /// <param name="parentComponentId">The Id of the Position's Parent Component.</param>
        /// <param name="code">The code for the Position/</param>
        public SimplePosition(Guid id, string name, Guid parentComponentId, string code)
        {
            Id = id;
            Name = name;
            ParentComponentId = parentComponentId;
            Code = code;
        }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="p">A <see cref="Position"/> object.</param>
        public SimplePosition(Position p)
        {
            Id = p.Id;
            Name = p.Name;
            ParentComponentId = p.ParentComponentId;
            Code = p.OrganizationId;
        }
    }
}
