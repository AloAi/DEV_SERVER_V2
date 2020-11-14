using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Aloai.Entity;
using Aloai.Models;

namespace Aloai.Auth
{
    public class AuthRepository : IDisposable
    {
        /// <summary>
        /// AuthRepository
        /// </summary>
        public AuthRepository()
        {
        }

        //Method used to Get User details will be used by
        //Authentication Provider class in Steps below
        /// <summary>
        /// Check token
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="token">Token string</param>
        /// <returns>Ok: true, Fail: false</returns>
        public bool CheckToken(AloaiDataContext context, string userId, string token)
        {
            var query = from d in context.M_USERS
                        where d.USER_ID == Convert.ToDecimal(userId)
                            && d.TOKEN == token
                        select d;

            if (!query.Any())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Update token
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <param name="token">Token string</param>
        /// <returns>Ok: true, Fail: false</returns>
        public bool UpdateToken(AloaiDataContext context, decimal userId, string token)
        {
            var query = from d in context.M_USERS
                        where d.USER_ID == userId
                        select d;

            if (query.Any())
            {
                M_USER user = query.Single();
                user.TOKEN = token;
                user.UPD_DATETIME = Utility.GetSysDateTime();
                context.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
        }
    }
}