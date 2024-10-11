﻿using CoffeeManagementAPI.DTOs.Report;
using CoffeeManagementAPI.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeManagementAPI.Controllers
{
    [ApiController]
    [Route("/api/v1/report")]
    [Authorize(Roles ="Admin")]
    public class ReportController : ControllerBase
    {
        IReportService _reportService;
        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet("getrevenue")]
        public async Task<IActionResult> GetRevenue([FromQuery] ReportRevenueInput report)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var revenue = await _reportService.GetRevenue(report.start, report.end);

            if(revenue == null)
            {
                return BadRequest("Something went wrong");
            }

            return Ok(revenue);
        }

        [HttpGet("getrevenuebydate/{date:datetime}")]
        public async Task<IActionResult> GetRevenueByDate([FromRoute] DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var revenueRecord = await _reportService.GetRevenueByDate(date);

            return Ok(revenueRecord);
        }

        [HttpGet("getproductreport")]

        public async Task<IActionResult> GetProductReport([FromQuery] ReportRevenueInput reportRevenueInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productReport = await _reportService.GetProductRevenue(reportRevenueInput.start, reportRevenueInput.end);

            return Ok(productReport);
        }

        [HttpGet("gettotalorder")]
        public async Task<IActionResult> GetOrderTotal([FromQuery] ReportRevenueInput reportRevenueInput)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var totalOrder = await _reportService.GetTotalOrder(reportRevenueInput.start, reportRevenueInput.end);

            return Ok(totalOrder);

        }

        [HttpGet("getreportbill")]
        public async Task<IActionResult> GetReportBill([FromQuery] ReportRevenueInput reportRevenueInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reportBill = await _reportService.GetReportBill(reportRevenueInput.start, reportRevenueInput.end);

            return Ok(reportBill);
        }

    }
}
