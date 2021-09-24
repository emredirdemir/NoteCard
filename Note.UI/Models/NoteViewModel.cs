using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Note.UI.Models
{
    public class NoteViewModel
    {
        public List<NoteCard> noteCards { get; set; }
        public List<Category> categories { get; set; }
        public NoteCard noteCard { get; set; }
    }
}
