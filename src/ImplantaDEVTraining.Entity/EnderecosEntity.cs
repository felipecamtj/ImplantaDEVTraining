using ImplantaDEVTraining.Common;
using System;

namespace ImplantaDEVTraining.Entity
{
    public class EnderecosEntity : BaseEntity
    {
        public override Guid Id { get; set; }
        public Guid IdProfissional { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
    }
}
