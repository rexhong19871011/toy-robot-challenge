using System;
using ToyRobotChallenge.Core.Command;
using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Simulator
{
    /// <summary>
    /// An interactive simulator, let the user enter robot commands
    /// </summary>
    public class InteractiveSimulator : Simulator
    {
        /// <summary>
        /// initialize an interactive simulator ctor
        /// </summary>
        /// <param name="robot">a new robot</param>
        public InteractiveSimulator(IRobot robot) : base(robot)
        {
        }

        /// <summary>
        /// execute a interactive simulator through user command string
        /// it will executes all user commands in a new command factory by a new command handler for the robot
        /// </summary>
        /// <param name="commandString">user command string</param>
        /// <param name="commandStringSeparator">command string separator for file processing simulator</param>
        /// <param name="ignoreCase">ignore case for command</param>
        public override void Execute(string commandString, string[] commandStringSeparator, bool ignoreCase)
        {
            var commandHandler = new CommandHandler(new CommandFactory(), Robot);

            commandHandler.Handle(commandString, commandStringSeparator, ignoreCase);
        }
    }
}
