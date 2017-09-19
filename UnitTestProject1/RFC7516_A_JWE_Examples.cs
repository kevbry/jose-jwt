using Jose;
using Jose.jwk;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1.util;

namespace UnitTestProject1
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7516#appendix-A
    /// </summary>
    [TestClass]
    public class RFC7516_A_JWE_Examples
    {
        /// <summary>
        /// https://tools.ietf.org/html/rfc7516#appendix-A.1
        /// </summary>
        [TestMethod]
        public void A1()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJSU0EtT0FFUCIsImVuYyI6IkEyNTZHQ00ifQ"
                + "."
                + "OKOawDo13gRp2ojaHV7LFpZcgV7T6DVZKTyKOMTYUmKoTCVJRgckCL9kiMT03JGe"
                + "ipsEdY3mx_etLbbWSrFr05kLzcSr4qKAq7YN7e9jwQRb23nfa6c9d-StnImGyFDb"
                + "Sv04uVuxIp5Zms1gNxKKK2Da14B8S4rzVRltdYwam_lDp5XnZAYpQdb76FdIKLaV"
                + "mqgfwX7XWRxv2322i-vDxRfqNzo_tETKzpVLzfiwQyeyPGLBIO56YJ7eObdv0je8"
                + "1860ppamavo35UgoRdbYaBcoh9QcfylQr66oc6vFWXRcZ_ZT2LawVCWTIy3brGPi"
                + "6UklfCpIMfIjf7iGdXKHzg"
                + "."
                + "48V1_ALb6US04U3b"
                + "."
                + "5eym8TW_c8SuK0ltJ3rpYIzOeDQz7TALvtu6UG9oMo4vpzs9tX_EFShS8iB7j6ji"
                + "SdiwkIr3ajwQzaBtQD_A"
                + "."
                + "XFBoMYUZodetZdvTiFvSkQ";
            string json = this.GetResource("RFC7516_A1.json");
            Assert.IsNotNull(json);
            var jwk = JWK.Parse(json);
            Assert.IsNotNull(jwk);
            var s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual("The true sign of intelligence is not knowledge but imagination.", s);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7516#appendix-A.2
        /// </summary>
        [TestMethod]
        public void A2()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJSU0ExXzUiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2In0"
                + "."
                + "UGhIOguC7IuEvf_NPVaXsGMoLOmwvc1GyqlIKOK1nN94nHPoltGRhWhw7Zx0-kFm"
                + "1NJn8LE9XShH59_i8J0PH5ZZyNfGy2xGdULU7sHNF6Gp2vPLgNZ__deLKxGHZ7Pc"
                + "HALUzoOegEI-8E66jX2E4zyJKx-YxzZIItRzC5hlRirb6Y5Cl_p-ko3YvkkysZIF"
                + "NPccxRU7qve1WYPxqbb2Yw8kZqa2rMWI5ng8OtvzlV7elprCbuPhcCdZ6XDP0_F8"
                + "rkXds2vE4X-ncOIM8hAYHHi29NX0mcKiRaD0-D-ljQTP-cFPgwCp6X-nZZd9OHBv"
                + "-B3oWh2TbqmScqXMR4gp_A"
                + "."
                + "AxY8DCtDaGlsbGljb3RoZQ"
                + "."
                + "KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY"
                + "."
                + "9hH0vgRfYgPnAHOd8stkvw";
            string json = this.GetResource("RFC7516_A2.json");
            Assert.IsNotNull(json);
            var jwk = JWK.Parse(json);
            Assert.IsNotNull(jwk);
            var s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual("Live long and prosper.", s);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7516#appendix-A.3
        /// </summary>
        [TestMethod]
        public void A3()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJBMTI4S1ciLCJlbmMiOiJBMTI4Q0JDLUhTMjU2In0"
                + "."
                + "6KB707dM9YTIgHtLvtgWQ8mKwboJW3of9locizkDTHzBC2IlrT1oOQ"
                + "."
                + "AxY8DCtDaGlsbGljb3RoZQ"
                + "."
                + "KDlTtXchhZTGufMYmOYGS4HffxPSUrfmqCHXaI9wOGY"
                + "."
                + "U0m_YmjN04DJvceFICbCVQ";
            string json = this.GetResource("RFC7516_A3.json");
            Assert.IsNotNull(json);
            var jwk = JWK.Parse(json);
            Assert.IsNotNull(jwk);
            var s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual("Live long and prosper.", s);
        }
    }
}
