using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Enums
{
    public enum FttrCsempek
    {
        [Description("Függőben tartás")]
        FuggobenTartas,

        [Description("Határidőn belűl vissza nem érkezett tértivevények")]
        HataridonBelulVisszaNemErkezettTertivevenyek,

        [Description("Archívum")]
        Archivum,

        [Description("Folyamatban lévő")]
        FolyamatbanLevo,

        [Description("Soron kívüli eljárások")]
        SoronKivuliEljarasok,

        [Description("Sürgős")]
        Surgos,

        [Description("Saját")]
        Sajat


    }
}
