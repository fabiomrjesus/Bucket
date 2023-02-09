using Data;

namespace Business
{
    public interface ISnapshotBusinessObject
    {
        public Task<List<string>> ContractInteractorsSnapshot(string contractAddress, string blockFilter, string rpcUrl,
            long timeWindow);

        public Task<List<ContractCreationRecord>> ContractCreationListSnapshot(string blockFilter, string rpcUrl,
            long timeWindow);

    }
}
