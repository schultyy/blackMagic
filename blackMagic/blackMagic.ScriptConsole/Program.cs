using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using blackMagic.Domain;
using blackMagic.Outlook;
using blackMagic.Repositories;
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

            var repository = new MailRepository(GetOutlookApplication());

            //dynamic repository = new ExpandoObject();
            //repository.GetMails = new Func<string, dynamic>(str =>
            //                                                    {
            //                                                        return "blah";
            //                                                        dynamic exp = new ExpandoObject();
            //                                                        exp.Subject = "blah";
            //                                                        return new[] { exp };
            //                                                    });

            scriptLoader.RegisterFunction<string, IEnumerable<IOutlookMail>>("GetMails", repository.GetMails);

            scriptLoader.RegisterConsole(Console.WriteLine);

            try
            {
                scriptLoader.Execute(File.ReadAllText(filename));
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

#if DEBUG
            Console.ReadLine();
#endif
        }

        private static Application GetOutlookApplication()
        {
            var application = new Application();
            return application;
        }
    }
}
