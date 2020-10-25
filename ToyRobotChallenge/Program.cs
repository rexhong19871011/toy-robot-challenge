using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToyRobotChallenge.Core.Robot;
using ToyRobotChallenge.Core.Simulator;
using ToyRobotChallenge.Core.Table;

namespace ToyRobotChallenge
{
    /// <summary>
    /// just using simple program.cs to run toy robot challenge application
    /// </summary>
    public class Program
    {
        // supported command line runtime options
        private static readonly string CmdLineOptionInteractive = "-interactive";
        private static readonly string CmdLineOptionHelp = "-help";

        // set up default sensitive case
        private static readonly bool IgnoreCase = true;

        // set up commandline string separator
        private static readonly string[] CmdLineStringSeparator = { " " };

        // set up default file extension for file simulator
        private static readonly string FileExtension = ".txt";

        // set up default table size
        private static readonly uint TableWidth = 5;
        private static readonly uint TableHeight = 5;

        /// <summary>
        /// main entrance to check if user input valid command line then execute relevant simulator
        /// </summary>
        /// <param name="args">commandline arguments</param>
        static void Main(string[] args)
        {
            try
            {
                // showing help description even if user didn't input any arguments
                if (args.Length == 0 || DoRunRelevantCommandLine(args, CmdLineOptionHelp))
                    ShowingCommandHelps();

                // checking and running simulator for command file processing 
                var fileCommandString = GetFileCommandStringFromInputArgs(args);
                if (!string.IsNullOrEmpty(fileCommandString))
                {
                    RunCommandFileProcessingSimulator(fileCommandString);
                }

                // checking and running interactive simulator
                var isRunInteractiveSimulator = DoRunRelevantCommandLine(args, CmdLineOptionInteractive);
                if (isRunInteractiveSimulator)
                {
                    RunInteractiveSimulator();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred in the main method of the \"Toy Robot Challange application\".  Exception message: {e.Message}");
                Console.WriteLine("\n\nPlease enter any keys to exit...");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// showing command helps
        /// </summary>
        private static void ShowingCommandHelps()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine("------------------------------Three ways to run toy robot challenge console---------------------------------------------");
            Console.WriteLine("");
            Console.Write("1. ToyRobotChallenge.exe");
            Console.ResetColor();
            Console.Write(" [-help] [-interactive] <input file 1> <input file 2> ... <input file n>\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("");
            Console.Write("2. dotnet run");
            Console.ResetColor();
            Console.Write(" [-help] [-interactive] <input file 1> <input file 2> ... <input file n>\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("(running .net core cmd,  you must install .NET CORE SDK 3.1 before you run command  as above)");
            Console.WriteLine("");
            Console.Write("3. ToyRobot");
            Console.ResetColor();
            Console.Write(" [-help] [-interactive] <input file 1> <input file 2> ... <input file n>\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("(running .net core for global tools, you must install .NET CORE SDK and install .NET CORE global tools (https://www.nuget.org/packages/ToyRobotChallenge) before you run command as above)");
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------------------------------------------");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("-help");
            Console.ResetColor();
            Console.Write("\t\tDisplays help description.\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("-interactive");
            Console.ResetColor();
            Console.Write("\tEnters interactive mode, use commands to control your robot.\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("<input file n>");
            Console.ResetColor();
            Console.Write("\t0 or more input files from which to extract and execute robot commands. Relative and absolute paths of files are supported, example: \'D:\\toy-robot-challenge\\ToyRobotChallenge.TestData\\example.txt\' or \'../ToyRobotChallenge.Tests/TestData/example.txt\' \n");
            Console.ResetColor();
            Console.WriteLine("");
        }

        /// <summary>
        /// get file command string. e.g. Sample.txt, Sample2.txt, D:\TestData\Sample.txt
        /// </summary>
        /// <param name="args">commandline arguments</param>
        /// <returns></returns>
        private static string GetFileCommandStringFromInputArgs(string[] args)
        {
            List<string> fileCommandStringList = new List<string>();
            foreach (var arg in args)
            {
                // filter out others commandline
                if (arg.Equals(CmdLineOptionInteractive, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal)
                    || arg.Equals(CmdLineOptionHelp, IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                    continue;

                fileCommandStringList.Add(arg);
            }

            return string.Join(" ", fileCommandStringList);
        }

        /// <summary>
        /// this main function is running interactive simulator
        /// </summary>
        private static void RunInteractiveSimulator()
        {
            // initializing a toy robot in the origin (0,0) facing to North
            var toyRobot = new ToyRobot(new Table(TableWidth, TableHeight));
            var interactiveSimulator = new InteractiveSimulator(toyRobot);

            // showing description text
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("These are valid command in the interactive mode");
            Console.WriteLine("PLACE x,y,f");
            Console.ResetColor();
            Console.WriteLine("- Where x and y is coordinates and f (facing) must be either NORTH, SOUTH, WEST or EAST");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("MOVE");
            Console.ResetColor();
            Console.WriteLine("- Will move the robot one unit in current direct");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("LEFT");
            Console.ResetColor();
            Console.WriteLine("- Will rotate the robot 90 degrees to the left");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("RIGHT");
            Console.ResetColor();
            Console.WriteLine("- Will rotate the robot 90 degrees to the right");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("REPORT");
            Console.ResetColor();
            Console.WriteLine("- The robot will say the current position and facing direction");

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------Constraints--------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("- The toy robot must not fall off the table during movement. This also includes the initial placement of the toy robot");
            Console.WriteLine("- Any move that would cause the robot to fall must be ignored");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("--------------------------------------------Sample command-----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("PLACE 1,1,NORTH MOVE MOVE RIGHT REPORT");
            Console.WriteLine("PLACE 1,2,NORTH MOVE MOVE RIGHT REPORT PLACE 3,2,EAST REPORT");
            Console.WriteLine("PLACE 2,3,NORTH MOVE MOVE LEFT PLACE 0,0,EAST REPORT");
            Console.WriteLine("");
            Console.WriteLine("Or you can just one by one input then press enter");
            Console.WriteLine("PLACE 1,1,NORTH");
            Console.WriteLine("MOVE");
            Console.WriteLine("RIGHT");
            Console.WriteLine("REPORT");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            Console.WriteLine($"Now you have a table size of {toyRobot.Table?.Width ?? 0} units x {toyRobot.Table?.Height ?? 0} units to play with... It's time to try some commands or you can input 'q' to quit in anytime");
            Console.ResetColor();
            Console.WriteLine("");

            while (true)
            {
                var commandString = Console.ReadLine();

                // if user wanna quit in the cmd
                if (string.Equals(commandString, "q", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(commandString, "quit", StringComparison.OrdinalIgnoreCase)
                    || string.Equals(commandString, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                interactiveSimulator.Execute(commandString, CmdLineStringSeparator, IgnoreCase);
            }
        }

        /// <summary>
        /// this main function is running simulator for file processing
        /// </summary>
        /// <param name="fileCommandString">file command string, e.g. Sample.txt, Sample2.txt, D:\TestData\Sample.txt</param>
        private static void RunCommandFileProcessingSimulator(string fileCommandString)
        {
            // initializing a toy robot with  in the origin (0,0) facing to North
            var toyRobot = new ToyRobot(new Table(TableWidth, TableHeight));
            var commandFileProcessingSimulator = new CommandFileProcessingSimulator(toyRobot, FileExtension);

            Console.WriteLine("");
            Console.WriteLine("Processing input command files.");
            Console.WriteLine($"You are running a table size is {toyRobot.Table?.Width ?? 0} units x {toyRobot.Table?.Height ?? 0} units.");
            Console.WriteLine("");
            Console.WriteLine("Result is here:");
            Console.WriteLine("");

            try
            {
                commandFileProcessingSimulator.Execute(fileCommandString, CmdLineStringSeparator, IgnoreCase);
            }
            // we catch specific exception from file simulator, then just console log to cmd but not stop application
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// check if run relevant command line or not
        /// </summary>
        /// <param name="args">commandline arguments</param>
        /// <param name="cmdLineOption">relevant cmdline option</param>
        /// <returns>run or not</returns>
        private static bool DoRunRelevantCommandLine(string[] args, string cmdLineOption)
        {
            return args.FirstOrDefault(arg => arg == cmdLineOption) != null;
        }
    }
}
