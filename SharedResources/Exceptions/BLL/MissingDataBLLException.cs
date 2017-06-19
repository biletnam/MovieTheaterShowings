using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Exceptions.BLL
{
    [Serializable]
    public class MissingDataBLLException : BLLException
    {
        public MissingDataBLLException() : base() { }

        public MissingDataBLLException(string message) : base(message) { }

        public MissingDataBLLException(string format, params object[] args) : base(string.Format(format, args)) { }

        public MissingDataBLLException(string message, Exception innerException) : base(message, innerException) { }

        public MissingDataBLLException(string format, Exception innerException, params object[] args) : base(string.Format(format, args), innerException) { }
    }
}