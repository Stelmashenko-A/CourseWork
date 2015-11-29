using System;
using System.Collections.Generic;

namespace Repository.Model
{
    public class Account
    {
        public string Id { get; protected set; }

        public Account(TwitterCredentials twitterCredentials)
        {
            TwitterCredentials = twitterCredentials;
            RegistrationTime = DateTime.Now;
            IsInitialized = false;
            Id = "accounts/" + TwitterCredentials.UserId;
            Following = new List<string>();

        }

        public TwitterCredentials TwitterCredentials { get; protected set; }

        public ulong LastReadedTweetId { get; set; }

        public ulong MaxId { get; set; }

        public ulong MinId { get; set; }

        public DateTime RegistrationTime { get; protected set; }

        public bool IsInitialized { get; protected set; }

        public IList<string> Following { get; set; } 

        public void MarkAsInitialized()
        {
            IsInitialized = true;
        }
    }
}