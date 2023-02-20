using Edis.Entities.Fany;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Fany
{
    public class IntezetModel
    {
        public int Id { get; set; }

        public bool TOROLT_FL { get; set; }

        public string Azonosito { get; set; }
        
        public string Azonosito2 { get; set; }
        
        public string Nev { get; set; }
        
        public string KozepesNev { get; set; }
        
        public string RovidNev { get; set; }
        
        public string TelefonFax { get; set; }
        
        public string Email { get; set; }
        
        public int? CimIranyitoszam { get; set; }
        
        public string CimUtca { get; set; }
        
        public string CimHazszam { get; set; }
        
        public int? LevelezesiCimIranyitoszam { get; set; }
        
        public string LevelezesiCimUtca { get; set; }
        
        public string LevelezesiCimHazszam { get; set; }
        
        public DateTime? MegszunesDatum { get; set; }
        
        public int Befogadokepesseg { get; set; }
        
        public int IntezetJellegKszId { get; set; }
        
        public int? CimHelysegId { get; set; }

        public string CimHelysegNev { get; set; }

        public bool Megjelenik { get; set; }

        public double? SzelessegiKoordinata { get; set; }
        public double? HosszusagiKoordinata { get; set; }
        
        public int? LevelezesiCimHelysegId { get; set; }

        public static explicit operator IntezetModel(Intezet item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<Intezet, IntezetModel>(item);
            if (item.CimHelyseg != null)
                model.CimHelysegNev = item.CimHelyseg?.Nev ?? string.Empty;
            return model;
        }



        public static explicit operator Intezet(IntezetModel model)
        {
            Intezet entity = new Intezet();
            entity = ValueInjecterUtilities.InjectViewModel<IntezetModel, Intezet>(model);

            return entity;
        }
    }
}
