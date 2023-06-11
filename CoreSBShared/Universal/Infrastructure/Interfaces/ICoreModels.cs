using System;
using MongoDB.Bson;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    //Generic id entity
    public interface ICoreDal<TKey>
    {
        public TKey Id { get; set; }
    }
    
    public interface ICoreDalIntg : ICoreDal<int>
    {
    }

    public interface ICoreDalStrg : ICoreDal<string>
    {
    }

    public interface IEntityGuidId : ICoreDal<Guid>
    {
    }
    //mongo specific
    public interface ICoreDalObjg : ICoreDal<ObjectId>
    {
    }
    
    
    
    //Type containing Id entities
    public interface ICoreDalInt
    {
        public int IdInt { get; set; }
    }
    public interface ICoreDalObj
    {
        public ObjectId IdStr { get; set; }
    }
    public interface ICoreDalString
    {
        public string IdObj { get; set; }
    }



    public interface ICoreCreated
    {
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
