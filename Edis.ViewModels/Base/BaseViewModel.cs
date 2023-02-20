using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Edis.ViewModels.Base
{

    public class BaseViewModel
    {
        [Key]
        public int Id { get; set; }
    }
}
