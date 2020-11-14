//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Threading.Tasks;
using Aloai.Auth;
using Aloai.Entity;
using Aloai.Enum;
using Aloai.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Extensions;
using Nancy;
using Nancy.Json;
using Nancy.Responses;
using NHibernate.Linq;

namespace Aloai.Controller
{
    /// <summary>
    /// User controller class.
    /// </summary>
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AloaiDataContext _context;
        public UserController(AloaiDataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get user list.
        /// </summary>
        /// <returns>List UserEntity.</returns>
        [HttpGet("GetUserList")]
        public ActionResult GetUserList()
        {
            List<M_USER> userList = _context.M_USERS.ToList();
            List<UserEntity> list = new List<UserEntity>();

            foreach (M_USER user in userList)
            {
                UserEntity entity = new UserEntity();
                entity.userId = user.USER_ID;
                entity.phoneNumber = user.PHONE_NUMBER;
                entity.modeUser = user.MODE_USER;
                entity.name = user.NAME;

                ImageInfoEntity avatar = new ImageInfoEntity();
                avatar.path = user.AVATAR;
                entity.avatar = avatar;

                list.Add(entity);
            }

            JavaScriptSerializer jon = new JavaScriptSerializer();

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = list
            });
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>UserEntity</returns>
        [HttpGet("GetUser/{id}")]
        public ActionResult GetUser(decimal id)
        {
            if (!Utility.CheckUserExists(_context, id))
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists",
                    Data = null
                });
            }

            M_USER user = _context.M_USERS.FirstOrDefault(x => x.USER_ID == id);

            UserEntity entity = new UserEntity();
            entity.userId = user.USER_ID;
            entity.phoneNumber = user.PHONE_NUMBER;
            entity.modeUser = user.MODE_USER;

            UserEntity uEntity = Utility.GetUserInfo(_context, user.USER_ID);

            entity.name = uEntity.name;
            entity.avatar = uEntity.avatar;
            M_HIRER_INFO hirer = _context.M_HIRER_INFOS.FirstOrDefault(x => x.USER_ID == id);

            entity.userId = hirer.USER_ID;
            entity.score = hirer.SCORE;
            entity.likeNum = hirer.LIKE_NUM;

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        /// <summary>
        /// Get user by phone number.
        /// </summary>
        /// <param name="id">Phone number.</param>
        /// <returns>UserEntity</returns>
        [HttpGet("GetUserByPhone/{id}")]
        public ActionResult GetUserByPhone(string id)
        {
            if (!Utility.CheckPhoneExists(_context, id))
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists",
                    Data = null
                });
            }

            // M_USER user = db.M_USER.FirstOrDefault(x => x.PHONE_NUMBER == id && );

            var query = from d in _context.M_USERS
                        where d.PHONE_NUMBER == id
                         && d.DELETE_FLG == 0
                        select d;

            M_USER user = query.Single();

            UserEntity entity = new UserEntity();
            entity.userId = user.USER_ID;
            entity.phoneNumber = user.PHONE_NUMBER;
            entity.modeDefault = user.MODE_DEFAULT;
            entity.modeUser = user.MODE_USER;
            entity.name = user.NAME;

            ImageInfoEntity avatar = new ImageInfoEntity();
            avatar.path = user.AVATAR;
            entity.avatar = avatar;

            M_HIRER_INFO hirer = _context.M_HIRER_INFOS.FirstOrDefault(x => x.USER_ID == entity.userId);

            entity.userId = hirer.USER_ID;
            entity.score = hirer.SCORE;
            entity.likeNum = hirer.LIKE_NUM;

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        /// <summary>
        /// Delete user.
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns>Success: true, Faile: false</returns>
        [HttpGet]
        public bool DeleteUser(decimal id)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.M_USERS
                            where d.USER_ID == id
                            select d;

                if (query.Any())
                {
                    M_USER user = query.Single();
                    _context.M_USERS.Add(user);
                    _context.SaveChanges();
                }

                // Remove image.
                if (!Utility.RemoveImage(_context, id, ImageType.Profile))
                {
                    return false;
                }

                // Commit transaction.
                tran.Commit();

                return true;
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Change worker status.
        /// </summary>
        /// <param name="entity">WorkerInfoEntity</param>
        /// <returns>Success: True; Fail: False</returns>
        [HttpPost]
        public bool ChangeWorkerStatus(PartnerInfoEntity entity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                if (!Utility.CheckUserExists(_context, entity.userId))
                {
                    return false;
                }

                var query = from d in _context.M_PARTNER_INFOS
                            where d.USER_ID == entity.userId
                            select d;

                M_PARTNER_INFO user = query.Single();
                user.STATUS = entity.status;

                _context.SaveChanges();

                // Commit transaction.
                tran.Commit();

                return true;
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Change hirer status.
        /// </summary>
        /// <param name="entity">HirerInfoEntity</param>
        /// <returns>Success: True; Fail: False</returns>
        [HttpPost]
        public bool ChangeHirerStatus(HirerInfoEntity entity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                if (!Utility.CheckUserExists(_context, entity.userId))
                {
                    return false;
                }

                var query = from d in _context.M_HIRER_INFOS
                            where d.USER_ID == entity.userId
                            select d;

                M_HIRER_INFO hirer = query.Single();
                hirer.STATUS = entity.status;

                _context.SaveChanges();

                // Commit transaction.
                tran.Commit();

                return true;
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Get avatar by User ID
        /// </summary>
        /// <param name="id">User ID</param>
        /// <returns>String avatar</returns>
        [HttpGet("GetAvatarByUserId/{id}")]
        public ActionResult GetAvatarByUserId(decimal id)
        {
            if (!Utility.CheckUserExists(_context, id))
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists",
                    Data = null
                });
            }

            M_USER user = _context.M_USERS.FirstOrDefault(x => x.USER_ID == id);

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = user.AVATAR
            });
        }

        /// <summary>
        /// SignIn.
        /// </summary>
        /// <param name="loginEntity">LoginEntity</param>
        /// <returns>Success: 200; Fail: 400; Exists: 201</returns>
        [HttpPost("SignIn")]
        public ActionResult SignIn([FromBody] LoginEntity loginEntity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            M_USER user = null;

            AuthorEntity author = new AuthorEntity();
            author.phoneNumber = loginEntity.phoneNumber;
            author.token = loginEntity.token;

            if (!Utility.ValidatiTokenId(author))
            {
                System.Web.Http.HttpError error = new System.Web.Http.HttpError("Error validati token id!");

                return Ok(new Result
                {
                    Status = 404,
                    Message = error.Message,
                    Data = null
                });
            }

            try
            {
                if (Utility.CheckPhoneExists(_context, loginEntity.phoneNumber))
                {
                    var query = from d in _context.M_USERS
                                where d.PHONE_NUMBER == loginEntity.phoneNumber
                                    && d.DELETE_FLG == 0
                                select d;

                    user = query.Single();

                    if (!string.IsNullOrEmpty(loginEntity.languageType))
                    {
                        user.LANGUAGE_TYPE = loginEntity.languageType;
                    }
                    else
                    {
                        user.LANGUAGE_TYPE = Constant.LANGUAGE_VN;
                    }

                    user.SIGNIN_LAST = Utility.GetSysDateTime();
                }
                else
                {
                    user = new M_USER();
                    user.NAME = string.Empty;
                    user.PHONE_NUMBER = loginEntity.phoneNumber;
                    user.MODE_DEFAULT = (int)Mode.Partner;
                    user.REG_DATETIME = Utility.GetSysDateTime();
                    user.MODE_USER = (int)Mode.Partner;
                    user.SIGNIN_LAST = Utility.GetSysDateTime();
                    user.BLOCK_FLG = (int)BlockFlag.NotBlock;
                    user.DELETE_FLG = (int)DeleteFlag.Using;

                    if (!string.IsNullOrEmpty(loginEntity.languageType))
                    {
                        user.LANGUAGE_TYPE = loginEntity.languageType;
                    }
                    else
                    {
                        user.LANGUAGE_TYPE = Constant.LANGUAGE_VN;
                    }

                    _context.M_USERS.Add(user);
                    _context.SaveChanges();

                    var query = from d in _context.M_USERS
                                where d.PHONE_NUMBER == loginEntity.phoneNumber
                                    && d.DELETE_FLG == 0
                                select d;

                    M_PARTNER_INFO worker = new M_PARTNER_INFO();
                    worker.USER_ID = user.USER_ID;
                    worker.SCORE = 0;
                    worker.STATUS = (int)Status.Offline;
                    worker.REG_DATETIME = Utility.GetSysDateTime();
                    _context.M_PARTNER_INFOS.Add(worker);
                    _context.SaveChanges();

                    M_HIRER_INFO hirer = new M_HIRER_INFO();
                    hirer.USER_ID = user.USER_ID;
                    hirer.SCORE = 0;
                    hirer.STATUS = (int)Status.Offline;
                    hirer.REG_DATETIME = Utility.GetSysDateTime();
                    _context.M_HIRER_INFOS.Add(hirer);
                    _context.SaveChanges();
                }

                _context.SaveChanges();

                // Commit transaction.
                tran.Commit();

                var queryUser = from d in _context.M_USERS
                                where d.PHONE_NUMBER == loginEntity.phoneNumber
                                    && d.DELETE_FLG == 0
                                select d;

                user = queryUser.Single();

                string token = Utility.GenerateToken(user, Utility.GetSysDateTime());
                AuthRepository auth = new AuthRepository();
                auth.UpdateToken(_context, user.USER_ID, token);

                if (loginEntity.modeUser == (int)Mode.Partner)
                {
                    PartnerEntity partnerEntity = Utility.GetPartnerInfo(_context, user.USER_ID);
                    partnerEntity.token = token;

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = string.Empty,
                        Data = partnerEntity
                    });
                }
                else
                {
                    UserEntity entity = new UserEntity();
                    entity.userId = user.USER_ID;
                    entity.phoneNumber = user.PHONE_NUMBER;
                    entity.modeDefault = user.MODE_DEFAULT;
                    entity.modeUser = user.MODE_USER;
                    entity.name = user.NAME;
                    ImageInfoEntity avatar = new ImageInfoEntity();
                    avatar.path = user.AVATAR;
                    entity.avatar = avatar;
                    entity.token = token;

                    M_HIRER_INFO hirer = _context.M_HIRER_INFOS.FirstOrDefault(x => x.USER_ID == entity.userId);

                    entity.userId = hirer.USER_ID;
                    entity.score = hirer.SCORE;
                    entity.likeNum = hirer.LIKE_NUM;

                    return Ok(new Result
                    {
                        Status = 200,
                        Message = string.Empty,
                        Data = entity
                    });
                }
            }
            catch (Exception ex)
            {
                // Rollback transaction.
                tran.Rollback();

                return Ok(new Result
                {
                    Status = 400,
                    Message = ex.Data.ToString(),
                    Data = null
                });
            }
        }

        [HttpPost("UpdateUserInfo")]
        public ActionResult UpdateUserInfo([FromBody] UserEntity userEntity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.M_USERS
                            where d.USER_ID == userEntity.userId
                            && d.DELETE_FLG == 0
                            && d.BLOCK_FLG == 0
                            select d;

                if (query.Any())
                {
                    M_USER user = query.Single();
                    user.NAME = userEntity.name;

                    _context.SaveChanges();

                    var query1 = from d in _context.M_PARTNER_INFOS
                                 where d.USER_ID == userEntity.userId
                                 select d;

                    if (userEntity.modeUser == (int)Mode.Partner)
                    {
                        M_PARTNER_INFO worker = query1.Single();
                        worker.USER_ID = query.FirstOrDefault().USER_ID;
                        worker.SCORE = 0;
                        worker.STATUS = (int)Status.Offline;
                        worker.REG_DATETIME = Utility.GetSysDateTime();
                        _context.SaveChanges();
                    }
                    else
                    {
                        var query2 = from d in _context.M_HIRER_INFOS
                                     where d.USER_ID == userEntity.userId
                                     select d;

                        M_HIRER_INFO hirer = query2.Single();
                        hirer.USER_ID = query.FirstOrDefault().USER_ID;
                        hirer.SCORE = 0;
                        hirer.STATUS = (int)Status.Offline;
                        hirer.REG_DATETIME = Utility.GetSysDateTime();
                        _context.SaveChanges();

                    }

                    var queryUser = from d in _context.M_USERS
                                    where d.USER_ID == userEntity.userId
                                        && d.DELETE_FLG == 0
                                    select d;

                    user = queryUser.Single();

                    string token = Utility.GenerateToken(user, Utility.GetSysDateTime());
                    AuthRepository auth = new AuthRepository();
                    auth.UpdateToken(_context, user.USER_ID, token);

                    if (user.MODE_USER == (int)Mode.Partner)
                    {
                        PartnerEntity partnerEntity = Utility.GetPartnerInfo(_context, user.USER_ID);
                        partnerEntity.token = token;

                        return Ok(new Result
                        {
                            Status = 200,
                            Message = string.Empty,
                            Data = partnerEntity
                        });
                    }
                    else
                    {
                        UserEntity entity = new UserEntity();
                        entity.userId = user.USER_ID;
                        entity.phoneNumber = user.PHONE_NUMBER;
                        entity.modeDefault = user.MODE_DEFAULT;
                        entity.modeUser = user.MODE_USER;
                        entity.name = user.NAME;
                        ImageInfoEntity avatar = new ImageInfoEntity();
                        avatar.path = user.AVATAR;
                        entity.avatar = avatar;
                        entity.token = token;

                        M_HIRER_INFO hirer = _context.M_HIRER_INFOS.FirstOrDefault(x => x.USER_ID == entity.userId);

                        entity.userId = hirer.USER_ID;
                        entity.score = hirer.SCORE;
                        entity.likeNum = hirer.LIKE_NUM;

                        return Ok(new Result
                        {
                            Status = 200,
                            Message = string.Empty,
                            Data = entity
                        });
                    }
                }
                else
                {
                    return Ok(new Result
                    {
                        Status = 404,
                        Message = string.Empty,
                        Data = null
                    });
                }
            }
            catch (Exception ex)
            {
                // Rollback transaction.
                tran.Rollback();
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Error system!",
                    Data = null
                });
            }
        }

        /// <summary>
        /// Check phone exists.
        /// </summary>
        /// <param name="id">Phone number</param>
        /// <returns>Exists: True; Not exists: False</returns>
        [HttpGet("CheckPhoneExists")]
        public bool CheckPhoneExists(string id)
        {
            return Utility.CheckPhoneExists(_context, id);
        }

        /// <summary>
        /// Update mode user.
        /// </summary>
        /// <param name="entity">LoginEntity</param>
        /// <returns>Ok: True; Fail: False</returns>
        [HttpPost("UpdateModeUser")]
        public bool UpdateModeUser(decimal id, decimal Mode)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.M_USERS
                            where d.USER_ID == id
                            select d;

                if (query.Any())
                {
                    M_USER user = query.Single();
                    user.MODE_DEFAULT = Mode;
                    _context.SaveChanges();
                }

                // Commit transaction.
                tran.Commit();

                return true;
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Check can login.
        /// </summary>
        /// <param name="id">Phone number</param>
        /// <returns>True : 200; Blocked : 406; Deleted: 409</returns>
        [HttpGet("CheckCanLogin")]
        public ActionResult CheckCanLogin(string id)
        {
            try
            {
                System.Web.Http.HttpError errorHttp = new System.Web.Http.HttpError("User can login!");

                var query = from d in _context.M_USERS
                            where d.PHONE_NUMBER == id
                             && d.DELETE_FLG == 0
                            select d;

                if (query.Any())
                {
                    M_USER user = query.Single();

                    // User is blocked.
                    if (user.BLOCK_FLG == 1 && user.BLOCK_FLG == (int)BlockFlag.Blocked)
                    {
                        errorHttp = new System.Web.Http.HttpError("User is blocked!");

                        return Ok(new Result
                        {
                            Status = 404,
                            Message = errorHttp.Message,
                            Data = null
                        });
                    }

                    // User is deleted.
                    //if (user.DELETE_FLG.HasValue && user.DELETE_FLG.Value != (int)DeleteFlag.Using)
                    //{
                    //    errorHttp = new HttpError("User is deleted!");
                    //    return Request.CreateResponse(HttpStatusCode.Conflict, errorHttp);
                    //}
                }

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
                System.Web.Http.HttpError error = new System.Web.Http.HttpError("Error system!");
                return Ok(new Result
                {
                    Status = 200,
                    Message = error.Message,
                    Data = null
                });
            }
        }

        /// <summary>
        /// Get user.
        /// </summary>
        /// <param name="id">User ID</param>
        /// <param name="language">Language type</param>
        /// <returns>UserEntity</returns>
        [HttpGet("GetUser/{id}/{language}")]
        public ActionResult GetUser(decimal id, string language)
        {
            if (!Utility.CheckUserExists(_context, id))
            {
                return Ok(new Result
                {
                    Status = 404,
                    Message = "Data not exists",
                    Data = null
                });
            }

            if (string.IsNullOrEmpty(language))
            {
                language = Constant.LANGUAGE_VN;
            }

            M_USER user = _context.M_USERS.FirstOrDefault(x => x.USER_ID == id);
            UserEntity entity = new UserEntity();

            entity.userId = user.USER_ID;
            entity.phoneNumber = user.PHONE_NUMBER;
            entity.modeDefault = user.MODE_DEFAULT;
            entity.modeUser = user.MODE_USER;
            entity.name = user.NAME;
            ImageInfoEntity avatar = new ImageInfoEntity();
            avatar.path = user.AVATAR;
            entity.avatar = avatar;

            M_HIRER_INFO hirer = _context.M_HIRER_INFOS.FirstOrDefault(x => x.USER_ID == id);

            entity.userId = hirer.USER_ID;
            entity.score = hirer.SCORE;
            entity.likeNum = hirer.LIKE_NUM;

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = entity
            });
        }

        /// <summary>
        /// Get user list.
        /// </summary>
        /// <returns>List UserEntity.</returns>
        [HttpGet("GetAllUser")]
        public ActionResult GetAllUser()
        {
            List<M_USER> userList = _context.M_USERS.ToList();
            List<User> list = new List<User>();

            foreach (M_USER user in userList)
            {
                User entity = new User();
                entity.userId = user.USER_ID;
                entity.phoneNumber = user.PHONE_NUMBER;
                entity.name = user.NAME;

                list.Add(entity);
            }

            return Ok(new Result
            {
                Status = 200,
                Message = string.Empty,
                Data = list
            });
        }

        /// <summary>
        /// Update user Avatar.
        /// </summary>
        /// <param name="userEntity">User Entity.</param>
        /// <returns>Ok: True; Fail: False</returns>
        [HttpPost("UpdateAvatar")]
        public bool UpdateAvatar(UserEntity userEntity)
        {
            IDbContextTransaction tran = _context.Database.BeginTransaction();

            try
            {
                var query = from d in _context.M_USERS
                            where d.PHONE_NUMBER == userEntity.phoneNumber
                              && d.DELETE_FLG == 0
                            select d;

                if (!query.Any())
                {
                    return false;
                }

                M_USER user = query.Single();

                if (userEntity.avatar != null && !string.IsNullOrEmpty(userEntity.avatar.path))
                {
                    string avartaPath;

                    if (Utility.UploadAvatar(_context, userEntity.userId, userEntity.avatar, userEntity.avatar.path, out avartaPath))
                    {
                        user.AVATAR = avartaPath;
                    }
                }

                // Commit transaction.
                tran.Commit();

                return true;
            }
            catch
            {
                // Rollback transaction.
                tran.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Update user Avatar.
        /// </summary>
        /// <param name="userEntity">User Entity.</param>
        /// <returns>Ok: True; Fail: False</returns>
        [HttpPost("UploadImage")]
        public ActionResult UploadImage()
        {
            try
            {
                List<ImageInfoEntity> pathList = new List<ImageInfoEntity>();
                string avartaPath = string.Empty;
                var files = Request.Form.Files;

                if (files != null && files != null && files.Count > 0)
                {
                    foreach (IFormFile file in files)
                    {
                        if (Utility.UploadImage(file, out avartaPath))
                        {
                            ImageInfoEntity image = new ImageInfoEntity();
                            image.path = avartaPath;
                            pathList.Add(image);
                        }
                    }
                }

                return Ok(new Result
                {
                    Status = 200,
                    Message = string.Empty,
                    Data = pathList
                });
            }
            catch
            {
                // Rollback transaction.
            }

            return Ok(new Result
            {
                Status = 404,
                Message = string.Empty,
                Data = null
            });
        }
    }
}