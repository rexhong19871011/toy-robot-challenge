using System.Runtime.Serialization;

namespace ToyRobotChallenge.Core.Robot
{
    /// <summary>
    /// Common direction enumeration for a robot
    /// </summary>
    public enum Direction
    {
        [EnumMember(Value = "NORTH")]
        NORTH,

        [EnumMember(Value = "SOUTH")]
        SOUTH,

        [EnumMember(Value = "WEST")]
        WEST,

        [EnumMember(Value = "EAST")]
        EAST
    }
}
