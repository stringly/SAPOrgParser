using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SAPOrgParser.Models;
using Shouldly;

namespace Application.UnitTests
{
    public class ParsedEntityTests
    {
        [Fact]
        public void Given_InputString_CanParseToTab()
        {
            // Arrange
            string input = "	Police Department												Police	O  10000074		Abdul Al'Aziz, Kosg M	01/01/1950	Unlimited";
            // /Act
            var ParsedEntity = new ParsedEntity(input);

            // Assert
            ParsedEntity.Assignment.ShouldBe("Police Department");
            ParsedEntity.Code.ShouldBe("Police");
            ParsedEntity.EntityTypeCode.ShouldBe("O");
            ParsedEntity.ID.ShouldBe("10000074");
            ParsedEntity.nestLevel.ShouldBe(1);         
        }
    }
}
