using Flunt.Notifications;
using System;

namespace ApiRobustas.Compartilhados.EntidadesBase
{
    /// <summary>
    /// Classe base para todas as entidades
    /// </summary>
    public abstract class Entidade : Notifiable<Notification>
    {
        public Guid Id { get; private set; }
        public DateTime DataCadastro { get; private set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
        }

        /// <summary>
        /// Comparação de entidades
        /// a entidade só é igual se tiver o mesmo tipo e o mesmo Id.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            var compareTo = obj as Entidade;

            if (!ReferenceEquals(this, compareTo))
            {
                if (ReferenceEquals(null, compareTo)) return false;

                return Id.Equals(compareTo.Id);
            }

            return true;
        }

        public static bool operator ==(Entidade a, Entidade b)
        {
            if (a is not null || !ReferenceEquals(b, null))
            {
                if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                    return false;

                return a.Equals(b);
            }

            return true;
        }

        /// <summary>
        /// para comparação de classes onde cada uma tem um código
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(Entidade a, Entidade b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id= {Id}]";
    }
}