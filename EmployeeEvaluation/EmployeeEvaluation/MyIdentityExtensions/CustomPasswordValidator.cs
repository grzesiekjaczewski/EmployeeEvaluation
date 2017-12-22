using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace EmployeeEvaluation.IdentityExtensions
{
    public class CustomPasswordValidator : PasswordValidator, IIdentityValidator<string>
    {
        override public Task<IdentityResult> ValidateAsync(string item)
        {
            if (String.IsNullOrEmpty(item) || item.Length < RequiredLength)
            {
                return Task.FromResult(IdentityResult.Failed(String.Format("Hasło powinno miec co najmniej {0} zanków", RequiredLength)));
            }
            //^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*(_|[^\w])).+$
            string patternUppercase = @"[A-Z]";
            if (RequireUppercase && !Regex.IsMatch(item, patternUppercase))
            {
                return Task.FromResult(IdentityResult.Failed("Wymagana jest co najmniej jedna wielka litera"));
            }
            string patternLowercase = @"[a-z]";
            if (RequireLowercase && !Regex.IsMatch(item, patternLowercase))
            {
                return Task.FromResult(IdentityResult.Failed("Wymagana jest co najmniej jedna mała litera"));
            }
            string patternDigit = @"[0-9]";
            if (RequireDigit && !Regex.IsMatch(item, patternDigit))
            {
                return Task.FromResult(IdentityResult.Failed("Wymagana jest co najmniej jedna cyfra"));
            }

            string patternSpecial = @"[_\W]";
            if (RequireNonLetterOrDigit && !Regex.IsMatch(item, patternSpecial))
            {
                return Task.FromResult(IdentityResult.Failed("Wymagany jest przynajmniej jeden znak specjalny"));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}