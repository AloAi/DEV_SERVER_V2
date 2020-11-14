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
    /// Question controller class.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public QuestionController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Insert question.
        /// </summary>
        /// <param name="questionEntity">QuestionEntity</param>
        /// <returns>Success: 200; Error: 400</returns>
        [HttpPost("InsertQuestion")]
        public ActionResult InsertQuestion(QuestionEntity questionEntity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                M_QUESTION question = new M_QUESTION();
                question.USER_ID = questionEntity.userId;
                question.SUBJECT = questionEntity.subject;
                question.CONTENT = questionEntity.content;
                question.QUESTION_DATE = Utility.GetSysDateTime();
                question.REG_DATETIME = Utility.GetSysDateTime();

                _context.M_QUESTIONS.Add(question);
                _context.SaveChanges();

                // Commit transaction.
                tran.Commit();

                System.Web.Http.HttpError errorHttp = new System.Web.Http.HttpError("Insert is success!");

                return Ok(new Result
                {
                    Status = 200,
                    Message = errorHttp.Message,
                    Data = null
                });
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                System.Web.Http.HttpError error = new System.Web.Http.HttpError("Error system!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = error.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get question.
        /// </summary>
        /// <returns>Success: 200; Error: 400</returns>
        [HttpGet("GetQuestion")]
        public ActionResult GetQuestion()
        {
            try
            {
                var query = from d in _context.M_QUESTIONS
                            orderby d.QUESTION_DATE descending
                            select d;

                if (query.Any())
                {
                    List<M_QUESTION> list = query.ToList();
                    List<QuestionEntity> entityList = new List<QuestionEntity>();

                    foreach (M_QUESTION question in list)
                    {
                        var user = from d in _context.M_USERS
                                   where d.USER_ID == question.USER_ID
                                   select d;

                        QuestionEntity entity = new QuestionEntity();
                        entity.faqId = question.FAQ_ID;
                        entity.userId = question.USER_ID;

                        // User name
                        if (user.Any())
                        {
                            entity.userName = user.Single().NAME;
                        }

                        entity.subject = question.SUBJECT;
                        entity.content = question.CONTENT;
                        entity.questionDate = question.QUESTION_DATE;

                        entityList.Add(entity);
                    }

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = string.Empty,
                        Data = entityList
                    });
                }
                else
                {
                    return Ok(new Result
                    {
                        Status = 404,
                        Message = "Data not exists.",
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = null
                });
            }
        }
    }
}