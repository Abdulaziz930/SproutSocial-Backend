using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Service.Exceptions
{
    public class RecordAlreadyExistException : Exception
    {
        public RecordAlreadyExistException(string message) : base(message)
        {
        }
    }
}
