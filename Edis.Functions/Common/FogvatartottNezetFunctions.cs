using Edis.Diagnostics;
using Edis.Entities.Common;
using Edis.Entities.Enums;
using Edis.Entities.Enums.Kodszotar;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.Functions.Fany;
using Edis.IoC.Attributes;
using Edis.Repositories.Contexts;
using Edis.Utilities;
using Edis.ViewModels.Fany;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using WebGrease.Css.Extensions;

namespace Edis.Functions.Common
{
    public class FogvatartottNezetFunctions : KonasoftBVFonixFunctionsBase<FogvatartottModel, FogvatartottNezet>, IFogvatartottNezetFunctions
    {
        public DbSet<FogvatartottNezet> Table
        {
            get { return this.KonasoftBVFonixContext.FogvatartottakNezet; }
        }


       

        [Inject]
        private IKodszotarFunctions KodszotarFunctions { get; set; }

        [Inject]
        private IAlkalmazasKontextusFunctions AlkalmazasKontextusFunctions { get; set; }
        [Inject]
        private IJogosultsagKezeloFunctions JogosultsagKezeloFunctions { get; set; }
       
        public List<int> jelenlevoStatuszok = new List<int>() { 2775, 2776, 2778, 2779, 2780, 2781, 2782, 2783, 2784, 2785, 2786, 2787, 2788, 2789, 2790, 2791, 2792, 2793, 2794, 2795, 2796, 4788, 4823 };
        public List<int> szabadultStatuszok = new List<int>() { 2798, 2799, 2800, 2801, 2802, 2803, 2804, 2805, 2806, 2807, 2808, 2809, 2810, 2811, 2812, 2813, 2814, 2815, 2816, 2817, 2818, 2819, 2820, 2821, 2822, 2823, 2824, 2825, 2826, 2827, 2828, 2829, 2830, 2831, 2832, 2833, 2834, 2835, 2836, 2837, 2838, 3186, 1002635, 1002652 };



    }

}
