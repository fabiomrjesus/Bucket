namespace WebApi
{
    public class SnapshotRequest
    {
        public string? RpcUrl { get; set; }
        public string? BlockNumberFilter { get; set; }
        public long TimeWindow { get; set; }
    }
}