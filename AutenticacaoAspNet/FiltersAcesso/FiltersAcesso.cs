using AutenticacaoAspNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AutenticacaoAspNet.FiltersAcesso
{
    public class FiltersAcesso : AuthorizeAttribute // herda da classe AuthorizeAttribute
    {
        private TipoUsuario[] _tiposAutorizados; //enum
        
        public FiltersAcesso(TipoUsuario[] tiposUsuariosAutorizados) //Construtor recebe os tipos permitidos no parametro
        {
            _tiposAutorizados = tiposUsuariosAutorizados;
        }

        //sobrescreve o metodo OnAuthorization da classe base AuthorizeAttribute
        //metodo chamado automaticamente quando o usuário tenta acessar determinada página.
        public override void OnAuthorization(AuthorizationContext filterContext) // parametro padrão do método
        {
            //Verifica se o tipo de usuário logado (User) está na lista de usuários permitidos, 
            // passados pelo Controller CadastroUsuariosController no construtor.
            bool autorizado = _tiposAutorizados.Any(t => filterContext.HttpContext.User.IsInRole(t.ToString()));

            if (!autorizado)
            {
                filterContext.Controller.TempData["ErrorAcessoNegado"] = "Você não tem acesso a essa tela. Seu tipo de usuário é Padrão!";
                filterContext.Result = new RedirectResult("Painel");
            }

        }
    }
}