﻿using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Document;
using Repository.Model;

namespace Repository
{
    public class AccountRepository : IRepository<Model.Account>
    {
        private readonly IDocumentStore _store;

        public AccountRepository()
        {
            _store = new DocumentStore
            {
                Url = "http://localhost:8081/",
                DefaultDatabase = "Twitty"
            };
            _store.Initialize();
        }

        public void Dispose()
        {
            //todo
        }

        public IDictionary<ulong, Model.Account> GetAll()
        {
            using (var session = _store.OpenSession())
            {

                return session.Load<Storage>("AccountInfoStorages/129").Data;

            }
        }

        public Model.Account Get(ulong userId)
        {
            using (var session = _store.OpenSession())
            {

                return session.Query<Storage>().First().Data[userId];
            }
        }

        public void Delete(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                session.Query<Storage>().First().Data.Remove(userId);
                session.SaveChanges();
            }
        }

        public void Save(ulong userId)
        {
            using (var session = _store.OpenSession())
            {
                session.SaveChanges();
            }
        }

        public void Add(ulong userId, Model.Account obj)
        {
            using (var session = _store.OpenSession())
            {

                session.Query<Storage>().First().Data.Add(userId,obj);
                session.SaveChanges();
            }
          /*  using (var session = _store.OpenSession())
            {
                //поиск элемента
                
                var accountInfoStorage = session.Load<Storage>("AccountInfoStorages/129");

                if (accountInfoStorage.Data.ContainsKey(userId))
                {
                    throw new Exception(" add ");
                }
                accountInfoStorage.Data.Add(userId,obj);
                session.Store(accountInfoStorage);
                session.SaveChanges();
            }*/
        }
    }

    
}