using Edis.Entities.JFK.FENY;
using Edis.Utilities;
using Edis.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.ViewModels.JFK.FENY
{
    public class MaganelzarasFofelugyelokViewModel : BaseViewModel
    {
        public string Fofelugyelo { get; set; }
        public int FegyelmiUgyId { get; set; }
        public virtual FegyelmiUgy FegyelmiUgy { get; set; }
        public int NaploId { get; set; }
        public virtual Naplo Naplo { get; set; }

        public static explicit operator MaganelzarasFofelugyelokViewModel(MaganelzarasFofelugyelok item)
        {
            var model = ValueInjecterUtilities.InjectViewModel<MaganelzarasFofelugyelok, MaganelzarasFofelugyelokViewModel>(item);
            return model;
        }

        public static explicit operator MaganelzarasFofelugyelok(MaganelzarasFofelugyelokViewModel model)
        {
            MaganelzarasFofelugyelok entity = new MaganelzarasFofelugyelok();
            entity = ValueInjecterUtilities.InjectViewModel<MaganelzarasFofelugyelokViewModel, MaganelzarasFofelugyelok>(model);

            return entity;
        }
    }
}
