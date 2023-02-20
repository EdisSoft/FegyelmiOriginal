using Edis.Entities.Fany;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{
    public class SzemelyzetModel
    {
        public int Id { get; set; }
        public string Azonosito { get; set; }

        private string nev;
        //public string Nev { get; set; }
        public string Nev
        {
            get
            {
                return nev;
            }
            set
            {
                if (value == null)
                    nev = "";
                else
                    nev = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
            }
        }


        public int IntezetiDolgozo { get; set; }

        public int? IntezetId { get; set; }

        public int RendfokozatKszId { get; set; }

        public string Rendfokozat { get; set; }
        public int BeosztasKszId { get; set; }

        public int? BesorolasKszId { get; set; }

        public string AdSid { get; set; }

        public static explicit operator SzemelyzetModel(Szemelyzet item)
        {
            SzemelyzetModel model = new SzemelyzetModel();
            model = ValueInjecterUtilities.InjectViewModel<Szemelyzet, SzemelyzetModel>(item);
            model.Rendfokozat = item.Rendfokozat?.Nev;
            return model;
        }




        public static explicit operator Szemelyzet(SzemelyzetModel model)
        {
            Szemelyzet entity = new Szemelyzet();
            entity = ValueInjecterUtilities.InjectViewModel<SzemelyzetModel, Szemelyzet>(model);

            return entity;
        }

        public override string ToString()
        {
            return $"I_ID: {IntezetId} - {Nev}";
        }
    }
}