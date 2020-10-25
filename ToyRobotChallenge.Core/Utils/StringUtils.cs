using System;

namespace ToyRobotChallenge.Core.Utils
{
    /// <summary>
    ///  String utility class
    /// </summary>
    public class StringUtils
    {
        /// <summary>
        /// get string array by string input and separator
        /// </summary>
        /// <param name="input">string input</param>
        /// <param name="separator">separator</param>
        /// <returns></returns>
        public static string[] GetArrayFromSplitInput(string input, string[] separator)
        {
            return input?.Split(separator, StringSplitOptions.None);
        }
    }
}
