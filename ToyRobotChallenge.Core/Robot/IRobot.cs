using ToyRobotChallenge.Core.Table;

namespace ToyRobotChallenge.Core.Robot
{
    /// <summary>
    /// A robot
    /// </summary>
    public interface IRobot
    {
        ITable Table { get; set; }
        uint? X { get; set; }
        uint? Y { get; set; }
        Direction? Direction { get; set; }
        bool IsPlaced { get; }

        void Place(uint? x, uint? y, Direction? direction);
        void Move();
        void TurnLeft();
        void TurnRight();
    }
}
