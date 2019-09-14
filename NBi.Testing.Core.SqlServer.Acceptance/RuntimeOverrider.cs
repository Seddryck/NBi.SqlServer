using System.IO;
using System.Diagnostics;
using System.Reflection;
using SysConfig = System.Configuration;
using System.Collections.Generic;
using NUnit.Framework;
using NBi.Testing.Acceptance;
using System;

namespace NBi.Testing.Core.SqlServer.Acceptance
{
    [TestFixture]
    public class RuntimeOverrider : BaseRuntimeOverrider
    {
        //By Acceptance Test Suite (file) create a Test Case
        [Test]
        [Category("Acceptance")]
        public override void RunPositiveTestSuite(string filename)
            => base.RunPositiveTestSuite(filename);

    }
}
