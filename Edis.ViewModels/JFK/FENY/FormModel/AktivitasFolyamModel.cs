using System;

namespace Edis.ViewModels.JFK.FENY
{
    public class AktivitasFolyamModel
    {
        public string Elrendelo { get; set; }
        public string ElrendeloRendfokozat { get; set; }
        public int FegyelmiUgyId { get; set; }
        public string Jelleg { get; set; }
        public string FegyelmiUgyTipus { get; set; }
        public string NyilvantartottStatusz { get; set; }
        public string AktNyilvantartasiSzam { get; set; }
        public int? FogvatartottId { get; set; }
        public string FogvatartottNev { get; set; }
        public string Kivizsgalo { get; set; }
        public string KivizsgaloRendfokozat { get; set; }
        public string KivizsgaloSid { get; set; }
        public string ViseltNev { get; set; }
        public string SzuletesiHely { get; set; }
        public DateTime SzuletesiDatum { get; set; }
        public string AnyjaNeve { get; set; }
        public string FogvatartottFogvatartasJellege { get; set; }
        public string FogvatartottVegrehajtasiFok { get; set; }
        public int FogvatartottAktualisBvIntezet { get; set; }
        public int UgyStatuszId { get; set; }
        public string UgyStatusz { get; set; }
        public int UgySzama { get; set; }
        public int UgyEve { get; set; }
        public string UgyIntezetAzon { get; set; }
        public string Intezet { get; set; }
        public DateTime? Hatarido { get; set; }
        public string Elhelyezes { get; set; }
        public string EsemenyMegnevezese { get; set; }
        public DateTime EsemenyDatuma { get; set; }
        public DateTime ElsoBefogadasDatuma { get; set; }
        public bool? FelfuggesztesiJavaslat { get; set; }
        public bool? Felfuggesztve { get; set; }
        public bool? HataridoModositasJavaslat { get; set; }
        public DateTime? ElsofokuTargyalasIdopontja { get; set; }
        public DateTime? MasodfokuTargyalasIdopontja { get; set; }
        public bool? Visszakuldve { get; set; }
        public bool? VanJogiKepviselet { get; set; }
        public int? FoFegyelmiUgyId { get; set; }
        public bool? KozvetitoiEljarasban { get; set; }
        public bool? Lezarva { get; set; }
        public int? FenyitesHossza { get; set; }
        public int? FenyitesHosszaMennyisegiEgysegCimkeId { get; set; }
        public string FenyitesMennyEgysCimke { get; set; }
        public DateTime? VegrehajtasKezdeteIdo { get; set; }
        public double? VegrehajtasHosszEddig { get; set; }
        public DateTime? VegrehajtasVegeIdo { get; set; }
        public int? HatarozatHozoJogkoreCimkeId { get; set; }
        public DateTime ErvenyessegKezd { get; set; }
        public string NaploTipusNev { get; set; }
        public string RogzitoSzemelyNev { get; set; }
        public string RogzitoSzemelyRendfokozat { get; set; }

        public bool? HasSertettFL { get; set; }
        public DateTime? KitoltveSzabadulasDatum { get; set; }
        public DateTime? FeltetelesSzabadulasDatum { get; set; }
        public bool? FeltetelesSzabadulasEngedFl { get; set; }
        public DateTime? SzallitasraElojegyezve { get; set; }
        public bool? SzakteruletiVelemenyreVarFL { get; set; }
        public int? IteletJellegKszId { get; set; }
        public string FegyelmiIntezet { get; set; }
        public int FegyelmiIntezetId { get; set; }
        public DateTime? KivizsgalasiHatarido { get; set; }

        public bool? EljarasAlaVontatMeghallgattukFL { get; set; }
        public DateTime? TenylegesSzabadulasDatuma
        {
            get
            {
                if (FeltetelesSzabadulasEngedFl == true || (IteletJellegKszId == 671 && FeltetelesSzabadulasDatum.HasValue))
                {
                    return FeltetelesSzabadulasDatum;
                }
                return KitoltveSzabadulasDatum;
            }
        }
        public string TenylegesSzabadulasDatumaSzoveg
        {
            get
            {
                string ret = string.Empty;
                if (FeltetelesSzabadulasEngedFl == true && FeltetelesSzabadulasDatum.HasValue)
                    ret = "Feltételesen: " + FeltetelesSzabadulasDatum.Value.ToString("yyyy.MM.dd");
                if (FeltetelesSzabadulasEngedFl == false && KitoltveSzabadulasDatum.HasValue)
                    ret = KitoltveSzabadulasDatum.Value.ToString("yyyy.MM.dd");

                return ret;
            }
        }
        public string ToltottIdoSzazalekban
        {
            get
            {
                string ret = string.Empty;
                if (TenylegesSzabadulasDatuma != null)
                {
                    ret = (Math.Abs((DateTime.Now.Subtract(ElsoBefogadasDatuma).TotalDays) / Math.Abs(TenylegesSzabadulasDatuma.Value.Subtract(ElsoBefogadasDatuma).TotalDays)) * 100).ToString("0");
                }
                else
                {
                    ret = "0";
                }
                return ret;
            }
        }

        public string IteletIdoSzovegesen
        {
            get
            {
                string ret = string.Empty;
                if (TenylegesSzabadulasDatuma != null)
                {
                    ret = "Összesen: " + GetElapsedTime(ElsoBefogadasDatuma, TenylegesSzabadulasDatuma.Value);
                }
                else
                {
                    ret = "Nincs kiszámítva a tényleges szabadulás dátuma";
                }
                return ret;
            }
        }

        private string GetElapsedTime(DateTime from_date, DateTime to_date)
        {
            int years;
            int months;
            int days;
            int hours;
            int minutes;
            int seconds;
            int milliseconds;

            //------------------
            // Handle the years.
            //------------------
            years = to_date.Year - from_date.Year;

            //------------------------
            // See if we went too far.
            //------------------------
            DateTime test_date = from_date.AddMonths(12 * years);

            if (test_date > to_date)
            {
                years--;
                test_date = from_date.AddMonths(12 * years);
            }

            //--------------------------------
            // Add months until we go too far.
            //--------------------------------
            months = 0;

            while (test_date <= to_date)
            {
                months++;
                test_date = from_date.AddMonths(12 * years + months);
            }

            months--;

            //------------------------------------------------------------------
            // Subtract to see how many more days, hours, minutes, etc. we need.
            //------------------------------------------------------------------
            from_date = from_date.AddMonths(12 * years + months);
            TimeSpan remainder = to_date - from_date;
            days = remainder.Days;
            hours = remainder.Hours;
            minutes = remainder.Minutes;
            seconds = remainder.Seconds;
            milliseconds = remainder.Milliseconds;

            return (years > 0 ? years.ToString() + " év " : "") +
                   (months > 0 ? months.ToString() + " hónap " : "") +
                   (days > 0 ? days.ToString() + " nap " : "");
        }
    }
}
