using CoreSBShared.Universal.Infrastructure.EF;
using CoreSBShared.Universal.Infrastructure.Interfaces;

namespace CoreSBBL.Logging.Infrastructure.TC
{

    // Interface for LogsEFStore
    public interface ILogsEFStore : IEFStore
    {
    }

    // Interface for LogsEFStoreG<T, K>
    public interface ILogsEFStoreG<T, K> : IEFStore<T, K>
        where T : class, ICoreDal<K>
    {
    }

    // Interface for LogsEFStoreGInt
    public interface ILogsEFStoreGInt : IEFStoreInt
    {
    }

    // Interface for LogsEFStoreG
    public interface ILogsEFStoreG : IEFStoreG
    {
    }
}
