using Swyft.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swyft.UI
{
    public interface IAuthView
    {
        public void DisplayAuthMenu();
        public bool Login();
        public void Register();
    }
}
