using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Log.RequestModels
{
    public class EmailRequestModel
    {
        [Required]
        [JsonProperty("Destinatario")]
        public string Destinatario { get; set; }
        [Required]
        [JsonProperty("Asunto")]
        public string Asunto { get; set; }
        [Required]
        [JsonProperty("Usuario")]
        public string Usuario { get; set; }
        [Required]
        [JsonProperty("IdHistoria")]
        public int IdHistoriaClinica { get; set; }
        [Required]
        [JsonProperty("IdMascota")]
        public int IdMascota { get; set; }
    }
}
