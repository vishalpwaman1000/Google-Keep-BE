using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_Keep_BE.Models
{
    public class GetNoteResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public List<GetNote> data { get; set; }
    }

    public class GetNote
    {
        public int NoteID { get; set; }
        public string Title { get; set; }
        public string Discription { get; set; }
        public string NoteColor { get; set; }
    }
}
