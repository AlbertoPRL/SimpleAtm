using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAtm.Application.Common.Exceptions;
public class TokenConfigurationException : Exception
{
    public TokenConfigurationException(string message)
        : base(message)
    {
    }
}
