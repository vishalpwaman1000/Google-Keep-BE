using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class GetTrashNoteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetTrashNote> data { get; set; }
    }

    public class GetTrashNote
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string NoteColor { get; set; }
        public string ReminderTime { get; set; }
        public bool IsArchieve { get; set; }
        public bool IsDelete { get; set; }
    }
}
