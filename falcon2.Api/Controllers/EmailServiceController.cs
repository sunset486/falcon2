using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;
using falcon2.Core.Services;
using falcon2.Api.Helpers;


namespace falcon2.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class EmailServiceController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailServiceController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmailAsync(string receiverEmail, string receiverFirstName, string Link)
        {
            try
            {
                string messageStatus = await _emailService.SendEmailAsync(receiverEmail, receiverFirstName, Link);
                return Ok(messageStatus);
            }
            catch(AppException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
