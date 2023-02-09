namespace Data
{
    public class SnapshotFilter
    {
        public List<long> BlocksToScan { get; set; } = new();
        public string? RpcUrl { get; set; }
        public long TimeWindow { get; set; }
    }
}
