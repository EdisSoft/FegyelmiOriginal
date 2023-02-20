using Edis.Entities.Fany;
using Edis.Entities.JFK.FENY;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using Edis.ViewModels.JFK.FENY;
using Edis.ViewModels.JFK.Nyomtatvany;
using System.Collections.Generic;

namespace Edis.Functions.JFK
{
    public interface INaploFunctions : IFunctionsBase<NaploViewModel, Naplo>
    {
        void CreateNaploBejegyzesDontesFegyelmiUgyElrendeleserol(List<int> fegyelmiUgyIds);
        void CreateNaploBejegyzesJogiKepviseletrol(List<int> fegyelmiUgyIds);

        List<NaploListaViewmodel> GetNaplobejegyzesekByFegyelmiUgyId(int fegyelmiUgyId);
        
        void ModifyNaploBejegyzes(FegyelmiUgyHatarozatRogziteseModel model);
        void ModifyNaploBejegyzes(FegyelmiUgyHatarozatRogziteseMasodfokonModel model);

        void NaploBejegyzesInaktivalasa(int naploBejegyzesId);
        void NaploBejegyzesAktivalasa(int naploBejegyzesId);


    }
}
