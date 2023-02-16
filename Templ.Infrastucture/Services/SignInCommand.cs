using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Templ.Infrastucture.Services;

public class SignInCommand
{
    public string UserName { get; set; } = string.Empty!;
    public string Password { get; set; } = string.Empty!;
}
