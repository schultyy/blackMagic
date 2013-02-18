using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;
using NUnit.Framework;
using libMagic.IO;

namespace blackMagic.Test
{
    [TestFixture]
    public class RequireTest
    {
        private ScriptEngine engine;

        [SetUp]
        public void Setup()
        {
            engine = new ScriptEngine();
            engine.EnableDebugging = true;
            engine.SetGlobalValue("nativeModule", new NativeModuleInstance(engine));
            engine.SetGlobalValue("console", new FirebugConsole(engine));
            engine.SetGlobalFunction("native_require", new Func<string, ObjectInstance>(identifier =>
            {
                switch (identifier)
                {
                    case "nativeModule":
                        return new NativeModuleInstance(engine);
                    default:
                        throw new ArgumentOutOfRangeException(string.Format("Unsupported module name: {0}", identifier));
                }
            }));

            engine.ExecuteFile("./Builtin/require.js");
        }

        [Test]
        public void CheckRequireIsExistent()
        {
            var require = engine.GetGlobalValue("require");
            Assert.IsNotNull(require);
            Assert.IsTrue(require is FunctionInstance);
        }

        [Test]
        public void CheckNativeModule()
        {
            Assert.NotNull(engine.GetGlobalValue("nativeModule"));
            var functionInstance = engine.GetGlobalValue("nativeModule.readFileSync");
            Assert.IsNotNull(functionInstance);
        }

        [Test]
        public void RequireMathModule()
        {
            var mathModule = engine.Evaluate(@"require('math');");

            Assert.AreNotEqual(Undefined.Value, mathModule);
            Assert.IsTrue(mathModule is ObjectInstance);
            var addMember = ((ObjectInstance)mathModule).GetPropertyValue("add");

            Assert.IsNotNull(addMember);
            Assert.IsFalse(addMember == Undefined.Value);

            var result = Convert.ToInt32(((FunctionInstance)addMember).Call(addMember, 1, 2));
            Assert.IsTrue(result == 3);
        }
    }
}
