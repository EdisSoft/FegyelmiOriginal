using Edis.ViewModels.Common;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY

{
    public class UserData
    {
        public int Id { get; set; }
        public IntezetModel RogzitoIntezet { get; set; }
        public string SzemelyzetSid { get; set; }
        public string SzemelyzetNev { get; set; }
        public List<IntezetModel> ValaszthatoIntezetek { get; set; }
        public List<int> ArchivUgyekEvei { get; set; }
        public List<string> Jogosultsagok { get; set; }
        //public IdNevParos Szakterulet { get; set; }
        public IdNevParos Jogkor { get; set; }
        public string PersonalHelpdeskLoginUrl { get; set; }
    }
}
