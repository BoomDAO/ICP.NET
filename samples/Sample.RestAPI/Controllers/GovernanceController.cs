using Microsoft.AspNetCore.Mvc;
using Sample.Shared;
using Sample.Shared.Governance;
using Sample.Shared.Governance.Models;

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
            ProposalInfo? info = await this.Client.GetProposalInfoAsync(id);
            return this.Ok(info);
        }
    }
}