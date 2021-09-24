using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.DataAccess.Abstract
{
    public interface INoteDal
    {
        Task<List<NoteCard>> ListNotes(int itemCount, int page);
        Task<NoteCard> GetNote(int Id);
        Task<List<NoteCard>> ListByCategoryId(int Id, int itemCount, int page);
        Task Delete(int Id);
        Task Add(NoteCard noteCard);
        Task Update(NoteCard noteCard);
        int CountOfNote();
        int GetByCountWithCategory(int Id);
    }
}
