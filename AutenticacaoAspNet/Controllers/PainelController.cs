using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutenticacaoAspNet.Controllers
{
    public class PainelController : Controller
    {
        // GET: Painel
        [Authorize] //Permite apenas usuários logados acessem a página
        public ActionResult Index()
        {
            if (User.IsInRole("Padrao"))
            {
                ViewBag.MensagemTipoUsuario = "Você é usuário padrão!";
            }
            return View();
        }

        [Authorize] //Permite apenas usuários logados acessem a página
        public ActionResult Mensagens()
        {
            return View();
        }
    }
}