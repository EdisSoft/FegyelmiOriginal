using System.ComponentModel;

namespace Edis.Entities.Enums
{
    public enum FoglMunkaraAlkamassagSzoveg
    {
        [Description("Egészséges, bármilyen munkával foglalkoztatható")]
        Egeszseges,
        [Description("Nem dolgozhat vegyszerrel")]
        Vegyszer,
        [Description("Tartós járást, állást nem végezhet")]
        Jaras,
        [Description("Nem dolgozhat több műszakban")]
        TobbMuszak,
        [Description("Zajos helyen nem dolgozhat")]
        Zajos,
        [Description("Poros, füstös helyen nem dolgozhat")]
        Poros,
        [Description("Kényszer helyzetben nem dolgozhat")]
        Kenyszer,
        [Description("Fogyatékkal élő")]
        Fogyatek,
        [Description("Fokozottan balesetveszélyes körülmények között nem dolgozhat")]
        Fokozott,
        [Description("Járványügyi érdekből kiemelt munkahelyen nem dolgozhat")]
        Jarvany,
        [Description("Körlet takarításon dolgozhat")]
        Korlet,
        [Description("Külső körlet munkán dolgozhat")]
        KulsoKorlet,
        [Description("Meghatározott teher emelése, szállítása")]
        Teher,
        [Description("Anyagmozgatás")]
        Anyagmozgatas,
        [Description("Maximum emelhető teher")]
        MaxTeher,
        [Description("Anyagmozgatási korlát")]
        AnyagKorlat,
        [Description("Díjazás nélküli munkavégzés maximális ideje")]
        DijNelkuliMunkMaxIdeje,
        [Description("Egyéb foglalkoztatást meghatározó tényező")]
        EgyebFoglMeghatTenyezo,
        [Description("Megjegyzés")]
        Megjegyzes


    }
}
