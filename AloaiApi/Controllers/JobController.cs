using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Entity;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Aloai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public JobController(AloaiDataContext context)
        {
            _context = context;
        }

        [HttpGet("GetJob/{id}")]
        public ActionResult GetJob([FromRoute] decimal id)
        {
            var todoItem = _context.V_SUGGEST_JOBS.FindAsync(id);

            if (todoItem == null)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = string.Empty,
                    Data = null
                });
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = todoItem
            });
        }

        // GET: api/GetSuggestJob/5
        [HttpGet("GetSuggestJob/{catalogCd}")]
        public ActionResult GetSuggestJob([FromRoute] decimal catalogCd)
        {
            string language = string.Empty;
            var query = from d in _context.V_SUGGEST_JOBS
                        where d.CATALOG_CD == catalogCd
                        select d;

            List<SuggestJob> suggestList = new List<SuggestJob>();

            if (query.Any())
            {
                foreach (V_SUGGEST_JOB detail in query.ToList())
                {
                    SuggestJob suggest = new SuggestJob();
                    suggest.suggestId = detail.SUGGEST_ID;

                    if (string.IsNullOrEmpty(language) || language.Equals(Constant.LANGUAGE_VN))
                    {
                        suggest.templateName = detail.TEMPLATE_TITLE;
                    }
                    else
                    {
                        suggest.templateName = detail.TEMPLATE_TITLE_EN;
                    }

                    suggest.dispOrder = detail.DISP_ORDER;
                    suggestList.Add(suggest);
                }
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = suggestList
            });
        }

        [HttpPost("CreateJob")]
        public ActionResult CreateJob(JobEntity job)
        {
            var item = new D_JOB
            {
                USER_ID = job.userId,
                SUGGEST_ID = job.suggestId,
                LATITUDE = job.location.latitude,
                LONGITUDE = job.location.longitude,
                CANCEL_FLG = 0,
                RENEW_NUM = 0,
                REG_DATETIME = Utility.GetSysDateTime(),
            };

            _context.D_JOBS.Add(item);
            _context.SaveChanges();

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = true
            });
        }

        [HttpPost("RenewJob/{jobId}")]
        public ActionResult RenewJob([FromRoute] decimal jobId)
        {
            double second = double.Parse(Utility.GetDefineValue(Constant.TIMER_CANCEL_JOB).value);
            DateTime date = Utility.GetSysDateTime();

            var query = from d in _context.D_JOBS
                        where d.JOB_ID == jobId
                           && ((d.UPD_DATETIME.HasValue && DateTime.Compare(d.UPD_DATETIME.Value.AddSeconds(second), date) >= 0)
                            || (!d.UPD_DATETIME.HasValue && DateTime.Compare(d.REG_DATETIME.AddSeconds(second), date) >= 0))
                        select d;

            if (query.Any())
            {
                D_JOB upd = query.Single();
                upd.RENEW_NUM = upd.RENEW_NUM + 1;
                upd.UPD_DATETIME = Utility.GetSysDateTime();

                _context.SaveChanges();

                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = true
                });
            }

            return Ok(new Result
            {
                Status = 404,
                Message = "Data not exists.",
                Data = false
            });
        }

        private static D_JOB ItemToDTO(D_JOB item)
        {
            return item;
        }

        [HttpGet("GetJobInfo/{jobId}")]
        public ActionResult GetJobInfo([FromRoute] decimal jobId)
        {
            V_JobEntity entity = null;

            var query = from d in _context.V_JOBS
                        where d.JOB_ID == jobId
                        select d;

            if (query.Any())
            {
                V_JOB job = query.Single();
                entity = new V_JobEntity();
                entity.jobId = job.JOB_ID;
                entity.userId = job.USER_ID;
                entity.phoneNumber = job.PHONE_NUMBER;

                ImageInfoEntity avatar = new ImageInfoEntity();
                avatar.path = job.AVATAR;

                entity.avatar = avatar;
                entity.score = job.SCORE;
                entity.likeNum = job.LIKE_NUM;

                TemplateEntity template = new TemplateEntity();
                template.templateCd = job.TEMPLATE_CD;
                template.templateTitle = job.TEMPLATE_TITLE;
                entity.template = template;

                Location loc = new Location();
                loc.latitude = job.LATITUDE;
                loc.longitude = job.LONGITUDE;
                entity.location = loc;

                Catalog catalog = new Catalog();
                catalog.catalogCd = job.CATALOG_CD;
                catalog.catalogName = job.CATALOG_NAME;
                entity.catalog = catalog;

                entity.dateTime = job.UPD_DATETIME.HasValue ? job.UPD_DATETIME.Value : job.REG_DATETIME;

                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = entity
                });
            }

            return Ok(new Result
            {
                Status = 404,
                Message = "Data not exists.",
                Data = null
            });
        }

        [HttpDelete("DeleteJob/{id}")]
        public async Task<IActionResult> DeleteJob([FromRoute] decimal id)
        {
            var todoItem = await _context.D_JOBS.FindAsync(id);

            if (todoItem == null)
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists.",
                    Data = false
                });
            }

            _context.D_JOBS.Remove(todoItem);
            _context.SaveChanges();

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = true
            });
        }

        /// <summary>
        /// Get Job by hirer ID with status is new.
        /// </summary>
        /// <param name="userId">Hirer ID</param>
        /// <returns>IHttpActionResult</returns>
        [HttpGet("GetJobByHirer/{userId}")]
        public ActionResult GetJobByHirer([FromRoute] decimal userId)
        {
            double second = double.Parse(Utility.GetDefineValue(Constant.TIMER_CANCEL_JOB).value);
            List<V_JobEntity> entityList = new List<V_JobEntity>();
            //string language = "vi";
            //var hirerInfo = from d in _context.M_USERS
            //                where d.USER_ID == userId
            //                select d;

            //if (!hirerInfo.Any())
            //{
            //    return Ok(new Result
            //    {
            //        Status = 404,
            //        Message = "Data not exists.",
            //        Data = null
            //    });
            //}

            //M_USER user = hirerInfo.Single();
            //language = user.LANGUAGE_TYPE;
            var query = from d in _context.V_JOBS
                        where d.USER_ID == userId
                           && d.CANCEL_FLG == 0
                        select d;
            DateTime date = Utility.GetSysDateTime();

            if (query.Any())
            {
                foreach (V_JOB job in query.ToList())
                {
                    if ((job.UPD_DATETIME.HasValue && DateTime.Compare(job.UPD_DATETIME.Value.AddSeconds(second), date) >= 0)
                        || (!job.UPD_DATETIME.HasValue && DateTime.Compare(job.REG_DATETIME.AddSeconds(second), date) >= 0))
                    {
                        job.CANCEL_FLG = 1;
                        _context.SaveChanges();
                        continue;
                    }

                    V_JobEntity entity = new V_JobEntity();
                    entity.jobId = job.JOB_ID;
                    entity.userId = job.USER_ID;
                    entity.userName = job.NAME;
                    entity.phoneNumber = job.PHONE_NUMBER;

                    ImageInfoEntity avatar = new ImageInfoEntity();
                    avatar.path = job.AVATAR;

                    entity.avatar = avatar;
                    entity.score = job.SCORE;
                    entity.likeNum = job.LIKE_NUM;

                    TemplateEntity template = new TemplateEntity();
                    template.templateCd = job.TEMPLATE_CD;
                    template.templateTitle = job.TEMPLATE_TITLE;
                    entity.template = template;

                    Location loc = new Location();
                    loc.latitude = job.LATITUDE;
                    loc.longitude = job.LONGITUDE;
                    entity.location = loc;

                    Catalog catalog = new Catalog();
                    catalog.catalogCd = job.CATALOG_CD;
                    catalog.catalogName = job.CATALOG_NAME;
                    entity.catalog = catalog;

                    entity.dateTime = job.UPD_DATETIME.HasValue ? job.UPD_DATETIME.Value : job.REG_DATETIME;
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