﻿using System;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    public class CreatedCl : ICreated
    {
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }
    
    /// <summary>
    /// Can be added to mongo to, but default object id created
    /// </summary>
    public class EntityEF : CreatedCl, IEntityEFInt
    {
        public int EfId { get; set; }
    }
    
    public class EntityMongo : CreatedCl, IEntityMongo
    {
        public ObjectId MongoId { get; set; }
    }

    [ElasticsearchType(IdProperty = "Id")]
    public class EntityElastic : IEntityStringId
    {
        public string Id { get; set; }
    }
}