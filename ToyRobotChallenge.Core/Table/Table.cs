using ToyRobotChallenge.Core.Command;

namespace ToyRobotChallenge.Core.Table
{
    /// <summary>
    /// The table on which our robot can move around. 
    /// </summary>
    public class Table : ITable
    {
        /// <summary>
        /// initialize table width is 5
        /// </summary>
        public uint Width { get; } = 5;

        /// <summary>
        /// initialize table height is 5
        /// </summary>
        public uint Height { get; } = 5;

        /// <summary>
        /// initialize a 5x5 table ctor
        /// </summary>
        public Table() {}

        /// <summary>
        /// initialize a table with alternative dimensions (N units x N units)
        /// </summary>
        /// <param name="width">table width</param>
        /// <param name="height">table height</param>
        public Table(uint width, uint height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Is the specified position valid on the table
        /// </summary>
        /// <param name="x">specified x position</param>
        /// <param name="y">specified y position</param>
        /// <returns></returns>
        public bool IsValidPosition(uint x, uint y)
        {
            return x < Width && y < Height;
        }
    }
}
