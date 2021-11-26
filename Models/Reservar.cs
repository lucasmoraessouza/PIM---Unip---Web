using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace protechHotel.Models
{
    public class Reservar
    {

        public int Id { get; set; }

        [DisplayName("Nome Completo")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string NomeCompleto { get; set; }

        [DisplayName("CPF")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Cpf { get; set; }

        [DisplayName("Quarto")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Quarto { get; set; }

        [DisplayName("Dia da reserva")]
        [Required(ErrorMessage = "Campo obrigatório")]
        public DateTime DataRerseva { get; set; }
        [DisplayName("Usuário")]
        public string User { get; set; }
    }
}
