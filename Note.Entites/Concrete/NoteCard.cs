using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Entities.Concrete
{
    public class NoteCard
    {
        public int Id { get; set; }
        public string NoteHeader { get; set; }
        public string NoteContent { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
    }
}
