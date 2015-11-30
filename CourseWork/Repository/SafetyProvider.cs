using System;
using System.Security.Cryptography;

namespace Repository
{
    public class SafetyProvider
    {
        private const int SaltByteSize = 64;
        private const int HashByteSize = 64;
        private const int RfcIterations = 1000;

        public void GetHash(string password, out string passwordHash, out string passwodSalt, out int iterations)
        {
            var cryptoProvider = new RNGCryptoServiceProvider();
            var salt=new byte[SaltByteSize];
            cryptoProvider.GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt) {IterationCount = RfcIterations};
            var hash = pbkdf2.GetBytes(HashByteSize);
            passwordHash = Convert.ToBase64String(hash);
            passwodSalt = Convert.ToBase64String(salt);
            iterations = RfcIterations;
        }

        public bool CheckPassword(string password, string hash, string salt, int iterations)
        {
            var byteHash = Convert.FromBase64String(hash);
            var byteSalt = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(password, byteSalt) { IterationCount = RfcIterations };
            var passwordHash = pbkdf2.GetBytes(HashByteSize);
            return ConstTimeEquals(passwordHash, byteHash);
        }
        //endless scroll  infinite scroll nginfinitescroll
        public bool ConstTimeEquals(byte[] firstByteArray, byte[] secondByteArray)
        {
            var difference = (uint) firstByteArray.Length ^ (uint) secondByteArray.Length;
            for (var i = 0; i < firstByteArray.Length&&i<secondByteArray.Length; i++)
            {
                difference |= (uint) (firstByteArray[i] ^ secondByteArray[i]);
            }
            return difference == 0;
        }

    }
}
