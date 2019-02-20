using Npgsql;
using senior_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace senior_back.Models
{
    public class Client : MainModel
    {
        public ClientViewModel client { get; set; }

        public bool Save()
        {
            string query = String.Format("insert into client (name, document, phone) values('{0}','{1}',{2})", client.nome, client.documento, client.telefone);
            return Execute(query);
        }

        public NpgsqlDataReader Select(string search)
        {
            string query = string.Format("select c.name, t.* from checkin t inner join client c on (c.id = t.id_client) where name like '%{0}%' or document like '%{1}%' or phone like '%{2}%'", search, search, search);
            return ExecuteSelect(query);
        }
    }
}
