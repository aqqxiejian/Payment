﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using System;
using System.Text;

namespace Essensoft.AspNetCore.Payment.Security
{
    public class SHA1WithRSA
    {
        public static string SignData(string data, AsymmetricKeyParameter key)
        {
            var signer = SignerUtilities.GetSigner("SHA1WithRSA");
            signer.Init(true, key);
            var bytes = Encoding.UTF8.GetBytes(data);
            signer.BlockUpdate(bytes, 0, bytes.Length);
            return Convert.ToBase64String(signer.GenerateSignature());
        }

        public static bool VerifyData(string data, string sign, AsymmetricKeyParameter key)
        {
            var verifier = SignerUtilities.GetSigner("SHA1WithRSA");
            verifier.Init(false, key);
            var bytes = Encoding.UTF8.GetBytes(data);
            verifier.BlockUpdate(bytes, 0, bytes.Length);
            return verifier.VerifySignature(Convert.FromBase64String(sign));
        }
    }
}
