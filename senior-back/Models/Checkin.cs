using Npgsql;
using senior_back.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace senior_back.Models
{
    public class Checkin : MainModel
    {
        public CheckinViewModel checkin { get; set; }

        public bool Save()
        {
            string query = String.Format("insert into checkin (id_client, checkin_date, checkout_date, has_car) values({0}, '{1}', '{2}', {3})", checkin.hospede.id, checkin.dataEntrada, checkin.dataSaida, checkin.adicionalVeiculo);
            return Execute(query);
        }

        public NpgsqlDataReader Select(bool op)
        {
            string query = string.Empty;
            if (op)
                query = string.Format("select c.name, t.* from checkin t inner join client c on (c.id = t.id_client) where checkout_date between now() and now()");
            else
                query = string.Format("select c.name, t.* from checkin t inner join client c on (c.id = t.id_client) where checkout_date < now()");

            return ExecuteSelect(query);
        }
    }
}
