using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Exceptions.BLL
{
    [Serializable]
    public class SqlBLLException : BLLException
    {
        public SqlBLLException() : base() { }

        public SqlBLLException(string message) : base(message) { }

        public SqlBLLException(string format, params object[] args) : base(string.Format(format, args)) { }

        public SqlBLLException(string message, Exception innerException) : base(message, innerException) { }

        public SqlBLLException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}