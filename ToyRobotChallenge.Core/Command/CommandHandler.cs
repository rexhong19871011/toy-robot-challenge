using System.Collections.Generic;
using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// Command handler class will handle all of valid command from command factory by command string
    /// </summary>
    public class CommandHandler : ICommandHandler
    {
        /// <summary>
        /// get command factory when creating command handler
        /// </summary>
        private readonly ICommandFactory _commandFactory;

        /// <summary>
        /// get the toy robot which we create from simulator
        /// </summary>
        private readonly IRobot _robot;

        /// <summary>
        /// initializing command handler ctor 
        /// </summary>
        /// <param name="commandFactory">command factory object</param>
        /// <param name="robot">toy robot object</param>
        public CommandHandler(ICommandFactory commandFactory, IRobot robot)
        {
            _commandFactory = commandFactory;
            _robot = robot;
        }

        /// <summary>
        /// Main function to handle validation and execution for all commands
        /// </summary>
        /// <param name="commandString">user command string</param>
        /// <param name="commandStringSeparator">command string separator for file processing simulator</param>
        /// <param name="ignoreCase">ignore case for command</param>
        public void Handle(string commandString, string[] commandStringSeparator, bool ignoreCase)
        {
            List<ICommand> commands = _commandFactory.CreateCommands(commandString, commandStringSeparator, ignoreCase);

            foreach (var command in commands)
            {
                // validate if current command is valid for a robot or not
                if (!command.Validate(_robot)) return;

                command.Execute(_robot);
            }
        }
    }
}
