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

            Console.WriteLine("Dude, this is the dark side of the force");

            if (args.Length == 0)
            {
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
