using CarRentalsApplication.DB;
using CarRentalsApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentalsApplication.Repository
{
    public class CarRegistrationRepository : ICarRegistrationRepository
    {
        DBConnection _dbentity = new DBConnection();

        public List<CarDisplayListModel> ListCarDisplay()
        {
            var da = from a in _dbentity.Car_Registration
                     orderby a.CarID_PK descending
                     select new CarDisplayListModel
                     {
                         CarID_PK = a.CarID_PK,
                         CarName = a.CarName,
                         CarNumber = a.CarNumber,
                         HourlyRate = a.HourlyRate
                     };
            return da.ToList();
        }
        public string Create(Car_Registration obj)
        {
            try
            {
                obj.RegisteredOn = DateTime.Now;
                obj.CarNumber = obj.CarNumber.Trim();
                _dbentity.Car_Registration.Add(obj);
                _dbentity.SaveChanges();
                return obj.CarNumber;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }
        public bool Delete(string carNo)
        {
            try
            {
                Car_Registration obj = _dbentity.Car_Registration.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
                _dbentity.Car_Registration.Remove(obj);
                _dbentity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCarRentedDtls(string carNo)
        {
            try
            {
                DBConnection _dbentity = new DBConnection();
                Car_Registration obj = _dbentity.Car_Registration.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
                obj.IsRented = true;
                obj.ModifiedOn = DateTime.Now;
                _dbentity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public Car_Rent_Details GetCarRentDtlsByCarNo(string carNo)
        {
            DBConnection _dbentity = new DBConnection();
            return _dbentity.Car_Rent_Details.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
        }
        public List<SelectListItem> GetAllCarNo()
        {
            try
            {
                var da = from a in _dbentity.Car_Registration
                         orderby a.CarID_PK ascending
                         select new SelectListItem
                         {
                             Text = a.CarNumber,
                             Value = a.CarNumber
                         };
                return da.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool IsCarRented(string carNo)
        {
            try
            {
                Car_Registration obj = _dbentity.Car_Registration.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
                if (obj.IsRented == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }           
        }
        public bool CreateCarRentDtls(Car_Rent_Details obj)
        {
            try
            {
                obj.CreatedOn = DateTime.Now;
                obj.CarNumber = obj.CarNumber.Trim();
                _dbentity.Car_Rent_Details.Add(obj);
                _dbentity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCarReturnedDtls(string carNo)
        {
            try
            {
                DBConnection _dbentity = new DBConnection();
                Car_Registration obj = _dbentity.Car_Registration.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
                obj.IsRented = false;
                obj.ModifiedOn = DateTime.Now;
                _dbentity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateCarRentReturnedDtls(string carNo)
        {
            try
            {
                Car_Rent_Details obj = _dbentity.Car_Rent_Details.ToList().Where(x => x.CarNumber == carNo).FirstOrDefault();
                DateTime date = DateTime.Now;
                TimeSpan time = DateTime.Now.TimeOfDay;
                obj.CarReturnDate = date + time;
                DateTime takenDate = DateTime.Parse(obj.CarTakenDate.ToString());
                DateTime returnDate = DateTime.Parse(obj.CarReturnDate.ToString());
                var timeDifference = returnDate.Subtract(takenDate).TotalHours;
                var hrRate = _dbentity.Car_Registration.FirstOrDefault(x => x.CarNumber == carNo).HourlyRate;
                obj.Price = Convert.ToDecimal(hrRate * timeDifference);
                obj.RentedTime = Convert.ToInt32(Math.Round(timeDifference, MidpointRounding.AwayFromZero));
                obj.ModifiedOn = DateTime.Now;
                _dbentity.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public interface ICarRegistrationRepository
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