using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class CreateNoteRequest
    {
        public string Title { get; set; }
        public string Discription { get; set; } 
        public string NoteColor { get; set; }
    }

    public class CreateNoteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
