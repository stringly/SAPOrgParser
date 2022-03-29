using System;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class used to store simple Component Info
    /// </summary>
    public class SimpleComponent
    {
        /// <summary>
        /// The Id of the Component
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The name of the Component
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The Id of the Component's Parent Component.
        /// </summary>
        public Guid? ParentComponentId { get; set; }
        /// <summary>
        /// The Code for the Component.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Integer used to set the order of the Component among it's parent's child components.
        /// </summary>
        public int LineupPosition { get; set; }
        public SimpleComponent() {}
        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="id">The Id of the Component</param>
        /// <param name="name">The name of the Component</param>
        /// <param name="parentComponentId">The Id of the Component's parent Component.</param>
        /// <param name="code">The code for the Component.</param>
        public SimpleComponent(Guid id, string name, Guid? parentComponentId, string code, int lineupPosition = 0)
        {
            id = Id;
            Name = name;
            ParentComponentId = parentComponentId;
            Code = code;
            LineupPosition = lineupPosition;
        }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="c">A <see cref="Component"/> object.</param>
        public SimpleComponent(Component c)
        {
            Id = c.Id;
            Name = c.Name;
            ParentComponentId = c.ParentComponentId;
            Code = c.OrganizationId;
            LineupPosition = c.LineupPosition;
        }
    }
}
