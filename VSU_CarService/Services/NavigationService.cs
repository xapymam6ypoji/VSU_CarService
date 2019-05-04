using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Regions;

namespace VSU_CarService.Services
{
    public interface INavigationService
    {
        void LoadToContentRegion(string view);
    }

    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;
        private const string CONTENT_REG = "ContentRegion";

        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void LoadToContentRegion(string view)
        {
            _regionManager.RequestNavigate(CONTENT_REG, view);
        }
    }
}
