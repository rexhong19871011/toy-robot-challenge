namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// A command handler for handling user commands via this interface
    /// </summary>
    public interface ICommandHandler
    {
        void Handle(string commandString, string[] commandStringSeparator, bool ignoreCase);
    }
}
