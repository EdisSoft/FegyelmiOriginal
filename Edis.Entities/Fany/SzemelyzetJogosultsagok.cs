using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class SzemelyzetJogosultsagok : IEnumerable<JogosultsagInformacio>
    {
        #region jellemzők
        private Szemelyzet BvAlkalmazott { get; set; }
        private List<SzemelyzetSzerepkor> MindenSzerepkor { get; set; }
        private List<GroupPrincipal> BvAlkalmazottGroupPrincipaljai { get; set; }
        private IDictionary<string, int> IntezetAzonosito2IdMegfeleltetes { get; set; }
        public bool CsakIntezet { get; set; }
        public string KivalasztottIntezetAzonosito2 { get; set; }
        #endregion

        #region konstruktor
        public SzemelyzetJogosultsagok(Szemelyzet bvAlkalmazott, List<GroupPrincipal> bvopAlkalmazottGroupPrincipaljai, List<SzemelyzetSzerepkor> mindenSzerepkor, IDictionary<string, int> intezetAzonosito2IdMegfeleltetes, bool csakIntezet, string kivalasztottIntezetAzonosito2)
            : this(bvAlkalmazott, bvopAlkalmazottGroupPrincipaljai, mindenSzerepkor, intezetAzonosito2IdMegfeleltetes, csakIntezet)
        {
            KivalasztottIntezetAzonosito2 = kivalasztottIntezetAzonosito2;
        }

        public SzemelyzetJogosultsagok(Szemelyzet bvAlkalmazott, List<GroupPrincipal> bvopAlkalmazottGroupPrincipaljai, List<SzemelyzetSzerepkor> mindenSzerepkor, IDictionary<string, int> intezetAzonosito2IdMegfeleltetes, bool csakIntezet)
        {
            BvAlkalmazott = bvAlkalmazott;
            MindenSzerepkor = mindenSzerepkor;
            BvAlkalmazottGroupPrincipaljai = bvopAlkalmazottGroupPrincipaljai;
            IntezetAzonosito2IdMegfeleltetes = intezetAzonosito2IdMegfeleltetes;
            CsakIntezet = csakIntezet;
            KivalasztottIntezetAzonosito2 = "";
        }
        #endregion konstruktor

        #region eljárások
        #region IEnumerable eljárásai
        public IEnumerator<JogosultsagInformacio> GetEnumerator()
        {
            return new SzemelyzetJogosultsagEnumerator(BvAlkalmazott, BvAlkalmazottGroupPrincipaljai, MindenSzerepkor, IntezetAzonosito2IdMegfeleltetes, CsakIntezet, KivalasztottIntezetAzonosito2);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion IEnumerable eljárásai

        public class SzemelyzetJogosultsagEnumerator : IEnumerator<JogosultsagInformacio>
        {
            private Szemelyzet _bvopAlkalmazott;
            private readonly List<GroupPrincipal> _bvopAlkalmazottSzemelyzetCsoportjai;
            private readonly IDictionary<string, SzemelyzetSzerepkor> _szerepkorSzotar;
            private IEnumerator<GroupPrincipal> _groupPrincipalEnum;
            private SzemelyzetSzerepkor _aktualisSzerepkor;
            private IEnumerator<SzemelyzetSzerepkorJogosultsag> _jogosultsagEnum;
            private JogosultsagInformacio _current;
            private readonly IDictionary<string, int> _intezetAzonosito2IdMegfeleltetes;
            private readonly bool _csakIntezet;
            private readonly string _kivalasztottIntezetAzonosito2;
            private int aktualisIntezetId;
            private string groupSidStr = "";

            public JogosultsagInformacio Current { get { return _current; } }

            public SzemelyzetJogosultsagEnumerator(Szemelyzet bvopAlkalmazott, List<GroupPrincipal> bvopAlkalmazottSzemelyzetCsoportjai,
                IEnumerable<SzemelyzetSzerepkor> szerepkorok, IDictionary<string, int> intezetAzonosito2IdMegfeleltetes, bool csakIntezet, string kivalasztottIntezetAzonosito2)
            {

                _bvopAlkalmazott = bvopAlkalmazott;
                _bvopAlkalmazottSzemelyzetCsoportjai = bvopAlkalmazottSzemelyzetCsoportjai;
                _intezetAzonosito2IdMegfeleltetes = intezetAzonosito2IdMegfeleltetes;
                _csakIntezet = csakIntezet;
                _kivalasztottIntezetAzonosito2 = kivalasztottIntezetAzonosito2;
                var teljesSzerepkorSzotar = szerepkorok.ToDictionary(szemelyzetSzerepkor => szemelyzetSzerepkor.Azonosito);
                _szerepkorSzotar = new Dictionary<string, SzemelyzetSzerepkor>();
               
                foreach (var groupPrincipal in bvopAlkalmazottSzemelyzetCsoportjai)
                {
                    if (GroupPrincipalSeged.IsFonixGroup(groupPrincipal))
                    {
                        var szerepkorNev = GroupPrincipalSeged.SzerepkorNeve(groupPrincipal);
                        if (teljesSzerepkorSzotar.ContainsKey(szerepkorNev))
                        {
                            if (!_szerepkorSzotar.ContainsKey(szerepkorNev))
                                _szerepkorSzotar.Add(szerepkorNev, teljesSzerepkorSzotar[szerepkorNev]);
                        }
                    }
                }
                Reset();
            }

            public bool MoveNext()
            {
                try
                {
                    if (_groupPrincipalEnum == null)
                    {
                        _groupPrincipalEnum = _bvopAlkalmazottSzemelyzetCsoportjai.GetEnumerator();
                    }
                    bool globalis = false;
                    bool vanKovetkezoJogosultsag = (_jogosultsagEnum != null && _jogosultsagEnum.MoveNext());
                    while (!vanKovetkezoJogosultsag)
                    {
                        bool vanKovetkezoSzemelyzetCsoport = (_groupPrincipalEnum != null &&
                                                              _groupPrincipalEnum.MoveNext());
                        if (!vanKovetkezoSzemelyzetCsoport)
                        {
                            return false;
                        }
#if !DEBUG
                        groupSidStr = _groupPrincipalEnum.Current.Sid.Value;
#else
                        var debugGroupPrincipal = (DebugGroupPrincipal)_groupPrincipalEnum.Current;
                        groupSidStr = debugGroupPrincipal.Sid.Value;
#endif
                        if (GroupPrincipalSeged.IsFonixGroup(_groupPrincipalEnum.Current))
                        {
                            var kovetkezoSzerepkorAzonosito =
                                GroupPrincipalSeged.SzerepkorNeve(_groupPrincipalEnum.Current);
                            var intezetAzonosito2 = GroupPrincipalSeged.IntezetAzonosito2(_groupPrincipalEnum.Current);
                            aktualisIntezetId = _intezetAzonosito2IdMegfeleltetes.ContainsKey(intezetAzonosito2)
                                                    ? _intezetAzonosito2IdMegfeleltetes[intezetAzonosito2]
                                                    : 0;
                            if ((_kivalasztottIntezetAzonosito2 == "" ||
                                 _kivalasztottIntezetAzonosito2 == intezetAzonosito2)
                                && _szerepkorSzotar.ContainsKey(kovetkezoSzerepkorAzonosito))
                            {
                                _aktualisSzerepkor = _szerepkorSzotar[kovetkezoSzerepkorAzonosito];
                                var jog = _aktualisSzerepkor.SzerepkorJogosultsagok;
                                _jogosultsagEnum = _aktualisSzerepkor.SzerepkorJogosultsagok.GetEnumerator();
                                globalis = _aktualisSzerepkor.Globalis;
                                vanKovetkezoJogosultsag = _jogosultsagEnum.MoveNext();
                            }
                        }
                    }

                    if (_csakIntezet)
                        _current = new JogosultsagInformacio(aktualisIntezetId);
                    else
                        _current = new JogosultsagInformacio(_bvopAlkalmazott.AdSid,
                                                             groupSidStr,
                                                             _aktualisSzerepkor.Azonosito,
                                                             _jogosultsagEnum.Current.SzemelyzetJogosultsag.Azonosito,
                                                             aktualisIntezetId,
                                                             true, globalis);
                    return true;
                }
                catch (Exception e)
                {
                    throw e;
                }
            }

            public void Reset()
            {
                _current = null;
                _groupPrincipalEnum = null;
                _aktualisSzerepkor = null;
                _jogosultsagEnum = null;
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public void Dispose()
            {
                _bvopAlkalmazott = null;
                _groupPrincipalEnum.Dispose();
                _current = null;
            }
        }
        #endregion eljárások
    }
}
