using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnapshotEngine.Model
{
    public interface IScanHandler
    {
        public long StartAt { get; set; }
        public long? EndAt { get; set; }
        public Action<BlockWithTransactions> OnBlockHandle { get; set; }
        public Action<TransactionVO> OnTransactionHandle { get; set; }
        public Action<TransactionReceiptVO> OnTransactionReceiptHandle { get; set; }
        public Action<ContractCreationVO> OnContractCreationHandle { get; set; }
        public Action<FilterLogVO> OnFilterLogHandle { get; set; }
    }
}
