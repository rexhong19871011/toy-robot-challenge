using System;
using System.IO;
using ToyRobotChallenge.Core.Command;
using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Utils;

namespace ToyRobotChallenge.Core.Simulator
{
    /// <summary>
    /// A command file processing simulator, let the user use a file path
    /// currently our command file processing simulator only 
    /// </summary>
    public class CommandFileProcessingSimulator : Simulator
    {
        /// <summary>
        /// file extension
        /// </summary>
        private readonly string _fileExt;

        /// <summary>
        /// initialize a command file processing simulator ctor
        /// </summary>
        /// <param name="robot">a new robot</param>
        /// <param name="fileExt">file extension, e.g. .txt</param>
        public CommandFileProcessingSimulator(IRobot robot, string fileExt) : base(robot)
        {
            _fileExt = fileExt;
        }

        /// <summary>
        /// execute a file processing simulator through file command string
        /// it will executes all user commands in a new command factory by a new command handler for the robot
        /// </summary>
        /// <param name="fileCommandString">file command string, e.g. Sample.txt, Sample2.txt, D:\TestData\Sample.txt</param>
        /// <param name="commandStringSeparator">command string separator for file processing simulator</param>
        /// <param name="ignoreCase">ignore case for command</param>
        /// <exception cref="FileNotFoundException">throw file not found exception</exception>
        public override void Execute(string fileCommandString, string[] commandStringSeparator, bool ignoreCase)
        {
            string[] fileNameArgs = StringUtils.GetArrayFromSplitInput(fileCommandString, commandStringSeparator);

            foreach (var fileNameArg in fileNameArgs)
            {
                #region validation by each file

                try
                {
                    if (!string.Equals(Path.GetExtension(fileNameArg) , _fileExt, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    {
                        throw new FileNotFoundException(
                            $"Invalid file extension ({Path.GetExtension(fileNameArg)}), current support file extension is ({_fileExt})");
                    }
                }
                catch (Exception ex) when (ex is ArgumentException)
                {
                    throw new FileNotFoundException($"Not found file extension by file name ({fileNameArg})");
                }

                if (!File.Exists(fileNameArg)
                  || !string.Equals(Path.GetExtension(fileNameArg), _fileExt, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                {
                    throw new FileNotFoundException($"Not found file by file name ({fileNameArg})");
                }

                #endregion

                // Read the file and process each line, after read all in the file, it will auto close and dispose file object
                using (var file = new StreamReader(fileNameArg))
                {
                    string fileCommandInOneLine;

                    // Define new command handler for each file
                    CommandHandler commandHandler = null;

                    while ((fileCommandInOneLine = file.ReadLine()) != null)
                    {
                        commandHandler = commandHandler ?? new CommandHandler(new CommandFactory(), Robot);

                        commandHandler.Handle(fileCommandInOneLine, commandStringSeparator, ignoreCase);
                    }
                }
            }
        }
    }
}
