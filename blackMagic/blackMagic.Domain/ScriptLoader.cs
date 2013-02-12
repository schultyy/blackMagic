using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronJS.Hosting;

namespace blackMagic.Domain
{
    public class ScriptLoader
    {
        private readonly CSharp.Context context;

        public ScriptLoader()
        {
            this.context = new IronJS.Hosting.CSharp.Context();
        }

        public void Execute(string script)
        {
            context.Execute(script);
        }

        public void RegisterGlobal(string name, object value)
        {
            context.SetGlobal(name, value);
        }

        //public void RegisterFunction<T>(string name, T function) where T : Func<>
        //{
        //    int parameterCount = function.Method.GetParameters().Count();
        //    context.SetGlobal(name, IronJS.Native.Utils.CreateFunction(context.Environment, parameterCount, function));
        //}
        public void RegisterConsole(Action<string> func)
        {
            context.SetGlobal("console", IronJS.Native.Utils.CreateFunction(context.Environment, 1, func));
        }
    }
}
