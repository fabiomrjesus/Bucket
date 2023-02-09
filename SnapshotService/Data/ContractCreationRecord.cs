namespace Data
{
    public class ContractCreationRecord
    {
        public string? ContractCreator { get; set; }
        public string? ContractCreationTransactionHash { get; set; }
        public long BlockNumber { get; set; }
        public string? ContractAddress { get; set; }
    }
}
