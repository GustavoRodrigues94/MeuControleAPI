using System;

namespace MeuControle.Dominio.Entidades
{
    public abstract class Entidade : IEquatable<Entidade>
    {
        protected Entidade()
        {
            Codigo = Guid.NewGuid();
        }

        public Guid Codigo { get; }

        public bool Equals(Entidade outra) => Codigo == outra?.Codigo;
    }
}
