using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using senior_back.Models;
using senior_back.Models.ViewModels;

namespace senior_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        [HttpPost("saveClient")]
        public string SaveClient([FromBody] dynamic json)
        {
            var model = new Client();
            model.client = JsonConvert.DeserializeObject<ClientViewModel>(json);
            model.Save();
            return "{'msg': 'usuário cadastrado com sucesso!'}";
        }


        [HttpPost("saveCheckin")]
        public string SaveCheckin([FromBody] string json)
        {
            var model = new Client();
            model.client = JsonConvert.DeserializeObject<ClientViewModel>(json);
            model.Save();
            return "{'msg': 'checkin cadastrado com sucesso!'}";
        }

        [HttpGet("consult")]
        public string Consult(string search = null)
        {
            var result = new Client().Select(search ?? "");

            var checkins = new List<CheckinViewModel>();
            while (result.Read())
            {
                var obj = new CheckinViewModel()
                {
                    hospede = new ClientViewModel()
                    {
                        nome = result[0].ToString()
                    },
                    dataEntrada = Convert.ToDateTime(result[3]),
                    dataSaida = Convert.ToDateTime(result[4]),
                    adicionalVeiculo = Convert.ToBoolean(result[5])
                };
                Calcula(obj);
                checkins.Add(obj);
            }
            return JsonConvert.SerializeObject(checkins);
        }



        [HttpGet("consult2")]
        public string Consult2(bool checkin = false)
        {
            var result = new Checkin().Select(checkin);
            var checkins = new List<CheckinViewModel>();
            while (result.Read())
            {
                var obj = new CheckinViewModel()
                {
                    hospede = new ClientViewModel()
                    {
                        nome = result[0].ToString()
                    },
                    dataEntrada = Convert.ToDateTime(result[3]),
                    dataSaida = Convert.ToDateTime(result[4]),
                    adicionalVeiculo = Convert.ToBoolean(result[5])
                };
                Calcula(obj);
                checkins.Add(obj);
            }
            return JsonConvert.SerializeObject(checkins);
        }

        private void Calcula(CheckinViewModel obj)
        {
            var value = 0;
            for (var day = obj.dataEntrada.Date; day <= obj.dataSaida.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Sunday || day.DayOfWeek == DayOfWeek.Saturday)
                {
                    value += 150;
                    if (obj.adicionalVeiculo)
                        value += 20;
                }
                else
                {
                    value += 120;
                    if (obj.adicionalVeiculo)
                        value += 15;
                }
            }
            obj.value = value;
        }
    }
}
