using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutenticacaoAspNet.FiltersAcesso;
using AutenticacaoAspNet.Models;

namespace AutenticacaoAspNet.Controllers
{
    public class CadastroUsuariosController : Controller
    {
        // GET: CadastroUsuarios

        // O Authorize permite apenas que usuários logados no sistema acessem a página.
        // O parametro "Role" está permitindo que apenas usuários do tipo Administrador acessem essa pagina (enum TipoUsuário)

        // [Authorize(Roles = "Administrador")]  Authorize padrão verificando se o usuário é do tipo Administrador

        // Controle de acesso criado para filtrar pelos tipos de usuários. Utilizado ao invés do [Authorize]
        [FiltersAcesso.FiltersAcesso( new[] { TipoUsuario.Administrador, TipoUsuario.CadastroUsuario })]
        public ActionResult Index()
        {
            return View();
        }
    }
}