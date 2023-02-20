using Edis.Entities.Fany;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{
    public class KodszotarModel 
    {
        #region mezők
        #endregion

        #region jellemzők

        public int Id { get; set; }

        public string Azonosito { get; set; }
        
        public string KodszotarCsoportAzonosito { get; set; }
        
        public string Nev { get; set; }
        
        public string RovidNev { get; set; }
        
        public int KodszotarCsoportId { get; set; }

        public bool Selected { get; set; }

        #endregion

        public static explicit operator KodszotarModel(Kodszotar item)
        {
            KodszotarModel model = new KodszotarModel();
            model = ValueInjecterUtilities.InjectViewModel<Kodszotar, KodszotarModel>(item);
            return model;
        }



        public static explicit operator Kodszotar(KodszotarModel model)
        {
            Kodszotar entity = new Kodszotar();
            entity = ValueInjecterUtilities.InjectViewModel<KodszotarModel, Kodszotar>(model);

            return entity;
        }
    }
}
