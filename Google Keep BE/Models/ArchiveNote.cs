using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class ArchiveNoteRequest
    {
        public int NoteID { get; set; }
    }

    public class ArchiveNoteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
