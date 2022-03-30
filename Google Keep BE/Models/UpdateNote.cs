using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class UpdateNoteRequest
    {
        public int NoteId { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string NoteColor { get; set; }
    }

    public class UpdateNoteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
