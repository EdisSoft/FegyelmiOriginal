using Edis.Entities.Fany;
using Edis.Utilities;
using System.Collections.Generic;
using Edis.ViewModels.Base;

namespace Edis.ViewModels.Fany
{
    public class IntezetiObjektumViewModel : BaseViewModel
    {
        public string Azonosito { get; set; }

        public string Nev { get; set; }

        public string RovidNev { get; set; }

        public int Befogadokepesseg { get; set; }

        public int? CimIranyitoszam { get; set; }

        public string CimUtca { get; set; }

        public string CimHazszam { get; set; }

        public int? LevelezesiCimIranyitoszam { get; set; }

        public string LevelezesiCimUtca { get; set; }

        public string LevelezesiCimHazszam { get; set; }

        public int IntezetId { get; set; }

        public int CimHelysegId { get; set; }

        public int LevelezesiCimHelysegId { get; set; }

        public virtual Intezet Intezet { get; set; }

        public virtual Helyseg CimHelyseg { get; set; }

        public virtual Helyseg LevelezesiCimHelyseg { get; set; }

        //[KesleltetettBetoltesu(KulsoKulcsJellemzoNeve = "Id")]
        public virtual IList<KorletViewModel> Korletek { get; set; }

        public string IntezetRovidNev
        {
            get { return Intezet.RovidNev; }
        }

        public string AzonositoNev
        {
            get { return string.Format("{0} - {1}", Azonosito, Nev); }
        }

        public string IntezetRovidNevNev
        {
            get { return string.Format("{0} - {1}", IntezetRovidNev, Nev); }
        }

        public IntezetiObjektumViewModel()
        {
            Korletek = new List<KorletViewModel>();
        }

        public static explicit operator IntezetiObjektumViewModel(IntezetiObjektum item)
        {
            IntezetiObjektumViewModel model = new IntezetiObjektumViewModel();
            model = ValueInjecterUtilities.InjectViewModel<IntezetiObjektum, IntezetiObjektumViewModel>(item);

            return model;
        }
    }
}
