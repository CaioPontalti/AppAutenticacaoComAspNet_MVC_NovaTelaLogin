using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using System.Web.Helpers;

[assembly: OwinStartup(typeof(AutenticacaoAspNet.Startup))]

namespace AutenticacaoAspNet
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",//Padrão
                LoginPath = new PathString("/Autenticacao/Login") //Direciona para tela de Login
            });

            
            /*
                Identificador para que não ocorra erro na propriedade da View '@Html.AntiForgeryToken()'
                O @Html.AntiForgeryToken() serve para q o formulário não aceite submit de fora da aplicação. Mecanismo de segurança.

                Login é um dos campos únicos do usuário. Poderia ser outro campo único
            */
            AntiForgeryConfig.UniqueClaimTypeIdentifier = "Login"; //using System.Web.Helpers;
        }
    }
}
