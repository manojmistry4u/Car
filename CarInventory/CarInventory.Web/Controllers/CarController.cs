using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CarInventory.Data;
using CarInventory.Repository;
using CarInventory.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarInventory.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class CarController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CarController));

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICarService _carservice;

        public CarController(UserManager<ApplicationUser> userManager, ICarService carservice)
        {
            _userManager = userManager;
            _carservice = carservice;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //string userid = _userManager.GetUserId(User);
            return View();
        }

       

        public PartialViewResult CarList()
        {
            string userid = _userManager.GetUserId(User);
            try
            {
                List<Cars> carlist = _carservice.GetCarsByUserId(userid);
                return PartialView("_CarList", carlist);
            }
            catch (Exception ex)
            {
                log.Info("Car List:" + ex.Message);
                throw;
            }
          
        }

        [HttpPost]
        public ActionResult CarAddUpdate(Cars model)
        {
            try
            {
                if (model.Id > 0)
                {
                    Cars updatecar = _carservice.FirstOrDefault(model.Id);
                    if (updatecar != null && updatecar.Id > 0)
                    {
                        updatecar.Brand = model.Brand;
                        updatecar.Model = model.Model;
                        updatecar.Year = model.Year;
                        updatecar.Price = model.Price;
                        updatecar.New = model.New;
                        updatecar.LastModifiedBy = _userManager.GetUserId(User);
                        updatecar.LastModifiedDate = DateTime.Now;
                        _carservice.Update(updatecar);
                    }
                }
                else
                {
                    model.CreatedBy = _userManager.GetUserId(User);
                    model.CreatedDate = DateTime.Now;
                    model.LastModifiedBy = _userManager.GetUserId(User);
                    model.LastModifiedDate = DateTime.Now;
                    _carservice.InsertAndGetId(model);
                }
                return Json(true);
            }
            catch (Exception ex)
            {
                log.Info("Car Add/Update Issue:" + ex.Message);
                return Json(false);
            }
        }

        [HttpGet]
        public IActionResult DeleteCar(long CarId)
        {
            try
            {
                _carservice.Delete(CarId);
                return Json(true);
            }
            catch (Exception ex)
            {

                log.Info("Car Delete Issue:" + ex.Message);
                return Json(false);
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}