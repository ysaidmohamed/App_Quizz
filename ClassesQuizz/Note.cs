using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassesQuizz
{
    class Note
    {
        private int noteQuizz;

        public Note(int uneNote)
        {
            NoteQuizz = uneNote;
        }

        public int NoteQuizz { get => noteQuizz; set => noteQuizz = value; }
    }
}
