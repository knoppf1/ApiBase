
using System;
using Dapper;

using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace apiBase.DataDapper
{
    public class DBQuery
    {
        private readonly string _connectionString;
        private MySqlConnection cnxDB;
        public DBQuery(string connectionString)
        {
            _connectionString = connectionString;
            cnxDB = new MySqlConnection(_connectionString);

        }
        public List<T> executeQuery<T>(String sql)
        {
            var result  = cnxDB.Query<T>(sql).AsList();
            cnxDB.Close();

            return result;
        }

        public T executeQuerySingle<T>(String sql)
        {
            var result = cnxDB.QuerySingle<T>(sql);
            cnxDB.Close();

            return result;
        }

        public List<T> executeQuery<T>(object comentario)
        {
            throw new NotImplementedException();
        }
    }
}
