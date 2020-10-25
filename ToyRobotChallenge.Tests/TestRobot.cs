using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Table;
using Xunit;

namespace ToyRobotChallenge.Tests
{
    /// <summary>
    /// This test class testing some cases involved in Robot feature (like PLACE, MOVE, LEFT, RIGHT)
    /// </summary>
    public class TestRobot
    {
        [Fact]
        public void TestPlacingAToyRobotShouldBePlacedCorrectly()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            uint xPosition = 1;
            uint yPosition = 3;
            Direction robotDirection = Direction.EAST;

            // Assert 
            // make sure robot is not placed
            Assert.True(!toyRobot.X.HasValue && !toyRobot.Y.HasValue && !toyRobot.Direction.HasValue && !toyRobot.IsPlaced);

            // ACT
            toyRobot.Place(xPosition, yPosition, robotDirection);

            // make sure robot is placed
            Assert.True(toyRobot.IsPlaced);
            Assert.True(toyRobot.X.HasValue && toyRobot.X.Value == xPosition);
            Assert.True(toyRobot.Y.HasValue && toyRobot.Y.Value == yPosition);
            Assert.True(toyRobot.Direction.HasValue && toyRobot.Direction.Value == robotDirection);
        }

        [Fact]
        public void TestPlacingAToyRobotShouldBePlacedWrongly()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            uint xPositionOutOfMaximum = 5;
            uint yPosition = 3;
            Direction robotDirection = Direction.EAST;

            // ACT
            toyRobot.Place(xPositionOutOfMaximum, yPosition, robotDirection);

            // Assert
            // make sure robot is NOT placed
            Assert.False(toyRobot.IsPlaced);
            Assert.False(toyRobot.X.HasValue);
            Assert.False(toyRobot.Y.HasValue);
            Assert.False(toyRobot.Direction.HasValue);
        }

        [Theory]
        [InlineData(1, 3, Direction.NORTH, 1, 4)]
        [InlineData(2, 1, Direction.WEST, 1, 1)]
        [InlineData(4, 2, Direction.EAST, 4, 2)] // this is key, because robot has no way can be move to
        [InlineData(0, 0, Direction.SOUTH, 0, 0)] // this is key, because robot has no way can be move to
        public void TestMovingAToyRobotShouldBeMoveToCorrectPosition(uint xPosition, uint yPosition, Direction direction, uint expectXPosition, uint expectYPosition)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            toyRobot.Place(xPosition, yPosition, direction);

            // ACT
            toyRobot.Move();

            // make sure robot moved correctly
            Assert.True(toyRobot.X == expectXPosition);
            Assert.True(toyRobot.Y == expectYPosition);
        }

        [Theory]
        [InlineData(Direction.NORTH, Direction.WEST)]
        [InlineData(Direction.WEST, Direction.SOUTH)]
        [InlineData(Direction.SOUTH, Direction.EAST)]
        [InlineData(Direction.EAST, Direction.NORTH)]
        public void TestTurningLeftAToyRobotShouldBeFacingTheCorrectDirection(Direction direction, Direction expectDirection)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            toyRobot.Place(0, 0, direction);

            // ACT
            toyRobot.TurnLeft();

            // make sure robot direction is correct
            Assert.True(toyRobot.Direction == expectDirection);
        }

        [Theory]
        // all wrong test data for turning left
        [InlineData(Direction.NORTH, Direction.SOUTH)]
        [InlineData(Direction.WEST, Direction.EAST)]
        [InlineData(Direction.SOUTH, Direction.NORTH)]
        [InlineData(Direction.EAST, Direction.WEST)]
        public void TestTurningLeftAToyRobotShouldBeFacingTheWrongDirection(Direction direction, Direction expectDirection)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            toyRobot.Place(0, 0, direction);

            // ACT
            toyRobot.TurnLeft();

            // make sure robot direction is wrong
            Assert.False(toyRobot.Direction == expectDirection);
        }

        [Theory]
        [InlineData(Direction.NORTH, Direction.EAST)]
        [InlineData(Direction.EAST, Direction.SOUTH)]
        [InlineData(Direction.SOUTH, Direction.WEST)]
        [InlineData(Direction.WEST, Direction.NORTH)]
        public void TestTurningRightAToyRobotShouldBeFacingTheCorrectDirection(Direction direction, Direction expectDirection)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            toyRobot.Place(0, 0, direction);

            // ACT
            toyRobot.TurnRight();

            // make sure robot direction is correct
            Assert.True(toyRobot.Direction == expectDirection);
        }

        [Theory]
        // all wrong test data for turning right
        [InlineData(Direction.NORTH, Direction.SOUTH)]
        [InlineData(Direction.WEST, Direction.EAST)]
        [InlineData(Direction.SOUTH, Direction.NORTH)]
        [InlineData(Direction.EAST, Direction.WEST)]
        public void TestTurningRightAToyRobotShouldBeFacingTheWrongDirection(Direction direction, Direction expectDirection)
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);
            var toyRobot = new ToyRobot(table);

            toyRobot.Place(0, 0, direction);

            // ACT
            toyRobot.TurnRight();

            // make sure robot direction is wrong
            Assert.False(toyRobot.Direction == expectDirection);
        }

        [Fact]
        // combining all actions of robots to make sure it returns expect result
        public void TestAToyRobotWithAllActionsShouldBeReturnCorrectPosAndDirection()
        {
            // Arrange
            uint width = 5;
            uint height = 5;
            var table = new Table(width, height);

            // initializing position for a toy robot
            var toyRobot = new ToyRobot(table);

            // add PLACE action
            toyRobot.Place(1, 1, Direction.NORTH);

            // add three move actions
            toyRobot.Move();
            toyRobot.Move();
            toyRobot.Move();

            // then turn right 
            toyRobot.TurnRight();

            // then turn left 
            toyRobot.TurnLeft();

            // Testing key point,  just move once again, should ignore the Move step, because robot position beyond table height
            toyRobot.Move();

            var expectXPostion = 1;
            var expectYPostion = 4;
            var expectDirection = Direction.NORTH;

            // Assert
            Assert.True(toyRobot.X == expectXPostion && toyRobot.Y == expectYPostion && toyRobot.Direction == expectDirection);
        }
    }
}
