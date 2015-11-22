﻿using System.Collections.Generic;
using System.Linq;
using LinqToTwitter;
using Repository;
using Repository.Model;
using Server.StatusTasks;
using Account = Repository.Model.Account;

namespace Server
{
    public class Initializer
    {
        private readonly Storage _storage;

        protected IList<Status> LoadStatuses(IQueryBuilder queryBuilder, ulong maxId, int count = 3200)
        {
            var statuses = new List<Status>();
            var tweetQuery = queryBuilder.BuildTaskByMaxId(maxId).ToList();

            
            statuses.AddRange(tweetQuery);


            while (statuses.Count < count)
            {
                tweetQuery = queryBuilder.BuildTaskByMaxId(statuses[statuses.Count - 1].StatusID - 1).ToList();

                statuses.AddRange(tweetQuery);
                if (tweetQuery.Count ==0)
                {
                    break;
                }
            }
            return statuses;
        }

        public IList<Status> LoadUserTimeLine(TwitterCredentials credentials)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            IQueryBuilder userStatusesBuilder = new QueryTimeLineBuilder(twitterContextBuilder.Build(credentials));
            return LoadStatuses(userStatusesBuilder, 3000);
        }

        public Initializer(Storage storage)
        {
            _storage = storage;
        }

        public void Initialize(Account account)
        {
            var twitterContextBuilder = new TwitterContextBuilder();
            var context = twitterContextBuilder.Build(account.TwitterCredentials);
            IQueryBuilder queryBuilder = new QueryTimeLineBuilder(context);
            var result = LoadStatuses(queryBuilder, account.MinId - 1);
            result= result.OrderByDescending(x => x.CreatedAt).ToList();
            account.MarkAsInitialized();
            account.MinId = result[result.Count - 1].StatusID;
            var following = Followers(context, account.TwitterCredentials.ScreenName);



            if (account.MaxId < result[0].StatusID)
            {
                account.MaxId = result[0].StatusID;
            }
            _storage.AddStatuses(result);
            _storage.UpdateIdsAccount(account, true);
            _storage.SetFollowing(account,following);
        }

        public void Initialize(IList<Account> accounts)
        {
            foreach (var account in accounts)
            {
                Initialize(account);
            }
        }

        protected IList<string> Followers(TwitterContext twitterCtx, string user)
        {
            Friendship friendship;
            List<string> result = new List<string>();
            long cursor = -1;
            do
            {
                friendship =
                    (from friend in twitterCtx.Friendship
                     where friend.Type == FriendshipType.FriendsList &&
                           friend.ScreenName == user &&
                           friend.Cursor == cursor
                     select friend)
                    .SingleOrDefaultAsync().Result;

                if (friendship != null &&
                    friendship.Users != null &&
                    friendship.CursorMovement != null)
                {
                    cursor = friendship.CursorMovement.Next;

                    
                }

                if (friendship != null) result.AddRange(friendship.Users.Select(x=>x.Name));
            } while (cursor != 0);
            var s = friendship;
            return result;
        } 
    }
}
