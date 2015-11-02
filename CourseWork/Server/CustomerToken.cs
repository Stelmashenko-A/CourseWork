using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class ConsumerToken
    {
        public static string ConsumerKey
        {
            get
            {
                if (string.IsNullOrEmpty(_consumerKey))
                {
                    _consumerKey = File.ReadAllLines(@"G:\Consumer keys\Key.txt", Encoding.Default)[0];
                }
                return _consumerKey;
            }
        }
        public static string ConsumerSecret {
            get
            {
                if (string.IsNullOrEmpty(_consumerSecret))
                {
                    _consumerSecret = File.ReadAllLines(@"G:\Consumer keys\KeySecret.txt", Encoding.Default)[0];
                }
                return _consumerSecret;
            }
        }

        private static string _consumerKey;

        private static string _consumerSecret;
    }
}
