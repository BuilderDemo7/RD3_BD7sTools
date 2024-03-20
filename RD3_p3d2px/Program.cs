using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RD3_p3d2px
{
    class Program
    {
        static readonly string ArgMagic = "-";

        static string inputFilePath = null;
        static string outputFilePath = "export.px";
        static bool InteractiveMode = false;
        static bool Silent = false;

        static readonly string fatalErr = "FATAL ERROR: ";
        static readonly string Err = "ERROR: ";

        static void Output(string output)
        {
            if (!Silent)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(output);
            }
        }
        static void Warning(string warn)
        {
            if (!Silent)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(warn);
            }
        }
        static void Error(string warn)
        {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(warn);
        }

        static void Convert()
        {

        }

        static void HandleArgs(string[] args)
        {
            Debug.WriteLine($"Handling {args.Length} args ...");

            int argId = 0;
            // argument processing
            foreach (var arg in args)
            {
                argId++;
                if (arg.Contains(ArgMagic)) // contains magic for commands
                {
                    argId--;
                    var command = arg.Replace(ArgMagic, "");
                    switch (command)
                    {
                        default:
                            // oh no, not recognized command!!
                            Output($"WARNING: Unknown command '{command}'");
                            continue;
                        case "convert":
                        case "c":
                            Convert();
                            continue;
                        case "silent":
                        case "s":
                            Silent = true;
                            Debug.WriteLine("SILENT MODE ENABLED");
                            continue;
                        case "p3d":
                            try
                            {
                                outputFilePath = args[argId + 1];
                                if (outputFilePath == "")
                                {
                                    Output(fatalErr + $"Output file name can't be empty!");
                                }
                                Output($"The output file is now: \"{outputFilePath}\"");
                                argId++;
                            }
                            catch (Exception ex)
                            {
                                Error($"{Err}Please insert a P3D file path!");
                            }
                            continue;
                        case "output":
                        case "o":
                            try
                            {
                                outputFilePath = args[argId + 1];
                                if (outputFilePath == "")
                                {
                                    Output(fatalErr + $"Output file name can't be empty!");
                                }
                                Output($"The output file is now: \"{outputFilePath}\"");
                                argId++;
                            }
                            catch(Exception ex)
                            {
                                Error($"{Err}Please insert a output file path!");
                            }
                            continue;
                    }
                }
                else { 
                    argId--;
                }
            }
        }

        static void ShowAllArguments()
        {
            Console.WriteLine(" -c (convert) --> Will convert from the configuration done");
            Console.WriteLine(" -p3d --> Specifies the .p3d file to be converted to .px");
            Console.WriteLine(" -o (output) --> Set the output file (default: export.px)");
            Console.WriteLine(" -s (silent) --> Will not output warning and normal messages but error messages on the console if used");
        }

        static void DoInteractiveMode()
        {
            Console.ForegroundColor = ConsoleColor.Gray;

            InteractiveMode = true;
            Console.Write("> ");
            string command = Console.ReadLine();
            HandleArgs( command.Replace("> ", "").Split(' ') );

            DoInteractiveMode();
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Valid arguments: ");
                ShowAllArguments();
                Console.WriteLine("");
                Console.WriteLine("No arguments inputed, interactive mode enabled.");
                DoInteractiveMode();
                return;
            }
            else
            {
                HandleArgs(args);
            }
        }
    }
}
