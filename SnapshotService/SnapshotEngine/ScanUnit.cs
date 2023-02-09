using System.Net.Http.Headers;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Web3;
using SnapshotEngine.Model;

namespace SnapshotEngine
{
    public class ScanUnit : IScanUnit
    {
        public async Task Scan(IEvmNetworkSettings settings, IScanHandler handler)
        {
            var web3 = new Web3(settings.RpcUrl);

            var processor = web3.Processing.Blocks.CreateBlockProcessor(steps =>
            {
                steps.BlockStep.AddSynchronousProcessorHandler(handler.OnBlockHandle);
                steps.TransactionStep.AddSynchronousProcessorHandler(handler.OnTransactionHandle);
                steps.TransactionReceiptStep.AddSynchronousProcessorHandler(handler.OnTransactionReceiptHandle);
                steps.ContractCreationStep.AddSynchronousProcessorHandler(handler.OnContractCreationHandle);
                steps.FilterLogStep.AddSynchronousProcessorHandler(handler.OnFilterLogHandle);
            });
            var cancellationToken = new CancellationToken();
            await (handler.EndAt != null ? processor.ExecuteAsync(handler.EndAt.Value, cancellationToken, handler.StartAt):
                    processor.ExecuteAsync(cancellationToken, handler.StartAt));

        }
    }
}