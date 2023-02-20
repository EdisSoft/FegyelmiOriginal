using Edis.Functions.Common;

using Edis.Functions.JFK;
using Edis.Functions.JFK.FENY;
using Edis.Functions.Base;
using Edis.IoC.Interfaces;
using Edis.IoC;
using Edis.Repositories;

namespace Edis.Functions.Fany
{
    public class ServiceLocator : IServiceLocator
    {
        public void Register(InjectionKernel kernel)
        {

            kernel.RegisterInRequestScope<IFogvatartottFunctions, FogvatartottFunctions>();
            kernel.RegisterInRequestScope<IIntezetFunctions, IntezetFunctions>();
            kernel.RegisterInRequestScope<ISzemelyzetFunctions, SzemelyzetFunctions>();
            kernel.RegisterInRequestScope<IAlkalmazasKontextusFunctions, AlkalmazasKontextusFunctions>();
            kernel.RegisterInRequestScope<IActiveDirectoryFunctions, ActiveDirectoryFunctions>();
            kernel.RegisterInRequestScope<IActiveDirectoryKezeloFunctions, ActiveDirectoryKezeloFunctions>();
            kernel.RegisterInRequestScope<IJogosultsagKezeloFunctions, JogosultsagKezeloFunctions>();
            kernel.RegisterInRequestScope<IJogosultsagCacheFunctions, JogosultsagCacheFunctions>();
            kernel.RegisterInRequestScope<IKodszotarFunctions, KodszotarFunctions>();

            kernel.RegisterInRequestScope<ITranzakcioAdatKontextusFunctions, TranzakcioAdatKontextusFunctions>();
            kernel.RegisterInRequestScope<ICimkeFunctions, CimkeFunctions>();
           

            #region Common
            kernel.RegisterInRequestScope<INyomtatvanySablonFunction, NyomtatvanySablonFunction>();
            kernel.RegisterInRequestScope<IFogvatartottNezetFunctions, FogvatartottNezetFunctions>();
            kernel.RegisterInRequestScope<IAdKezeloFunctions, AdKezeloFunctions>();
            #endregion
           
          
           
            #region JFK
            kernel.RegisterInRequestScope<IFanyFegyelmiUgyFunctions, FanyFegyelmiUgyFunctions>();
            kernel.RegisterInRequestScope<IFegyelmiUgyFunctions, FegyelmiUgyFunctions>();
            kernel.RegisterInRequestScope<IFenyitesDashboardFunctions, FenyitesDashboardFunctions>();
            kernel.RegisterInRequestScope<IIktatottNyomtatvanyokFunctions, IktatottNyomtatvanyokFunctions>();
            kernel.RegisterInRequestScope<IIktatottDokumentumokFunctions, IktatottDokumentumokFunctions>();
            kernel.RegisterInRequestScope<IEsemenyekFunctions, EsemenyekFunctions>();
            kernel.RegisterInRequestScope<IEsemenyResztvevoFunctions, EsemenyResztvevoFunctions>();
            kernel.RegisterInRequestScope<INaploFunctions, NaploFunctions>();
            kernel.RegisterInRequestScope<IFelfuggesztesFunctions, FelfuggesztesFunctions>();
            kernel.RegisterInRequestScope<IFeltoltesFunctions, FeltoltesFunctions>();
            kernel.RegisterInRequestScope<ISzakteruletiVelemenyKeresekFunctions, SzakteruletiVelemenyKeresekFunctions>();
            kernel.RegisterInRequestScope<IVegrehajtoSzakteruletekFunctions, VegrehajtoSzakteruletekFunctions>();
            kernel.RegisterInRequestScope<IFenyitesVegrehajtasFunctions, FenyitesVegrehajtasFunctions>();
            kernel.RegisterInRequestScope<IMaganelzarasFofelugyelokFunctions, MaganelzarasFofelugyelokFunctions>();
            #endregion
         

        }
    }
}




