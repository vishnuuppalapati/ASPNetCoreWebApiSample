using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FidoClientSdk.Models;
using FidoClientSdk.Interfaces;

namespace FidoClientExtendedApi.Controllers
{
    [ApiController]
    [Route("api/fidoClient")]
    public class FidoClientController : ControllerBase
    {
        private readonly ILogger<FidoClientController> _logger;
        private readonly IFidoApiClient _fidoApiClient;

        // ✅ This is your only constructor
        public FidoClientController(ILogger<FidoClientController> logger, IFidoApiClient fidoApiClient)
        {
            _logger = logger;
            _fidoApiClient = fidoApiClient;
        }

        [HttpPost("makecredentialrequest")]
        public async Task<IActionResult> MakeCredentialRequest([FromBody] MakeCredentialRequest request)
        {
            var result = await _fidoApiClient.MakeCredentialRequestAsync(request);
            return Ok(result);
        }

        [HttpPost("makecredentialresponse")]
        public async Task<IActionResult> MakeCredentialResponse([FromBody] MakeCredentialFinishRequest request)
        {
            var result = await _fidoApiClient.MakeCredentialResponseAsync(request);
            return Ok(result);
        }

        [HttpPost("getassertionrequest")]
        public async Task<IActionResult> GetAssertionRequest([FromBody] GetAssertionRequest request)
        {
            var result = await _fidoApiClient.GetAssertionRequestAsync(request);
            return Ok(result);
        }

        [HttpPost("getassertionresponse")]
        public async Task<IActionResult> GetAssertionResponse([FromBody] GetAssertionFinishRequest request)
        {
            var result = await _fidoApiClient.GetAssertionResponseAsync(request);
            return Ok(result);
        }

        [HttpPost("setservercredentials")]
        public async Task<IActionResult> SetServerCredentials([FromBody] ServerCredentialsDto request)
        {
            var result = await _fidoApiClient.ServerCredentialsRequestAsync(request);
            return Ok(result);
        }
    }
}
