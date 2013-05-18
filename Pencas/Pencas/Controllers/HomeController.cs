using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pencas.Models;

namespace Pencas.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            //PencasDB db = new PencasDB();
            //DAOUsuario usuarioDAO = new DAOUsuario();
            //List<Usuario> usuarios = usuarioDAO.GetUsuarios();
            //ViewBag.Valor = usuarios[0].Nombre;

            return View();
        }

        public PartialViewResult EnviarPronostico(string email, string token, string resultados)
        {
            DAOUsuario usuarioDAO = new DAOUsuario();
            DAOPronostico pronosticoDAO = new DAOPronostico();
            try
            {
                if (DateTime.Now > new DateTime(2013, 6, 14))
                {
                    ViewBag.Mensaje = "El plazo para enviar pronósticos ha finalizado";
                    ViewBag.ColorTexto = "Red";
                    return PartialView("_Mensaje");
                }

                usuarioDAO.AsociarMailToken(email, token);
                Usuario u = usuarioDAO.GetUsuarioPorEmail(email);
                pronosticoDAO.GuardarPronostico(resultados, u);

                ViewBag.Mensaje = "Su pronóstico ha sido enviado. Para consultarlo deberá ingresar su usuario y clave y presionar el botón \"Ver Pronóstico\" arriba de la tabla";
                ViewBag.ColorTexto = "Green";
                return PartialView("_Mensaje");
            }
            catch (YaEstaAsociadoElTokenConOtroUsuarioException) { 
                ViewBag.Mensaje = "Token inválido";
                ViewBag.ColorTexto = "Red";
                return PartialView("_Mensaje");
            }
            catch (NoExisteElTokenException) { 
                ViewBag.Mensaje = "Token inválido";
                ViewBag.ColorTexto = "Red";
                return PartialView("_Mensaje");
            }
            catch (YaExisteUsuarioException)
            {
                Usuario u = usuarioDAO.GetUsuarioPorEmail(email);
                if (!token.Equals(usuarioDAO.GetTokenPorEmail(email)))
                {
                    ViewBag.Mensaje = "Token inválido";
                    ViewBag.ColorTexto = "Red";
                    return PartialView("_Mensaje");
                }
                if (u.EnvioPronostico)
                {
                    ViewBag.Mensaje = "Ya ha realizado su pronóstico. Click aquí para verlo";
                    ViewBag.ColorTexto = "Red";
                    return PartialView("_Mensaje");
                }
                else
                {
                    ViewBag.ColorTexto = "Red";
                    return PartialView("_Mensaje");
                }
            }
        }

        public ActionResult VerPronostico()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
