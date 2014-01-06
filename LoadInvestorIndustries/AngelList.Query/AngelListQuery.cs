using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AngelList.Interfaces;

namespace AngelList.Query
{
    /// <summary>
    /// Abstract base class for implementations of IAngelListQuery. 
    /// </summary>
    /// <typeparam name="TReturnType">Return type of the query.</typeparam>
    public abstract class AngelListQuery<TReturnType> : IAngelListQuery<TReturnType>
    {
        abstract public TReturnType Result { get; protected set; }

        public string QueryName
        {
            get
            {
                return this.GetType().Name;
            }
        }

        public Type ResultType
        {
            get { return typeof(TReturnType); }
        }

        protected IAngelListClient AngelListClient { get; set; }

        public AngelListQuery(IAngelListClient angelListClient)
        {
            if (angelListClient == null)
            {
                throw new ArgumentNullException("angelListClient");
            }

            this.AngelListClient = angelListClient;
        }

        abstract public TReturnType Execute();
    }
}
