using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Common
{
    public class IdNevParos
    {
        public int Id { get; set; }
        public string Nev { get; set; }

        public IdNevParos()
        { }
        public IdNevParos(int id, string nev)
        {
            Id = id;
            Nev = nev;
        }
    }
}
