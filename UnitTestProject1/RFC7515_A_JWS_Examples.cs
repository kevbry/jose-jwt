using Jose;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Jose.jwk;
using UnitTestProject1.util;
using System.Text;

namespace UnitTestProject1
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7515#appendix-A
    /// </summary>
    [TestClass]
    public class RFC7515_A_JWS_Examples
    {
        /// <summary>
        /// https://tools.ietf.org/html/rfc7515#appendix-A.1
        /// </summary>
        [TestMethod]
        public void A1()
        {
            const string TOKEN = ""
                + "eyJ0eXAiOiJKV1QiLA0KICJhbGciOiJIUzI1NiJ9"
                + "."
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ"
                + "."
                + "dBjftJeZ4CVP-mB92K27uhbUJU1p1r_wW1gFWFOEjXk";
            const string PAYLOAD = ""
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ";
            string json = this.GetResource("RFC7515_A1.json");
            Assert.IsNotNull(json);
            JWK jwk = JWK.Parse(json);
            Assert.IsNotNull(jwk);
            string s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual(Encoding.UTF8.GetString(Base64Url.Decode(PAYLOAD)), s);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7515#appendix-A.2
        /// </summary>
        [TestMethod]
        public void A2()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJSUzI1NiJ9"
                + "."
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ"
                + "."
                + "cC4hiUPoj9Eetdgtv3hF80EGrhuB__dzERat0XF9g2VtQgr9PJbu3XOiZj5RZmh7"
                + "AAuHIm4Bh-0Qc_lF5YKt_O8W2Fp5jujGbds9uJdbF9CUAr7t1dnZcAcQjbKBYNX4"
                + "BAynRFdiuB--f_nZLgrnbyTyWzO75vRK5h6xBArLIARNPvkSjtQBMHlb1L07Qe7K"
                + "0GarZRmB_eSN9383LcOLn6_dO--xi12jzDwusC-eOkHWEsqtFZESc6BfI7noOPqv"
                + "hJ1phCnvWh6IeYI2w9QOYEUipUTI8np6LbgGY9Fs98rqVt5AXLIhWkWywlVmtVrB"
                + "p0igcN_IoypGlUPQGe77Rw";
            const string PAYLOAD = ""
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ";
            string json = this.GetResource("RFC7515_A2.json");
            JWK jwk = JWK.Parse(json);
            string s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual(Encoding.UTF8.GetString(Base64Url.Decode(PAYLOAD)), s);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7515#appendix-A.3
        /// </summary>
        [TestMethod]
        public void A3()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJFUzI1NiJ9"
                + "."
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ"
                + "."
                + "DtEhU3ljbEg8L38VWAfUAqOyKAM6-Xx-F4GawxaepmXFCgfTjDxw5djxLa8ISlSA"
                + "pmWQxfKTUJqPP3-Kg6NU1Q";
            const string PAYLOAD = ""
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ";
            string json = this.GetResource("RFC7515_A3.json");
            JWK jwk = JWK.Parse(json);
            string s = JWT.Decode(TOKEN, jwk.Key);
            Assert.AreEqual(Encoding.UTF8.GetString(Base64Url.Decode(PAYLOAD)), s);
        }
    }
}
