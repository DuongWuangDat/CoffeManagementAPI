using CoffeeManagementAPI.DTOs.SendEmail;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Factory;
using CoffeeManagementAPI.Commands;
using CoffeeManagementAPI.Invoker;
using CoffeeManagementAPI.Interface.StrategyInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/sendemail")]
    [Authorize]
    public class SendEmailController : ControllerBase
    {
        SendVoucherFactory _sendVoucherFactory;
        ISendVoucherStrategy _sendVoucherStrategy;
        public SendEmailController(SendVoucherFactory sendVoucherFactory)
        {
            _sendVoucherFactory = sendVoucherFactory;
            _sendVoucherStrategy = _sendVoucherFactory.GetSendVoucher("EMAIL");
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDTO sendEmailDTO)
        {
            var command = new SendNotificationCommand(_sendVoucherStrategy, sendEmailDTO.email, sendEmailDTO.code);

            var invoker = new NotificationInvoker();
            invoker.AddCommand(command);

            var (isSuccess, err) = await invoker.ProcessCommandsAsync();

            if (!isSuccess)
                return BadRequest(new ApiError(err));

            return Ok(new { message = "Send email successfully" });
        }

        [HttpPost("sendmany")]
        public async Task<IActionResult> SendManyEmail([FromBody] SendManyEmialDTO sendManyEmialDTO)
        {
            var listEmail = sendManyEmialDTO.listEmail;
            var listVoucher = sendManyEmialDTO.listVoucher;
            List<EmailResult> emailResult = new List<EmailResult>();
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < listEmail.Length; i++)
            {
                var email = listEmail[i];
                foreach (string voucher in listVoucher)
                {
                    var task = Task.Run(async () =>
                    {
                        var command = new SendNotificationCommand(_sendVoucherStrategy, email, voucher);
                        var (isSuccess, err) = await command.ExecuteAsync();
                        Console.WriteLine(email);

                        lock (emailResult)
                        {
                            emailResult.Add(new EmailResult
                            {
                                Email = email,
                                Voucher = voucher,
                                Success = isSuccess,
                                ErrorMsg = isSuccess ? "" : err
                            });
                        }
                    });
                    tasks.Add(task);
                }
            }
            await Task.WhenAll(tasks);
            return Ok(new
            {
                results = emailResult.ToArray(),
            });
        }

    }
}
