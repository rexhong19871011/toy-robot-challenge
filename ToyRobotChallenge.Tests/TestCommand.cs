using System.Collections.Generic;
using ToyRobotChallenge.Core.Command;
using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Table;
using ToyRobotChallenge.Core.Utils;
using Xunit;

namespace ToyRobotChallenge.Tests
{
    /// <summary>
    /// This test class testing some cases involved in Command feature
    /// </summary>
    public class TestCommand
    {
        [Theory]
        [InlineData("Place", true, true)]
        [InlineData("Place", false, false)]
        [InlineData("move1", true, false)]
        [InlineData("left", true, true)]
        [InlineData("right", true, true)]
        [InlineData("RIGHT", false, true)]
        [InlineData("Report", true, true)]
        [InlineData("1Report", true, false)]
        public void TestCastingCommandTypeShouldReturnCorrectType(string commandType, bool ignoreCase, bool expectCastingResult)
        {
            // Act
            var castingResult = EnumUtils.TryParse(commandType, ignoreCase, out CommandType commandTypeResult);

            // Assert
            Assert.True(castingResult == expectCastingResult);
        }

        [Fact]
        public void TestCreatingCommandsByFactoryShouldReturnCommandListIsEmpty()
        {
            // Arrange
            var commandStringSeparator = " ";
            var ignoreCase = true;
            var commandList = new List<string>
            {
                //all wrong command
                "PLACE X,Y,NORTH",
                "MOVE1",
                "REPORT1",
                "LEFT1",
                "RIGHT1"
            };

            var commandString = string.Join(commandStringSeparator, commandList);

            var expectCommandCount = 0;

            // ACT
            var result = new CommandFactory().CreateCommands(commandString, new[] { commandStringSeparator }, ignoreCase);

            // Assert
            Assert.True(result.Count == expectCommandCount);
        }

        [Fact]
        public void TestCreatingCommandsByFactoryShouldReturnCorrectCount()
        {
            // Arrange
            var commandStringSeparator = " ";
            var ignoreCase = true;
            var commandList = new List<string>
            {
                "PLACE 1,1,NORTH",
                "MOVE1", //wrong one
                "MOVE",
                "REPORT",
                "LEFT",
                "RIGHT"
            };

            var commandString = string.Join(commandStringSeparator, commandList);

            var expectCommandCount = 5;

            // ACT
            var result = new CommandFactory().CreateCommands(commandString, new[] { commandStringSeparator }, ignoreCase);

            // Assert
            Assert.True(result.Count == expectCommandCount);
        }

        [Fact]
        public void TestCreatingCommandsWithSensitiveCaseByFactoryShouldReturnCorrectCount()
        {
            // Arrange
            var commandStringSeparator = " ";
            var ignoreCase = false;
            var commandList = new List<string>
            {
                "PLACE 1,1,NORTH",
                "MOVE1", //wrong one
                "Move", // wrong one because we didn't ignore case
                "REPORT",
                "LEFT",
                "RIGHT"
            };

            var commandString = string.Join(commandStringSeparator, commandList);

            var expectCommandCount = 4;

            // ACT
            var result = new CommandFactory().CreateCommands(commandString, new[] { commandStringSeparator }, ignoreCase);

            // Assert
            Assert.True(result.Count == expectCommandCount);
        }

        [Fact]
        public void TestExecutingInTheCommandHandlerByCommandStringReturnCorrectPosAndDirectionInAToyRobot()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table)
            {
                X = 0,
                Y = 0,
                Direction = Direction.NORTH
            };
            var commandStringSeparator = " ";
            var ignoreCase = true;
            string commandString = "PLACE 1,2,NORTH MOVE MOVE RIGHT REPORT PLACE 3,2,EAST REPORT";

            // ACT
            var commandHandler = new CommandHandler(new CommandFactory(), toyRobot);

            Assert.NotNull(commandHandler);

            commandHandler.Handle(commandString, new[] { commandStringSeparator }, ignoreCase);

            var expectX = 3;
            var expectY = 2;
            var expectDirection = Direction.EAST;

            // Assert
            Assert.True(toyRobot.X == expectX && toyRobot.Y == expectY && toyRobot.Direction == expectDirection);
        }


        [Fact]
        public void TestValidationOfCommandHandlerIfNoPlaceCommandShouldBeReturnValidationFailed()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table)
            {
                // we didn't set up initial position and direction for the toy robot
            };
            var commandStringSeparator = " ";
            var ignoreCase = true;
            string commandString = "NORTH MOVE  REPORT"; // no PLACE command in there, which means robot IS NOT Placed

            // ACT
            var commandHandler = new CommandHandler(new CommandFactory(), toyRobot);

            Assert.NotNull(commandHandler);

            // Test key: when we use command handler it will auto validate  each command before executing command
            commandHandler.Handle(commandString, new[] { commandStringSeparator }, ignoreCase);

            // Assert
            Assert.False(toyRobot.IsPlaced);// no PLACE Command, so it will return false obviously
            Assert.False(toyRobot.X.HasValue);
            Assert.False(toyRobot.Y.HasValue);
            Assert.False(toyRobot.Direction.HasValue);
        }
    }
}
