using System;
using System.Linq;

namespace SAPOrgParser.Models
{
    /// <summary>
    /// Class that represents an Org Entity parsed from a string.
    /// </summary>
    public class ParsedEntity
    {
        /// <summary>
        /// The Id of the Entity.
        /// </summary>
        public Guid uniqueId { get; } = Guid.NewGuid();
        /// <summary>
        /// The name of the entity.
        /// </summary>
        public string Assignment { get; set; }
        /// <summary>
        /// The text contained in the "Code" field of the SAP flat file.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// The Entity Type Code of the Entity.
        /// </summary>
        public string EntityTypeCode { get; set; }
        /// <summary>
        /// The SAP ID of the entity.
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// An integer representinog the nesting level of the entity.
        /// </summary>
        public int nestLevel { get; set; }
        /// <summary>
        /// Creates a new instance of the class
        /// </summary>
        /// <param name="assignment">A string containing the entity's name.</param>
        /// <param name="code">A string containing a code for the entity.</param>
        /// <param name="entityTypeCode">A string containing the Entity Type code of the entity.</param>
        /// <param name="id">A string containing the Id of the entity.</param>
        /// <param name="nestLevel">An integer representing the entity's relative nesting level in the Organization.</param>
        public ParsedEntity(
            string assignment,
            string code,
            string entityTypeCode,
            string id,
            int nestLevel)
        {
            this.Assignment = assignment;
            this.Code = code;
            this.EntityTypeCode = entityTypeCode;
            this.ID = id;
            this.nestLevel = nestLevel;
        }
        /// <summary>
        /// Creates a new instance of the class.
        /// </summary>
        /// <param name="input">A string containing the quasi-tab-delimited data for an entity obtained from the SAP Organizational Flat File output.</param>
        public ParsedEntity(string input)
        {
            var splitString = input.Split(Constants.Tab);
            int count = 0;
            
            foreach (string s in splitString)
            {
                if (s.Equals(""))
                {
                    count++;
                }
                else
                {
                    nestLevel = count;
                    splitString = splitString.Where(s => !s.Equals("")).ToArray();
                    completeParsing(splitString);
                    break;
                }
            }
        }
        private void completeParsing(string[] detailString)
        {
            try
            {
                Assignment = detailString[0];
                Code = detailString[1];
                var splitCode = detailString[2].Split(" ");
                EntityTypeCode = splitCode[0];
                ID = splitCode[2];
            }
            catch(Exception ex)
            {
                throw new Exception(innerException: ex, message: $"Failed to parse string: {String.Join(",", detailString)}");
            }
        }
    }
}
