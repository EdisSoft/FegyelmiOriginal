using Edis.Entities.Fany;
using Edis.Utilities;
using Edis.ViewModels.Base;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{
    public class FanyFegyelmiUgyViewModel : BaseViewModel
    { 
        public bool Torolt { get; set; }

        public int FogvatartottId { get; set; }

        public int SertettFogvatartottId { get; set; }

        public int IntezetId { get; set; }

        public int UgyIntSorszam { get; set; }

        public int UgyEv { get; set; }

        public int StatuszKszId { get; set; }

        public KodszotarModel Statusz { get; set; }

        public IntezetModel Intezet { get; set; }

        //public FogvatartottNezetModel Fogvatartott { get; set; }

        //public FogvatartottNezetModel SertettFogvatartott { get; set; }


        public static explicit operator FanyFegyelmiUgyViewModel(FanyFegyelmiUgy item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<FanyFegyelmiUgy, FanyFegyelmiUgyViewModel>(item);

            if (item.Intezet != null)
            {
                model.Intezet = (IntezetModel)item.Intezet;
            }

            if (item.Statusz != null)
            {
                model.Statusz = (KodszotarModel)item.Statusz;
            }

            //if (item.Fogvatartott != null)
            //{
            //    model.Fogvatartott = (FogvatartottNezetModel)item.Fogvatartott;
            //}

            //if (item.SertettFogvatartott != null)
            //{
            //    model.SertettFogvatartott = (FogvatartottNezetModel)item.SertettFogvatartott;
            //}

            return model;
        }

        public static explicit operator FanyFegyelmiUgy(FanyFegyelmiUgyViewModel model)
        {
            FanyFegyelmiUgy entity = new FanyFegyelmiUgy();
            entity = ValueInjecterUtilities.InjectViewModel<FanyFegyelmiUgyViewModel, FanyFegyelmiUgy>(model);

            return entity;
        }
    }
}
