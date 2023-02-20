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
    public class ExtendedBaseEntity : FanyBaseEntity
    {
       
        [Column("FELV_TRANZ_ID")]
        public int FelvTranzId { get; set; }

        [Column("MOD_TRANZ_ID")]
        public int ModTranzId { get; set; }

        [Column("ERVENYESSEG_KEZD")]
        public DateTime ErvenyessegKezdete { get; set; }

        [NotMapped]
        public DateTime? ElozoErvenyessegKezdete { get; set; }

        public virtual void OnCreate(int tranzakcioId)
        {
            this.FelvTranzId = tranzakcioId;
            this.ModTranzId = tranzakcioId;
            this.ErvenyessegKezdete = DateTime.Now;
            this.ElozoErvenyessegKezdete = this.ErvenyessegKezdete;
        }

        public virtual void OnModify(int tranzakcioId)
        {
            this.ModTranzId = tranzakcioId;
            this.ElozoErvenyessegKezdete = this.ErvenyessegKezdete;
            this.ErvenyessegKezdete = DateTime.Now;
            
        }

        public string HistoryTableName
        {
            get { return ((TableAttribute)this.GetType().GetCustomAttributes(typeof(TableAttribute), true).First()).Name + "_hist"; }
        }

        public virtual Dictionary<string, object> GetInsertHistoricalDictionary()
        {
            Dictionary<string, object> result = this.GetHistoricalDictionary();

            result.Add(HistTranzIdColumn, 0);
            result.Add(ErvenyessegKezdeteColumn, this.ErvenyessegKezdete);
            result.Add(ErvenyessegVegeColumn, new DateTime(3001, 1, 1, 0, 0, 0));

            return result;
        }

        public virtual Dictionary<string, object> GetUpdateHistoricalDictionary()
        {
            Dictionary<string, object> result = this.GetHistoricalDictionary();

            result.Add(HistTranzIdColumn, this.ModTranzId);
            result.Add(ErvenyessegKezdeteColumn, this.ElozoErvenyessegKezdete);
            result.Add(ErvenyessegVegeColumn, this.ErvenyessegKezdete);

            return result;
        }

        private Dictionary<string, object> GetHistoricalDictionary()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            foreach (PropertyInfo property in this.GetType().GetProperties().Where(x => x.IsDefined(typeof(ColumnAttribute), true)))
            {
                string key = ((ColumnAttribute)property.GetCustomAttributes(typeof(ColumnAttribute), true).First()).Name;
                object value = property.GetValue(this, null);

                if (key != ErvenyessegKezdeteColumn)
                    result.Add(key, value);
            }

            return result;
        }

        private const string HistTranzIdColumn = "HIST_TRANZ_ID";
        private const string ErvenyessegVegeColumn = "ERVENYESSEG_VEGE";
        private const string ErvenyessegKezdeteColumn = "ERVENYESSEG_KEZD";
    }
}
