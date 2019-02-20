using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using senior_back.Models.ViewModels;

namespace senior_back.Models
{
    public class MainModel
    {
        private static NpgsqlConnection connection { get; set; }
        private void GetConnection()
        {
            try
            {
                string connstring = String.Format("Server=localhost;Port=5432;User Id=postgres;Password=123;Database=senior;");
                connection = new NpgsqlConnection(connstring);
                connection.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public bool Execute(string query)
        {
            this.GetConnection();
            using (NpgsqlCommand pgsqlcommand = new NpgsqlCommand(query, connection))
                pgsqlcommand.ExecuteNonQuery();
            this.Disconnect();
            return true;
        }

        public NpgsqlDataReader ExecuteSelect(string query)
        {
            this.GetConnection();
            var command = new NpgsqlCommand(query, connection);
            NpgsqlDataReader dr = command.ExecuteReader();
            return dr;
        }
        private void Disconnect()
        {
            if (connection != null)
                connection.Close();
        }
    }
}
