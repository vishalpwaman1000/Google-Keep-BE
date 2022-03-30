using Google_Keep_BE.DashboardDataAccess;
using Google_Keep_BE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        public readonly IDashboardDA _dashboardDA;
        public DashBoardController(IDashboardDA dashboardDA)
        {
            _dashboardDA = dashboardDA;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteRequest request)
        {
            CreateNoteResponse response = new CreateNoteResponse();
            try
            {
                response = await _dashboardDA.CreateNote(request);
            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetNote()
        {
            GetNoteResponse response = new GetNoteResponse();
            try
            {
                response = await _dashboardDA.GetNote();
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateNote(UpdateNoteRequest request)
        {
            UpdateNoteResponse response = new UpdateNoteResponse();
            try
            {
                response = await _dashboardDA.UpdateNote(request);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

    }
}
