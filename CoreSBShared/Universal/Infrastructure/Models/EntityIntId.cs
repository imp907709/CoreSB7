using System;
using CoreSBShared.Universal.Infrastructure.Interfaces;

namespace CoreSBShared.Universal.Infrastructure.Models
{
    public class EntityIntId : IEntityIntId, ICreated
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }
    
    public class EntityStringId : IEntityStringId, ICreated
    {
        public string Id { get; set; }
        
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }
}