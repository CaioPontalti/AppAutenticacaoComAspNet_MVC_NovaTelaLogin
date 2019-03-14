using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutenticacaoAspNet.ViewModels
{
    public class CadastroUsuarioViewModel
    {
        [Required(ErrorMessage ="O campo Nome é obrigatório")]
        [MaxLength(100, ErrorMessage ="O tamanho máximo deve ser de 100 caracteres.")]
        //[Display(Name = "teste")]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O campo Login é obrigátorio.")]
        [MaxLength(50, ErrorMessage = "O tamanho máximo deve ser de 50 caracteres.")]
        public string Login { get; set; }

        [Required]
        [MinLength(6, ErrorMessage ="A senha deve conter a menos 6 caracteres.")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "A senha deve conter a menos 6 caracteres.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage ="Os campos 'Senha' e 'Confirmar Senha' estão diferentes.")]
        [Display(Name = "Confirmar senha.")]
        public string ConfirmarSenha { get; set; }
    }
}