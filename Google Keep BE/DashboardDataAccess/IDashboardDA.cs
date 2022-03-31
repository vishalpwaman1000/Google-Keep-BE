using Google_Keep_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.DashboardDataAccess
{
    public interface IDashboardDA
    {
        public Task<CreateNoteResponse> CreateNote(CreateNoteRequest request);
        public Task<GetNoteResponse> GetNote();
        public Task<UpdateNoteResponse> UpdateNote(UpdateNoteRequest request);
        public Task<GetReminderNoteResponse> GetReminderNote();
        public Task<TrashNoteResponse> TrashNote(TrashNoteRequest request);
        public Task<GetTrashNoteResponse> GetTrashNote();
        public Task<ArchiveNoteResponse> ArchiveNote(ArchiveNoteRequest request);
        public Task<GetArchiveNoteResponse> GetArchiveNote();
        public Task<ChangeNoteColorResponse> ChangeNoteColor(ChangeNoteColorRequest request);
    }
}
