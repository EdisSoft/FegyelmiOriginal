﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY.Email
{
    public class TargyiErtesitesEmailItem
    {
        //public int SzemelyId { get; set; }
        public List<ErtesitendoSzemely> ErtesitendoSzemelyek { get; set; }

        public List<FegyelmiUgyItem> KorlatozottFegyelmiList { get; set; }
        public List<FegyelmiUgyItem> EngedelyezettFegyelmiList { get; set; }

        public TargyiErtesitesEmailItem()
        {
            ErtesitendoSzemelyek = new List<ErtesitendoSzemely>();
            KorlatozottFegyelmiList = new List<FegyelmiUgyItem>();
            EngedelyezettFegyelmiList = new List<FegyelmiUgyItem>();
        }

        public TargyiErtesitesEmailData GetEmailData()
        {
            TargyiErtesitesEmailData data = new TargyiErtesitesEmailData()
            {
                KorlatozottFegyelmiList = KorlatozottFegyelmiList,
                EngedelyezettFegyelmiList = EngedelyezettFegyelmiList,
                CimzettekTitle = (ErtesitendoSzemelyek.Count == 1 && !ErtesitendoSzemelyek[0].Email.Contains(";")) ? (string.IsNullOrEmpty(ErtesitendoSzemelyek[0].SzemelyNev) ? "Címzett" : (ErtesitendoSzemelyek[0].SzemelyNev + " " + ErtesitendoSzemelyek[0].SzemelyBeosztas)) : "Címzettek",
                Datum = DateTime.Now.ToShortDateString(),
                EmailAddresses = string.Join(";", ErtesitendoSzemelyek.Select(x => x.Email))
            };
            return data;
        }
    }
}
