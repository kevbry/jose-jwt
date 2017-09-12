using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Jose.lang.util;

namespace Jose.lang
{
    [TestClass]
    public class LangTest
    {
        [TestMethod]
        public void Test1()
        {
            RSAParameters parameters = new RSAParameters();
            IDictionary<string, object> header = new Dictionary<string, object>()
            {
            };
            header.Get<string>("n")
                .Map(Base64Url.Decode)
                .Set(out parameters.Modulus);
        }
    }
}
