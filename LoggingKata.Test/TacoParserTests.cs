using System;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;
using LoggingKata;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Theory]
        [InlineData("-86.889051, 33.556383, Taco Bell Birmingham")] // Should parse (longitude, latitude, location).
        public void ShouldParse(string str)
        {
            // Arrange
            var parser = new TacoParser();

            // Act
            var result = parser.Parse(str);

            // Assert
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData(null)] // Cannot parse null input.
        [InlineData("")] // Cannot parse empty strings.
        [InlineData("1234, 1234")] // Cannot parse arrays of length < 2.
        [InlineData("ABCD, 1234, Location")] // Longitude must have numeric entry.  
        [InlineData("1234, ABCD, Location")] // Latitude must have numeric entry.
        [InlineData("-190.05, 85.50, Location")] // Longitude out of range.
        [InlineData("170.02, 100.20, Location")] // Latitude out of range.
        public void ShouldFailParse(string str)
        {
            // Arrange
            var parser = new TacoParser();

            // Act
            var result = parser.Parse(str);

            // Assert
            Assert.Null(result);
        }
    }
}
