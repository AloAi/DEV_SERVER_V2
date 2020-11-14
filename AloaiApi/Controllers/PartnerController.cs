using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Entity;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Aloai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public PartnerController(AloaiDataContext context)
        {
            _context = context;
        }

        [HttpGet("GetPartnerInfo/{userId}")]
        public ActionResult GetPartnerInfo([FromRoute] decimal userId)
        {
            PartnerEntity entity = Utility.GetPartnerInfo(_context, userId);

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        [HttpPost("UpdatePartnerInfo")]
        public ActionResult UpdatePartnerInfo(PartnerEntity partner)
        {
            using (IDbContextTransaction tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (Utility.UpdatePartner(_context, partner))
                    {
                        tran.Commit();
                    }

                    PartnerEntity entity = Utility.GetPartnerInfo(_context, partner.userId);

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = string.Empty,
                        Data = entity
                    });
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                }
            }

            return Ok(new Result
            {
                Status = 404,
                Message = string.Empty,
                Data = null
            });
        }

        [HttpPost("UpdateFixLocation/{userId}/{fixLocationFlg}")]
        public ActionResult UpdateFixLocation(decimal userId, decimal fixLocationFlg)
        {
            using (IDbContextTransaction tran = _context.Database.BeginTransaction())
            {
                try
                {
                    var queryPartner = from d in _context.M_PARTNER_INFOS
                                       where d.USER_ID == userId
                                       select d;

                    if (queryPartner.Any())
                    {
                        M_PARTNER_INFO partner = queryPartner.Single();
                        partner.FIX_LOCATION_FLG = fixLocationFlg;
                        partner.UPD_DATETIME = Utility.GetSysDateTime();

                        _context.SaveChanges();
                        tran.Commit();
                    }

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = string.Empty,
                        Data = true
                    });
                }
                catch (Exception)
                {
                    tran.Rollback();
                }

            }

            return Ok(new Result
            {
                Status = 404,
                Message = string.Empty,
                Data = false
            });
        }

        private static PartnerEntity ItemToDTO(PartnerEntity item)
        {
            return item;
        }
    }
}