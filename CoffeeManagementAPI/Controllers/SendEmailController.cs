using CoffeeManagementAPI.DTOs.SendEmail;
using CoffeeManagementAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/sendemail")]
    [Authorize]
    public class SendEmailController: ControllerBase
    {
        ISendMailService _sendMailService;

        public SendEmailController(ISendMailService sendMailService)
        {
            _sendMailService = sendMailService;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDTO sendEmailDTO)
        {
            var isSuccess = await _sendMailService.SendMail(sendEmailDTO.email, sendEmailDTO.code);
            if (!isSuccess) {
                return BadRequest("Something went wrong");
            }

            return Ok(new
            {
                message= "Send email successfully"
            });

        }

    }
}
