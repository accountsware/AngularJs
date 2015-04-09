﻿using System;
using AngularJs.Core.Modals.Base;

namespace Angular.Data.IRepository.Base
{
    public interface IDataContext : IDisposable
    {
        int SaveChanges();
        void SyncObjectState<TEntity>(TEntity entity) where TEntity : class, IObjectState;
        void SyncObjectsStatePostCommit();
    }
}