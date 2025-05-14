using VisionHive.Domain.Enums;

namespace VisionHive.DTO.Response
{
    public class MotoResponse
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public bool EstaComLote { get; set; }
        public string CategoriaEntrada { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public StatusType Status { get; set; }

    }
}
