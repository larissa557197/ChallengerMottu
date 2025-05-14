using Microsoft.AspNetCore.Mvc;
using VisionHive.Domain.Enums;

namespace VisionHive.DTO.Request
{
    public class MotoRequest
    {
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public bool EstaComLote { get; set; }
        public CategoriaEntrada Categoria { get; set; }
    }
}
