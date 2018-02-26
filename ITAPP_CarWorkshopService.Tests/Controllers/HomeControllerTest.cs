using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ITAPP_CarWorkshopService;
using ITAPP_CarWorkshopService.Controllers;

namespace ITAPP_CarWorkshopService.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Przygotowanie
            HomeController controller = new HomeController();

            // Wykonanie
            ViewResult result = controller.Index() as ViewResult;

            // Sprawdzenie
            Assert.IsNotNull(result);
            Assert.AreEqual("Home Page", result.ViewBag.Title);
        }
    }
}
