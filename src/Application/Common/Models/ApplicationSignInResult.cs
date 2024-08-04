using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleAtm.Application.Common.Models;
public class ApplicationSignInResult
{
    internal ApplicationSignInResult(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }

    public ApplicationSignInResult(bool succeeded, string token)
    {
        Succeeded = succeeded;
        Token = token;
        Errors = Array.Empty<string>();
    }

    public bool Succeeded { get; init; }

    public string[] Errors { get; init; }

    public string? Token { get; set; }

    public static ApplicationSignInResult Success()
    {
        return new ApplicationSignInResult(true, Array.Empty<string>());
    }

    public static ApplicationSignInResult Failure(IEnumerable<string> errors)
    {
        return new ApplicationSignInResult(false, errors);
    }
}
