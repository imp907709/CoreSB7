using System;
using CoreSBShared.Universal.Interfaces;

namespace CoreSBShared.Universal.Models
{
    public class EntityIntId : IEntityIntId, ICreated
    {
        public int Id { get; set; }
        
        public string CreatedBy { get; set; } = String.Empty;
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? Modified { get; set; } = null;
    }
}