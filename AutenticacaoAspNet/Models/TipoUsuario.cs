using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutenticacaoAspNet.Models
{
    public enum TipoUsuario
    {
        Padrao,
        Administrador,
        CadastroUsuario //Criado para fazer o controle de acesso via "AutenticacaoAspNet.FiltersAcesso"
    }
}