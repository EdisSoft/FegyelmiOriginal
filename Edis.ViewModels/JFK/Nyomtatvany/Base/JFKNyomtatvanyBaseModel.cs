using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.Nyomtatvany
{
    public class JFKNyomtatvanyBaseModel
    {
        public DateTime NyomtatasIdopontja { get; set; } = DateTime.Now;

        public int IktatasiAlszam { get; set; }
        public string NyomtatasIdopontjaString
        {
            get
            {
                return $"{NyomtatasIdopontja.Year}. év {NyomtatasIdopontja.Month}. hónap {NyomtatasIdopontja.Day}. napon {NyomtatasIdopontja.Hour}:{NyomtatasIdopontja.Minute} órakor";
            }
        }
    }
}
