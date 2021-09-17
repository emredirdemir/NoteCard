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
        private INoteDal noteDal; 
        public NoteManager(INoteDal noteDal)
        {
            this.noteDal = noteDal;
        }
        public Task<List<NoteCard>> GetAll()
        {
            return noteDal.ListNotes();
        }
    }
}
