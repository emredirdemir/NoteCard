using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Business.Abstract
{
    public interface INoteService
    {
        Task<List<NoteCard>> GetAll();
    }
}
