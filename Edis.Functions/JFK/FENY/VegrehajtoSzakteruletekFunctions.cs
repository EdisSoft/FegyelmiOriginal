using Edis.Diagnostics;
using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.Functions.Fany;
using Edis.IoC.Attributes;
using Edis.Utilities;
using Edis.ViewModels.JFK;
using Edis.ViewModels.JFK.FENY;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Functions.JFK.FENY
{
    public class VegrehajtoSzakteruletekFunctions : KonasoftBVFonixFunctionsBase<VegrehajtoSzakteruletekViewModel, VegrehajtoSzakteruletek>, IVegrehajtoSzakteruletekFunctions
    {
        [Inject]
        IJogosultsagCacheFunctions JogosultsagCacheFunctions { get; set; }
        [Inject]
        ISzemelyzetFunctions SzemelyzetFunctions { get; set; }

        public RendszerBeallitasokModel GetRendszerbeallitasokModalData(int intezetId)
        {

            RendszerBeallitasokModel model = new RendszerBeallitasokModel();
            model.LetetesekOptions = new List<KSelect2ItemModel>();
            model.RendezvenySzervezokOptions = new List<KSelect2ItemModel>();

            VegrehajtoSzakteruletek entity = KonasoftBVFonixContext.VegrehajtoSzakteruletek.AsNoTracking().FirstOrDefault(x => x.IntezetId == intezetId);

            // intézeti userek kigyűjtése
            var intezetiUsers = SzemelyzetFunctions.GetIntezetiAlkalmazottak();

            // akiknek nincs email címe
            foreach (var user in intezetiUsers)
            {
                var email = new ActiveDirectoryKezeloFunctions().KeresEmailcim(user.Sid);
                if (email != null && email.Trim() != "")
                {
                    model.LetetesekOptions.Add(new KSelect2ItemModel() {
                        id = $"{user.Displayname.ToTitleCase()}{(user.Rendfokozat == null ? "" : " " + user.Rendfokozat)} <{email}>",
                        text = $"{user.Displayname.ToTitleCase()} {(user.Rendfokozat == null ? "" : " " + user.Rendfokozat)}" });
                    model.RendezvenySzervezokOptions.Add(new KSelect2ItemModel() {
                        id = $"{user.Displayname.ToTitleCase()}{(user.Rendfokozat == null ? "" : " " + user.Rendfokozat)} <{email}>",
                        text = $"{user.Displayname.ToTitleCase()} {(user.Rendfokozat == null ? "" : " " + user.Rendfokozat)}" });
                }
                //ertesitendoSzemelyek.Add(ugy.ElrendeloSid, new ErtesitendoSzemely()
                //{
                //    Email = email,
                //    SzemelySid = ugy.ElrendeloSid,
                //    SzemelyNev = ugy.ElrendeloNev,
                //    SzemelyBeosztas = ugy.ElrendeloBeosztas
                //});
            }


            if (entity != null)
            {
                model.LetetesekIds = entity.LetetesCimzettLista?.Split(';').ToList() ?? new List<string>();
                //model.LetetesekOptions = model.LetetesekIds.Select(x => new KSelect2ItemModel { id = x, text = x }).ToList();
                model.RendezvenySzervezokIds = entity.RendezvenySzervezoCimzettLista?.Split(';').ToList() ?? new List<string>();
                //model.RendezvenySzervezokOptions = model.RendezvenySzervezokIds.Select(x => new KSelect2ItemModel { id = x, text = x }).ToList();
            }
            else
            {
                model.LetetesekIds = new List<string>();
                model.RendezvenySzervezokIds = new List<string>();
                //model.LetetesekOptions = new List<KSelect2ItemModel>();
                //model.RendezvenySzervezokOptions = new List<KSelect2ItemModel>();
            }

            //model.RendezvenySzervezokOptions.AddRange(intezetiUsers.Select(x => new KSelect2ItemModel() { id = x.Sid, text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList());
            //model.LetetesekOptions.AddRange(intezetiUsers.Select(x => new KSelect2ItemModel() { id = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)), text = x.Displayname + (x.Rendfokozat == null ? "" : (" " + x.Rendfokozat)) }).ToList());

            return model;
        }

        public void SaveRendszerbeallitasokModalData(RendszerBeallitasokModel model)
        {
            int intezetId = JogosultsagCacheFunctions.AktualisIntezet.Id;

            //List<string> menthetoLetetesek = new List<string>();
            //List<string> ujLetetesek = new List<string>();
            string letetesCimzettLista = "";
            //List<string> menthetoRendezvenySzervezok = new List<string>();
            //List<string> ujRendezvenySzervezok = new List<string>();
            string rendezvenySzervezoCimzettLista = "";

            if (model.LetetesekIds != null)
            {


                //foreach (var letetes in model.LetetesekIds)
                //{
                //        menthetoLetetesek.Add(letetes);
                //};

                letetesCimzettLista = $"{string.Join(";", model.LetetesekIds)}";
            }



            if (model.RendezvenySzervezokIds != null)
            {
                //foreach (var rendezvenySzervezo in model.RendezvenySzervezokIds)
                //{
                //        menthetoRendezvenySzervezok.Add(rendezvenySzervezo);
                //};

                rendezvenySzervezoCimzettLista = $"{string.Join(";", model.RendezvenySzervezokIds)}";
            }

            using (DbContextTransaction transaction = KonasoftBVFonixContext.Database.BeginTransaction())
            {
                try
                {
                    VegrehajtoSzakteruletek entity = KonasoftBVFonixContext.VegrehajtoSzakteruletek.AsNoTracking().FirstOrDefault(x => x.IntezetId == intezetId);

                    if (entity != null)
                    {
                        if (letetesCimzettLista == "") letetesCimzettLista = null;
                        if (rendezvenySzervezoCimzettLista == "") rendezvenySzervezoCimzettLista = null;
                        entity.LetetesCimzettLista = letetesCimzettLista;
                        entity.RendezvenySzervezoCimzettLista = rendezvenySzervezoCimzettLista;
                        Modify((VegrehajtoSzakteruletekViewModel)entity);
                        KonasoftBVFonixContext.SaveChanges();
                    }
                    else
                    {
                        entity = new VegrehajtoSzakteruletek()
                        {
                            IntezetId = intezetId,
                            LetetesCimzettLista = letetesCimzettLista,
                            RendezvenySzervezoCimzettLista = rendezvenySzervezoCimzettLista
                        };

                        KonasoftBVFonixContext.VegrehajtoSzakteruletek.Add(entity);
                        KonasoftBVFonixContext.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    Log.Error($"Hiba a rendszerbeállítások módosítása során (intezetId: {intezetId})", ex);
                    transaction.Rollback();

                    throw;
                }
            }
        }

    }
}
