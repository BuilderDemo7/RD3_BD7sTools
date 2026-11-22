using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RD3_p3d2px
{
    class Program
    {
        private static readonly string[] Options =
            { "-p3d", "-o", "-output", "-s", "-silent", "-c", "-convert", "-h", "--help" };

        private string inputFilePath;
        private string outputFilePath = "export.px";

        private static int Error(string message, int exitCode = 2)
        {
            Console.Error.WriteLine("ERROR: " + message);
            return exitCode;
        }

        private static void ShowHelp()
        {
            Console.WriteLine("Usage: RD3_p3d2px -p3d <input.p3d> [-o <output.px>] -c [-s]");
            Console.WriteLine("  -c, -convert  Request conversion (currently unimplemented).");
            Console.WriteLine("  -p3d          Set the input path.");
            Console.WriteLine("  -o, -output   Set the output path (default: export.px).");
            Console.WriteLine("  -s, -silent   Suppress normal status output.");
            Console.WriteLine("  -h, --help    Show this help.");
        }

        private int HandleArgs(string[] args)
        {
            bool convert = false;
            bool silent = args.Contains("-s") || args.Contains("-silent");
            for (int i = 0; i < args.Length; ++i)
            {
                string option = args[i];
                switch (option)
                {
                    case "-h":
                    case "--help":
                        ShowHelp();
                        return 0;
                    case "-c":
                    case "-convert":
                        convert = true;
                        break;
                    case "-s":
                    case "-silent":
                        break;
                    case "-p3d":
                    case "-o":
                    case "-output":
                        if (i + 1 == args.Length || String.IsNullOrWhiteSpace(args[i + 1]) || Options.Contains(args[i + 1]))
                            return Error("Missing path after " + option + ".");
                        string path = args[++i];
                        if (option == "-p3d")
                            inputFilePath = path;
                        else
                            outputFilePath = path;
                        if (!silent)
                            Console.WriteLine("{0}: {1}", option == "-p3d" ? "Input" : "Output", path);
                        break;
                    default:
                        return Error("Unknown argument '" + option + "'.");
                }
            }

            if (!convert)
                return 0;
            if (!File.Exists(inputFilePath))
                return Error("Pass an existing P3D file with -p3d.");
            return Error("Conversion is not implemented. No output was written to '" + outputFilePath + "'.", 3);
        }

        private void RunInteractive()
        {
            ShowHelp();
            Console.WriteLine("Interactive mode. Enter a command or close the input stream to exit.");
            while (true)
            {
                Console.Write("> ");
                string command = Console.ReadLine();
                if (command == null)
                    return;
                string[] args = Regex.Matches(command, @"""[^""]*""|[^\s""]+")
                    .Cast<Match>().Select(match => match.Value.Trim('"')).ToArray();
                HandleArgs(args);
            }
        }

        private static int Main(string[] args)
        {
            var program = new Program();
            if (args.Length != 0)
                return program.HandleArgs(args);
            program.RunInteractive();
            return 0;
        }
    }
}
