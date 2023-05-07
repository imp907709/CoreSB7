using System;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    public class CreatedCl : ICreated
    {
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }
    
    public class EntityEF : CreatedCl, IEntityEFInt
    {
        public int EfId { get; set; }
    }
    
    public class EntityMongo : CreatedCl, IEntityMongo
    {
        public ObjectId MongoId { get; set; }
    }
}