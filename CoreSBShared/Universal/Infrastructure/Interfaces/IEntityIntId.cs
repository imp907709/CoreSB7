using System;
using MongoDB.Bson;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }


    
    public interface IEntityIntId : IEntity<int>
    {
    }
    
    public interface IEntityStringId : IEntity<string>
    {
    }
    
    public interface IEntityGuidId : IEntity<Guid>
    {
    }
    
    //mongo specific
    public interface IEntityObjectId : IEntity<ObjectId>
    {
    }
}