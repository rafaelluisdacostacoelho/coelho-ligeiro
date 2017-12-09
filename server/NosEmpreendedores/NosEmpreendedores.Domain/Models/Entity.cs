using System;

namespace NosEmpreendedores.Domain.Models
{
    public abstract class Entity
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }
            set
            {
                if (value == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                else
                {
                    _id = value;
                }
            }
        }
    }
}