﻿using System.Runtime.ConstrainedExecution;
using VisionHive.Domain;
using VisionHive.Domain.Enums;

namespace VisionHive.Infrastructure.Persistence
{
    public class Moto: Audit
    {
        public Guid Id { get; private set; }
        public string Placa { get; private set; }
        public string Chassi { get; private set; }
        public bool EstaComLote { get; private set; }
        public CategoriaEntrada Categoria { get; private set; }



        public Guid AreaId { get; private set; }
        public Area Area { get; private set; }

        public Moto(string placa, string chassi, bool estaComLote, CategoriaEntrada categoria, Guid areaId)
        {
            ValidarDados(placa, chassi, categoria);

            Id = Guid.NewGuid();
            Placa = placa;
            Chassi = chassi;
            EstaComLote = estaComLote;
            Categoria = categoria;
            AreaId = areaId;

            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
            Status = StatusType.Ativo;
        }

        private void ValidarDados(string placa, string chassi, CategoriaEntrada categoria)
        {
            if (categoria != CategoriaEntrada.Sucata)
            {
                if (string.IsNullOrWhiteSpace(placa))
                    throw new Exception("Placa não pode ser vazia para motos que não são sucata.");

                if (string.IsNullOrWhiteSpace(chassi))
                    throw new Exception("Chassi não pode ser vazio para motos que não são sucata.");
            }
        }

        internal static Moto Create(string placa, string chassi, bool estaComLote, CategoriaEntrada categoria, Guid areaId)
        {
            return new Moto(placa, chassi, estaComLote, categoria, areaId);
        }

        public void AtualizarDados(string placa, string chassi, bool estaComLote, CategoriaEntrada categoria, Guid areaId)
        {
            ValidarDados(placa, chassi, categoria);
                Placa = placa;
                Chassi = chassi;
                EstaComLote = estaComLote;
                Categoria = categoria;
                AreaId = areaId;

                DateModified = DateTime.UtcNow; 
            
        }

        internal static Moto Create(string placa, string chassi, bool estaComLote, CategoriaEntrada categoria)
        {
            throw new NotImplementedException();
        }

        internal void AtualizarDados(string placa, string chassi, bool estaComLote, CategoriaEntrada categoria)
        {
            throw new NotImplementedException();
        }
    }
}
