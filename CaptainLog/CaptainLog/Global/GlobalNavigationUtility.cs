using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CaptainLog.Global
{
    public static class GlobalNavigationUtility
    {
        public static NavigationService MainFrameNavigationService
        {
            get; set;
        }

        public static NavigationService MainPageFrameNavigationService
        {
            get; set;
        }
    }
}
