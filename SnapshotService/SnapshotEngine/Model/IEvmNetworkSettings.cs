namespace SnapshotEngine.Model
{
    public interface IEvmNetworkSettings
    {
        public string? RpcUrl { get; set; }
        public long TimeWindow { get; set; }
    }
}
