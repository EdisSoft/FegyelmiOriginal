namespace Edis.ViewModels.JFK.FENY
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Edis.Utilities;
    using Edis.ViewModels.Base;
    using System.ComponentModel.DataAnnotations;
    using Edis.Entities.JFK.FENY;
    using Edis.ViewModels.Common;
    using Edis.ViewModels.Fany;

    public class IktatottDokumentumokViewModel : BaseViewModel
    {
        
		public int RogzitoSzemelyId { get; set; }

		public int RogzitoIntezetId { get; set; }

		public DateTime ErvenyessegKezd { get; set; }

		public bool ToroltFl { get; set; }

		public DateTime LetrehozasDatuma { get; set; }

		public int? KeziJavitasAzonosito { get; set; }

		public int FegyelmiUgyId { get; set; }

		public int DokumentumTipusCimkeId { get; set; }

		public System.Byte Alszam { get; set; }

        public bool AktivFl { get; set; }

        public int? ModositoSzemelyId { get; set; }

        public DateTime? ModositasDatuma { get; set; }

        public string DokumentumAdat { get; set; }

        public int? NaploId { get; set; }
        public bool? InaktivFl { get; set; }

        public CimkeModel DokumentumTipus { get; set; }
        //public SzemelyzetModel RogzitoSzemely { get; set; }
        //public SzemelyzetModel ModositoSzemely { get; set; }

		public static explicit operator IktatottDokumentumokViewModel(IktatottDokumentumok item)
		{
			var model = ValueInjecterUtilities.InjectViewModel<IktatottDokumentumok, IktatottDokumentumokViewModel>(item);

            if (item.DokumentumTipus != null)
            {
                model.DokumentumTipus = (CimkeModel)item.DokumentumTipus;
            }

            //if (item.RogzitoSzemely != null)
            //{
            //    model.RogzitoSzemely = (SzemelyzetModel)item.RogzitoSzemely;
            //}

            //if (item.ModositoSzemely != null)
            //{
            //    model.ModositoSzemely = (SzemelyzetModel)item.ModositoSzemely;
            //}

            //if (item.Naplo != null && item.Naplo.InaktivFl == true)
            //{
            //    model.InaktivFl = true;
            //}

            return model;
		}

		public static explicit operator IktatottDokumentumok(IktatottDokumentumokViewModel model)
		{
			IktatottDokumentumok entity = new IktatottDokumentumok();
			entity = ValueInjecterUtilities.InjectViewModel<IktatottDokumentumokViewModel, IktatottDokumentumok> (model);

			return entity;
		}
    }
}
