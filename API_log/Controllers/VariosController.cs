using API_Log.Context;
using API_Log.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLogin.Helper;
using System;
using System.Linq;

namespace API_Log.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VariosController : Controller
    {
        private readonly AppDbContext context;

        public VariosController(AppDbContext context)
        {
            this.context = context;
        }
        // GET: VariosController
        public ActionResult Index()
        {
            return View();
        }

        // GET: VariosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VariosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VariosController/Create
        [HttpPost]
        [Route("Registrar")]
        public ActionResult Post([FromBody] Tbl_Empleados empleado)
        {
             try 
            { 
                var empleadoExist= context.Tbl_Empleados.Where(p=>p.ID_Empleado==empleado.ID_Empleado).FirstOrDefault();
                if (empleadoExist!=null)
                {
                    return Ok("Username ya registrado");
                
                }
                else
                {
                    var hash = HashHelper.Hash(empleado.Contraseña);
                    empleado.Contraseña = hash.Password;
                    context.Tbl_Empleados.Add(empleado);
                    context.SaveChanges();

                }
                return Ok("Registrado");
            }

            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: VariosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VariosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: VariosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VariosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
