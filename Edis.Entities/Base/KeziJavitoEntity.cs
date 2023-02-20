using Edis.Entities.Fany;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    public class KeziJavitoEntity : KonaExtendedEntity
    {

        [Column("letrehozas_datuma")]
        public DateTime LetrehozasDatuma { get; set; }

        [Column("kezi_javitas_azonosito")]
        public int? KeziJavitasAzonosito { get; set; }

    }

    public class KeziJavitoEntityStatuszFigyelessel : KeziJavitoEntity
    {
        [Column("statusz_ksz_id")]
        public int StatuszKszId { get; set; }
        [Column("aktualis_statusz_ervenyessegenek_kezdete")]
        public DateTime AktualisStatuszErvenyessegenekKezdete { get; set; }
        [ForeignKey("StatuszKszId")]
        public virtual Kodszotar Statusz { get; set; }
    }
}
