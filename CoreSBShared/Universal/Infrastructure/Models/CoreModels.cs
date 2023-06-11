using System;
using CoreSBShared.Universal.Infrastructure.Interfaces;
using MongoDB.Bson;
using Nest;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    /// <summary>
    ///     Created modified date time tracking
    ///     Core class, shared by all layers and models
    /// </summary>
    public class CoreCreated : ICoreCreated
    {
        // If created with some Id entity - user, person ...
        public int? CreatedById { get; set; } = null;

        // Not only user with Id, system, unknown and others is possible
        public string? CreatedBy { get; set; } = null;

        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }

        
    public interface ICoreTag
    {
        public int Index { get; set; }
        public string Text { get; set; }
    }
    
   
    // Type containing Id
    public class CoreDalint : CoreCreated, ICoreDalInt
    {
        public int IdInt { get; set; }
    }
    public class CoreDalObj : CoreCreated, ICoreDalObj
    {
        public ObjectId IdStr { get; set; }
    }
    public class CoreDalStrg : ICoreDalStrg
    {
        public string Id { get; set; }
    }
    
    public class CoreDalInt : ICoreDal<int>
    {
        public int Id { get; set; }
    }

    public class CoreDalString : ICoreDal<string>
    {
        public string Id { get; set; }
    }
    
    //elk model
    [ElasticsearchType(IdProperty = "Id")]
    public class CoreElastic : CoreDalStrg
    {}

    //Core dal generic ids
    public class CoreDalIntg : CoreCreated, ICoreDalIntg
    {
        public int Id { get; set; }
    }
    public class CoreDalobjg : CoreCreated, ICoreDalObjg
    {
        public ObjectId Id { get; set; }
    }
    public class CoreDalStringg : CoreCreated, ICoreDalStrg
    {
        public string Id { get; set; }
    }
    
    
    /// <summary>
    ///     Bl and API layer specific models
    /// </summary>
    public class CoreBL : CoreDalint
    {
    }

    public class CoreAPI : CoreDalint
    {
    }
}
