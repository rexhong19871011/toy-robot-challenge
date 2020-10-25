using System.Runtime.Serialization;

namespace ToyRobotChallenge.Core.Command
{
    /// <summary>
    /// Some command types for toy robot
    /// </summary>
    public enum CommandType
    {
        [EnumMember(Value = "PLACE")]
        PLACE,

        [EnumMember(Value = "MOVE")]
        MOVE,

        [EnumMember(Value = "LEFT")]
        LEFT,

        [EnumMember(Value = "RIGHT")]
        RIGHT,

        [EnumMember(Value = "REPORT")]
        REPORT,

        //TODO: new feature maybe add Back, Undo commands
    }
}
