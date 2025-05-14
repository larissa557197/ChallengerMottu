using VisionHive.Domain;
using VisionHive.Domain.Enums;

namespace VisionHive.Infrastructure.Persistence
{
    public class Area : Audit
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; }


        // relacionamento com Moto
        private readonly List<Moto> _motos = new();
        public IReadOnlyCollection<Moto> Motos => _motos.AsReadOnly();

        private Area (string nome)
        {
            ValidarDados(nome);
            Id = Guid.NewGuid();
            Nome = nome;

            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            Status = StatusType.Ativo;
        }
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
            _motos.Add(moto);
        }
    }
}
