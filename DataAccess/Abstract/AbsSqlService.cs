using Entity.Security;
using Entity.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public abstract class AbsSqlService
    {
        SqlConnection connection;
        private readonly SqlDataProcess sqlDataProcess;
        protected AbsSqlService(string connectionString)
        {
            connection = new();
            //string a = SecurityProcess.GetInstance().Encrypt("Server=10.1.137.53;Database=MargAppDB;User ID=ETO;Password=");
            //connection.ConnectionString = SecurityProcess.Decrypt(connectionString);
            connection.ConnectionString = connectionString;
            sqlDataProcess = new SqlDataProcess();
        }

        SqlConnection OpenConnection()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            return connection;
        }

        void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        internal void CloseDataReader(SqlDataReader dataReader)
        {
            if (dataReader != null)
            {
                if (!dataReader.IsClosed)
                {
                    dataReader.Close();
                }
            }
        }

        public async Task<SqlCommand> ExecuteAsync(string commandText, params SqlParameter[] parameters) // update table set Age=15
        {
            using SqlCommand command = new SqlCommand();
            command.CommandText = commandText;
            command.Connection = OpenConnection();
            command.CommandType = CommandType.Text;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await command.ExecuteNonQueryAsync();
            CloseConnection();
            return command;
        }

        public async Task<SqlDataProcess> ReaderAsync(string commandText, params SqlParameter[] parameters) //select ....
        {
            using SqlCommand command = new SqlCommand();
            command.CommandText = commandText;
            command.Connection = OpenConnection();
            command.CommandType = CommandType.Text;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            return sqlDataProcess.SetProcess(command.Connection, dataReader);
        }

        public async Task<SqlCommand> StoredAsync(string procedureName, params SqlParameter[] parameters)// exec PersonelGuncelle
        {
            using SqlCommand command = new SqlCommand();
            command.CommandText = procedureName;
            command.Connection = OpenConnection();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 50;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            await command.ExecuteNonQueryAsync();
            CloseConnection();
            return command;
        }

        public async Task<SqlDataProcess> StoreReaderAsync(string procedureName, params SqlParameter[] parameters)
        {
            using SqlCommand command = new SqlCommand();
            command.CommandText = procedureName;
            command.Connection = OpenConnection();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandTimeout = 50;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            SqlDataReader dataReader = await command.ExecuteReaderAsync();
            return sqlDataProcess.SetProcess(command.Connection, dataReader);
        }

        public async Task<DataTable> GetDataTableAsync(string commandText, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter dataAdapter = new SqlDataAdapter())
            {
                dataAdapter.SelectCommand = await StoredAsync(commandText, parameters);
                using DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
    public class SqlDataProcess
    {
        internal SqlConnection SqlConnection { get; set; }
        internal SqlDataReader SqlDataReader { get; set; }

        internal void CloseSqlConnection()
        {
            if (SqlConnection == null)
                return;

            if (SqlConnection.State == ConnectionState.Open)
            {
                SqlConnection.Close();
            }
        }

        internal SqlDataProcess SetProcess(SqlConnection sqlConnection, SqlDataReader dataReader)
        {
            SqlDataReader = dataReader;
            SqlConnection = sqlConnection;
            return this;
        }
    }
}
