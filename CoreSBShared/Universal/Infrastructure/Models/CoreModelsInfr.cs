using System;
using System.Collections.Generic;
using System.Linq;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    /// <summary>
    /// Created modified date time traking 
    /// Core class, shared by all layers and models
    /// </summary>
    public class CreatedCL : ICreated
    {
        // If created with some Id entity - user, person ...
        public int? CreatedById { get; set; } = null;
        
        // Not only user with Id, system, unknown and others is possible
        public string CreatedBy { get; set; } = null;
        
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }

    /// <summary>
    /// Int id representation
    /// Core class, shared by all layers and models with int id
    /// 
    /// Can be added to mongo to, but default object id created
    /// </summary>
    public class EntityEF : CreatedCL, IEntityEFInt
    {
        public int EfId { get; set; }
    }

    public class EntityMongo : CreatedCL, IEntityMongo
    {
        public ObjectId MongoId { get; set; }
    }

    [ElasticsearchType(IdProperty = "Id")]
    public class EntityElastic : IEntityStringId
    {
        public string Id { get; set; }
    }
    
// Enum replacement
    public class Tag : EntityEF
    {
        public IList<Tag> Tags { get; set; }

        public Tag Get(int idx) => this.Tags?.FirstOrDefault(s => s?.index == idx);
        public Tag Get(string txt) => this.Tags?.FirstOrDefault(s => s?.Text == txt);
        
        public int index { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// Bl and API layer specific models
    /// </summary>
    public class CoreBL : EntityEF
    {

    }

    public class CoreAPI : EntityEF
    {
        
    }
}