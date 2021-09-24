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
        Task<NoteCard> GetNote(int Id);
        Task<List<NoteCard>> GetByCategoryId(int Id);
        void Delete(int Id);
        void Update(NoteCard noteCard);
        void Create(NoteCard noteCard);
    }
}
