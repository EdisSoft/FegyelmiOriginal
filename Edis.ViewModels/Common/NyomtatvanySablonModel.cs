using Edis.Entities.Common;
using Edis.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.Common
{
    public class NyomtatvanySablonModel
    {
        [Key]
        public int Id { get; set; }

        public string ModulMegnevezese { get; set; }
        
        public string NyomtatvanyNeve { get; set; }
        
        public string NyomtatvanyLeirasa { get; set; }
        
        public byte[] NyomtatvanySablonDocx { get; set; }

        public bool? FoosztalyVezetoAlairasaSzukseges { get; set; }


        public static explicit operator NyomtatvanySablonModel(NyomtatvanySablon item)
        {
            NyomtatvanySablonModel model = new NyomtatvanySablonModel();
            model = ValueInjecterUtilities.InjectViewModel<NyomtatvanySablon, NyomtatvanySablonModel>(item);
            return model;
        }

        public static explicit operator NyomtatvanySablon(NyomtatvanySablonModel model)
        {
            NyomtatvanySablon entity = new NyomtatvanySablon();
            entity = ValueInjecterUtilities.InjectViewModel<NyomtatvanySablonModel, NyomtatvanySablon>(model);

            return entity;
        }
    }
}
