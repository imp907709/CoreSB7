using System;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface ICreated
    {
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}