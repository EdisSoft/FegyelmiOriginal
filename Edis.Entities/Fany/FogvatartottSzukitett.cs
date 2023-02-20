using Edis.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    /// <summary>
    /// Fogvatartott legfontosabb alapadatai
    /// </summary>
    [Table("FOGV_SZUKITETT")]
    public class FogvatartottSzukitett : BaseEntity
    {

        /// <summary>
        /// A fogvatartott törölt
        /// </summary>
        public int Torolve { get; set; }

        /// <summary>
        /// Fogvatartott aktuális azonosítója
        /// </summary>
        [Column("AKT_FOGV_AZON_KOD")]
        public string FogvatartottAktualisAzonosito { get; set; } //ft_fogv_az

        /// <summary>
        /// Fogvatartott nyilvántartási azonosítója
        /// </summary>
        [Column("NYILV_FOGV_AZON_KOD")]
        public string FogvatartottNyilvantartasiAzonosito { get; set; } //ft_fogv_az

        /// <summary>
        /// Fogvatartott aktuális intézetének FANY egyedi ID-je
        /// </summary>
        [Column("AKTUALIS_BVINT_ID")]
        public int AktualisIntezetId { get; set; }

        /// <summary>
        /// Fogvatartott aktuális intézetének háromjegyű azonosítója (pl. '101')
        /// </summary>
        public string AktualisIntezetAzonosito { get; set; } //ft_aktint

        /// <summary>
        /// Fogvatartott aktuális intézetének négyjegyű azonosítója (pl. 'FVRS')
        /// </summary>
        public string AktualisIntezetAzonosito2 { get; set; } //ft_aktint

        /// <summary>
        /// Fogvatartott aktuális intézetének neve
        /// </summary>
        public string AktualisIntezetNev { get; set; }

        /// <summary>
        /// Fogvatartott aktuális intézetének rövid neve
        /// </summary>
        public string AktualisIntezetRovidNev { get; set; }

        /// <summary>
        /// Fogvatartott nyilvántartó intézetének FANY egyedi ID-je
        /// </summary>
        public int NyilvantartoIntezetId { get; set; }

        /// <summary>
        /// Fogvatartott nyilvántartó intézetének háromjegyű azonosítója (pl. '101')
        /// </summary>
        public string NyilvantartoIntezetAzonosito { get; set; } //ft_nyt_int ft_int_az2??

        /// <summary>
        /// Fogvatartott nyilvántartó intézetének négyjegyű azonosítója (pl. 'FVRS')
        /// </summary>
        public string NyilvantartoIntezetAzonosito2 { get; set; } //ft_aktint

        /// <summary>
        /// Fogvatartott nyilvántartó intézetének neve
        /// </summary>
        public string NyilvantartoIntezetNev { get; set; }

        /// <summary>
        /// Fogvatartott nyilvántartó intézetének rövid neve
        /// </summary>
        public string NyilvantartoIntezetRovidNev { get; set; }

        /// <summary>
        /// Nyilvántartási státusz kódszótár azonosítója
        /// </summary>
        public string NyilvantartasiStatuszAzonosito { get; set; } //ft_ny_stat

        /// <summary>
        /// Nyilvántartási státusz neve
        /// </summary>
        public string NyilvantartasiStatuszNev { get; set; }

        /// <summary>
        /// Fogvatartott családi neve
        /// </summary>
        public string CsaladiNev { get; set; } //ft_cs_nev

        /// <summary>
        /// Fogvatartott utóneve
        /// </summary>
        public string Utonev { get; set; } //ft_u_nev

        /// <summary>
        /// Fogvatartott születési családi neve
        /// </summary>
        public string SzuletesiCsaladiNev { get; set; } //ft_lcs_nev

        /// <summary>
        /// Fogvatartott születési utóneve
        /// </summary>
        public string SzuletesiUtonev { get; set; } //ft_lu_nev

        /// <summary>
        /// Fogvatartott anyja neve
        /// </summary>
        public string AnyjaNeve { get; set; } //ft_any_nev

        /// <summary>
        /// Fogvatartott születési dátuma
        /// </summary>
        public DateTime SzuletesiDatum { get; set; } //ft_szu_dat

        /// <summary>
        /// Fogvatartott születési helységének neve
        /// </summary>
        public string SzuletesiHelyNeve { get; set; } //ft_szuhely

        /// <summary>
        /// Fogvatartott nemének FANY kódszótár ID-je
        /// </summary>
        public int NemId { get; set; }

        /// <summary>
        /// Fogvatartott nemének kódszótár azonosítója
        /// </summary>
        public string NemAzonosito { get; set; } //ft_neme

        /// <summary>
        /// Fogvatartott neme
        /// </summary>
        public string NemNev { get; set; }

        /// <summary>
        /// Fogvatartott fő állampolgárságának FANY kódszótár ID-je
        /// </summary>
        public int? FoAllampolgarsagId { get; set; }

        /// <summary>
        /// Fogvatartott fő állampolgárságának kódszótár azonosítója
        /// </summary>
        public string FoAllampolgarsagAzonosito { get; set; } //ft_allpolg

        /// <summary>
        /// Fogvatartott fő állampolgárságának neve
        /// </summary>
        public string FoAllampolgarsagNev { get; set; }

        /// <summary>
        /// Fogvatartott további állampolgárságainak FANY kódszótár ID-je
        /// </summary>
        public IList<int> AllampolgarsagokId { get; set; }

        /// <summary>
        /// Fogvatartott további állampolgárságainak kódszótár azonosítója
        /// </summary>
        public IList<string> AllampolgarsagokAzonosito { get; set; }

        /// <summary>
        /// Fogvatartott fő állampolgárságainak neve
        /// </summary>
        public IList<string> AllampolgarsagokNev { get; set; }

        /// <summary>
        /// Fogvatartott személyi azonosítója
        /// </summary>
        public string SzemelyiAzonositoJel { get; set; } //ft_szem_az

        /// <summary>
        /// Fogvatartott tajszáma
        /// </summary>
        public string TajSzam { get; set; } //ft_tajszam

        /// <summary>
        /// Fogvatartott állandó lakcímének irányítószáma
        /// </summary>
        public int? AllandoLakcimIranyitoszam { get; set; } //ft_allak_i

        /// <summary>
        /// Fogvatartott állandó lakcím helységének neve
        /// </summary>
        public string AllandoLakcimHelysegnev { get; set; } //ft_allak_h

        /// <summary>
        /// Fogvatartott állandó lakcím utcája
        /// </summary>
        public string AllandoLakcimUtca { get; set; } //ft_allak_u

        /// <summary>
        /// Fogvatartott állandó lakcímének házszáma
        /// </summary>
        public string AllandoLakcimHazszam { get; set; } //ft_allak_s?

        /// <summary>
        /// Fogvatartott bejelentett lakcímének irányítószáma
        /// </summary>
        public int? BejelentettLakcimIranyitoszam { get; set; } //ft_idlak_i

        /// <summary>
        /// Fogvatartott bejelentett lakcím helységének neve
        /// </summary>
        public string BejelentettLakcimHelysegnev { get; set; } //ft_idlak_h

        /// <summary>
        /// Fogvatartott bejelentett lakcím utcája
        /// </summary>
        public string BejelentettLakcimUtca { get; set; } //ft_idlak_u

        /// <summary>
        /// Fogvatartott bejelentett lakcímének házszáma
        /// </summary>
        public string BejelentettLakcimHazszam { get; set; } //ft_idlak_s?

        /// <summary>
        /// Fogvatartott aktuális szabadulásának dátuma
        /// </summary>
        public DateTime? AktualisSzabadulasDatuma { get; set; }

        /// <summary>
        /// Fogvatartott aktuális befogadásának dátuma
        /// </summary>
        public DateTime? AktualisBefogadasDatuma { get; set; }

        /// <summary>
        /// Védett személy
        /// </summary>
        [Column("VEDETT_FL")]
        public int Vedett { get; set; }

        /// <summary>
        /// Fogvatartott kis indexképe
        /// </summary>
        public byte[] KisIndexKep { get; set; }

        /// <summary>
        /// Fogvatartott nagy indexképe
        /// </summary>
        public byte[] NagyIndexKep { get; set; }

        public static FogvatartottSzukitett Anonymize(FogvatartottSzukitett u)
        {
            if (u.Vedett != 1)
                return u;

            u.Utonev = u.FogvatartottAktualisAzonosito;
            u.CsaladiNev = u.FogvatartottAktualisAzonosito;
            u.SzuletesiUtonev = u.FogvatartottAktualisAzonosito;
            u.TajSzam = u.FogvatartottAktualisAzonosito;
            u.SzuletesiHelyNeve = u.FogvatartottAktualisAzonosito;
            u.SzuletesiDatum = new DateTime(1900, 01, 01);
            u.SzuletesiCsaladiNev = u.FogvatartottAktualisAzonosito;
            u.SzemelyiAzonositoJel = u.FogvatartottAktualisAzonosito;
            u.BejelentettLakcimUtca = u.FogvatartottAktualisAzonosito;
            u.AllandoLakcimHazszam = u.FogvatartottAktualisAzonosito;
            u.AllandoLakcimUtca = u.FogvatartottAktualisAzonosito;
            u.AllandoLakcimHelysegnev = u.FogvatartottAktualisAzonosito;
            u.AllandoLakcimIranyitoszam = 9999;
            u.BejelentettLakcimUtca = u.FogvatartottAktualisAzonosito;
            u.BejelentettLakcimIranyitoszam = 9999;
            u.BejelentettLakcimHelysegnev = u.FogvatartottAktualisAzonosito;
            u.BejelentettLakcimHazszam = u.FogvatartottAktualisAzonosito;
            u.KisIndexKep = null;
            u.NagyIndexKep = null;
            u.AnyjaNeve = u.FogvatartottAktualisAzonosito;
            return u;
        }
    }
}