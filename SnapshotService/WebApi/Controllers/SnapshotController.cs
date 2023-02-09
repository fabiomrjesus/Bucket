using Business;
using Microsoft.AspNetCore.Mvc;
using SnapshotEngine;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SnapshotController : ControllerBase
    {
        
        private readonly ILogger<SnapshotController> _logger;
        private readonly ISnapshotBusinessObject _snapshotBusinessObject;
        public SnapshotController(ILogger<SnapshotController> logger, ISnapshotBusinessObject snapshotBusinessObject)
        {
            _logger = logger;
            _snapshotBusinessObject = snapshotBusinessObject;
        }

        [HttpPost("ContractCreation")]
        public async Task<IActionResult> NewSnapshot(SnapshotRequest request)
        {
            var result = await _snapshotBusinessObject.ContractCreationListSnapshot(request.BlockNumberFilter??"", 
                                                                         request.RpcUrl??"", 
                                                                         request.TimeWindow);
            return Ok(result);
        }


        [HttpPost("ContractInteractors")]
        public async Task<IActionResult> ContractInteractors(SnapshotRequest request, string contractAddress)
        {
            var result = await _snapshotBusinessObject.ContractInteractorsSnapshot(contractAddress,
                request.BlockNumberFilter ?? "",
                request.RpcUrl ?? "",
                request.TimeWindow);
            return Ok(result);
        }
    }
}