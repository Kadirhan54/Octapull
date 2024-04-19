using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octapull.Domain.Common
{
    public interface ICreatedByEntity
    {
        // TODO : Remove question marks 
        public Guid? CreatedByUserId { get; set; }
        public DateTimeOffset? CreatedOn { get; set; }
    }
}
