using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class JogosultsagInformacio
    {
        #region jellemzők
        private readonly string _szemelySid;
        private readonly string _szemelyzetCsoportSid;
        private readonly string _szerepkorAzonosito;
        private readonly string _jogosultsagAzonosito;
        private readonly int? _bvIntezetId;
        private readonly bool _vanJogosultsaga;
        private readonly bool _globalis;
        #endregion

        #region jellemzők
        public string SzemelySid { get { return _szemelySid; } }
        public string SzemelyzetCsoportSid { get { return _szemelyzetCsoportSid; } }
        public string SzerepkorAzonosito { get { return _szerepkorAzonosito; } }
        public string JogosultsagAzonosito { get { return _jogosultsagAzonosito; } }
        public int? BvIntezetId { get { return _bvIntezetId; } }
        public bool VanJogosultsaga { get { return _vanJogosultsaga; } }
        public bool Globalis { get { return _globalis; } }
        #endregion

        #region konstruktor
        public JogosultsagInformacio(string szemelySid, string szemelyzetCsoportSid,
                                     string szerepkorAzonosito, string jogosultsagAzonosito,
                                     int? bvIntezetId, bool vanJogosultsaga, bool globalis)
        {
            _szemelySid = szemelySid;
            _szemelyzetCsoportSid = szemelyzetCsoportSid;
            _szerepkorAzonosito = szerepkorAzonosito;
            _jogosultsagAzonosito = jogosultsagAzonosito;
            _bvIntezetId = bvIntezetId;
            _vanJogosultsaga = vanJogosultsaga;
            _globalis = globalis;
        }

        public JogosultsagInformacio(int? bvIntezetId)
        {
            _bvIntezetId = bvIntezetId;
        }
        #endregion

        #region eljárások

        #endregion eljárások
    }
}
