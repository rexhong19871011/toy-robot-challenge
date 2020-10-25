using System;

namespace ToyRobotChallenge.Core.Utils
{
    /// <summary>
    /// enumeration utility class
    /// </summary>
    public class EnumUtils
    {
        public static bool TryParse<TEnum>(string value, bool ignoreCase, out TEnum result)
            where TEnum : struct
        {
            return Enum.TryParse<TEnum>(value, ignoreCase, out result) 
                   && !int.TryParse(value, out int number); //ignore value is number, only try parse enum by string value not INT
        }
    }
}
