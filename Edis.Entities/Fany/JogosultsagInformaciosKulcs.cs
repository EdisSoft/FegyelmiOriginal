using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Fany
{
    public class JogosultsagInformaciosKulcs
    {
        #region jellemzők
        public String SzemelySid { get; private set; }
        public String JogosultsagAzonosito { get; private set; }
        public int? BvIntezetId { get; private set; }
        #endregion

        #region konstruktor
        public JogosultsagInformaciosKulcs(String szemelySid, String jogosultsagAzonosito, int? bvIntezetId)
        {
            SzemelySid = szemelySid;
            JogosultsagAzonosito = jogosultsagAzonosito;
            BvIntezetId = bvIntezetId;
        }
        #endregion


        #region eljárások
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(JogosultsagInformaciosKulcs)) return false;
            return Equals((JogosultsagInformaciosKulcs)obj);
        }

        public bool Equals(JogosultsagInformaciosKulcs other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.SzemelySid, SzemelySid) && Equals(other.JogosultsagAzonosito, JogosultsagAzonosito) && other.BvIntezetId.Equals(BvIntezetId);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (SzemelySid != null ? SzemelySid.GetHashCode() : 0);
                result = (result * 397) ^ (JogosultsagAzonosito != null ? JogosultsagAzonosito.GetHashCode() : 0);
                result = (result * 397) ^ (BvIntezetId.HasValue ? BvIntezetId.Value : 0);
                return result;
            }
        }
        #endregion eljárások
    }
}
