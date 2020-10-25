using System.Collections.Generic;
using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Simulator;
using ToyRobotChallenge.Core.Table;
using Xunit;

namespace ToyRobotChallenge.Tests
{
    /// <summary>
    /// This test class testing some cases involved in different Simulators (like Interactive simulator , processing file simulator)
    /// </summary>
    public class TestSimulator
    {
        [Fact]
        public void TestExecutingAnInteractiveSimulatorByCommandStringShouldReturnCorrectResult()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);
            var interactiveSimulator = new InteractiveSimulator(toyRobot);

            var commandStringSeparator = " ";
            var ignoreCase = true;
            var commandList = new List<string>
            {
                "PLACE 1,2,NORTH MOVE",
                "MOVE1", // (Test key) this command shouldn't execute
                "MOVE RIGHT", 
                "PLACE 0,0,EAST", // using new place, ignoring all of previous actions
                "MOVE LEFT REPORT"
            };
            var commandString = string.Join(commandStringSeparator, commandList);

            //Assert 
            Assert.NotNull(interactiveSimulator);

            //ACT
            interactiveSimulator.Execute(commandString, new[] { commandStringSeparator }, ignoreCase);

            var expectX = 1;
            var expectY = 0;
            var expectDirection = Direction.NORTH;

            Assert.True(toyRobot.X == expectX && toyRobot.Y == expectY && toyRobot.Direction == expectDirection);
        }

        [Fact]
        public void TestExecutingAnInteractiveSimulatorUsingSensitiveByCommandStringShouldReturnRobotIsNotPlaced()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);
            var interactiveSimulator = new InteractiveSimulator(toyRobot);

            var commandStringSeparator = " ";
            var ignoreCase = false;
            var commandList = new List<string>
            {
                "place 1,2,NORTH", // TEST KEY: place is sensitive, so it will always return validation is false before executing command
                "REPORT"
            };
            var commandString = string.Join(commandStringSeparator, commandList);

            //Assert 
            Assert.NotNull(interactiveSimulator);

            //ACT
            interactiveSimulator.Execute(commandString, new[] { commandStringSeparator }, ignoreCase);

            // Assert
            Assert.False(toyRobot.IsPlaced);// no PLACE Command, so it will return false obviously
            Assert.False(toyRobot.X.HasValue);
            Assert.False(toyRobot.Y.HasValue);
            Assert.False(toyRobot.Direction.HasValue);
        }

        [Fact]
        public void TestExecutingACommandFileProcessingSimulatorByOneFileReturnCorrectResult()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);
            var fileExtension = ".txt";
            var commandFileProcessingSimulator = new CommandFileProcessingSimulator(toyRobot, fileExtension);

            var commandStringSeparator = " ";
            var ignoreCase = true;
            var fileCommandList = new List<string>
            {
                "./TestData/example1.txt"
            };
            var fileCommandString = string.Join(commandStringSeparator, fileCommandList);

            //Assert 
            Assert.NotNull(commandFileProcessingSimulator);

            //ACT
            commandFileProcessingSimulator.Execute(fileCommandString, new[] { commandStringSeparator }, ignoreCase);

            var expectX = 4;
            var expectY = 2;
            var expectDirection = Direction.SOUTH;

            Assert.True(toyRobot.X == expectX && toyRobot.Y == expectY && toyRobot.Direction == expectDirection);
        }

        [Fact]
        public void TestExecutingACommandFileProcessingSimulatorByMultipleFilesReturnCorrectResult()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);
            var fileExtension = ".txt";
            var commandFileProcessingSimulator = new CommandFileProcessingSimulator(toyRobot, fileExtension);

            var commandStringSeparator = " ";
            var ignoreCase = true;
            var fileCommandList = new List<string>
            {
                "./TestData/example1.txt",
                "./TestData/example.txt" // TEST KEY: will always get robot position and direction following last file result
            };
            var fileCommandString = string.Join(commandStringSeparator, fileCommandList);

            //Assert 
            Assert.NotNull(commandFileProcessingSimulator);

            //ACT
            commandFileProcessingSimulator.Execute(fileCommandString, new[] { commandStringSeparator }, ignoreCase);

            var expectX = 0;
            var expectY = 1;
            var expectDirection = Direction.NORTH;

            Assert.True(toyRobot.X == expectX && toyRobot.Y == expectY && toyRobot.Direction == expectDirection);
        }
    }
}
