using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Exceptions.BLL
{
    public class BLLException : Exception
    {
        public BLLException() : base() { }

        public BLLException(string message) : base(message) { }

        public BLLException(string format, params object[] args) : base(string.Format(format, args)) { }

        public BLLException(string message, Exception innerException) : base(message, innerException) { }

        public BLLException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}
