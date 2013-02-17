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
            engine.SetGlobalValue("nativeModule", new NativeModuleInstance(engine));

            engine.ExecuteFile("./Builtin/require.js");
        }

        [Test]
        public void CheckRequireIsExistent()
        {
            var require = engine.GetGlobalValue<FunctionInstance>("require");
            Assert.IsNotNull(require);
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
            var mathModule = engine.Evaluate("require('math');");

            Assert.IsNotNull(mathModule);
        }
    }
}
