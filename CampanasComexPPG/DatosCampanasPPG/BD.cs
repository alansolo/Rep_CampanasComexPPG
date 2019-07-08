using EntidadesCampanasPPG.BD;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtilidadesCampanasPPG;

namespace DatosCampanasPPG
{
    public class BD
    {
        private SqlConnection sqlConnection;
        private SqlCommand command;
        protected string connectionString;

        public BD()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MktCampanas"].ToString();
        }
        public BD(string dataBaseConnection)
        {
            connectionString = ConfigurationManager.ConnectionStrings[dataBaseConnection].ToString();
        }
        protected string ExecuteScalar(string spName, List<SqlParameter> parameters)
        {
            string result = string.Empty;

            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    foreach (SqlParameter item in parameters)
                        command.Parameters.Add(item);

                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    result = (string)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    //Write log error
                    ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: "+ ex.Source + " Error: " + ex.Message);
                    throw ex;
                }
            }

            return result;
        }
        protected int ExecuteNonQuery(string spName, List<SqlParameter> parameters)
        {
            int result = 0;

            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    foreach (SqlParameter item in parameters)
                        command.Parameters.Add(item);

                    command.Connection = sqlConnection;
                    sqlConnection.Open();
                    result = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    //Write log error
                    ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
                    throw ex;
                }
            }

            return result;
        }
        
        //protected int ExecuteNonQuery(string spName, List<SqlParameterGroup> parametersGroup)
        //{
        //    SqlTransaction transaction;
        //    int result = 0;

        //    using (sqlConnection = new SqlConnection(connectionString))
        //    {
        //        sqlConnection.Open();
        //        transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);

        //        foreach (SqlParameterGroup paramGroup in parametersGroup)
        //        {
        //            try
        //            {
        //                command = new SqlCommand();
        //                command.Connection = sqlConnection;
        //                command.Transaction = transaction;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = spName;

        //                foreach (SqlParameter item in paramGroup.Parameter)
        //                    command.Parameters.Add(item);

        //                result = command.ExecuteNonQuery();
        //            }
        //            catch (Exception ex)
        //            {
        //                result = 0;
        //                transaction.Rollback();

        //                //Write log error
        //                LogFile.WriteLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
        //                throw ex;
        //            }
        //        }

        //        transaction.Commit();
        //    }

        //    return result;
        //}
        protected DataTable ExecuteNonQueryDataTable(string spName, List<SqlParameter> parameters)
        {
            DataSet dataSet = null;
            DataTable dataTable = new DataTable();

            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;

                    foreach (SqlParameter item in parameters)
                        command.Parameters.Add(item);

                    command.Connection = sqlConnection;
                    sqlConnection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataSet = new DataSet("dsResult");
                    dataAdapter.Fill(dataSet);

                    if (dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    //Write log error
                    ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
                    throw ex;
                }
            }

            return dataTable;
        }
        protected DataTable ExecuteDataTable(string spName, List<SqlParameterGroup> parametersGroup)
        {
            DataSet dataSet = null;
            DataTable dataTable = new DataTable();
            DataTable dtTotal = new DataTable();
            SqlTransaction transaction;

            using (sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                transaction = sqlConnection.BeginTransaction(IsolationLevel.ReadCommitted);

                foreach (SqlParameterGroup paramGroup in parametersGroup)
                {
                    try
                    {
                        command = new SqlCommand();
                        command.Connection = sqlConnection;
                        command.Transaction = transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = spName;

                        foreach (SqlParameter item in paramGroup.ListSqlParameter)
                            command.Parameters.Add(item);

                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        dataSet = new DataSet("dsResult");
                        dataAdapter.Fill(dataSet);

                        if (dataSet.Tables.Count > 0)
                        {
                            dataTable = dataSet.Tables[0];

                            dtTotal.Merge(dataTable);
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        //Write log error
                        ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
                        throw ex;
                    }
                }

                transaction.Commit();
            }

            return dtTotal;
        }
        protected DataTable ExecuteDataTable(string spName, List<SqlParameter> parameters)
        {
            DataSet dataSet = null;
            DataTable dataTable = new DataTable();

            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    foreach (SqlParameter item in parameters)
                        command.Parameters.Add(item);

                    command.Connection = sqlConnection;
                    sqlConnection.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataSet = new DataSet("dsResult");
                    dataAdapter.Fill(dataSet);

                    if (dataSet.Tables.Count > 0)
                    {
                        dataTable = dataSet.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    //Write log error
                    ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
                    throw ex;
                }
            }

            return dataTable;
        }
        protected DataSet ExecuteDataSet(string spName, List<SqlParameter> parameters)
        {
            DataSet dataSet = null;

            using (sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    command = new SqlCommand();
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = spName;
                    foreach (SqlParameter item in parameters)
                        command.Parameters.Add(item);

                    command.Connection = sqlConnection;
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                    dataSet = new DataSet("dsResult");
                    dataAdapter.Fill(dataSet);
                }
                catch (Exception ex)
                {
                    //Write log error
                    ArchivoLog.EscribirLog(null, DateTime.Now.ToString("dd/MM/yyyy mm:ss") + ": Method: " + ex.Source + " Error: " + ex.Message);
                    throw ex;
                }
            }

            return dataSet;
        }

    }
}
