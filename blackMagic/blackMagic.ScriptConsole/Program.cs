using System;
using System.IO;
using System.Linq;
using libMagic;
using Exception = System.Exception;

namespace blackMagic.ScriptConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("blackMagic");

            if (args.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("No input file specified");
                Console.ForegroundColor = ConsoleColor.White;
#if DEBUG
                Console.ReadLine();
#endif
                return;
            }

            var filename = args.First();

            var scriptLoader = new ScriptLoader();

            try
            {
                scriptLoader.RunScript(File.ReadAllText(filename));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
