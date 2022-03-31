using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class ChangeNoteColorRequest
    {
        public int NoteID { get; set; }
        public string NoteColor { get; set; }
    }

    public class ChangeNoteColorResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
