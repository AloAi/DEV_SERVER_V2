//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
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
    /// <summary>
    /// Notify controller class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class NotifyController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public NotifyController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get notify count number.
        /// </summary>
        /// <param name="id">User receive ID</param>
        /// <param name="modeUser">Receive mode user</param>
        /// <returns>Count</returns>
        [HttpGet("GetNotifyCount/{id}/{modeUser}")]
        public ActionResult GetNotifyCount([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            int count = 0;

            var query = from d in _context.D_NOTIFYS
                        where d.READED_FLG == (int)ReadedFlg.New
                        && d.RECEIVE_MODE_USER == modeUser
                        && d.USER_RECIEVE_ID == id
                        select d;

            if (query.Any())
            {
                count = query.ToList().Count();
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = count
            });
        }

        /// <summary>
        /// Get notify list.
        /// </summary>
        /// <param name="id">User receive ID</param>
        /// <param name="modeUser">Receive mode user</param>
        /// <returns>List NotifyEntity</returns>
        [HttpGet("GetNotifyList/{id}/{modeUser}")]
        public ActionResult GetNotifyList([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            int notifyMax = int.Parse(Utility.GetDefineValue(Constant.NOTIFY_NUMBER_MAX).value);

            var query = (from d in _context.D_NOTIFYS
                         where d.READED_FLG == (int)ReadedFlg.New
                         && d.RECEIVE_MODE_USER == modeUser
                         && d.USER_RECIEVE_ID == id
                         orderby d.NOTIFY_DATE descending
                         select d).Take(notifyMax);

            var user = from d in _context.M_USERS
                       where d.USER_ID == id
                       select d;

            if (!query.Any() || !user.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = false
                });
            }

            string languageType = user.Single().LANGUAGE_TYPE.Trim();

            List<D_NOTIFY> notifyList = query.ToList();
            List<NotifyEntity> entityList = new List<NotifyEntity>();

            foreach (D_NOTIFY notify in notifyList)
            {
                NotifyEntity entity = new NotifyEntity();
                entity.notifyId = notify.NOTIFY_ID;
                entity.notifyType = notify.NOTIFY_TYPE;
                entity.objectId = notify.OBJECT_ID;
                entity.notifyDate = notify.NOTIFY_DATE;
                entity.userSendId = notify.USER_SEND_ID;
                entity.userReceiveId = notify.USER_RECIEVE_ID;
                entity.receiveModeUser = notify.RECEIVE_MODE_USER;
                entity.content = notify.CONTENT;

                if (notify.NOTIFY_TYPE == (int)NotifyType.Estimation)
                {
                    entity.content = string.Format(Utility.GetMessageInfo(_context, languageType, notify.NOTIFY_TYPE).messageContent, notify.CONTENT);
                }
                else if (notify.NOTIFY_TYPE == (int)NotifyType.Job)
                {
                    string status = string.Empty;

                    var queryHistory = from d in _context.D_HISTORYS
                                       join m in _context.M_NAMES on d.STATUS equals m.CD
                                       where d.EXCHANGE_ID == notify.OBJECT_ID
                                        && m.TYPE_NAME == "HISTORY_STATUS"
                                       select m;

                    if (queryHistory.Any())
                    {
                        if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                        {
                            status = queryHistory.Single().NAME;
                        }
                        else
                        {
                            status = queryHistory.Single().NAME_EN;
                        }

                        //entity.Content = Utility.GetMessageInfo(languageType, notify.NOTIFY_TYPE).MessageContent;
                        entity.content = string.Format(Utility.GetMessageInfo(_context, languageType, notify.NOTIFY_TYPE).messageContent, notify.CONTENT, status);
                    }
                }
                else
                {
                    entity.content = string.Format(Utility.GetSystemMessageInfo(_context, languageType, notify.OBJECT_ID).message);
                }

                entityList.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }

        /// <summary>
        /// Get notify by id.
        /// </summary>
        /// <param name="id">Notify ID</param>
        /// <returns>NotifyEntity</returns>
        [HttpGet("GetNotifyById/{id}")]
        public ActionResult GetNotifyById([FromRoute] decimal id)
        {
            var query = from d in _context.D_NOTIFYS
                        where d.NOTIFY_ID == id
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = false
                });
            }

            D_NOTIFY notify = query.Single();

            NotifyEntity entity = new NotifyEntity();
            entity.notifyId = notify.NOTIFY_ID;
            entity.notifyType = notify.NOTIFY_TYPE;
            entity.objectId = notify.OBJECT_ID;
            entity.notifyDate = notify.NOTIFY_DATE;
            entity.userSendId = notify.USER_SEND_ID;
            entity.userReceiveId = notify.USER_RECIEVE_ID;
            entity.receiveModeUser = notify.RECEIVE_MODE_USER;
            entity.content = notify.CONTENT;

            var user = from d in _context.M_USERS
                       where d.USER_ID == notify.USER_RECIEVE_ID
                       select d;

            if (!user.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = false
                });
            }

            string languageType = user.Single().LANGUAGE_TYPE;

            if (notify.NOTIFY_TYPE == (int)NotifyType.Estimation)
            {
                entity.content = string.Format(Utility.GetMessageInfo(_context, languageType, notify.NOTIFY_TYPE).messageContent, notify.CONTENT);
            }
            else if (notify.NOTIFY_TYPE == (int)NotifyType.Job)
            {
                string status = string.Empty;

                var queryHistory = from d in _context.D_HISTORYS
                                   join m in _context.M_NAMES on d.STATUS equals m.CD
                                   where d.EXCHANGE_ID == notify.OBJECT_ID
                                    && m.TYPE_NAME == "HISTORY_STATUS"
                                   select m;

                if (queryHistory.Any())
                {
                    if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                    {
                        status = queryHistory.Single().NAME;
                    }
                    else
                    {
                        status = queryHistory.Single().NAME_EN;
                    }

                    entity.content = string.Format(Utility.GetMessageInfo(_context, languageType, notify.NOTIFY_TYPE).messageContent, notify.CONTENT, status);
                }
            }
            else
            {
                entity.content = string.Format(Utility.GetSystemMessageInfo(_context, languageType, notify.OBJECT_ID).message);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        /// <summary>
        /// Get notify by id.
        /// </summary>
        /// <param name="id">Notify ID</param>
        /// <returns>NotifyEntity</returns>
        [HttpGet("GetSystemMessage/{id}")]
        public ActionResult GetSystemMessage([FromRoute] decimal id)
        {
            var query = from d in _context.D_NOTIFYS
                        where d.NOTIFY_ID == id
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = string.Empty
                });
            }

            D_NOTIFY notify = query.Single();

            NotifyEntity entity = new NotifyEntity();
            entity.notifyId = notify.NOTIFY_ID;
            entity.notifyType = notify.NOTIFY_TYPE;
            entity.objectId = notify.OBJECT_ID;
            entity.notifyDate = notify.NOTIFY_DATE;
            entity.userSendId = notify.USER_SEND_ID;
            entity.userReceiveId = notify.USER_RECIEVE_ID;
            entity.receiveModeUser = notify.RECEIVE_MODE_USER;
            entity.content = notify.CONTENT;

            if (entity.notifyType == (int)NotifyType.System)
            {
                var user = from d in _context.M_USERS
                           where d.USER_ID == notify.USER_RECIEVE_ID
                           select d;

                M_USER userM = user.Single();

                var message = from d in _context.M_SYSTEM_MESSAGES
                              where d.MESSAGE_CD == entity.objectId
                                    && d.LANGUAGE_TYPE == userM.LANGUAGE_TYPE
                              select d;

                if (message.Any())
                {
                    entity.content = message.SingleOrDefault().MESSAGE_CONTENT;
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity.content
            });
        }

        /// <summary>
        /// Get system message by code
        /// </summary>
        /// <param name="id">system message ID</param>
        /// <returns>NotifyEntity</returns>
        [HttpGet("GetSystemMessageByCd/{id}/{language}")]
        public ActionResult GetSystemMessageByCd([FromRoute] decimal id, [FromRoute] string language)
        {
            var query = from d in _context.M_SYSTEM_MESSAGES
                        where d.MESSAGE_CD == id
                        && d.LANGUAGE_TYPE == language
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = string.Empty
                });
            }

            M_SYSTEM_MESSAGE notify = query.Single();

            SystemMessageEntity entity = new SystemMessageEntity();
            entity.messageId = notify.MESSAGE_ID;
            entity.messageCd = notify.MESSAGE_CD;
            entity.languageType = notify.LANGUAGE_TYPE;
            entity.message = notify.MESSAGE;
            entity.messageContent = notify.MESSAGE_CONTENT;

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity.messageContent
            });
        }

        /// <summary>
        /// Update notify.
        /// </summary>
        /// <param name="id">Notify ID</param>
        /// <returns>Success: 200; Not exists: 404</returns>
        [HttpPost("UpdateNotify/{id}")]
        public ActionResult UpdateNotify([FromRoute] decimal id)
        {
            System.Web.Http.HttpError errorHttp = null;

            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.D_NOTIFYS
                            where d.NOTIFY_ID == id
                            select d;

                if (!query.Any())
                {
                    errorHttp = new System.Web.Http.HttpError("Notify is not exists!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = errorHttp.Message,
                        Data = false
                    });
                }

                D_NOTIFY notify = query.Single();
                notify.READED_FLG = (int)ReadedFlg.Readed;
                notify.UPD_DATETIME = Utility.GetSysDateTime();

                // Commit transaction.
                tran.Commit();

                errorHttp = new System.Web.Http.HttpError("Update notify is success!");

                return Ok(new Result
                {
                    Status = 200,
                    Message = errorHttp.Message,
                    Data = true
                });
            }
            catch (System.Exception)
            {
                // Rollback transaction.
                tran.Rollback();
                errorHttp = new System.Web.Http.HttpError("Update notify is not success!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = errorHttp.Message,
                    Data = false
                });
            }
        }

        /// <summary>
        /// Readed all notify.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>Success: 200; Not exists: 404</returns>
        [HttpPost("ReadAll/{id}/{modeUser}")]
        public ActionResult ReadAll([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            System.Web.Http.HttpError errorHttp = null;

            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.D_NOTIFYS
                            where d.USER_RECIEVE_ID == id
                                && d.RECEIVE_MODE_USER == modeUser
                                && d.READED_FLG == (int)ReadedFlg.New
                            select d;

                if (!query.Any())
                {
                    errorHttp = new System.Web.Http.HttpError("Update notify is success!");

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = errorHttp.Message,
                        Data = true
                    });
                }

                List<D_NOTIFY> notifyList = query.ToList();

                foreach (D_NOTIFY notify in notifyList)
                {
                    notify.READED_FLG = (int)ReadedFlg.Readed;
                    notify.UPD_DATETIME = Utility.GetSysDateTime();
                }

                // Commit transaction.
                tran.Commit();

                errorHttp = new System.Web.Http.HttpError("Update notify is success!");

                return Ok(new Result
                {
                    Status = 200,
                    Message = errorHttp.Message,
                    Data = true
                });
            }
            catch (System.Exception)
            {
                // Rollback transaction.
                tran.Rollback();
                errorHttp = new System.Web.Http.HttpError("Update notify is not success!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = errorHttp.Message,
                    Data = false
                });
            }
        }
    }
}