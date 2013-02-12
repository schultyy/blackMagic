using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using blackMagic.Domain;

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

            scriptLoader.Execute(File.ReadAllText(filename));
#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
