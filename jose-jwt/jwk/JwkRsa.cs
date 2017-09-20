using System.Collections.Generic;
using System.Security.Cryptography;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JwkRsa : AbstractJwkAlgorithm<RSA,RSAParameters>
    {
        // kty=RSA
        // n, e
        // d, p, q, dp, dq, qi, 
        // oth, (r, d, t)

        protected override RSAParameters ExportParameters(RSA key, bool includePrivateParameters)
        {
            return key.ExportParameters(includePrivateParameters);
        }

        protected override IDictionary<string, object> CreateJson(JWK jwk, RSAParameters parameters, bool includePrivateParameters)
        {
            IDictionary<string, object> header = new Dictionary<string, object>(jwk.Header);
            header.Set("kty", "RSA");
            header.Set("n", parameters.Modulus);
            header.Set("e", parameters.Exponent);
            if(includePrivateParameters)
            {
                header.Set("d", parameters.D);
                header.Set("p", parameters.P);
                header.Set("q", parameters.Q);
                header.Set("dp", parameters.DP);
                header.Set("dq", parameters.DQ);
                header.Set("qi", parameters.InverseQ);
            }
            return header;
        }

        protected override RSAParameters CreateParameters(IDictionary<string, object> header)
        {
            RSAParameters parameters = new RSAParameters();
            parameters.Modulus = header.GetBytes("n");
            parameters.Exponent = header.GetBytes("e");
            if (header.ContainsKey("d"))
            {
                parameters.D = header.GetBytes("d");
                parameters.P = header.GetBytes("p");
                parameters.Q = header.GetBytes("q");
                parameters.DP = header.GetBytes("dp");
                parameters.DQ = header.GetBytes("dq");
                parameters.InverseQ = header.GetBytes("qi");
            }
            return parameters;
        }

        protected override RSA CreateAlgorithm(RSAParameters parameters)
        {
            var rsa = RSA.Create();
            rsa.ImportParameters(parameters);
            return rsa;
        }
    }
}
