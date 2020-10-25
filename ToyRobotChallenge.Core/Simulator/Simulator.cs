using ToyRobotChallenge.Core.Command;
using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Simulator
{
    /// <summary>
    /// This is where it all comes together.  Create a simulator and give it a robot to control
    /// </summary>
    public abstract class Simulator : ISimulator
    {
        /// <summary>
        /// get a robot when creating a simulator
        /// </summary>
        public IRobot Robot { get; }

        /// <summary>
        /// initialize simulator ctor with a robot
        /// </summary>
        /// <param name="robot">robot object</param>
        protected Simulator(IRobot robot)
        {
            Robot = robot;
        }

        /// <summary>
        /// abstract methods to override for derived classes
        /// </summary>
        /// <param name="commandString">user command string</param>
        /// <param name="commandStringSeparator">command string separator for file processing simulator</param>
        /// <param name="ignoreCase">ignore case for command</param>
        public abstract void Execute(string commandString, string[] commandStringSeparator, bool ignoreCase);
    }
}
