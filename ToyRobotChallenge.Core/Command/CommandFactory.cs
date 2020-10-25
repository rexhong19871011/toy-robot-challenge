using System.Collections.Generic;
using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Utils;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// The whole Command pattern invites a Factory pattern. The advantage of doing this is that your CommandHandler class doesn't have the responsibility of knowing all the possible commands
    /// and benefit is the commands can be easily tested/mocked
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        /// <summary>
        /// create commands in the list through user command string
        /// </summary>
        /// <param name="commandString">user command string</param>
        /// <param name="commandStringSeparator">command string separator for file processing simulator</param>
        /// <param name="ignoreCase">ignore case for command</param>
        /// <returns>return a command list</returns>
        public List<ICommand> CreateCommands(string commandString, string[] commandStringSeparator, bool ignoreCase = true)
        {
            string[] commandArgs = StringUtils.GetArrayFromSplitInput(commandString, commandStringSeparator);

            if (commandArgs == null) return null;

            List<ICommand> commandList = new List<ICommand>();
            uint xPosition = default(uint);
            uint yPosition = default(uint);
            Direction direction = default(Direction);
            bool hasValidPlaceCommand = false;

            for (int i = 0; i < commandArgs.Length; i++)
            {
                // filter out invalid command
                if (!EnumUtils.TryParse(commandArgs[i], ignoreCase, out CommandType commandType)) continue;

                //checking specific case only for Place command 
                if (commandType == CommandType.PLACE)
                {
                    var placePositionAndDirectionArray = StringUtils.GetArrayFromSplitInput(commandArgs[i + 1], new[] { "," });

                    if (placePositionAndDirectionArray.Length != 3) continue;

                    // so we got correct Place command length is 3 and format value like Place 1,1,North
                    i += 1; // +1 is because we need make sure next loop is excluding place position and direction part
                    hasValidPlaceCommand =
                        uint.TryParse(placePositionAndDirectionArray[0], out xPosition) //  checking x position if it is unit value
                        && uint.TryParse(placePositionAndDirectionArray[1], out yPosition) // checking y position if it is unit value
                        && EnumUtils.TryParse(placePositionAndDirectionArray[2], ignoreCase, out direction); // checking if it is direction type
                }

                switch (commandType)
                {
                    case CommandType.PLACE when hasValidPlaceCommand:
                        commandList.Add(new PlaceCommand(xPosition, yPosition, direction)); break;
                    case CommandType.MOVE:
                        commandList.Add(new MoveCommand()); break;
                    case CommandType.LEFT:
                        commandList.Add(new LeftCommand()); break;
                    case CommandType.RIGHT:
                        commandList.Add(new RightCommand()); break;
                    case CommandType.REPORT:
                        commandList.Add(new ReportCommand()); break;
                }
            }

            return commandList;
        }
    }
}
