using Aloai.Auth;
using Aloai.Entity;
using Aloai.Enum;
using Aloai.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Aloai
{
    public static class Utility
    {
        /// <summary>
        /// システム日時取得
        /// </summary>
        /// <returns>システム日時</returns>
        public static DateTime GetSysDateTime()
        {
            string sql = "SELECT GETDATE()";

            return DateTime.Parse(SqlHelper.ExecuteScalar(sql).ToString());
        }

        /// <summary>
        /// Check login exists.
        /// </summary>
        /// <param name="loginEntity">Login entity</param>
        /// <returns>Exists: true, Not Exists: fale</returns>
        public static bool CheckLoginExists(LoginEntity loginEntity)
        {
            //PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

            //var query = from d in db.M_LOGIN
            //        where d.PHONE_NUMBER == loginEntity.PhoneNumber
            //        select d;

            //if (query.Any())
            //{
            //    return true;
            //}

            return false;
        }

        /// <summary>
        /// Check login exists.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Exists: true, Not Exists: fale</returns>
        public static bool CheckLoginExists(string phoneNumber)
        {
            // PartTimeDataClassesDataContext db = new PartTimeDataClassesDataContext();

            // var query = from d in db.M_LOGIN
            //             where d.PHONE_NUMBER == phoneNumber
            //             select d;

            // if (query.Any())
            // {
            //     return true;
            // }

            return false;
        }

        /// <summary>
        /// Validati Token Id
        /// </summary>
        /// <param name="author">Author entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool ValidatiTokenId(AuthorEntity author)
        {
            // TODO
            //AuthorUtility.Author auth = new AuthorUtility.Author();

            //bool result = auth.ValidatiTokenId(author.Token, author.PhoneNumber).Result;

            //return result;

            return true;
        }

        /// <summary>
        /// JSONにフォーマットされたトークンを返す
        /// </summary>
        /// <param name="user"></param>
        /// <param name="expires"></param>
        /// <returns>JSON型のトークン</returns>
        public static string GenerateToken(M_USER user, DateTime expires)
        {
            var handler = new JwtSecurityTokenHandler();

            // ClaimsIdentityは識別情報を格納
            ClaimsIdentity identity = new ClaimsIdentity(
                new[] {
                    new Claim("UserID", user.USER_ID.ToString()),
                    new Claim("UserPhone", user.PHONE_NUMBER),
                    new Claim("UserName", user.NAME),
                }
            );

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires
            });

            return handler.WriteToken(securityToken);
        }

        /// <summary>
        /// Get define entity
        /// </summary>
        /// <param name="controlName">Control name</param>
        /// <returns>Define entity</returns>
        public static DefineEntity GetDefineValue(string controlName)
        {
            string sql = "SELECT * FROM M_DEFINE WHERE CONTROL_NAME = '" + controlName + "'";

            DataTable data = SqlHelper.FillData(sql);
            DefineEntity entity = new DefineEntity();

            if (data.Rows.Count > 0)
            {
                entity.controlName = controlName;
                entity.dataType = decimal.Parse(data.Rows[0]["DATA_TYPE"].ToString());
                entity.value = data.Rows[0]["VALUE"].ToString();
                entity.memo = data.Rows[0]["MEMO"].ToString();
            }

            return entity;
        }

        /// <summary>
        /// Update user/ company infomation.
        /// </summary>
        /// <param name="db">DataContext.</param>
        /// <param name="userEntity">User entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool UpdateUser(AloaiDataContext db, UserEntity userEntity)
        {
            var query = from d in db.M_USERS
                        where d.USER_ID == userEntity.userId
                        select d;

            M_USER user = query.Single();

            user.PHONE_NUMBER = userEntity.phoneNumber;
            user.NAME = userEntity.name;

            if (userEntity.avatar != null)
            {
                string avartaPath = string.Empty;

                if (Utility.UploadAvatar(db, userEntity.userId, userEntity.avatar, userEntity.avatar.path, out avartaPath))
                {

                    user.AVATAR = avartaPath;
                }
            }

            user.MODE_DEFAULT = userEntity.modeDefault;
            user.UPD_DATETIME = Utility.GetSysDateTime();

            List<T_PARTNER_CATALOG_UNIT> workerList = new List<T_PARTNER_CATALOG_UNIT>();

            db.SaveChanges();

            return true;
        }

        /// <summary>
        /// Update score
        /// </summary>
        /// <param name="db">PartTimeDataClassesDataContext</param>
        /// <param name="exchangeId">Exchange Id</param>
        /// <param name="score">Score</param>
        /// <param name="modeUserEstimation">Mode user estimation</param>
        /// <returns>OK: True; Fail: False</returns>
        public static bool UpdateScore(AloaiDataContext db, decimal contactId, decimal score, decimal modeUserEstimation)
        {
            int count = 0;
            decimal scoreAll = 0;

            try
            {
                if (modeUserEstimation == (decimal)Mode.Hirer)
                {
                    var queryWorker = from d in db.V_CONTACT_INFOS
                                      where d.CONTACT_ID == contactId
                                      select d.WORKER_ID;

                    if (!queryWorker.Any())
                    {
                        return false;
                    }

                    var query = from d in db.D_REVIEWS
                                join c in db.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                                where c.WORKER_ID == queryWorker.Single()
                                   && d.REVIEW_MODE_USER == (decimal)Mode.Hirer
                                select d;

                    if (query.Any())
                    {
                        count = query.ToList().Count();

                        var queryScore = (from d in db.D_REVIEWS
                                          join c in db.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                                          where c.WORKER_ID == queryWorker.Single()
                                             && d.REVIEW_MODE_USER == (decimal)Mode.Hirer
                                          select d.SCORE).Sum();

                        scoreAll = queryScore;
                    }

                    var worker = from d in db.M_PARTNER_INFOS
                                 where d.USER_ID == queryWorker.Single()
                                 select d;

                    // Update if exists.
                    if (worker.Any())
                    {
                        M_PARTNER_INFO info = worker.Single();

                        decimal totalScore = scoreAll + score;
                        info.SCORE = Math.Round(totalScore / (count + 1), 1);
                        info.UPD_DATETIME = GetSysDateTime();
                    }
                    else
                    {
                        M_PARTNER_INFO info = new M_PARTNER_INFO();
                        info.USER_ID = queryWorker.Single();
                        decimal totalScore = scoreAll + score;
                        info.SCORE = Math.Round(totalScore / (count + 1), 1);
                        info.REG_DATETIME = GetSysDateTime();

                        db.M_PARTNER_INFOS.Add(info);
                    }
                }
                else
                {
                    var queryHirer = from d in db.V_CONTACT_INFOS
                                     where d.CONTACT_ID == contactId
                                     select d.HIRER_ID;

                    if (!queryHirer.Any())
                    {
                        return false;
                    }

                    var query = from d in db.D_REVIEWS
                                join c in db.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                                where c.HIRER_ID == queryHirer.Single()
                                   && d.REVIEW_MODE_USER == (decimal)Mode.Partner
                                select d;

                    if (query.Any())
                    {
                        count = query.ToList().Count();

                        var queryScore = (from d in db.D_REVIEWS
                                          join c in db.V_CONTACT_INFOS on d.CONTACT_ID equals c.CONTACT_ID
                                          where c.HIRER_ID == queryHirer.Single()
                                             && d.REVIEW_MODE_USER == (decimal)Mode.Partner
                                          select d.SCORE).Sum();

                        scoreAll = queryScore;
                    }

                    var hirer = from d in db.M_HIRER_INFOS
                                where d.USER_ID == queryHirer.Single()
                                select d;

                    // Update if exists.
                    if (hirer.Any())
                    {
                        M_HIRER_INFO info = hirer.Single();

                        decimal totalScore = scoreAll + score;
                        info.SCORE = Math.Round(totalScore / (count + 1), 1);
                        info.UPD_DATETIME = GetSysDateTime();
                    }
                    else
                    {
                        M_HIRER_INFO info = new M_HIRER_INFO();
                        info.USER_ID = queryHirer.Single();
                        decimal totalScore = scoreAll + score;
                        info.SCORE = Math.Round(totalScore / (count + 1), 1);
                        info.REG_DATETIME = GetSysDateTime();

                        db.M_HIRER_INFOS.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Insert notify.
        /// </summary>
        /// <param name="db">PartTimeDataClassesDataContext</param>
        /// <param name="notifyType">Notify type</param>
        /// <param name="id">Object ID</param>
        /// <param name="userSendId">User send ID</param>
        /// <param name="userReceiveId">User receive ID</param>
        /// <param name="receiveModeUser">Receive mode user</param>
        /// <returns>Success: True; Fail: False</returns>
        public static bool InsertNotify(AloaiDataContext db, decimal notifyType, decimal id, decimal userSendId
            , decimal userReceiveId, decimal receiveModeUser)
        {
            try
            {
                string senderName = string.Empty;

                // Check User receive notify leaved company but job is exists.
                // If true is not receive notify.
                if (notifyType != (int)NotifyType.System)
                {
                    var queryUser = from d in db.M_USERS
                                    where d.USER_ID == userReceiveId
                                    select d;

                    if (!queryUser.Any())
                    {
                        return true;
                    }

                    var queryExchange = from d in db.V_CONTACT_INFOS
                                        where d.CONTACT_ID == id
                                        select d;

                    if (!queryExchange.Any())
                    {
                        return true;
                    }
                }

                D_NOTIFY notify = new D_NOTIFY();
                notify.NOTIFY_TYPE = notifyType;
                notify.OBJECT_ID = id;
                notify.USER_SEND_ID = userSendId;
                notify.USER_RECIEVE_ID = userReceiveId;
                notify.RECEIVE_MODE_USER = receiveModeUser;
                notify.READED_FLG = (int)ReadedFlg.New;
                notify.REG_DATETIME = GetSysDateTime();
                notify.NOTIFY_DATE = GetSysDateTime();
                notify.CONTENT = string.Empty;

                var query = from d in db.M_USERS
                            where d.USER_ID == userSendId
                            select d;

                if (!query.Any())
                {
                    return false;
                }

                M_USER user = query.Single();

                notify.CONTENT = string.IsNullOrEmpty(user.NAME) ? user.PHONE_NUMBER : user.NAME;

                db.D_NOTIFYS.Add(notify);
                db.SaveChanges();
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get message info.
        /// </summary>
        /// <param name="languageType">Language type</param>
        /// <param name="cd">Message cd</param>
        /// <returns></returns>
        public static MessageEntity GetMessageInfo(AloaiDataContext db, string languageType, decimal cd)
        {
            languageType = languageType.Trim();

            var query = from d in db.M_MESSAGES
                        where d.LANGUAGE_TYPE.Equals(languageType)
                            && d.MESSAGE_CD == cd
                        select d;

            MessageEntity entity = new MessageEntity();

            if (query.Any())
            {
                M_MESSAGE mess = query.Single();
                entity.messageId = mess.MESSAGE_ID;
                entity.messageCd = mess.MESSAGE_CD;
                entity.manguageType = mess.LANGUAGE_TYPE;
                entity.messageContent = mess.MESSAGE_CONTENT;
            }

            return entity;
        }

        /// <summary>
        /// Get system message info.
        /// </summary>
        /// <param name="languageType">Language type</param>
        /// <param name="cd">Message cd</param>
        /// <returns></returns>
        public static SystemMessageEntity GetSystemMessageInfo(AloaiDataContext db, string languageType, decimal cd)
        {
            var query = from d in db.M_SYSTEM_MESSAGES
                        where d.LANGUAGE_TYPE.Equals(languageType)
                            && d.MESSAGE_CD == cd
                        select d;

            SystemMessageEntity entity = new SystemMessageEntity();

            if (query.Any())
            {
                M_SYSTEM_MESSAGE mess = query.Single();
                entity.messageId = mess.MESSAGE_ID;
                entity.messageCd = mess.MESSAGE_CD;
                entity.languageType = mess.LANGUAGE_TYPE;
                entity.message = mess.MESSAGE;
                entity.messageContent = mess.MESSAGE_CONTENT;
            }

            return entity;
        }

        /// <summary>
        /// Get user info/ company info.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>user info/ company info.</returns>
        public static UserEntity GetUserInfo(AloaiDataContext db, decimal? userId)
        {
            UserEntity userInfo = new UserEntity();

            if (!userId.HasValue)
            {
                return userInfo;
            }

            var user = from d in db.M_USERS
                       where d.USER_ID == userId.Value
                       select d;

            if (!user.Any())
            {
                return userInfo;
            }

            M_USER mUser = user.Single();
            userInfo.name = mUser.NAME;

            ImageInfoEntity avatar = new ImageInfoEntity();
            avatar.path = mUser.AVATAR;
            userInfo.avatar = avatar;

            return userInfo;
        }

        /// <summary>
        /// Get hirer/ company info.
        /// </summary>
        /// <param name="hirerId">Hirer Id.</param>
        /// <returns>Hirer Info Entity</returns>
        public static HirerInfoEntity GetHirerInfo(AloaiDataContext db, decimal hirerId)
        {
            HirerInfoEntity hirerEntity = new HirerInfoEntity();

            // User info.
            var user = from d in db.M_USERS
                       where d.USER_ID == hirerId
                       select d;

            if (user.Any())
            {
                M_USER userEntity = user.Single();
                // Hirer info.
                var hirerInfo = from d in db.M_HIRER_INFOS
                                where d.USER_ID == hirerId
                                select d;

                if (hirerInfo.Any())
                {
                    M_HIRER_INFO info = hirerInfo.Single();

                    hirerEntity.userId = info.USER_ID;
                    hirerEntity.score = info.SCORE.Value;
                    hirerEntity.likeNum = info.LIKE_NUM;
                    hirerEntity.status = info.STATUS;

                    ImageInfoEntity avatar = new ImageInfoEntity();
                    avatar.path = userEntity.AVATAR;
                    hirerEntity.avatar = avatar;

                    hirerEntity.name = userEntity.NAME;
                    hirerEntity.phoneNumber = userEntity.PHONE_NUMBER;
                    hirerEntity.token = userEntity.TOKEN;
                }
            }

            return hirerEntity;
        }

        /// <summary>
        /// Get worker/ company info.
        /// </summary>
        /// <param name="workerId">Worker Id.</param>
        /// <returns>Worker Info Entity</returns>
        public static PartnerInfoEntity GetWorkerInfo(AloaiDataContext db, decimal workerId)
        {
            PartnerInfoEntity workerEntity = new PartnerInfoEntity();

            // User info.
            var user = from d in db.M_USERS
                       where d.USER_ID == workerId
                       select d;

            if (user.Any())
            {
                // Worker info.
                var workerInfo = from d in db.M_PARTNER_INFOS
                                 where d.USER_ID == workerId
                                 select d;

                if (workerInfo.Any())
                {
                    M_PARTNER_INFO info = workerInfo.Single();

                    workerEntity.userId = info.USER_ID;
                    workerEntity.score = info.SCORE.Value;
                    workerEntity.introduce = info.INTRODUCE;
                    workerEntity.fitLocationFlg = info.FIX_LOCATION_FLG;

                    Location loc = new Location();
                    loc.longitude = info.LONGITUDE;
                    loc.latitude = info.LATITUDE;
                    loc.address = info.ADDRESS;

                    workerEntity.location = loc;
                    workerEntity.verifyFlg = info.VERIFY_FLG;
                    workerEntity.verifyDate = info.VERIFY_DATE;
                    workerEntity.verifyDateFrom = info.VERIFY_DATE_FROM;
                    workerEntity.verifyDateTo = info.VERIFY_DATE_TO;
                    workerEntity.likeNum = info.LIKE_NUM;
                    workerEntity.status = info.STATUS;
                    workerEntity.regDatetime = info.REG_DATETIME;
                    workerEntity.updDatetime = info.UPD_DATETIME;
                }
            }

            return workerEntity;
        }

        /// <summary>
        /// Get worker catalog list.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>List WorkerEntity</returns>
        public static List<PartnerCatalogEntity> GetWorkerList(AloaiDataContext db, decimal userId)
        {
            List<PartnerCatalogEntity> workerEntityList = new List<PartnerCatalogEntity>();

            var user = from d in db.M_USERS
                       where d.USER_ID == userId
                       select d;

            if (!user.Any())
            {
                return workerEntityList;
            }

            var query3 = from d in db.V_PARTNERS
                         where d.USER_ID == userId
                         select d;

            if (query3.Any())
            {
                foreach (V_PARTNER worker in query3.ToList())
                {
                    PartnerCatalogEntity wkEntity = new PartnerCatalogEntity();
                    wkEntity.userId = worker.USER_ID;

                    Catalog catalog = new Catalog();
                    catalog.catalogCd = worker.CATALOG_CD;
                    wkEntity.cost = worker.COST;

                    Unit unit = new Unit();
                    unit.unitCd = worker.UNIT_CD;

                    string languageType = user.Single().LANGUAGE_TYPE;

                    if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                    {
                        catalog.catalogName = worker.CATALOG_NAME;
                        unit.unitName = worker.UNIT_NAME;
                    }
                    else
                    {
                        catalog.catalogName = worker.CATALOG_NAME_EN;
                        unit.unitName = worker.UNIT_NAME_EN;
                    }

                    wkEntity.catalog = catalog;
                    wkEntity.unit = unit;
                    workerEntityList.Add(wkEntity);
                }
            }

            return workerEntityList;
        }

        /// <summary>
        /// Get user name/ company name.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Name</returns>
        public static string GetUserName(AloaiDataContext db, decimal userId)
        {
            String name = string.Empty;

            var user = from d in db.M_USERS
                       where d.USER_ID == userId
                       select d;

            if (!user.Any())
            {
                return name;
            }

            name = user.Single().NAME;

            return name;
        }

        /// <summary>
        /// Get user avatar/ company logo.
        /// </summary>
        /// <param name="userId">User Id.</param>
        /// <returns>Avatar</returns>
        public static string GetUserAvatar(AloaiDataContext db, decimal userId)
        {
            String avatar = string.Empty;

            var user = from d in db.M_USERS
                       where d.USER_ID == userId
                       select d;

            if (!user.Any())
            {
                return avatar;
            }

            avatar = user.Single().AVATAR;

            return avatar;
        }

        /// <summary>
        /// Get catalog name.
        /// </summary>
        /// <param name="languageType">Language type</param>
        /// <param name="cd">Catalog cd</param>
        /// <returns></returns>
        public static string GetCatalogName(AloaiDataContext db, string languageType, decimal? cd)
        {
            string name = string.Empty;

            var query = from d in db.M_CATALOGS
                        where d.CATALOG_CD == cd
                        select d;

            if (query.Any())
            {
                M_CATALOG catalog = query.Single();

                if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                {
                    name = catalog.CATALOG_NAME;
                }
                else
                {
                    name = catalog.CATALOG_NAME_EN;
                }
            }

            return name;
        }

        /// <summary>
        /// Get unit name.
        /// </summary>
        /// <param name="languageType">Language type</param>
        /// <param name="cd">Unit cd</param>
        /// <returns></returns>
        public static string GetUnitName(AloaiDataContext db, string languageType, decimal? cd)
        {
            string name = string.Empty;

            var query = from d in db.M_UNITS
                        where d.UNIT_CD == cd
                        select d;

            if (query.Any())
            {
                M_UNIT unit = query.Single();

                if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                {
                    name = unit.UNIT_NAME;
                }
                else
                {
                    name = unit.UNIT_NAME_EN;
                }
            }

            return name;
        }

        /// <summary>
        /// Check user exists.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Exists: true, Not Exists: fale</returns>
        public static bool CheckUserExists(AloaiDataContext db, decimal userId)
        {
            var query = from d in db.M_USERS
                        where d.USER_ID == userId
                        select d;

            if (query.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check Phone exists.
        /// </summary>
        /// <param name="phoneNumber">Phone number</param>
        /// <returns>Exists: true, Not Exists: false</returns>
        public static bool CheckPhoneExists(AloaiDataContext db, string phoneNumber)
        {
            var query = from d in db.M_USERS
                        where d.PHONE_NUMBER == phoneNumber
                         && d.DELETE_FLG == 0
                        select d;

            if (query.Any())
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get image info list.
        /// </summary>
        /// <param name="db">DataContext</param>
        /// <param name="objectId">Object ID</param>
        /// <param name="objectType">Object type</param>
        /// <returns>List image info entity</returns>
        public static List<ImageInfoEntity> GetImageList(AloaiDataContext db, decimal objectId, ImageType objectType)
        {
            List<ImageInfoEntity> imageInfoEntity = new List<ImageInfoEntity>();


            var query = from d in db.M_IMAGE_DETAILS
                        where d.OBJECT_ID == objectId
                          && d.OBJECT_TYPE == (int)objectType
                        select d;

            // Delete image not used.
            if (query.Any())
            {
                List<M_IMAGE_DETAIL> list = query.ToList();

                foreach (M_IMAGE_DETAIL del in list)
                {
                    ImageInfoEntity entity = new ImageInfoEntity();
                    //entity.image = del.IMAGE_NAME;
                    entity.path = del.IMAGE_PATH;

                    imageInfoEntity.Add(entity);
                }
            }

            return imageInfoEntity;
        }

        /// <summary>
        /// Upload image to hosting.
        /// </summary>
        /// <param name="db">DataContext</param>
        /// <param name="objectId">Object ID</param>
        /// <param name="objectType">Object type</param>
        /// <param name="imageInfoEntity">Image info entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool UploadImage(AloaiDataContext db, decimal objectId, ImageType objectType, List<ImageInfoEntity> imageInfoEntity)
        {
            if (imageInfoEntity == null || imageInfoEntity.Count == 0)
            {
                if (!RemoveImage(db, objectId, objectType))
                {
                    return false;
                }

                return true;
            }

            int index = 0;

            var delQuery = from d in db.M_IMAGE_DETAILS
                           where d.OBJECT_ID == objectId
                             && d.OBJECT_TYPE == (int)objectType
                           select d;

            // Delete image not used.
            if (delQuery.Any())
            {
                List<M_IMAGE_DETAIL> list = delQuery.ToList();

                foreach (M_IMAGE_DETAIL del in list)
                {
                    var exists = from d in imageInfoEntity
                                 where Path.GetFileName(d.path.Trim('\"')) == del.IMAGE_NAME
                                 select d;

                    if (!exists.Any())
                    {
                        string path = $"Upload/";
                        string fileName = Path.GetFileName(del.IMAGE_PATH);

                        string directoryName = AppDomain.CurrentDomain.BaseDirectory + path;
                        string ext = Path.GetExtension(del.IMAGE_PATH);

                        fileName = System.IO.Path.Combine(directoryName, fileName) + ext;

                        // Check if file exists with its full path    
                        if (File.Exists(fileName))
                        {
                            // If file found, delete it.
                            File.Delete(fileName);
                        }
                    }

                    db.M_IMAGE_DETAILS.Remove(del);
                }
            }

            foreach (ImageInfoEntity imageInfo in imageInfoEntity)
            {
                if (!string.IsNullOrEmpty(imageInfo.path)) continue;

                index++;
                var thisFileName = Path.GetFileName(imageInfo.path.Trim('\"'));

                M_IMAGE_DETAIL image = new M_IMAGE_DETAIL();
                image.OBJECT_ID = objectId;
                image.OBJECT_TYPE = (int)objectType;
                image.ROW_NO = index;
                image.IMAGE_NAME = Path.GetFileName(imageInfo.path);
                image.IMAGE_PATH = imageInfo.path;

                db.M_IMAGE_DETAILS.Add(image);
            }

            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Remove image in hosting.
        /// </summary>
        /// <param name="db">DataContext</param>
        /// <param name="objectId">Object ID</param>
        /// <param name="objectType">Object type</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool RemoveImage(AloaiDataContext db, decimal objectId, ImageType objectType)
        {
            var delQuery = from d in db.M_IMAGE_DETAILS
                           where d.OBJECT_ID == objectId
                             && d.OBJECT_TYPE == (int)objectType
                           select d;

            // Delete image not used.
            if (delQuery.Any())
            {
                List<M_IMAGE_DETAIL> list = delQuery.ToList();

                foreach (M_IMAGE_DETAIL del in list)
                {
                    string path = $"Upload/";
                    string fileName = Path.GetFileName(del.IMAGE_PATH);

                    string directoryName = AppDomain.CurrentDomain.BaseDirectory + path;
                    string ext = Path.GetExtension(del.IMAGE_PATH);

                    fileName = System.IO.Path.Combine(directoryName, fileName) + ext;

                    // Check if file exists with its full path    
                    if (File.Exists(fileName))
                    {
                        // If file found, delete it.
                        File.Delete(fileName);
                    }

                    db.M_IMAGE_DETAILS.Remove(del);
                }
            }

            return true;
        }

        /// <summary>
        /// Create image name.
        /// </summary>
        /// <returns>Image name</returns>
        public static string CreateImageName()
        {
            string result = string.Empty;

            DateTime date = Utility.GetSysDateTime();

            result += Constant.KEY_UPLOAD;
            result += date.ToString(Constant.KEY_DATE);

            string list = Constant.KEY_LIST;
            Random ran = new Random();

            int lengh = ran.Next(10, 30);

            for (int i = 0; i < lengh; i++)
            {
                int index = ran.Next(0, list.Length - 1);
                string value = list.Substring(index, 1);
                result += value;
            }

            return result;
        }

        /// <summary>
        /// Check file exists.
        /// </summary>
        /// <param name="imageName">Image name</param>
        /// <returns>True: Exists, False: Not exists</returns>
        public static bool CheckExistFile(string imageName)
        {
            if (imageName.Length < Constant.KEY_UPLOAD.Length + Constant.KEY_DATE.Length + 10)
            {
                return false;
            }

            DateTime date = Utility.GetSysDateTime();

            string key = Constant.KEY_UPLOAD;

            string subStr = imageName.Substring(0, key.Length);

            // Header is not KEY_UPLOAD
            if (!key.Equals(subStr))
            {
                return false;
            }

            subStr = imageName.Substring(key.Length, Constant.KEY_DATE.Length);
            DateTime? dateTime = ConvertToDateTime(subStr);

            if (!dateTime.HasValue)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Convert to date time.
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Date time</returns>
        public static DateTime? ConvertToDateTime(string value)
        {
            DateTime? date = null;
            CultureInfo enUS = new CultureInfo("en-US");

            if (value.Length > 10)
            {
                DateTime datetime;

                if (DateTime.TryParseExact(value, Constant.KEY_DATE, enUS, DateTimeStyles.None, out datetime))
                {
                    date = datetime;
                }
            }

            return date;
        }

        public static System.Drawing.Bitmap Base64ToImage(string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }

        /// <summary>
        /// Convert image to byte array.
        /// </summary>
        /// <param name="imageIn">Image</param>
        /// <returns>Byte array</returns>
        public static byte[] ImageToByteArray(System.Drawing.Image imageIn, System.Drawing.Imaging.ImageFormat format)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, format);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Update user/ company infomation.
        /// </summary>
        /// <param name="db">DataContext.</param>
        /// <param name="userEntity">User entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool UpdatePartner(AloaiDataContext db, PartnerEntity partnerEntity)
        {
            var query = from d in db.M_USERS
                        where d.USER_ID == partnerEntity.userId
                        select d;

            M_USER user = query.Single();

            //user.PHONE_NUMBER = partnerEntity.PhoneNumber;
            user.NAME = partnerEntity.name;

            if (partnerEntity.avatar != null && !string.IsNullOrEmpty(partnerEntity.avatar.path))
            {
                string avartaPath;

                if (Utility.UploadAvatar(db, partnerEntity.userId, partnerEntity.avatar, partnerEntity.avatar.path, out avartaPath))
                {
                    user.AVATAR = avartaPath;
                }
            }

            user.UPD_DATETIME = Utility.GetSysDateTime();
            db.SaveChanges();

            var queryPartner = from d in db.M_PARTNER_INFOS
                               where d.USER_ID == partnerEntity.userId
                               select d;

            if (queryPartner.Any())
            {
                M_PARTNER_INFO partner = queryPartner.Single();
                partner.INTRODUCE = partnerEntity.introduce;
                partner.FIX_LOCATION_FLG = partnerEntity.fixLocationFlg ? 1 : 0;

                if (partnerEntity.fixLocationFlg)
                {
                    partner.LONGITUDE = partnerEntity.location.longitude;
                    partner.LATITUDE = partnerEntity.location.latitude;
                    partner.ADDRESS = partnerEntity.location.address;
                }
                else
                {
                    partner.LONGITUDE = string.Empty;
                    partner.LATITUDE = string.Empty;
                    partner.ADDRESS = string.Empty;
                }

                partner.UPD_DATETIME = Utility.GetSysDateTime();

                db.SaveChanges();
            }

            var queryDel = from d in db.T_PARTNER_CATALOG_UNITS
                           where d.USER_ID == partnerEntity.userId
                           select d;

            if (queryDel.Any())
            {
                db.T_PARTNER_CATALOG_UNITS.RemoveRange(queryDel.ToList());
                db.SaveChanges();
            }

            if (partnerEntity.partnerCatalog != null)
            {
                T_PARTNER_CATALOG_UNIT worker = new T_PARTNER_CATALOG_UNIT();

                worker.USER_ID = partnerEntity.userId;
                worker.CATALOG_CD = partnerEntity.partnerCatalog.catalog.catalogCd;
                worker.COST = partnerEntity.partnerCatalog.cost;
                worker.UNIT_CD = partnerEntity.partnerCatalog.unit.unitCd;

                worker.REG_DATETIME = Utility.GetSysDateTime();

                db.T_PARTNER_CATALOG_UNITS.Add(worker);
                db.SaveChanges();
            }

            UploadImage(db, partnerEntity.userId, ImageType.Profile, partnerEntity.imageInfoList);

            return true;
        }

        public static PartnerEntity GetPartnerInfo(AloaiDataContext db, decimal userId)
        {
            PartnerEntity partnerEntity = null;

            // User info.
            var user = from d in db.M_USERS
                       where d.USER_ID == userId
                       select d;

            if (user.Any())
            {
                partnerEntity = new PartnerEntity();
                M_USER userEntity = user.Single();

                partnerEntity.phoneNumber = userEntity.PHONE_NUMBER;
                partnerEntity.name = userEntity.NAME;

                ImageInfoEntity avatar = new ImageInfoEntity();
                avatar.path = userEntity.AVATAR;
                partnerEntity.avatar = avatar;

                // Hirer info.
                var hirerInfo = from d in db.M_PARTNER_INFOS
                                where d.USER_ID == userId
                                select d;

                if (hirerInfo.Any())
                {
                    M_PARTNER_INFO info = hirerInfo.Single();

                    partnerEntity.userId = info.USER_ID;
                    partnerEntity.introduce = info.INTRODUCE;
                    partnerEntity.fixLocationFlg = info.FIX_LOCATION_FLG == 1 ? true : false;

                    Location location = new Location();
                    location.longitude = info.LONGITUDE;
                    location.latitude = info.LATITUDE;
                    location.address = info.ADDRESS;
                    partnerEntity.location = location;
                    partnerEntity.verifyFlg = info.VERIFY_FLG == 1 ? true : false;
                    partnerEntity.likeNum = info.LIKE_NUM;
                    partnerEntity.score = info.SCORE;
                }

                var catalogUnit = from d in db.V_PARTNERS
                                  where d.USER_ID == userId
                                  select d;

                if (catalogUnit.Any())
                {
                    partnerEntity.partnerCatalog = new PartnerCatalogEntity();

                    foreach (V_PARTNER catalog in catalogUnit.ToList())
                    {
                        partnerEntity.partnerCatalog.userId = catalog.USER_ID;

                        Catalog cal = new Catalog();
                        cal.catalogCd = catalog.CATALOG_CD;
                        Unit unit = new Unit();
                        unit.unitCd = catalog.UNIT_CD;

                        string languageType = user.Single().LANGUAGE_TYPE;

                        if (string.IsNullOrEmpty(languageType) || languageType.Equals(Constant.LANGUAGE_VN))
                        {
                            cal.catalogName = catalog.CATALOG_NAME;
                            unit.unitName = catalog.UNIT_NAME;
                        }
                        else
                        {
                            cal.catalogName = catalog.CATALOG_NAME_EN;
                            unit.unitName = catalog.UNIT_NAME_EN;
                        }

                        partnerEntity.partnerCatalog.catalog = cal;
                        partnerEntity.partnerCatalog.unit = unit;
                        partnerEntity.partnerCatalog.cost = catalog.COST;
                        break;
                    }
                }

                partnerEntity.imageInfoList = GetImageList(db, partnerEntity.userId, ImageType.Profile);
            }

            return partnerEntity;
        }

        /// <summary>
        /// Upload image to hosting.
        /// </summary>
        /// <param name="db">DataContext</param>
        /// <param name="objectId">Object ID</param>
        /// <param name="objectType">Object type</param>
        /// <param name="imageInfoEntity">Image info entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool UploadAvatar(AloaiDataContext db, decimal objectId, ImageInfoEntity avatar, string imagePath, out string avartaPath)
        {
            ImageType objectType = ImageType.Avatar;
            avartaPath = avatar.path;

            if (avatar != null && !string.IsNullOrEmpty(avatar.path))
            {
                string path = $"Upload/";
                string fileName = Path.GetFileName(avatar.path);

                string directoryName = AppDomain.CurrentDomain.BaseDirectory + path;
                string ext = Path.GetExtension(avatar.path);

                fileName = System.IO.Path.Combine(directoryName, fileName) + ext;

                if (File.Exists(fileName))
                {
                    var delQuery = from d in db.M_IMAGE_DETAILS
                                   where d.OBJECT_ID == objectId
                                     && d.OBJECT_TYPE == (int)objectType
                                   select d;

                    // Delete image not used.
                    if (delQuery.Any())
                    {
                        M_IMAGE_DETAIL list = delQuery.Single();

                        if (avatar.path == list.IMAGE_PATH) return true;

                        // Check if file exists with its full path
                        if (File.Exists(list.IMAGE_PATH))
                        {
                            // If file found, delete it.
                            File.Delete(list.IMAGE_PATH);
                        }

                        db.M_IMAGE_DETAILS.Remove(list);
                    }

                    if (avatar.image != null)
                    {
                        var thisFileName = Path.GetFileName(avatar.path.Trim('\"'));

                        M_IMAGE_DETAIL image = new M_IMAGE_DETAIL();
                        image.OBJECT_ID = objectId;
                        image.OBJECT_TYPE = (int)objectType;
                        image.ROW_NO = 1;
                        image.IMAGE_NAME = Path.GetFileName(thisFileName);
                        image.IMAGE_PATH = avatar.path;
                        avartaPath = avatar.path;
                        db.M_IMAGE_DETAILS.Add(image);
                    }

                    db.SaveChanges();
                }
            }

            return true;
        }

        /// <summary>
        /// バイナリ データへのイメージの変換
        /// </summary>
        /// <param name="imagePath">イメージパス</param>
        /// <returns>バイナリ データ</returns>
        public static byte[] ImageToBinary(string imagePath)
        {
            FileStream fileStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);
            fileStream.Close();

            return buffer;
        }

        /// <summary>
        /// Upload image to hosting.
        /// </summary>
        /// <param name="db">DataContext</param>
        /// <param name="objectId">Object ID</param>
        /// <param name="objectType">Object type</param>
        /// <param name="imageInfoEntity">Image info entity</param>
        /// <returns>Ok: true, Fail: false</returns>
        public static bool UploadImage(IFormFile file, out string avartaPath)
        {
            avartaPath = string.Empty;

            if (file != null)
            {
                var thisFileName = Path.GetFileName(file.FileName.Trim('\"'));

                string filename = Utility.CreateImageName();

                //System.Drawing.Image imageFile = Utility.Base64ToImage(avatar);
                string directoryName = Utility.GetDefineValue(Constant.UPLOAD_PATH).value.ToString();

                string ext = Path.GetExtension(thisFileName);

                filename = System.IO.Path.Combine(directoryName, filename) + ext;

                //Deletion exists file  
                if (File.Exists(filename))
                {
                    File.Delete(filename);
                }

                if (Utility.UploadImage(file, filename))
                {
                    avartaPath = filename;
                }
            }

            return true;
        }

        /// <summary>
        /// Method to write file onto the disk
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static bool UploadImage(IFormFile file, string filename)
        {
            try
            {
                string path = $"Upload/";
                string fileName = Path.GetFileName(filename);

                string directoryName = AppDomain.CurrentDomain.BaseDirectory + path;

                fileName = System.IO.Path.Combine(directoryName, fileName);

                //Check if directory exist
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + path))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + path); //Create directory if it doesn't exist
                }

                using (var bits = new FileStream(fileName, FileMode.Create))
                {
                    file.CopyTo(bits);
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}