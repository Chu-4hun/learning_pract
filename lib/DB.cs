using System;
using System.Collections.Generic;
using Npgsql;

namespace InventoryOfEquipment.Lib
{
    public class DB
    {
        private string _server;
        private string _databaseName;
        private string _userName;
        private string _password;

        public DB(string server, string DatabaseName, string UserName, string password)
        {
            this._server = server;
            this._databaseName = DatabaseName;
            this._userName = UserName;
            this._password = password;
            IsConnect();
        }

        public List<Dictionary<string, object>> execute(string sql, Dictionary<string, object> data = null)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
            var cmd = new NpgsqlCommand(sql, this.Connection);
            if (data != null)
            {
                foreach (var parametr in data)
                {
                    cmd.Parameters.AddWithValue(parametr.Key, parametr.Value);
                }
            }
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                bool start = true;
                int number = 0;
                while (start)
                {
                    try
                    {
                        row.Add(reader.GetName(number), reader.GetValue(number));
                        number++;
                    }
                    catch (Exception)
                    {
                        start = false;
                    }
                }

                result.Add(row);
            }
            reader.Close();
            return result;
        }

        private NpgsqlConnection Connection { get; set; }

        private bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(this._databaseName))
                    return false;
                string connstring = string.Format("Host={0};Username={1};Password={2};Database={3}", this._server,
                    this._userName,
                    this._password, this._databaseName);
                Connection = new NpgsqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        private void Close()
        {
            Connection.Close();
        }
    }
}