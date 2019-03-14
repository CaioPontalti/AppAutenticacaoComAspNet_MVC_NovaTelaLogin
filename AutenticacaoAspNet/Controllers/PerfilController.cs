using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutenticacaoAspNet.ViewModels;
using AutenticacaoAspNet.Models;
using System.Security.Claims;
using AutenticacaoAspNet.Utils;

namespace AutenticacaoAspNet.Controllers
{
    public class PerfilController : Controller
    {
        private UsuariosContext db = new UsuariosContext();
        
        // GET: Perfil
        [Authorize]
        public ActionResult AlterarSenha()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult AlterarSenha(AlterarSenhaViewModel alterarSenhaView)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
                                       //using System.Security.Claims;
            var identity = User.Identity as ClaimsIdentity; //Captura o usuário logado, fazendo o cast (conversão) para o tipo ClaimsIdentity
            var login = identity.Claims.FirstOrDefault(u => u.Type == "Login").Value;

            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == login);

            if (alterarSenhaView.SenhaAtual.CriptografarSenha() != usuario.Senha)
            {
                ModelState.AddModelError("SenhaAtual", "A Senha digitada está incorreta!");
                return View();
            }

            if (alterarSenhaView.NovaSenha.CriptografarSenha() == usuario.Senha)
            {
                ModelState.AddModelError("NovaSenha", "A Senha nova está igual antiga!");
                return View();
            }

            usuario.Senha = alterarSenhaView.NovaSenha.CriptografarSenha();
            db.Entry(usuario).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            TempData["Mensagem"] = "Senha alterada com sucesso!";
            
            return RedirectToAction("Index", "Painel");
        }
    }
}