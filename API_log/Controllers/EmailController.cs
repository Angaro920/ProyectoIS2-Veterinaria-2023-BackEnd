using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API_Log.RequestModels;
using API_Log.ResponseModels;
using API_Log.Utilities;
using System;
using API_Log.Context;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace API_Log.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        public readonly AppDbContext dbContext;
        public EmailController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
 
        [HttpPost, Route("sendEmail")]
        public async Task<ActionResult> RequestToken([FromBody] EmailRequestModel request)
        {
            EmailResponseModel emailResponseModel = new EmailResponseModel();
            var user = dbContext.TblEmpleados.FirstOrDefault(f => f.Usuario == request.Usuario);
            var Mascota = dbContext.TblMascotas.FirstOrDefault(f => f.IdMascota == request.IdMascota);
            var Historia = dbContext.TblHistoriaClinica.FirstOrDefault(f => f.IdHistoriaClinica == request.IdHistoriaClinica);
            try
            {

                var mensaje = "<html>" +
                    "<body>" +
                    "<h1>Historia Clinica de la veterinaria</h1></br>" +
                    "<p><h1>De:</h1></p>" + request.Usuario +
                    "<p><h1>Nombre de la mascota:</h1></p>" + Mascota.Nombre +
                    "<p><h1>Realizada en la fecha:</h1></p>" + Historia.Fecha 
                    + "<p><h1>Se diagnostico:</h1> </p>" + Historia.Diagnostico+
                    "<p><h1>Para lo cual se orden:</h1> </p>a" + Historia.OrdenMedica+
                      "</br><p>Notificación Automatica. <b>No responder a este correo.</b></p>";
                //var mensaje = request.Usuario;
                EmailService emailService = new EmailService();
                emailService.EnviarCorreo(request.Destinatario, request.Asunto, mensaje, true);

                emailResponseModel.Respuesta = 1;
                emailResponseModel.Mensaje = "Correo electrónico enviado correctamente.";

                return Ok(emailResponseModel);
            }
            catch(Exception e)
            {
                emailResponseModel.Respuesta = 0;
                emailResponseModel.Mensaje = "Error: " + e;
                return BadRequest(emailResponseModel);
            }
        }
    }
}
