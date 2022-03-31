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

        [HttpGet]
        public async Task<IActionResult> GetReminderNote()
        {
            GetReminderNoteResponse response = new GetReminderNoteResponse();
            try
            {

                response = await _dashboardDA.GetReminderNote();

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> TrashNote(TrashNoteRequest request)
        {
            TrashNoteResponse response = new TrashNoteResponse();
            try
            {

                response = await _dashboardDA.TrashNote(request);

            }catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetTrashNote()
        {
            GetTrashNoteResponse response = new GetTrashNoteResponse();
            try
            {

                response = await _dashboardDA.GetTrashNote();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ArchiveNote(ArchiveNoteRequest request)
        {
            ArchiveNoteResponse response = new ArchiveNoteResponse();
            try
            {

                response = await _dashboardDA.ArchiveNote(request);

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetArchiveNote()
        {
            GetArchiveNoteResponse response = new GetArchiveNoteResponse();
            try
            {

                response = await _dashboardDA.GetArchiveNote();

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> ChangeNoteColor(ChangeNoteColorRequest request)
        {
            ChangeNoteColorResponse response = new ChangeNoteColorResponse();
            try
            {

                response = await _dashboardDA.ChangeNoteColor(request);

            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }
    }
}
