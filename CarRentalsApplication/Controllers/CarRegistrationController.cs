using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarRentalsApplication.DB;
using CarRentalsApplication.Repository;
using CarRentalsApplication.Services;
using System.IO;

namespace CarRentalsApplication.Controllers
{
    public class CarRegistrationController : Controller
    {
        private ICarRegistrationService _service;
        public CarRegistrationController()
        {
            _service = new CarRegistrationService(new CarRegistrationRepository());
        }
        // GET: CarRegistration
        #region Car Registration
        public ActionResult Index()
        {
            return View(_service.ListCarDisplay());
        }
        public ActionResult Create()
        {
            Car_Registration obj = new Car_Registration();
            return View(obj);
        }
        [HttpPost]
        public ActionResult Create(Car_Registration obj, HttpPostedFileBase carImgUpload)
        {
            string path = string.Empty;
            if (carImgUpload != null)
            {
                string filename = "";
                string fileNameWithoutExtention = Path.GetFileNameWithoutExtension(carImgUpload.FileName);
                string extension = Path.GetExtension(carImgUpload.FileName);
                filename = fileNameWithoutExtention + "_" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + extension;
                DirectoryInfo MyDirectory = new DirectoryInfo(Request.PhysicalApplicationPath + "Uploads\\" + "Car_Images");
                if (!MyDirectory.Exists)
                {
                    MyDirectory.Create();
                }
                path = MyDirectory.FullName + "\\" + filename;
                carImgUpload.SaveAs(path);
            }
            obj.CarImagePath = path;
            var carNo = _service.Create(obj);
            TempData["CarNo"] = carNo;
            return RedirectToAction("Index");
        }
        #endregion
        #region Rent Car
        public ActionResult RentCar()
        {
            Car_Rent_Details obj = new Car_Rent_Details();
            ViewBag.CarNoList = _service.GetAllCarNo();
            return View(obj);
        }
        [HttpPost]
        public ActionResult RentCar(Car_Rent_Details obj)
        {
            bool isCarRented = _service.IsCarRented(obj.CarNumber);
            if (isCarRented == false)
            {
                _service.CreateCarRentDtls(obj);
                _service.UpdateCarRentedDtls(obj.CarNumber);
                TempData["Message"] = "Rent Successfull";
            }
            else
            {
                TempData["Message"] = "Already Rented";
            }
            return RedirectToAction("Index", "MainMenu");
        }
        #endregion

        #region Return Car
        public ActionResult ReturnCar()
        {
            Car_Rent_Details obj = new Car_Rent_Details();
            ViewBag.CarNoList = _service.GetAllCarNo();
            return View();
        }
        [HttpPost]
        public ActionResult ReturnCar(Car_Rent_Details obj)
        {
            bool isCarRented = _service.IsCarRented(obj.CarNumber);
            if (isCarRented == true)
            {
                _service.UpdateCarRentReturnedDtls(obj.CarNumber);
                _service.UpdateCarReturnedDtls(obj.CarNumber);
                Car_Rent_Details objNew = _service.GetCarRentDtlsByCarNo(obj.CarNumber);
                TempData["ReturnDtls"] = objNew.RentedTime + "*" + objNew.Price;
                TempData["Message"] = "Returned";
            }
            else
            {
                TempData["Message"] = "Not Rented";
            }
            return RedirectToAction("Index", "MainMenu");
        }
        #endregion

        #region Remove car
        public ActionResult RemoveCar()
        {
            Car_Rent_Details obj = new Car_Rent_Details();
            ViewBag.CarNoList = _service.GetAllCarNo();
            return View(obj);
        }
        [HttpPost]
        public ActionResult RemoveCar(Car_Rent_Details obj)
        {
           bool value = _service.Delete(obj.CarNumber);
            if (true)
            {
                TempData["Message"] = "Deleted";
            }
            else
            {
                TempData["Message"] = "Error Occured";
            }
            return RedirectToAction("Index", "MainMenu");
        }
        #endregion
    }
}