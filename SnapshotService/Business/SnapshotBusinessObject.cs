using Data;
using Microsoft.Extensions.Logging;
using Nethereum.RPC.Eth.DTOs;
using SnapshotEngine;
using SnapshotEngine.Model;

namespace Business
{
    public class SnapshotBusinessObject :ISnapshotBusinessObject
    {
        private readonly IScanUnit _scanUnit;
        private readonly ILogger _logger;

        private void LogBlock(BlockWithTransactions block)
        {
            _logger.LogInformation($"PROCESSING BLOCK #{(long)block.Number.Value}");
        }

        private void LogTransaction(TransactionVO tx)
        {
            _logger.LogInformation($"PROCESSING BLOCK #{(long)tx.Block.Number.Value} - TX #{((long)tx.Transaction.TransactionIndex.Value) + 1}");
        }

        private void AddToListIfInteractedWithAddress(TransactionVO tx, string address, List<string> list)
        {
            if (tx.Transaction.To == address)
            {
                list.Add(tx.Transaction.From);
            }
        }

        public SnapshotBusinessObject(ILogger<SnapshotBusinessObject> logger, IScanUnit scanUnit)
        {
            _scanUnit = scanUnit;
            _logger = logger;   
        }

        public async Task<List<string>> ContractInteractorsSnapshot(string contractAddress, string blockFilter, string rpcUrl, long timeWindow)
        {
            SnapshotFilter snapshotFilter = new()
                { BlocksToScan = new List<long>() { long.Parse(blockFilter) }, RpcUrl = rpcUrl, TimeWindow = timeWindow };
            EvmNetworkSettings networkSettings = new() { RpcUrl = snapshotFilter.RpcUrl, TimeWindow = snapshotFilter.TimeWindow };

            var list = new List<string>();

            var scanFilter = new ScanHandler(snapshotFilter.BlocksToScan.First(),
                snapshotFilter.BlocksToScan.First() + 2,
                LogBlock, onFilterLogHandle: null, onContractCreationHandle: null, onTransactionReceiptHandle: null,
                onTransactionHandle: (tx)=>AddToListIfInteractedWithAddress(tx, contractAddress, list));

            await _scanUnit.Scan(networkSettings, scanFilter);
            return list;
        }

        public async Task<List<ContractCreationRecord>> ContractCreationListSnapshot(string blockFilter, string rpcUrl, long timeWindow)
        {
            //TODO - Convert blockfilter to a list of blocks
            //TODO - Validate input
            SnapshotFilter snapshotFilter = new()
                { BlocksToScan = new List<long>() {long.Parse(blockFilter)}, RpcUrl = rpcUrl, TimeWindow = timeWindow };
            
            //TODO - Step over Execution Manager and storage manager
            EvmNetworkSettings networkSettings = new(){RpcUrl = snapshotFilter.RpcUrl, TimeWindow = snapshotFilter.TimeWindow};

            var list = new List<ContractCreationRecord>();

            var scanFilter = new ScanHandler(snapshotFilter.BlocksToScan.First(),
                snapshotFilter.BlocksToScan.First() + 10, LogBlock, LogTransaction, 
                null,
                onFilterLogHandle: null,
                onContractCreationHandle: (cc) =>
                {
                    if (cc.FailedCreatingContract) return;
                    var result = new ContractCreationRecord()
                    {
                        BlockNumber = (long)cc.Block.Number.Value,
                        ContractCreationTransactionHash = cc.TransactionHash,
                        ContractCreator = cc.Transaction.From,
                        ContractAddress = cc.ContractAddress,
                    };
                    list.Add(result);
                });
            await _scanUnit.Scan(networkSettings, scanFilter);
            return list;
        }
    }
}