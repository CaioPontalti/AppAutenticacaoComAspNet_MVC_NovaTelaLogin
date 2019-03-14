using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutenticacaoAspNet.Models;
using AutenticacaoAspNet.ViewModels;
using AutenticacaoAspNet.Utils;
using System.Security.Claims;

namespace AutenticacaoAspNet.Controllers
{
    public class AutenticacaoController : Controller
    {
        private UsuariosContext db = new UsuariosContext();

        // GET: Autenticacao
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(CadastroUsuarioViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            //Valida se já existe um Login igual cadastrado
            if (db.Usuarios.Count(u => u.Login == viewModel.Login) > 0)
            {
                                    //Qual o campo
                ModelState.AddModelError("Login", "Esse login já está em uso!");
                return View(viewModel);
            }

            //Cria o usuário que será persistido no BD recebendo as informações do ViewModel
            Usuario usuario = new Usuario()
            {
                Nome = viewModel.Nome,
                Login = viewModel.Login,
                Senha = viewModel.Senha.CriptografarSenha() //Metodo de extesão criado para criptografar a senha
            };

            db.Usuarios.Add(usuario);
            db.SaveChanges();

            TempData["Mensagem"] = "Cadastro realizado com sucesso! Efetue o Login.";

            return RedirectToAction("Login", "Autenticacao");
            
            //return RedirectToAction("Index", "Home");  /* redireciona para: View, Controller*/
        }


        public ActionResult Login(string ReturnUrl)
        {
            var viewModel = new LoginViewModel()
            {
                UrlRetorno = ReturnUrl
            };

            return View(viewModel);
        }
        
        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            //Valida se as informações na tela estão OK
            //if (!ModelState.IsValid)
            //{
            //    return View(loginViewModel);
            //}

            //Busca o usuario no BD
            var usuario = db.Usuarios.FirstOrDefault(u => u.Login == loginViewModel.Login);

            if (usuario == null)
            {
                ModelState.AddModelError("Login", "Login " + loginViewModel.Login + " não encontrado!");
                ViewBag.Login = loginViewModel.Login;
                return View();
            }

            if (loginViewModel.Senha == null)
            {
                return View();
            }

            if (usuario.Senha != loginViewModel.Senha.CriptografarSenha())
            {
                ModelState.AddModelError("Senha", " Usuário " + loginViewModel.Login + " Senha inválida");
                ViewBag.Login = loginViewModel.Login;
                return View();
            }

            //Essa classe serve para definir caracteristicas a respeito de um usuário
            //Definir a identidade do usuário.

            //Essas informações são passados como um array do tipo Claim => (classe).
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, usuario.Nome), //primeiro parametro é um string. Pode ser tanto 
                new Claim("Login", usuario.Login),         // através do ClaimTypes ou colocar o nome direto.
                new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString())  // ClaimTypes.Role => Define o papel do usuário dentro da aplic. Permissões.
            },  "ApplicationCookie"); //=> está no arquivo Startup -- guarda as informações do usuário quando ele faz o login.

            Request.GetOwinContext().Authentication.SignIn(identity); //=> identity populado anteriormente.

            if (!String.IsNullOrWhiteSpace(loginViewModel.UrlRetorno) || Url.IsLocalUrl(loginViewModel.UrlRetorno))
                return Redirect(loginViewModel.UrlRetorno);
            else
                return RedirectToAction("Index", "Painel");
        }

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie"); //=> está no arquivo Startup
            return RedirectToAction("Login", "Autenticacao");
        }
    }
}