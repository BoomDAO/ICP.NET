using Microsoft.AspNetCore.Mvc;
using Sample.Shared;

namespace Sample.RestAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GovernanceController : ControllerBase
    {
        public GovernanceApiClient Client { get; }
        public GovernanceController(GovernanceApiClient client)
        {
            this.Client = client;
        }

        [Route("GetProposalInfo/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProposalInfo(ulong id)
        {
            Shared.Models.ProposalInfo? info = await this.Client.GetProposalInfoAsync(id);
            return this.Ok(info);
        }
    }
}