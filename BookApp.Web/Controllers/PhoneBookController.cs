using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookApp.Core;
using BookApp.Infrastructure;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using BookApp.Core.ViewModels;

namespace BookApp.Web.Controllers
{
    public class PhoneBookController : Controller
    {
        private UnitOfWork _repository;
        private Person _person;
        private Address _address;
        private Phone _phone;

        public PhoneBookController()
        {
            _repository= new UnitOfWork(new BookContext());
            _person= new Person();
            _address = new Address();
            _phone=new Phone();
        }    

        public ActionResult Index()
        {
            return View();

        }
       
        [HttpPost]
        public JsonResult GetContactList(PaginationFilterDTO model)
        {
            try
            {
                var responseData = _repository.Person.GetAllData(model); 
                return Json(responseData, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { success = false, errors = e.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateContactViewModel info)
        {
            try
            {
                    if (!_repository.Address.FindByZipCode(info.PostNumber))  //is it a new person?
                    {
                        //this is a new person
                        _person.FullName = info.FullName;
                    _repository.Person.Add(_person);
                    _repository.Complete();

                        _address.PersonIdn = _person.ID;
                        _address.PostNumber = info.PostNumber;
                        _address.Street = info.Street;
                        _address.Unit = info.Unit;
                    _repository.Address.Add(_address);
                    _repository.Complete();

                        _phone.PersonIdn = _person.ID;
                        _phone.PhoneNumber = info.PhoneNumber;
                        _phone.Flag = info.Flag;
                    _repository.Phone.Add(_phone);
                    _repository.Complete();

                        return new HttpStatusCodeResult(HttpStatusCode.Created,"your contact added successfully!");
                    }
                else
                {
                  throw new ArgumentException("This address has already existed.");
                }
            }
            catch(ArgumentException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
          
        }

        [HttpGet]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var person = _repository.Person.FindPerson(id);
                _repository.Person.Remove(person);
                _repository.Complete();
                return new HttpStatusCodeResult(HttpStatusCode.OK, "your contact Deleted successfully!");
            }
            catch (ArgumentException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }

        }

        [HttpGet]
        public ActionResult DeletePhoneConfirmed(int id)
        {
            try
            {
                _phone = _repository.Phone.FindById(id);
                _repository.Phone.Remove(_phone);
                _repository.Complete();
                return new HttpStatusCodeResult(HttpStatusCode.OK, "your phone Deleted successfully!");

            }
            catch (ArgumentException e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
           
        }

        [HttpPost]
        public ActionResult AddPhone(Phone info)
        {
            try
            {
                _repository.Phone.Add(info);
                _repository.Complete();
                 return new HttpStatusCodeResult(HttpStatusCode.Created, "your phone added successfully!");
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, e.Message);
            }
        }

        public ActionResult GetAllPhone(int PersonId)
        {
           var ListPhones= _repository.Phone.FingByPersonId(PersonId);
            return View("_Phone", ListPhones);
        }
    }
}
