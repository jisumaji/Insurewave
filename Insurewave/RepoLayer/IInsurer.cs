using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer
{
    public interface IInsurer
    {
        public void GetAllPendings();
        public void GetAllOngoing();
    }
}
