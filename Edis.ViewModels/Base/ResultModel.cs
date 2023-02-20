using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Base
{
    public class ResultModel
    {
        public int Id { get; set; }

        /// <summary>
        /// 1: success, 2: info, 3: warning, 4: error
        /// </summary>
        public int Status { get; set; } 

        public string Message { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public object Details { get; set; }

    }
}
