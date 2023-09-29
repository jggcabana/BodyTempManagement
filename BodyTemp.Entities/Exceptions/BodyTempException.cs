using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BodyTemp.Entities.Exceptions
{
    public class BodyTempException : Exception
    {
        public BodyTempException()
        {

        }

        public BodyTempException(string message)
            : base(message)
        {

        }
    }
}
