//-------------------------------------------------------------------------------------------
// <copyright>
//     Copyright (c) 2019 Aloai Company. All rights reserved.
// </copyright>
//-------------------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Aloai.Entity;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace Aloai
{
    /// <summary>
    /// Sql helper class.
    /// </summary>
    public sealed class SqlHelper
    {
        public SqlHelper()
        {
            connection = new SqlConnection(ConnectString);
        }

        public string _connectString;

        public string ConnectString
        {
            get
            {
                return _connectString;
            }
            set
            {
                _connectString = value;
                connection = new SqlConnection(_connectString);
            }
        }

        /// <summary>
        /// Connection string.
        /// </summary>
        public static SqlConnection connection;

        /// <summary>
        /// Execute sql.
        /// </summary>
        /// <param name="sqlString">Sql string.</param>
        /// <returns>Result</returns>
        public static object ExecuteScalar(string sqlString)
        {
            SqlCommand command = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                return command.ExecuteScalar();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// ExecuteNonQuery
        /// </summary>
        /// <param name="sqlString">Sql string.</param>
        /// <returns>OK : True; Fail: False</returns>
        public static bool EndExecuteNonQuery(string sqlString)
        {
            SqlCommand command = new SqlCommand(sqlString, connection);

            try
            {
                connection.Open();
                command.ExecuteScalar();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }
        }

        /// <summary>
        /// Fill data.
        /// </summary>
        /// <param name="sqlString">Sql string.</param>
        /// <returns>Datattable</returns>
        public static DataTable FillData(string sqlString)
        {
            SqlCommand command = new SqlCommand(sqlString, connection);
            SqlDataAdapter ad = new SqlDataAdapter(command);
            DataTable table = new DataTable();

            try
            {
                connection.Open();
                ad.Fill(table);
            }
            catch (Exception)
            {

            }
            finally
            {
                connection.Close();
            }

            return table;
        }
    }
}