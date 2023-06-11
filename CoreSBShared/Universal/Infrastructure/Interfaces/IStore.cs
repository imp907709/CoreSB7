using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreSBShared.Universal.Infrastructure.Interfaces
{
    public interface IStore
    {
        public void CreateDB();
        public void DropDB();
    }
}
