using SnapshotEngine.Model;

namespace SnapshotEngine
{
    public interface IScanUnit
    {
        Task Scan(IEvmNetworkSettings settings, IScanHandler scanFilter);
    }
}
