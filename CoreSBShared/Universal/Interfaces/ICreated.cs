using System;

namespace CoreSBShared.Universal.Interfaces
{
    public interface ICreated
    {
        public string CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}