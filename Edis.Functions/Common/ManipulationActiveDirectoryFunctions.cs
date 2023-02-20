namespace Edis.Functions.Common
{
    using System.Data.Entity;
    using Edis.Functions.Base;
    using Edis.ViewModels.Common;
    using Edis.Entities.Common;

    public class ManipulationActiveDirectoryFunctions : KonasoftBVFonixFunctionsBase<ManipulationActiveDirectoryViewModel, ManipulationActiveDirectory>, IManipulationActiveDirectoryFunctions
    {

        public DbSet<ManipulationActiveDirectory> Table => KonasoftBVFonixContext.ManipulationActiveDirectory;        
    }
}
