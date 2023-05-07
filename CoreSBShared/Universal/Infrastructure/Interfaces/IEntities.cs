using System;
using MongoDB.Bson;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface IEntity<TKey>
    {
        public TKey Id { get; set; }
    }

    public interface IEntityEFInt
    {
        public int EfId { get; set; }
    }
    public interface IEntityMongo
    {
        public ObjectId MongoId { get; set; }
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
    
    public interface ICreated
    {
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}