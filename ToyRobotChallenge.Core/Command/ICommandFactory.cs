using System.Collections.Generic;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// A command factory for commands via this interface
    /// </summary>
    public interface ICommandFactory
    {
        List<ICommand> CreateCommands(string commandString, string[] commandStringSeparator, bool ignoreCase = true);
    }
}
