namespace SnapshotEngine.Model
{
    public class EvmNetworkSettings : IEvmNetworkSettings
    {
        public string? RpcUrl { get; set; }
        public long TimeWindow { get; set; }
    }
}
