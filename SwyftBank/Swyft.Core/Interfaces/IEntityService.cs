using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.Core.Interfaces
{
    public interface IEntityService
    {
        // void Create(params object[] list);

        // IEntity Get(int id);

        void Edit(int id);

        void Delete(int id);
    }
}
