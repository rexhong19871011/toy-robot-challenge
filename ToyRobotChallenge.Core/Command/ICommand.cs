using ToyRobotChallenge.Core.Robot;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// A command for a robot as this interface
    /// </summary>
    public interface ICommand
    {
        void Execute(IRobot robot);
        bool Validate(IRobot robot);
    }
}
