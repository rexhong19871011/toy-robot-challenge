namespace ToyRobotChallenge.Core.Table
{
    /// <summary>
    /// A table for the robot to roam on
    /// </summary>
    public interface ITable
    {
        uint Width { get; }
        uint Height { get; }

        bool IsValidPosition(uint x, uint y);
    }
}