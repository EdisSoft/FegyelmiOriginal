using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Edis.Entities.Base
{
    public class EntityOperation : Attribute
    {
        public OperationType[] Operations { get; set; }

        public bool ContainsCreate { get { return Operations.Any(x => x == OperationType.Create); } }

        public bool ContainsDelete { get { return Operations.Any(x => x == OperationType.Delete); } }

        public bool ContainsUpdate { get { return Operations.Any(x => x == OperationType.Update); } }

        public EntityOperation(params OperationType[] operations)
        {
            Operations = operations;
        }
    }

    public enum OperationType
    {
        Create,
        Update,
        Delete
    }
}
