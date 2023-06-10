using System;
using System.Collections.Generic;
using System.Linq;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    /// <summary>
    /// Created modified date time tracking 
    /// Core class, shared by all layers and models
    /// </summary>
    public class CreatedCore : ICreatedCore
    {
        // If created with some Id entity - user, person ...
        public int? CreatedById { get; set; } = null;
        
        // Not only user with Id, system, unknown and others is possible
        public string? CreatedBy { get; set; } = null;
        
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }

    /// <summary>
    /// Int id representation
    /// Core class, shared by all layers and models with int id
    /// 
    /// Can be added to mongo to, but default object id created
    /// </summary>
    public class EFCore : CreatedCore, IEntityEFInt
    {
        public int EfId { get; set; }
    }

    public class MongoCore : CreatedCore, IEntityMongo
    {
        public ObjectId MongoId { get; set; }
    }

    [ElasticsearchType(IdProperty = "Id")]
    public class ElasticCore : IEntityStringId
    {
        public string Id { get; set; }
    }

    // Enum replacement
    public class TagEF : EFCore
    {
        public int index { get; set; }
        public string Text { get; set; }
    }
    
    /// <summary>
    /// Bl and API layer specific models
    /// </summary>
    public class CoreBL : EFCore
    {

    }

    public class CoreAPI : EFCore
    {
        
    }
}
