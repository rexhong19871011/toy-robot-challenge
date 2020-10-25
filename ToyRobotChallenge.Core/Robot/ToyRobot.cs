using System;
using ToyRobotChallenge.Core.Table;

namespace ToyRobotChallenge.Core.Robot
{
    /// <summary>
    /// A toy robot including common properties and actions
    /// </summary>
    public class ToyRobot : IRobot
    {
        /// <summary>
        /// get/set the table that the robot is to move around in
        /// </summary>
        public ITable Table { get; set; }

        /// <summary>
        /// X position of robot in the table
        /// </summary>
        public uint? X { get; set; }

        /// <summary>
        /// Y position of robot in the table
        /// </summary>
        public uint? Y { get; set; }

        /// <summary>
        /// Direction where the toy robot facing to
        /// </summary>
        public Direction? Direction { get; set; }

        /// <summary>
        /// Check if the robot is placed or not, no matter where the robot position is
        /// </summary>
        public bool IsPlaced => X.HasValue && Y.HasValue && Direction.HasValue;

        /// <summary>
        /// initialize toy robot ctor
        /// </summary>
        /// <param name="table">table for the toy robot</param>
        public ToyRobot(ITable table)
        {
            Table = table;
        }

        /// <summary>
        /// Place will put the toy robot on the table in position X,Y and facing NORTH, SOUTH, EAST or WEST.
        /// </summary>
        /// <param name="x">X position of robot in the table</param>
        /// <param name="y">Y position of robot in the table</param>
        /// <param name="direction">direction where the toy robot facing to</param>
        /// <exception cref="ArgumentNullException">throw if x,y position or direction is null</exception>
        /// <exception cref="NullReferenceException">throw if table is null</exception>
        public void Place(uint? x, uint? y, Direction? direction)
        {
            if (!x.HasValue) throw new ArgumentNullException(nameof(x), "X position of robot cannot be null");

            if (!y.HasValue) throw new ArgumentNullException(nameof(y), "X position of robot cannot be null");

            if (!direction.HasValue) throw new ArgumentNullException(nameof(direction), "direction of robot cannot be null");

            if (Table == null)  throw new NullReferenceException("table cannot be null");

            if (!Table.IsValidPosition(x.Value, y.Value)) return;

            // set new place for current robot after validation
            X = x.Value;
            Y = y.Value;
            Direction = direction.Value;
        }

        /// <summary>
        ///  move the toy robot one unit forward
        /// </summary>
        /// <exception cref="InvalidOperationException">throw exception if current direction of the robot is unrecognized</exception>
        public void Move()
        {
            if (Table == null) throw new NullReferenceException("table cannot be null");

            if (!IsPlaced) return;

            // calculate new X, Y position of the robot according to current X position
            var newX = X;
            var newY = Y;
            switch (Direction)
            {
                case Robot.Direction.EAST:
                    newX = X.Value + 1; break;
                case Robot.Direction.WEST:
                    newX = X.Value - 1; break;
                case Robot.Direction.NORTH:
                    newY = Y.Value + 1; break;
                case Robot.Direction.SOUTH:
                    newY = Y.Value - 1; break;

                default:
                    throw new InvalidOperationException("current direction of the robot is unrecognized");
            }

            if (!Table.IsValidPosition(newX.Value, newY.Value)) return;

            // update X,Y position
            X = newX;
            Y = newY;
        }

        /// <summary>
        /// Turn left will rotate the robot 90 degrees in the specified direction without changing the position of the toy robot
        /// </summary>
        /// <exception cref="InvalidOperationException">throw exception if current direction of the robot is unrecognized</exception>
        public void TurnLeft()
        {
            if (Table == null) throw new NullReferenceException("table cannot be null");

            if (!IsPlaced) return;

            // update current direction after robot turned left
            Direction = Direction switch
            {
                Robot.Direction.NORTH => Robot.Direction.WEST,
                Robot.Direction.EAST => Robot.Direction.NORTH,
                Robot.Direction.SOUTH => Robot.Direction.EAST,
                Robot.Direction.WEST => Robot.Direction.SOUTH,
                _ => throw new InvalidOperationException("current direction of the robot is unrecognized")
            };
        }

        /// <summary>
        /// Turn right will rotate the robot 90 degrees in the specified direction without changing the position of the toy robot
        /// </summary>
        /// <exception cref="InvalidOperationException">throw exception if current direction of the robot is unrecognized</exception>
        public void TurnRight()
        {
            if (Table == null) throw new NullReferenceException("table cannot be null");

            if (!IsPlaced) return;

            // update current direction after robot turned right
            Direction = Direction switch
            {
                Robot.Direction.NORTH => Robot.Direction.EAST,
                Robot.Direction.EAST => Robot.Direction.SOUTH,
                Robot.Direction.SOUTH => Robot.Direction.WEST,
                Robot.Direction.WEST => Robot.Direction.NORTH,
                _ => throw new InvalidOperationException("current direction of the robot is unrecognized")
            };
        }
    }
}
