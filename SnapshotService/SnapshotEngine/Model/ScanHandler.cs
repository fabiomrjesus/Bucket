using Nethereum.RPC.Eth.DTOs;

namespace SnapshotEngine.Model
{
    public class ScanHandler : IScanHandler
    {
        public ScanHandler(long block, 
                            Action<BlockWithTransactions>? onBlockHandle, 
                            Action<TransactionVO>? onTransactionHandle, 
                            Action<TransactionReceiptVO>? onTransactionReceiptHandle, 
                            Action<ContractCreationVO>? onContractCreationHandle, 
                            Action<FilterLogVO>? onFilterLogHandle)
        {
            StartAt = block;
            EndAt = block;
            OnBlockHandle = onBlockHandle ?? (_ => {}) ;
            OnTransactionHandle = onTransactionHandle ?? (_ => { });
            OnTransactionReceiptHandle = onTransactionReceiptHandle ?? (_ => { });
            OnContractCreationHandle = onContractCreationHandle ?? (_ => { });
            OnFilterLogHandle = onFilterLogHandle ?? (_ => { });
        }

        public ScanHandler(long startAt, long? endAt, Action<BlockWithTransactions>? onBlockHandle, Action<TransactionVO>? onTransactionHandle, Action<TransactionReceiptVO>? onTransactionReceiptHandle, Action<ContractCreationVO>? onContractCreationHandle, Action<FilterLogVO>? onFilterLogHandle)
        {
            StartAt = startAt;
            EndAt = endAt;
            OnBlockHandle = onBlockHandle ?? (_ => { });
            OnTransactionHandle = onTransactionHandle ?? (_ => { });
            OnTransactionReceiptHandle = onTransactionReceiptHandle ?? (_ => { });
            OnContractCreationHandle = onContractCreationHandle ?? (_ => { });
            OnFilterLogHandle = onFilterLogHandle ?? (_ => { });
        }


        public ScanHandler(long? startAt, long? endAt, Action<BlockWithTransactions>? onBlockHandle, Action<TransactionVO>? onTransactionHandle, Action<TransactionReceiptVO>? onTransactionReceiptHandle, Action<ContractCreationVO>? onContractCreationHandle, Action<FilterLogVO>? onFilterLogHandle)
        {
            StartAt = startAt??1;
            EndAt = endAt;
            OnBlockHandle = onBlockHandle ?? (_ => { });
            OnTransactionHandle = onTransactionHandle ?? (_ => { });
            OnTransactionReceiptHandle = onTransactionReceiptHandle ?? (_ => { });
            OnContractCreationHandle = onContractCreationHandle ?? (_ => { });
            OnFilterLogHandle = onFilterLogHandle ?? (_ => { });
        }

        public long StartAt { get; set; }
        public long? EndAt { get; set; }
        public Action<BlockWithTransactions> OnBlockHandle { get; set; }
        public Action<TransactionVO> OnTransactionHandle { get; set; }
        public Action<TransactionReceiptVO> OnTransactionReceiptHandle { get; set; }
        public Action<ContractCreationVO> OnContractCreationHandle { get; set; }
        public Action<FilterLogVO> OnFilterLogHandle { get; set; }
    }
}
