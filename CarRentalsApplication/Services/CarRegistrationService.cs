using CarRentalsApplication.DB;
using CarRentalsApplication.Models;
using CarRentalsApplication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarRentalsApplication.Services
{
    public class CarRegistrationService : ICarRegistrationService
    {
        private ICarRegistrationRepository _repository;

        public CarRegistrationService(ICarRegistrationRepository repository)
        {
            _repository = repository;
        }
        public List<CarDisplayListModel> ListCarDisplay()
        {
            return _repository.ListCarDisplay();
        }
        public string Create(Car_Registration obj)
        {
            obj.CarNumber = CreateCarNo(8);
            return _repository.Create(obj);
        }
        public bool Delete(string carNo)
        {
            return _repository.Delete(carNo);
        }
        public bool UpdateCarRentedDtls(string carNo)
        {
            return _repository.UpdateCarRentedDtls(carNo);
        }
        public Car_Rent_Details GetCarRentDtlsByCarNo(string carNo)
        {
            return _repository.GetCarRentDtlsByCarNo(carNo);
        }
        public string CreateCarNo(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
        public List<SelectListItem> GetAllCarNo()
        {
            return _repository.GetAllCarNo();
        }
        public bool IsCarRented(string carNo)
        {
            return _repository.IsCarRented(carNo);
        }
        public bool CreateCarRentDtls(Car_Rent_Details obj)
        {
            DateTime date = DateTime.Now;
            TimeSpan time = DateTime.Now.TimeOfDay;
            obj.CarTakenDate = date + time;
            return _repository.CreateCarRentDtls(obj);
        }
        public bool UpdateCarReturnedDtls(string carNo)
        {
            return _repository.UpdateCarReturnedDtls(carNo);
        }
        public bool UpdateCarRentReturnedDtls(string carNo)
        {
            return _repository.UpdateCarRentReturnedDtls(carNo);
        }
    }
    public interface ICarRegistrationService
    {
        List<CarDisplayListModel> ListCarDisplay();
        string Create(Car_Registration obj);
        bool Delete(string carNo);
        bool UpdateCarRentedDtls(string carNo);
        Car_Rent_Details GetCarRentDtlsByCarNo(string carNo);
        List<SelectListItem> GetAllCarNo();
        bool IsCarRented(string carNo);
        bool CreateCarRentDtls(Car_Rent_Details obj);
        bool UpdateCarReturnedDtls(string carNo);
        bool UpdateCarRentReturnedDtls(string carNo);
    }
}