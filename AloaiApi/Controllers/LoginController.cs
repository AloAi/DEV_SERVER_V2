using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Aloai.Entity;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Nancy;
using Nancy.Json;

namespace Aloai.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public LoginController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// GetLogin list.
        /// </summary>
        /// <returns>List LoginEntity</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult GetLoginList()
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();
            //List<M_LOGIN> emps = db.M_LOGIN.ToList();
            List<LoginEntity> list = new List<LoginEntity>();

            //foreach (M_LOGIN login in emps)
            //{
            //    Entity.LoginEntity entity = new LoginEntity();

            //    entity.UserId = login.USER_ID;
            //    entity.PhoneNumber = login.PHONE_NUMBER;
            //    entity.Password = Utility.Decrypt(login.PASSWORD);
            //    list.Add(entity);
            //}

            // string json = JsonConvert.SerializeObject(list);
            return Ok(new JavaScriptSerializer().Serialize(list));
        }

        /// <summary>
        /// Get Login by ID
        /// </summary>
        /// <param name="id">Login ID</param>
        /// <returns>LoginEntity</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult GetLogin(decimal id)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();
            //M_LOGIN login = db.M_LOGIN.FirstOrDefault(x => x.USER_ID == id);
            LoginEntity entity = new LoginEntity();

            //if (login != null)
            //{
            //    entity.UserId = login.USER_ID;
            //    entity.PhoneNumber = login.PHONE_NUMBER;
            //    entity.Password = Utility.Decrypt(login.PASSWORD);
            //}

            return Ok(new JavaScriptSerializer().Serialize(entity));
        }

        /// <summary>
        /// Get Login by phone number.
        /// </summary>
        /// <param name="id">Phone number</param>
        /// <returns>LoginEntity</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public ActionResult GetLoginByPhone(string id)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();
            //M_LOGIN login = db.M_LOGIN.FirstOrDefault(x => x.PHONE_NUMBER == id);
            LoginEntity entity = new LoginEntity();

            //if (login != null)
            //{
            //    entity.UserId = login.USER_ID;
            //    entity.PhoneNumber = login.PHONE_NUMBER;
            //    entity.Password = Utility.Decrypt(login.PASSWORD);
            //}

            return Ok(new JavaScriptSerializer().Serialize(entity));
        }

        /// <summary>
        /// Get Login by phone number.
        /// </summary>
        /// <param name="id">Phone number</param>
        /// <returns>LoginEntity</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public LoginEntity GetLoginByPhoneEntity(string id)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();
            //M_LOGIN login = db.M_LOGIN.FirstOrDefault(x => x.PHONE_NUMBER == id);
            LoginEntity entity = new LoginEntity();

            //if (login != null)
            //{
            //    entity.UserId = login.USER_ID;
            //    entity.PhoneNumber = login.PHONE_NUMBER;
            //    entity.Password = Utility.Decrypt(login.PASSWORD);
            //}

            return entity;
        }

        /// <summary>
        /// Insert login.
        /// </summary>
        /// <param name="loginEntity">LoginEntity</param>
        /// <returns>Success: 200; Fail: 400; Exists: 201</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult InsertLogin(LoginEntity loginEntity)
        {
            using (IDbContextTransaction trans = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (Utility.CheckLoginExists(loginEntity))
                    //{
                    //    HttpError error = new HttpError("Phone number is exists!");
                    //    return Request.CreateErrorResponse(HttpStatusCode.Created, error);
                    //}

                    //M_LOGIN loginAdd = new M_LOGIN();
                    //loginAdd.PHONE_NUMBER = loginEntity.PhoneNumber;
                    //string password = Utility.Encrypt(loginEntity.Password);
                    //loginAdd.PASSWORD = password;

                    //db.M_LOGIN.InsertOnSubmit(loginAdd);
                    //db.SubmitChanges();

                    //var query = from d in db.M_LOGIN
                    //            where d.PHONE_NUMBER == loginEntity.PhoneNumber
                    //            select d;

                    //if (query.Any())
                    //{
                    //    M_USER user = new M_USER();
                    //    user.USER_ID = query.FirstOrDefault().USER_ID;
                    //    user.NAME = loginEntity.UserName;
                    //    user.SEX = loginEntity.Sex;
                    //    user.INTRODUCE = string.Empty;
                    //    user.MODE_DEFAULT = loginEntity.ModeDefault;
                    //    user.REG_DATETIME = Utility.GetSysDateTime();
                    //    user.MODE_USER = loginEntity.ModeDefault;

                    //    db.M_USER.InsertOnSubmit(user);

                    //    M_WORKER_INFO worker = new M_WORKER_INFO();
                    //    worker.USER_ID = query.FirstOrDefault().USER_ID;
                    //    worker.SCORE = 0;
                    //    worker.RECEIVE_CNT = 0;
                    //    worker.CANCEL_CNT = 0;
                    //    worker.STATUS = (int)Status.Offline;
                    //    worker.REG_DATETIME = Utility.GetSysDateTime();
                    //    db.M_WORKER_INFO.InsertOnSubmit(worker);

                    //    M_HIRER_INFO hirer = new M_HIRER_INFO();
                    //    hirer.USER_ID = query.FirstOrDefault().USER_ID;
                    //    hirer.SCORE = 0;
                    //    hirer.POST_CNT = 0;
                    //    hirer.CANCEL_CNT = 0;
                    //    hirer.STATUS = (int)Status.Offline;
                    //    hirer.REG_DATETIME = Utility.GetSysDateTime();
                    //    db.M_HIRER_INFO.InsertOnSubmit(hirer);
                    //}

                    //db.SubmitChanges();

                    // Commit transaction.
                    trans.Commit();
                    HttpError errorHttp = new HttpError("Insert is success!");

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = errorHttp.Message,
                        Data = true
                    });
                }
                catch
                {
                    // Rollback transaction.
                    trans.Rollback();
                    HttpError error = new HttpError("Error system!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = error.Message,
                        Data = null
                    });
                }
            }
        }

        /// <summary>
        /// Check login with phone number and password.
        /// </summary>
        /// <param name="loginEntity">LoginEntity</param>
        /// <returns>Login ok: UserEntity; Login fail: NotFound</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult CheckLogin(LoginEntity loginEntity)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

            //if (!Utility.CheckLoginExists(loginEntity))
            //{
            //    return NotFound();
            //}

            //M_LOGIN login = db.M_LOGIN.FirstOrDefault(x => x.PHONE_NUMBER == loginEntity.PhoneNumber);
            //string password = Utility.Encrypt(loginEntity.Password);

            //if (login.PASSWORD.Equals(password))
            //{
            //    var query = from d in db.M_USER
            //                where d.USER_ID == login.USER_ID
            //                select d;

            //    if (query.Any())
            //    {
            //        M_USER user = query.First();
            //        UserEntity entity = new UserEntity();
            //        entity.UserId = user.USER_ID;
            //        entity.Name = user.NAME;
            //        entity.Sex = user.SEX;
            //        entity.BirthDay = user.BIRTHDAY;
            //        entity.Avatar = user.AVATAR;
            //        entity.Introduce = user.INTRODUCE;
            //        entity.ModeDefault = user.MODE_DEFAULT;
            //        entity.AccountType = user.ACCOUNT_TYPE;
            //        entity.MemberType = user.MEMBER_TYPE;
            //        entity.ModeUser = user.MODE_USER;

            //        var query3 = from d in db.M_WORKER
            //                    where d.USER_ID == user.USER_ID
            //                    select d;

            //        List<WorkerEntity> workerEntityList = new List<WorkerEntity>();

            //        if (query3.Any())
            //        {
            //            foreach (M_WORKER worker in query3.ToList())
            //            {
            //                WorkerEntity wkEntity = new WorkerEntity();
            //                wkEntity.UserId = worker.USER_ID;
            //                wkEntity.CatalogCd = worker.CATALOG_CD;

            //                var query1 = from d in db.M_CATALOG
            //                             where d.CD == worker.CATALOG_CD
            //                             select d;

            //                if (query1.Any())
            //                {
            //                    wkEntity.CatalogName = query1.First().NAME;
            //                }

            //                wkEntity.Cost = worker.COST;
            //                wkEntity.UnitCd = worker.UNIT_CD;

            //                var query2 = from d in db.M_UNIT
            //                             where d.CD == worker.UNIT_CD
            //                             select d;

            //                if (query2.Any())
            //                {
            //                    wkEntity.UnitName = query2.First().NAME;
            //                }

            //                workerEntityList.Add(wkEntity);
            //            }
            //        }

            //        entity.WorkerEntityList = workerEntityList;

            //        return Ok(new JavaScriptSerializer().Serialize(entity));
            //    }
            //}

            return Ok(new Result
            {
                Status = 404,
                Message = "Not found.",
                Data = null
            });
        }

        /// <summary>
        /// Change password.
        /// </summary>
        /// <param name="loginEntity">LoginEntity</param>
        /// <returns>Success: 200; Error: 400; Not exists: 201</returns>
        [Microsoft.AspNetCore.Mvc.HttpPost]
        public ActionResult ChangePassword(LoginEntity loginEntity)
        {
            using (IDbContextTransaction trans = _context.Database.BeginTransaction())
            {
                try
                {
                    //if (!Utility.CheckLoginExists(loginEntity))
                    //{
                    //    HttpError error = new HttpError("Phone number is not exists!");
                    //    return Request.CreateErrorResponse(HttpStatusCode.Created, error);
                    //}

                    //M_LOGIN login = db.M_LOGIN.FirstOrDefault(x => x.PHONE_NUMBER == loginEntity.PhoneNumber);
                    //login.PASSWORD = Utility.Encrypt(loginEntity.Password);
                    //db.SubmitChanges();

                    // Commit transaction.
                    trans.Commit();

                    HttpError errorHttp = new HttpError("Password is changed!");
                    return Ok(errorHttp);
                }
                catch
                {
                    // Rollback transaction.
                    trans.Rollback();
                    HttpError error = new HttpError("Error system!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = error.Message,
                        Data = null
                    });
                }
            }
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">Login id</param>
        /// <returns>Success: true, Faile: false</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public bool DeleteLogin(string id)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

            //try
            //{
            //    // Open transaction.
            //    db.Connection.Open();
            //    db.Transaction = db.Connection.BeginTransaction();

            //    var query = from d in db.M_LOGIN
            //                where d.PHONE_NUMBER == id
            //                select d;

            //    if (query.Any())
            //    {
            //        M_LOGIN login = query.Single();
            //        db.M_LOGIN.DeleteOnSubmit(login);
            //        db.SubmitChanges();
            //    }

            //    // Commit transaction.
            //    db.Transaction.Commit();
            //    return true;
            //}
            //catch
            //{
            //    // Rollback transaction.
            //    db.Transaction.Rollback();
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// Check login exists.
        /// </summary>
        /// <param name="id">Phone number</param>
        /// <returns>Exists: True; Not exists: False</returns>
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public bool CheckLoginExists(string id)
        {
            return Utility.CheckLoginExists(id);
        }
    }
}