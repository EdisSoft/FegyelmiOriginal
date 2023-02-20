using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Base;

namespace Edis.Entities.Common
{
    [Table("nyomtatvany_sablon")]
    public class NyomtatvanySablon : KonaExtendedEntity
    {

        [Column("MODUL_MEGNEVEZESE")]
        public string ModulMegnevezese { get; set; }

        [Column("NYOMTATVANY_NEVE")]
        public string NyomtatvanyNeve { get; set; }

        [Column("NYOMTATVANY_LEIRASA")]
        public string NyomtatvanyLeirasa { get; set; }

        [Column("NYOMTATVANY_SABLON_DOCX")]
        public byte[] NyomtatvanySablonDocx { get; set; }

        [Column("FOOSZTALYVEZETO_ALAIRASA_SZUKSEGES")]
        public bool? FoosztalyVezetoAlairasaSzukseges { get; set; }

    }
}
