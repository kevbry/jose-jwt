using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jose;
using System.Security.Cryptography;

namespace Jose.jwk
{
    public abstract class AbstractJwkAlgorithm<K,T> : IJwkAlgorithm where K : AsymmetricAlgorithm
    {
        protected abstract T CreateParameters(IDictionary<string, object> header);
        protected abstract K CreateAlgorithm(T parameters);
        protected abstract T ExportParameters(K key, bool includePrivateParameters);
        protected abstract IDictionary<string, object> CreateJson(JWK jwk, T parameters, bool includePrivateParameters);

        public JWK Parse(IDictionary<string, object> header, JwtSettings settings = null)
        {
            return new JWK()
            {
                Header = header,
                Key = CreateAlgorithm(CreateParameters(header))
            };
        }

        public IDictionary<string, object> Serialize(JWK jwk, bool includePrivateParameters, JwtSettings settings = null)
        {
            T parameters = ExportParameters((K)jwk.Key, includePrivateParameters);
            return CreateJson(jwk, parameters, includePrivateParameters);
        }
    }
}
