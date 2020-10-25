using System;
using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// A Place command for processing robot command
    /// </summary>
    public class PlaceCommand : ICommand
    {
        /// <summary>
        /// x position which is placed to
        /// </summary>
        private readonly uint _xPosition;

        /// <summary>
        /// y position which is placed to
        /// </summary>
        private readonly uint _yPosition;

        /// <summary>
        /// direction which is placed to
        /// </summary>
        private readonly Direction _direction;

        /// <summary>
        /// initialize place command ctor
        /// </summary>
        /// <param name="xPosition">x position which is placed to</param>
        /// <param name="yPosition">y position which is placed to</param>
        /// <param name="direction">direction which is placed to</param>
        public PlaceCommand(uint xPosition, uint yPosition, Direction direction)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;
            _direction = direction;
        }

        /// <summary>
        /// executing place action by the robot
        /// </summary>
        /// <param name="robot">the robot</param>
        public void Execute(IRobot robot)
        {
            robot?.Place(_xPosition, _yPosition, _direction);
        }

        /// <summary>
        /// validate if the Place command is valid for a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        /// <returns>validate success or failed</returns>
        public bool Validate(IRobot robot)
        {
            // we haven't specific logic need to validate before executing place command
            return true;
        }
    }

    /// <summary>
    /// A Move command for processing robot command
    /// </summary>
    public class MoveCommand : ICommand
    {
        /// <summary>
        /// executing move action by the robot
        /// </summary>
        /// <param name="robot">the robot</param>
        public void Execute(IRobot robot)
        {
            robot?.Move();
        }

        /// <summary>
        /// validate if the Move command is valid for a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        /// <returns>validate success or failed</returns>
        public bool Validate(IRobot robot)
        {
            return robot?.IsPlaced ?? false;
        }
    }

    /// <summary>
    /// A Left command for processing robot command
    /// </summary>
    public class LeftCommand : ICommand
    {
        /// <summary>
        /// executing turn left action by the robot
        /// </summary>
        /// <param name="robot">the robot</param>
        public void Execute(IRobot robot)
        {
            robot?.TurnLeft();
        }

        /// <summary>
        /// validate if the Left command is valid for a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        /// <returns>validate success or failed</returns>
        public bool Validate(IRobot robot)
        {
            return robot?.IsPlaced ?? false;
        }
    }

    /// <summary>
    /// A Right command for processing robot command
    /// </summary>
    public class RightCommand : ICommand
    {
        /// <summary>
        /// executing turn right action by the robot
        /// </summary>
        /// <param name="robot">the robot</param>
        public void Execute(IRobot robot)
        {
            robot?.TurnRight();
        }

        /// <summary>
        /// validate if the Right command is valid for a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        /// <returns>validate success or failed</returns>
        public bool Validate(IRobot robot)
        {
            return robot?.IsPlaced ?? false;
        }
    }

    /// <summary>
    /// A Report command for processing robot command
    /// </summary>
    public class ReportCommand : ICommand
    {
        /// <summary>
        /// record a string as a report
        /// </summary>
        /// <param name="robot">the robot</param>
        public void Execute(IRobot robot)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"My X position is {robot.X} and my Y position {robot.Y}. I'm facing {robot.Direction.ToString()?.ToUpper()}");
            Console.ResetColor();
            Console.WriteLine("");
        }

        /// <summary>
        /// validate if the Report command is valid for a robot
        /// </summary>
        /// <param name="robot">the robot</param>
        /// <returns>validate success or failed</returns>
        public bool Validate(IRobot robot)
        {
            return robot?.IsPlaced ?? false;
        }
    }
}
