using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutenticacaoAspNet.ViewModels
{
    public class AlterarSenhaViewModel
    {
        [Required(ErrorMessage = "Informe a senha Atual!")]
        [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string SenhaAtual { get; set; }

        [Required(ErrorMessage = "Informe a nova Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres!")]
        [DataType(DataType.Password)]
        [Display(Name = "Nova Senha")]
        public string NovaSenha { get; set; }


        [Required(ErrorMessage = "Confirme sua nova Senha")]
        [MinLength(6, ErrorMessage = "A senha deve ter ao menos 6 caracteres!")]
        [DataType(DataType.Password)]
        [Compare(nameof(NovaSenha), ErrorMessage = "Os campos Nova Senha e Confirmar Senha estão diferentes!")] //Compara com a propriedade da NovaSenha
        [Display(Name ="Confirmar Senha")]
        public string ConfirmarSenha { get; set; }
    }
}