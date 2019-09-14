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
        [Test]
        [TestCase("Batch.nbits")]
        [Category("Acceptance")]
        public void RunPositiveTestSuite2(string filename)
            => base.RunPositiveTestSuite(filename);

    }
}
