using Note.Business.Abstract;
using Note.DataAccess.Abstract;
using Note.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Note.Business.Concrete
{
    public class NoteManager : INoteService
    {
        private INoteDal _noteDal; 
        public NoteManager(INoteDal noteDal)
        {
            _noteDal = noteDal;
        }

        public void Delete(int Id)
        {
            _noteDal.Delete(Id);
        }

        public Task<NoteCard> GetNote(int Id)
        {
           return _noteDal.GetNote(Id);
        }

        public Task<List<NoteCard>> GetAll()
        {
            return _noteDal.ListNotes();
        }

        public Task<List<NoteCard>> GetByCategoryId(int Id)
        {
            return _noteDal.ListByCategoryId(Id);
        }

        public void Update(NoteCard noteCard)
        {
            _noteDal.Update(noteCard);
        }

        void INoteService.Create(NoteCard noteCard)
        {
            _noteDal.Add(noteCard);
        }
    }
}
