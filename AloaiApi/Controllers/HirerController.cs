using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aloai.Entity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

namespace Aloai.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HirerController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public HirerController(AloaiDataContext context)
        {
            _context = context;
        }

        [HttpGet("GetHirerInfo/{userId}")]
        public ActionResult GetHirerInfo([FromRoute] decimal userId)
        {
            HirerInfoEntity hirerInfo = Utility.GetHirerInfo(_context, userId);

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = hirerInfo
            });
        }

        [HttpPost("UpdateHirerInfo")]
        public ActionResult UpdateHirerInfo(HirerInfoEntity hirer)
        {
            var query = from d in _context.M_USERS
                        where d.USER_ID == hirer.userId
                        select d;

            if (query.Any())
            {
                M_USER upd = query.Single();
                upd.NAME = hirer.name;

                string avartaPath;

                if (Utility.UploadAvatar(_context, hirer.userId, hirer.avatar, hirer.avatar.path, out avartaPath))
                {
                    upd.AVATAR = avartaPath;
                }

                upd.UPD_DATETIME = Utility.GetSysDateTime();

                _context.SaveChanges();

                HirerInfoEntity hirerInfo = Utility.GetHirerInfo(_context, hirer.userId);

                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = hirerInfo
                });
            }

            return Ok(new Result
            {
                Status = 404,
                Message = string.Empty,
                Data = null
            });
        }

        private static HirerInfoEntity ItemToDTO(HirerInfoEntity item)
        {
            return item;
        }


    }
}