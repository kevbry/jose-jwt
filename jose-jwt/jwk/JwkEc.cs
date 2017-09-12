using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Jose.jwk.util;

namespace Jose.jwk
{
    public class JwkEc : AbstractJwkAlgorithm<ECDsa,ECParameters>
    {
        // kty=EC
        // crv, x, y
        // d

        private static IDictionary<string, ECCurve> curves = new Dictionary<string, ECCurve>()
        {
            { "P-256" , ECCurve.NamedCurves.nistP256 },
            { "P-384" , ECCurve.NamedCurves.nistP384 },
            { "P-521" , ECCurve.NamedCurves.nistP521 },
        };

        public ECCurve CurveFromHeader(string crv)
        {
            ECCurve value;
            if(!curves.TryGetValue(crv, out value))
            {
                throw new ArgumentOutOfRangeException("crv", crv, "Invalid");
            }
            return value;
        }

        public string CurveToHeader(ECCurve curve)
        {
            foreach(var i in curves)
            {
                var test = i.Value;
                // compare ECCurve
                if (object.Equals(test, curve)) return i.Key;
                if (test.Oid == null || curve.Oid == null) continue;
                // compare ECCurve.Oid
                if (object.Equals(test.Oid, curve.Oid)) return i.Key;
                if (test.Oid.Value != null)
                {
                    // compare ECCurve.Oid.Value
                    if (object.Equals(test.Oid.Value, curve.Oid.Value)) return i.Key;
                }
                if (test.Oid.FriendlyName != null)
                {
                    // compare ECCurve.Oid.FriendlyName
                    if (object.Equals(test.Oid.FriendlyName, curve.Oid.FriendlyName)) return i.Key;
                }
            }
            throw new ArgumentOutOfRangeException("curve", curve, "Invalid");
        }

        protected override ECParameters ExportParameters(ECDsa key, bool includePrivateParameters)
        {
            return key.ExportParameters(includePrivateParameters);
        }

        protected override IDictionary<string, object> CreateJson(JWK jwk, ECParameters parameters, bool includePrivateParameters)
        {
            IDictionary<string, object> header = new Dictionary<string, object>(jwk.Header);
            header.Set("kty", "EC");
            header.Set("crv", CurveToHeader(parameters.Curve));
            header.Set("x", parameters.Q.X);
            header.Set("y", parameters.Q.Y);
            if (includePrivateParameters)
            {
                header.Set("d", parameters.D);
            }
            return header;
        }

        protected override ECParameters CreateParameters(IDictionary<string, object> header)
        {
            ECParameters parameters = new ECParameters();
            parameters.Curve = CurveFromHeader(header.GetString("crv"));
            parameters.Q.X = header.GetBytes("x");
            parameters.Q.Y = header.GetBytes("y");
            if(header.ContainsKey("d"))
            {
                parameters.D = header.GetBytes("d");
            }
            return parameters;
        }

        protected override ECDsa CreateAlgorithm(ECParameters parameters)
        {
            var ec = new ECDsaCng();
            ec.ImportParameters(parameters);
            return ec;
        }
    }
}
