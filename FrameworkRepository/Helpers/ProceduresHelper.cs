using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using FrameworkRepository.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FrameworkRepository.Helpers
{
    internal static class ProceduresHelper
    {
        private static object[] AddParameters(this DbCommand cmd, object[] parameters)
        {
            var parameterNames = new object[parameters.Length];

            cmd.Parameters.Clear();

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i] == null ||
                    !parameters[i].GetType().IsSubclassOf(typeof(DbParameter)))
                {
                    var parameter = cmd.CreateParameter();
                    parameter.ParameterName = $"@p{i}";
                    parameter.Value = parameters[i] ?? DBNull.Value;

                    cmd.Parameters.Add(parameter);
                    parameterNames[i] = parameter.ParameterName;
                }
                else
                {
                    cmd.Parameters.Add(parameters[i]);
                }
            }

            return parameterNames;
        }

        private static async Task CheckConnectionState(this DbConnection connection)
        {
            if (connection.State == ConnectionState.Connecting)
            {
                var retryCount = 1;
                while (connection.State == ConnectionState.Connecting && retryCount <= 10)
                {
                    await Task.Delay(500);
                }

                if (connection.State == ConnectionState.Connecting)
                    throw new Exception("Couldn't open a database connection.");
            }

            if (connection.State != ConnectionState.Open) connection.Open();
        }


        public static T GetScalar<T>(this DatabaseFacade database, string command, params object[] parameters)
        {
            var con = database.GetDbConnection();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                var parameterNames = cmd.AddParameters(parameters);

                cmd.CommandText = $"Exec {string.Format(command, parameterNames)}";
                con.CheckConnectionState().Wait();

                var result = cmd.ExecuteScalar();

                return result == DBNull.Value || result == null ? default(T) : (T)result;
            }
        }

        public static List<T> GetPrimitiveList<T>(this DatabaseFacade database, string command, params object[] parameters)
        {
            var list = new List<T>(10);

            var con = database.GetDbConnection();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                var parameterNames = cmd.AddParameters(parameters);

                cmd.CommandText = $"Exec {string.Format(command, parameterNames)}";
                con.CheckConnectionState().Wait();

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    list.Add((T)reader[0]);
                }

                return list;
            }
        }

        public static RawDataModel GetRawData(this DatabaseFacade database, string command, params object[] parameters)
        {
            var result = new RawDataModel();

            var con = database.GetDbConnection();
            using (var cmd = con.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                var parameterNames = cmd.AddParameters(parameters);

                if (parameterNames.All(a => a == null))
                {
                    cmd.CommandText = command.Split(' ').FirstOrDefault();
                    cmd.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    cmd.CommandText = $"Exec {string.Format(command, parameterNames)}";
                    cmd.CommandType = CommandType.Text;
                }

                con.CheckConnectionState().Wait();

                var reader = cmd.ExecuteReader();

                result.Headers = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

                while (reader.Read())
                {
                    var row = new object[reader.FieldCount];

                    reader.GetValues(row);

                    result.Rows.Add(row);
                }
            }

            return result;
        }
    }
}
