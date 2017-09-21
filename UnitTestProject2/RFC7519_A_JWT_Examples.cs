using Jose;
using Jose.jwk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject2.util;
using Xunit;

namespace UnitTestProject2
{
    /// <summary>
    /// https://tools.ietf.org/html/rfc7519#appendix-A
    /// </summary>
    public class RFC7519_A_JWT_Examples
    {
        /// <summary>
        /// https://tools.ietf.org/html/rfc7519#appendix-A.1
        /// </summary>
        [Fact]
        public void A1()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJSU0ExXzUiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2In0"
                + "."
                + "QR1Owv2ug2WyPBnbQrRARTeEk9kDO2w8qDcjiHnSJflSdv1iNqhWXaKH4MqAkQtM"
                + "oNfABIPJaZm0HaA415sv3aeuBWnD8J-Ui7Ah6cWafs3ZwwFKDFUUsWHSK-IPKxLG"
                + "TkND09XyjORj_CHAgOPJ-Sd8ONQRnJvWn_hXV1BNMHzUjPyYwEsRhDhzjAD26ima"
                + "sOTsgruobpYGoQcXUwFDn7moXPRfDE8-NoQX7N7ZYMmpUDkR-Cx9obNGwJQ3nM52"
                + "YCitxoQVPzjbl7WBuB7AohdBoZOdZ24WlN1lVIeh8v1K4krB8xgKvRU8kgFrEn_a"
                + "1rZgN5TiysnmzTROF869lQ"
                + "."
                + "AxY8DCtDaGlsbGljb3RoZQ"
                + "."
                + "MKOle7UQrG6nSxTLX6Mqwt0orbHvAKeWnDYvpIAeZ72deHxz3roJDXQyhxx0wKaM"
                + "HDjUEOKIwrtkHthpqEanSBNYHZgmNOV7sln1Eu9g3J8"
                + "."
                + "fiK51VwhsxJ-siBMR-YFiA";
            const string PAYLOAD = ""
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ";
            string json = Helpers.ReadResource(typeof(RFC7516_A_JWE_Examples), "RFC7516_A2.json");
            Assert.NotNull(json);
            var jwk = JWK.Parse(json);
            Assert.NotNull(jwk);
            var s = JWT.Decode(TOKEN, jwk.Key);
            Assert.Equal(Encoding.UTF8.GetString(Base64Url.Decode(PAYLOAD)), s);
        }
        /// <summary>
        /// https://tools.ietf.org/html/rfc7519#appendix-A.2
        /// </summary>
        [Fact]
        public void A2()
        {
            const string TOKEN = ""
                + "eyJhbGciOiJSU0ExXzUiLCJlbmMiOiJBMTI4Q0JDLUhTMjU2IiwiY3R5IjoiSldU"
                + "In0"
                + "."
                + "g_hEwksO1Ax8Qn7HoN-BVeBoa8FXe0kpyk_XdcSmxvcM5_P296JXXtoHISr_DD_M"
                + "qewaQSH4dZOQHoUgKLeFly-9RI11TG-_Ge1bZFazBPwKC5lJ6OLANLMd0QSL4fYE"
                + "b9ERe-epKYE3xb2jfY1AltHqBO-PM6j23Guj2yDKnFv6WO72tteVzm_2n17SBFvh"
                + "DuR9a2nHTE67pe0XGBUS_TK7ecA-iVq5COeVdJR4U4VZGGlxRGPLRHvolVLEHx6D"
                + "YyLpw30Ay9R6d68YCLi9FYTq3hIXPK_-dmPlOUlKvPr1GgJzRoeC9G5qCvdcHWsq"
                + "JGTO_z3Wfo5zsqwkxruxwA"
                + "."
                + "UmVkbW9uZCBXQSA5ODA1Mg"
                + "."
                + "VwHERHPvCNcHHpTjkoigx3_ExK0Qc71RMEParpatm0X_qpg-w8kozSjfNIPPXiTB"
                + "BLXR65CIPkFqz4l1Ae9w_uowKiwyi9acgVztAi-pSL8GQSXnaamh9kX1mdh3M_TT"
                + "-FZGQFQsFhu0Z72gJKGdfGE-OE7hS1zuBD5oEUfk0Dmb0VzWEzpxxiSSBbBAzP10"
                + "l56pPfAtrjEYw-7ygeMkwBl6Z_mLS6w6xUgKlvW6ULmkV-uLC4FUiyKECK4e3WZY"
                + "Kw1bpgIqGYsw2v_grHjszJZ-_I5uM-9RA8ycX9KqPRp9gc6pXmoU_-27ATs9XCvr"
                + "ZXUtK2902AUzqpeEUJYjWWxSNsS-r1TJ1I-FMJ4XyAiGrfmo9hQPcNBYxPz3GQb2"
                + "8Y5CLSQfNgKSGt0A4isp1hBUXBHAndgtcslt7ZoQJaKe_nNJgNliWtWpJ_ebuOpE"
                + "l8jdhehdccnRMIwAmU1n7SPkmhIl1HlSOpvcvDfhUN5wuqU955vOBvfkBOh5A11U"
                + "zBuo2WlgZ6hYi9-e3w29bR0C2-pp3jbqxEDw3iWaf2dc5b-LnR0FEYXvI_tYk5rd"
                + "_J9N0mg0tQ6RbpxNEMNoA9QWk5lgdPvbh9BaO195abQ"
                + "."
                + "AVO9iT5AV4CzvDJCdhSFlQ";
            const string PAYLOAD = ""
                + "eyJpc3MiOiJqb2UiLA0KICJleHAiOjEzMDA4MTkzODAsDQogImh0dHA6Ly9leGFt"
                + "cGxlLmNvbS9pc19yb290Ijp0cnVlfQ";

            // decrypt with key from JWE A2

            string json1 = Helpers.ReadResource(typeof(RFC7516_A_JWE_Examples), "RFC7516_A2.json");
            Assert.NotNull(json1);
            var jwk1 = JWK.Parse(json1);
            Assert.NotNull(jwk1);
            var s1 = JWT.Decode(TOKEN, jwk1.Key);

            // verify signature with key from JWS A2

            string json2 = Helpers.ReadResource(typeof(RFC7515_A_JWS_Examples), "RFC7515_A2.json");
            Assert.NotNull(json2);
            var jwk2 = JWK.Parse(json2);
            Assert.NotNull(jwk2);
            var s2 = JWT.Decode(s1, jwk2.Key);

            Assert.Equal(Encoding.UTF8.GetString(Base64Url.Decode(PAYLOAD)), s2);
        }
    }
}
