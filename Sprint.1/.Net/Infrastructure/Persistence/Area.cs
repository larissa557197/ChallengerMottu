using VisionHive.Domain;
using VisionHive.Domain.Enums;

namespace VisionHive.Infrastructure.Persistence
{
    public class Area : Audit
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }


        // relacionamento 1:N com Moto
        public ICollection<Moto> Motos { get; private set; } = new List<Moto>();

        private Area (string nome)
        {
            ValidarDados(nome);
            Id = Guid.NewGuid();
            Nome = nome;

            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            Status = StatusType.Ativo;
        }

        protected Area() { }

        internal static Area Create(string nome)
        {
            return new Area(nome);
        }

        private void ValidarDados(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome da área não pode ser vazio.");
        }

        public void AtualizarDados(string novoNome)
        {
            ValidarDados(novoNome);
            Nome = novoNome;
            DateModified = DateTime.UtcNow;
        }

        public void AdicionarMoto(Moto moto)
        {
            if (moto == null)
                throw new Exception("Moto não pode ser nula.");
            Motos.Add(moto);
        }
    }
}
