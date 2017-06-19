using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Exceptions.DAL
{
    public class SqlDALException : DALException
    {
        public SqlDALException() : base() { }

        public SqlDALException(string message) : base(message) { }

        public SqlDALException(string format, params object[] args) : base(string.Format(format, args)) { }

        public SqlDALException(string message, Exception innerException) : base(message, innerException) { }

        public SqlDALException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}