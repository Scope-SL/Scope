namespace Scope.Patcher
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Models;
    using Newtonsoft.Json;

    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Scope.Patcher.exe <path to x64dbg exported patches>");
                Console.WriteLine("Usage: Scope.Patcher.exe <path to generated patches> <path to assembly>");
            }
            
            if (args.Length == 1)
            {
                if (!File.Exists(args.ElementAt(0)))
                {
                    Error("Unable to find file: " + args.ElementAt(0));
                }

                try
                {
                    var collection = Deserializer.Read(args.ElementAt(0));
                    if (!collection.CanPatch)
                    {
                        Error("The file is incomplete!, aborting...");
                    }
                    var json = JsonSerializer.Create();
                    using (TextWriter tw = new StreamWriter("patches.json"))
                    {
                        using var jw = new JsonTextWriter(tw);
                        jw.WriteComment(collection.AssemblyName);
                        json.Serialize(jw, collection);
                    }
                }
                catch (InvalidDataException e)
                {
                    Error("The patch file is invalid: " + e.Message);
                }
                catch (Exception e)
                {
                    Error("An error occurred while reading the patch file: " + e.Message);
                }
            }
            
            if (args.Length == 2)
            {
                if (!File.Exists(args.ElementAt(0)) || !File.Exists(args.ElementAt(1)))
                {
                    Error("Unable to find files!");
                }

                PatchCollection collection;
                string lines;
                using (TextReader tr = new StreamReader(args.ElementAt(0)))
                {
                    lines = tr.ReadToEnd();
                    collection = JsonConvert.DeserializeObject<PatchCollection>(lines);
                }

                if (collection is null)
                {
                    Error("Unable to read the file!");
                }

                if (Regex.Match(lines, @"\/\*[^\\]+\*\/", RegexOptions.Multiline | RegexOptions.IgnoreCase).Success)
                {
                    collection!.AssemblyName = lines.Substring(lines.IndexOf(@"/*"));
                }

                if (!collection!.CanPatch)
                {
                    Error("The patch file is incomplete!, aborting...");
                }
                
                collection.Patch(args.ElementAt(1), out var expected);
                if (!expected)
                {
                    Warn("Old values didnt match!");
                }
            }
        }
        
        internal static void Error(string message, bool exit = false)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
            if (exit)
            {
                Console.Read();
                Environment.Exit(0);
            }
        }
        
        internal static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}