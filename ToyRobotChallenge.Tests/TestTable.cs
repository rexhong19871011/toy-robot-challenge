using ToyRobotChallenge.Core.Table;
using Xunit;

namespace ToyRobotChallenge.Tests
{
    /// <summary>
    /// This test class testing some cases involved in Table feature 
    /// </summary>
    public class TestTable
    {
        [Fact]
        public void TestTableInitialSizeShouldBeSetupSuccess()
        {
            // Arrange
            uint width = 5;
            uint height = 5;

            // Act
            var table = new Table(width, height);

            // Assert
            Assert.True(table.Width == width && table.Height == height);
        }

        [Theory]
        [InlineData(3, 4, true)] // table position validation successfully
        [InlineData(6, 6, false)]  // table position validation failed
        public void TestTableSpecifiedPositionWithMultiCaseShouldReturnExpectResult(uint xPosition, uint yPosition, bool expectValid)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);

            // Act
            var isValid = table.IsValidPosition(xPosition, yPosition);

            // Assert
            Assert.True(expectValid == isValid);
        }
    }
}
