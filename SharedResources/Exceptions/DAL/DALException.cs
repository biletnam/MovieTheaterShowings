using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Exceptions.DAL
{
    public class DALException : Exception
    {
        public DALException() : base() { }

        public DALException(string message) : base(message) { }

        public DALException(string format, params object[] args) : base(string.Format(format, args)) { }

        public DALException(string message, Exception innerException) : base(message, innerException) { }

        public DALException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}