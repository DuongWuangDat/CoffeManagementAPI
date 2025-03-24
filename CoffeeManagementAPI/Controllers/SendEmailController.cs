using CoffeeManagementAPI.DTOs.SendEmail;
using CoffeeManagementAPI.ErrorHandler;
using CoffeeManagementAPI.Factory;
using CoffeeManagementAPI.Interface;
using CoffeeManagementAPI.Interface.StrategyInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/sendemail")]
    [Authorize]
    public class SendEmailController: ControllerBase
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
            var (isSuccess,err) = await _sendVoucherStrategy.SendVoucher(sendEmailDTO.email, sendEmailDTO.code);
            if (!isSuccess) {
                return BadRequest(new ApiError(err));
            }

            return Ok(new
            {
                message= "Send email successfully"
            });

        }

        [HttpPost("sendmany")]
        public async Task<IActionResult> SendManyEmail([FromBody] SendManyEmialDTO sendManyEmialDTO)
        {
            var listEmail = sendManyEmialDTO.listEmail;
            var listVoucher = sendManyEmialDTO.listVoucher;
            List<EmailResult> emailResult = new List<EmailResult>();
            List<Task> tasks = new List<Task>();
            
            for( int i=0; i<listEmail.Length; i++)
            {
                var index = i;
                foreach (string voucher in listVoucher)
                {
                    var task = Task.Run(async () =>
                    {
                        var (isSuccess, err) = await _sendVoucherStrategy.SendVoucher(listEmail[index], voucher);
                        Console.WriteLine(index);
                        if (!isSuccess)
                        {
                            emailResult.Add(new()
                            {
                                Email = listEmail[index],
                                ErrorMsg = err.ToString(),
                                Voucher = voucher,
                                Success = false,
                            });
                        }
                        else
                        {
                            emailResult.Add(new()
                            {
                                Email = listEmail[index],
                                ErrorMsg = "",
                                Voucher = voucher,
                                Success = true,
                            });
                        }
                    });
                    tasks.Add(task);
                }
                
                
            }
            await Task.WhenAll(tasks);
            return Ok(new
            {
                results= emailResult.ToArray(),
            });
        }

    }
}
