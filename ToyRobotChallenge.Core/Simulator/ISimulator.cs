using ToyRobotChallenge.Core.Command;
using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Simulator
{
    /// <summary>
    /// A simulator to control a robot 
    /// </summary>
    public interface ISimulator
    {
        IRobot Robot { get; }

        void Execute(string commandString, string[] commandStringSeparator, bool ignoreCase);
    }
}
