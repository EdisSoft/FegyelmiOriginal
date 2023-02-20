using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Edis.Entities.Fany;
using Edis.Functions.Base;
using Edis.ViewModels.Fany;
using System.DirectoryServices.AccountManagement;

namespace Edis.Functions.Fany
{
    public interface IIntezetFunctions : IDbSetBase<Intezet>, IFunctionsBase<IntezetModel, Intezet>
    {
        Intezet KeresesById(int id);

        List<Intezet> IntezetListaLegyujtes(string sid, bool isNincsJogosolutsaga = false);

        List<Intezet> IntezetListaLegyujtesAdCsoport(UserPrincipal user);        
    }
}
