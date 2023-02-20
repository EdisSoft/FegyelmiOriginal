namespace Edis.Entities
{
    public class BvShopVasarlasString
    {
        public const string VASARLASI_ADATOK_XML_ELEJE = "<vasarlasiAdatok xmlns=\"http://www.konasoft.hu/fonix\"><terminalAzonosito>{0}</terminalAzonosito><fogvatartottiKartyaAzonosito>{1}</fogvatartottiKartyaAzonosito><vasarlasDatum>{2}</vasarlasDatum><vasarlasiTetelek>";
        public const string VASARLASI_ADATOK_XML_TETEL = "<vasarlasiTetel><termekNev>{0}</termekNev><termekEgysegar>{1}</termekEgysegar><termekKod>{2}</termekKod><termekMennyiseg>{3}</termekMennyiseg><mennyisegMertekegyseg>{4}</mennyisegMertekegyseg></vasarlasiTetel>";
        public const string VASARLASI_ADATOK_XML_VEGE = "</vasarlasiTetelek></vasarlasiAdatok>";
        public const string TELEFONKARTYA_FELTOLTES = "Telefonkártya feltöltés";
    }
}
