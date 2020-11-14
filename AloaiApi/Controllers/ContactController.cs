using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Entity;
using Aloai.Enum;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Nancy;
using Nancy.Json;
using NHibernate.Linq;

namespace Aloai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public ContactController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insert contact history.
        /// </summary>
        /// <param name="entity">Contact entity</param>
        /// <returns>Success: 200; Error: 400; Not found: 404</returns>
        [HttpPost("InsertContactHistory")]
        public ActionResult InsertContactHistory(ContactEntity entity)
        {
            System.Web.Http.HttpError errorHttp = null;

            if (entity == null)
            {
                errorHttp = new System.Web.Http.HttpError("ContactEntity is not found!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = errorHttp.Message,
                    Data = false
                });
            }

            IDbContextTransaction transaction = _context.Database.BeginTransaction();

            try
            {
                // Open transaction.
                D_CONTACT contact = new D_CONTACT();

                contact.USER_RECIEVE_ID = entity.userRecieveId;
                contact.REG_MODE_USER = entity.contactModeUser;
                contact.CONTACT_USER_ID = entity.contactUserId;
                contact.CATALOG_CD = entity.catalogCd;
                contact.CONTACT_DATE = Utility.GetSysDateTime();

                _context.D_CONTACTS.Add(contact);
                _context.SaveChanges();

                // Commit transaction.
                transaction.Commit();

                errorHttp = new System.Web.Http.HttpError("Insert is success!");

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
                transaction.Rollback();

                System.Web.Http.HttpError error = new System.Web.Http.HttpError("Error system!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = error.Message,
                    Data = false
                });
            }
        }

        /// <summary>
        /// Get estimation.
        /// </summary>
        /// <param name="id">Exchage ID</param>
        /// <param name="EstModeUser">Estimation mode user</param>
        /// <returns>EstimationEntity</returns>
        [HttpGet("GetContactHistory/{id}/{modeUser}")]
        public ActionResult GetContactHistory([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            //int contactMax = int.Parse(Utility.GetDefineValue(Constant.CONTACT_NUMBER_MAX).Value);
            List<V_ContactEntity> entityList = new List<V_ContactEntity>();

            //var query = (from d in db.V_CONTACT_HISTORY
            //            where d.USER_ID == id
            //               && d.MODE_USER == (int)ModeUser
            //          orderby d.CONTACT_DATE descending
            //            select d).Take(contactMax);

            var query = from d in _context.V_CONTACT_HISTORYS
                        where (d.USER_RECIEVE_ID == id && d.REG_MODE_USER != (int)modeUser)
                        || (d.CONTACT_USER_ID == id && d.REG_MODE_USER == (int)modeUser)
                        orderby d.CONTACT_DATE descending
                        select d;

            if (query.Any())
            {
                foreach (V_CONTACT_HISTORY contact in query.ToList())
                {
                    V_ContactEntity entity = new V_ContactEntity();

                    entity.contactId = contact.CONTACT_ID;
                    entity.contactModeUser = contact.REG_MODE_USER;
                    entity.contactUserId = contact.CONTACT_USER_ID;
                    entity.contactUserId = contact.USER_RECIEVE_ID;
                    entity.contactUserName = contact.REG_MODE_USER != (int)modeUser ? contact.CONTACT_USER_NAME : contact.RECIEVE_USER_NAME;
                    entity.contactDate = contact.CONTACT_DATE;
                    entity.catalogCd = contact.CATALOG_CD;
                    entity.callType = contact.REG_MODE_USER == modeUser ? (int)CallType.Call : (int)CallType.Receive;

                    entityList.Add(entity);
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }
    }
}