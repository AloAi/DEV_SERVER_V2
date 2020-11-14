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
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    /// Estimation controller class.
    /// </summary>
    public class EstimationController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public EstimationController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get estimation by worker ID.
        /// </summary>
        /// <param name="id">Worker ID</param>
        /// <returns>List EstimationEntity</returns>
        [HttpGet("GetEstimationByPartner/{id}")]
        public ActionResult GetEstimationByPartner([FromRoute] decimal id)
        {
            DefineEntity define = Utility.GetDefineValue(Constant.COMMENT_NUMBER_MAX);
            List<ReviewEntity> entityList = new List<ReviewEntity>();
            M_USER user = _context.M_USERS.FirstOrDefault(x => x.USER_ID == id);
            List<D_REVIEW> estimationList = new List<D_REVIEW>();

            var query = from d in _context.D_REVIEWS
                        join c in _context.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                        where c.WORKER_ID == id
                            && d.REVIEW_MODE_USER == (int)Mode.Hirer
                        orderby d.REVIEW_DATE descending
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = null
                });
            }

            estimationList = query.ToList();

            foreach (D_REVIEW estimation in estimationList)
            {
                ReviewEntity entity = new ReviewEntity();
                entity.reviewId = estimation.REVIEW_ID;
                entity.contactId = estimation.CONTACT_ID;
                entity.reviewUserId = estimation.REVIEW_USER_ID;
                entity.reviewModeUser = estimation.REVIEW_MODE_USER;
                entity.reviewDate = estimation.REVIEW_DATE;
                entity.score = estimation.SCORE;
                entity.comment = estimation.COMMENT;

                var queryName = from d in _context.M_USERS
                                where d.USER_ID == estimation.REVIEW_USER_ID
                                select d.NAME;

                if (queryName.Any())
                {
                    entity.reviewUserName = queryName.Single();
                }

                entityList.Add(entity);

                if (entityList.Count == int.Parse(define.value))
                {
                    break;
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }

        /// <summary>
        /// Get estimation by hirer ID.
        /// </summary>
        /// <param name="id">Hirer ID</param>
        /// <returns>List EstimationEntity</returns>
        [HttpGet("GetEstimationByHirer/{id}")]
        public ActionResult GetEstimationByHirer([FromRoute] decimal id)
        {
            DefineEntity define = Utility.GetDefineValue(Constant.COMMENT_NUMBER_MAX);
            List<ReviewEntity> entityList = new List<ReviewEntity>();
            M_USER user = _context.M_USERS.FirstOrDefault(x => x.USER_ID == id);
            List<D_REVIEW> estimationList = new List<D_REVIEW>();

            var query = from d in _context.D_REVIEWS
                        join c in _context.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                        where c.HIRER_ID == id
                            && d.REVIEW_MODE_USER == (int)Mode.Partner
                        //&& d.COMMENT != string.Empty
                        orderby d.REVIEW_DATE descending
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = null
                });
            }

            estimationList = query.ToList();

            foreach (D_REVIEW estimation in estimationList)
            {
                ReviewEntity entity = new ReviewEntity();
                entity.reviewId = estimation.REVIEW_ID;
                entity.contactId = estimation.CONTACT_ID;
                entity.reviewUserId = estimation.REVIEW_USER_ID;
                entity.reviewModeUser = estimation.REVIEW_MODE_USER;
                entity.reviewDate = estimation.REVIEW_DATE;
                entity.score = estimation.SCORE;
                entity.comment = estimation.COMMENT;

                var queryName = from d in _context.M_USERS
                                where d.USER_ID == estimation.REVIEW_USER_ID
                                select d.NAME;

                if (queryName.Any())
                {
                    entity.reviewUserName = queryName.Single();
                }

                entityList.Add(entity);

                if (entityList.Count == int.Parse(define.value))
                {
                    break;
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entityList
            });
        }

        /// <summary>
        /// Get Estimation ById with EstModeUser
        /// </summary>
        /// <param name="id">Exchage ID</param>
        /// <param name="EstModeUser">Estimation mode user</param>
        /// <returns>EstimationEntity</returns>
        [HttpGet("GetEstimationById/{id}/{estModeUser}")]
        public ActionResult GetEstimationById([FromRoute] decimal id, [FromRoute] decimal estModeUser)
        {
            var query = from d in _context.D_REVIEWS
                        where d.CONTACT_ID == id
                        //&& d.EST_MODE_USER == EstModeUser
                        orderby d.REVIEW_MODE_USER
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = null
                });
            }

            List<D_REVIEW> estimationList = query.ToList();
            List<ReviewEntity> entityList = new List<ReviewEntity>();
            ReviewEntity result = new ReviewEntity();

            foreach (D_REVIEW estimation in estimationList)
            {
                result.reviewId = estimation.REVIEW_ID;
                result.contactId = estimation.CONTACT_ID;

                if (estimation.REVIEW_MODE_USER == estModeUser)
                {
                    result.isSended = true;
                    result.reviewUserId = estimation.REVIEW_USER_ID;
                    result.reviewModeUser = estimation.REVIEW_MODE_USER;
                    result.reviewDate = estimation.REVIEW_DATE;
                    result.score = estimation.SCORE;
                    result.comment = estimation.COMMENT;

                    var user = from d in _context.M_USERS
                               where d.USER_ID == estimation.REVIEW_USER_ID
                               select d;

                    if (user.Any())
                    {
                        result.reviewUserName = user.Single().NAME;
                    }
                }
                else
                {
                    result.isReceived = true;
                    result.reviewUserId = estimation.REVIEW_USER_ID;
                    result.reviewDateReceive = estimation.REVIEW_DATE;
                    result.scoreReceive = estimation.SCORE;
                    result.commentReceive = estimation.COMMENT;

                    var user = from d in _context.M_USERS
                               where d.USER_ID == estimation.REVIEW_USER_ID
                               select d;

                    if (user.Any())
                    {
                        result.reviewUserName = user.Single().NAME;
                    }
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = result
            });
        }

        /// <summary>
        /// Get estimated by id.
        /// </summary>
        /// <param name="id">Estimate ID</param>
        /// <returns>EstimationEntity</returns>
        [HttpGet("GetEstimatedById/{id}")]
        public ActionResult GetEstimatedById([FromRoute] decimal id)
        {
            var query = from d in _context.D_REVIEWS
                        where d.REVIEW_ID == id
                        select d;

            if (!query.Any())
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = null
                });
            }

            D_REVIEW estimation = query.Single();
            List<ReviewEntity> entityList = new List<ReviewEntity>();

            ReviewEntity result = new ReviewEntity();
            result.reviewId = estimation.REVIEW_ID;
            result.contactId = estimation.CONTACT_ID;
            result.reviewUserId = estimation.REVIEW_USER_ID;
            result.reviewModeUser = estimation.REVIEW_MODE_USER;
            result.reviewDate = estimation.REVIEW_DATE;
            result.score = estimation.SCORE;
            result.comment = estimation.COMMENT;

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = result
            });
        }

        /// <summary>
        /// Insert estimation info.
        /// </summary>
        /// <param name="entity">EstimationEntity</param>
        /// <returns>Success: 200; Error: 400; Not found: 404</returns>
        [HttpPost("InsertEstimation")]
        public ActionResult InsertEstimation(ReviewEntity entity)
        {
            System.Web.Http.HttpError errorHttp = null;

            if (entity == null)
            {
                errorHttp = new System.Web.Http.HttpError("EstimationEntity is not found!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = errorHttp.Message,
                    Data = null
                });
            }

            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.D_REVIEWS
                            where d.CONTACT_ID == entity.contactId
                            && d.REVIEW_MODE_USER == (int)entity.reviewModeUser
                            select d;

                if (query.Any())
                {
                    errorHttp = new System.Web.Http.HttpError("Estimation is exists!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = errorHttp.Message,
                        Data = null
                    });
                }

                decimal userReceiveId;
                decimal receiveModeUser;
                D_REVIEW estimation = new D_REVIEW();

                estimation.CONTACT_ID = entity.contactId;
                estimation.REVIEW_USER_ID = entity.reviewUserId;
                estimation.REVIEW_MODE_USER = entity.reviewModeUser;
                estimation.REVIEW_DATE = Utility.GetSysDateTime();
                estimation.SCORE = entity.score.Value;
                estimation.COMMENT = entity.comment;

                _context.D_REVIEWS.Add(estimation);
                _context.SaveChanges();

                if (!UpdateScore(_context, entity.contactId, entity.score.Value, entity.reviewModeUser))
                {
                    // Rollback transaction.
                    tran.Rollback();
                    errorHttp = new System.Web.Http.HttpError("Insert is not success!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = errorHttp.Message,
                        Data = null
                    });
                }

                if (entity.reviewModeUser == (decimal)Mode.Hirer)
                {
                    var queryWorker = from d in _context.V_CONTACT_INFOS
                                      where d.CONTACT_ID == entity.contactId
                                      select d.WORKER_ID;

                    if (!queryWorker.Any())
                    {
                        // Rollback transaction.
                        tran.Rollback();
                        errorHttp = new System.Web.Http.HttpError("Update is not success!");

                        return Ok(new Result
                        {
                            Status = 404,
                            Message = errorHttp.Message,
                            Data = null
                        });
                    }

                    userReceiveId = queryWorker.Single();
                    receiveModeUser = (decimal)Mode.Partner;
                }
                else
                {
                    var queryHirer = from d in _context.V_CONTACT_INFOS
                                     where d.CONTACT_ID == entity.contactId
                                     select d.HIRER_ID;

                    if (!queryHirer.Any())
                    {
                        // Rollback transaction.
                        tran.Rollback();
                        errorHttp = new System.Web.Http.HttpError("Update is not success!");

                        return Ok(new Result
                        {
                            Status = 404,
                            Message = errorHttp.Message,
                            Data = null
                        });
                    }

                    userReceiveId = queryHirer.Single();
                    receiveModeUser = (decimal)Mode.Hirer;
                }


                // Insert notify.
                if (!Utility.InsertNotify(_context, (int)NotifyType.Estimation, entity.contactId, entity.reviewUserId, userReceiveId, receiveModeUser))
                {
                    // Rollback transaction.
                    tran.Rollback();
                    errorHttp = new System.Web.Http.HttpError("Update is not success!");

                    return Ok(new Result
                    {
                        Status = 404,
                        Message = errorHttp.Message,
                        Data = null
                    });
                }


                // Commit transaction.
                tran.Commit();

                errorHttp = new System.Web.Http.HttpError("Insert is success!");

                return Ok(new Result
                {
                    Status = 200,
                    Message = errorHttp.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                // Rollback transaction.
                tran.Rollback();

                System.Web.Http.HttpError error = new System.Web.Http.HttpError(ex.Message);

                return Ok(new Result
                {
                    Status = 404,
                    Message = error.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get Estimation By exchangeID
        /// </summary>
        /// <param name="id">Exchage ID</param>
        /// <param name="EstModeUser">Estimation mode user</param>
        /// <returns>EstimationEntity</returns>
        [HttpGet("GetEstimationByExchangeId/{id}/{modeUser}")]
        public ActionResult GetEstimationByExchangeId([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            int mode = (int)Mode.Hirer;

            if (modeUser == (int)Mode.Hirer)
            {
                mode = (int)Mode.Partner;
            }

            string query =
                "    SELECT A.EXCHANGE_ID, D.ESTIMATOR_USER_ID, D.EST_MODE_USER, D.SCORE, D.COMMENT, D.ESTIMATION_DATE " +
                "         , C.EXCHANGE_ID AS EXCHANGE_ID_RECEIVE, C.ESTIMATOR_USER_ID AS ESTIMATOR_USER_ID_RECEIVE " +
                "         , C.EST_MODE_USER AS EST_MODE_USER_RECEIVE, C.SCORE AS SCORE_RECEIVE, C.COMMENT AS COMMENT_RECEIVE, " +
                "           C.ESTIMATION_DATE AS ESTIMATION_DATE_RECEIVE " +
                "      FROM D_EXCHANGE A " +
                " LEFT JOIN D_ESTIMATION C ON (A.EXCHANGE_ID = C.EXCHANGE_ID AND C.EST_MODE_USER = " + modeUser + ") " +
                " LEFT JOIN D_ESTIMATION D ON (A.EXCHANGE_ID = D.EXCHANGE_ID AND D.EST_MODE_USER = " + mode + ")  " +
                "     WHERE A.EXCHANGE_ID = " + id + " ";

            DataTable data = SqlHelper.FillData(query);

            if (data.Rows.Count == 0)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = null
                });
            }

            ReviewEntity result = new ReviewEntity();

            result.contactId = data.Rows[0]["EXCHANGE_ID"] == null ? (decimal)data.Rows[0]["EXCHANGE_ID_RECEIVE"] : (decimal)data.Rows[0]["EXCHANGE_ID"];

            if (!string.IsNullOrEmpty(data.Rows[0]["ESTIMATION_DATE"].ToString()))
            {
                result.reviewDate = DateTime.Parse(data.Rows[0]["ESTIMATION_DATE"].ToString());
            }

            if (!string.IsNullOrEmpty(data.Rows[0]["SCORE"].ToString()))
            {
                result.isSended = true;
                result.score = (decimal)data.Rows[0]["SCORE"];
            }

            result.comment = data.Rows[0]["COMMENT"] == null ? string.Empty : data.Rows[0]["COMMENT"].ToString();

            if (!string.IsNullOrEmpty(data.Rows[0]["ESTIMATION_DATE_RECEIVE"].ToString()))
            {
                result.reviewDateReceive = DateTime.Parse(data.Rows[0]["ESTIMATION_DATE_RECEIVE"].ToString());
            }

            if (!string.IsNullOrEmpty(data.Rows[0]["SCORE_RECEIVE"].ToString()))
            {
                result.isReceived = true;
                result.scoreReceive = (decimal)data.Rows[0]["SCORE_RECEIVE"];
            }

            result.commentReceive = string.IsNullOrEmpty(data.Rows[0]["COMMENT_RECEIVE"].ToString()) ? string.Empty : data.Rows[0]["COMMENT_RECEIVE"].ToString();

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = result
            });
        }

        /// <summary>
        /// Get estimation.
        /// </summary>
        /// <param name="id">Exchage ID</param>
        /// <param name="EstModeUser">Estimation mode user</param>
        /// <returns>EstimationEntity</returns>
        [HttpGet("GetEstimation/{id}/{modeUser}")]
        public ActionResult GetEstimation([FromRoute] decimal id, [FromRoute] decimal modeUser)
        {
            if (modeUser == (int)Mode.Hirer)
            {
                ActionResult result = GetEstimationByHirer(id);

                return result;
            }
            else
            {
                ActionResult result = GetEstimationByPartner(id);

                return result;
            }
        }

        /// <summary>
        /// Update score.
        /// </summary>
        /// <param name="db">PartTimeDataClassesDataContext</param>
        /// <param name="exchangeId">Exchange Id</param>
        /// <param name="score">Score</param>
        /// <param name="modeUserEstimation">Mode user estimation</param>
        /// <returns>OK: True; Fail: False</returns>
        public bool UpdateScore(AloaiDataContext db, decimal contactId, decimal score, decimal modeUserEstimation)
        {
            if (modeUserEstimation == (decimal)Mode.Hirer)
            {
                var queryWorker = from d in db.V_CONTACT_INFOS
                                  where d.CONTACT_ID == contactId
                                  select d.WORKER_ID;

                // Account is company.
                if (queryWorker.Any())
                {
                    if (!Utility.UpdateScore(db, contactId, score, modeUserEstimation))
                    {
                        return false;
                    }
                }
            }
            else
            {
                var queryHirer = from d in db.V_CONTACT_INFOS
                                 where d.CONTACT_ID == contactId
                                 select d.HIRER_ID;

                // Account is company.
                if (queryHirer.Any())
                {
                    if (!Utility.UpdateScore(db, contactId, score, modeUserEstimation))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}