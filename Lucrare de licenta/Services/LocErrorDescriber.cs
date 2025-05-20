using Microsoft.AspNetCore.Identity;

namespace Lucrare_de_licenta.Services
{
    public class LocErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DefaultError()
            => new IdentityError { Code = nameof(DefaultError), Description = "A apărut o eroare neprevăzută." };

        public override IdentityError ConcurrencyFailure()
            => new IdentityError { Code = nameof(ConcurrencyFailure), Description = "Eroare de concurență optimistă, obiectul a fost modificat." };

        public override IdentityError PasswordMismatch()
            => new IdentityError { Code = nameof(PasswordMismatch), Description = "Parolă incorectă." };

        public override IdentityError InvalidToken()
            => new IdentityError { Code = nameof(InvalidToken), Description = "Token invalid." };

        public override IdentityError LoginAlreadyAssociated()
            => new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = "Un utilizator cu acest nume de utilizator există deja." };

        public override IdentityError InvalidUserName(string? userName)
            => new IdentityError { Code = nameof(InvalidUserName), Description = $"Numele de utilizator '{userName}' este invalid, poate conține doar litere sau cifre." };

        public override IdentityError InvalidEmail(string? email)
            => new IdentityError { Code = nameof(InvalidEmail), Description = $"Adresa de email '{email}' este invalidă." };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError { Code = nameof(DuplicateUserName), Description = $"Numele de utilizator '{userName}' este deja folosit." };

        public override IdentityError DuplicateEmail(string email)
            => new IdentityError { Code = nameof(DuplicateEmail), Description = $"Adresa de email '{email}' este deja folosită." };

        public override IdentityError InvalidRoleName(string? role)
            => new IdentityError { Code = nameof(InvalidRoleName), Description = $"Numele rolului '{role}' este invalid." };

        public override IdentityError DuplicateRoleName(string role)
            => new IdentityError { Code = nameof(DuplicateRoleName), Description = $"Numele rolului '{role}' este deja folosit." };

        public override IdentityError UserAlreadyHasPassword()
            => new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = "Utilizatorul are deja o parolă setată." };

        public override IdentityError UserLockoutNotEnabled()
            => new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = "Blocarea nu este activată pentru acest utilizator." };

        public override IdentityError UserAlreadyInRole(string role)
            => new IdentityError { Code = nameof(UserAlreadyInRole), Description = $"Utilizatorul este deja în rolul '{role}'." };

        public override IdentityError UserNotInRole(string role)
            => new IdentityError { Code = nameof(UserNotInRole), Description = $"Utilizatorul nu este în rolul '{role}'." };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError { Code = nameof(PasswordTooShort), Description = $"Parolele trebuie să aibă cel puțin {length} caractere." };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = "Parolele trebuie să conțină cel puțin un caracter special (non-alfanumeric)." };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError { Code = nameof(PasswordRequiresDigit), Description = "Parolele trebuie să conțină cel puțin o cifră ('0'-'9')." };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError { Code = nameof(PasswordRequiresLower), Description = "Parolele trebuie să conțină cel puțin o literă mică ('a'-'z')." };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError { Code = nameof(PasswordRequiresUpper), Description = "Parolele trebuie să conțină cel puțin o literă mare ('A'-'Z')." };

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            => new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = $"Parolele trebuie să conțină cel puțin {uniqueChars} caractere diferite." };
    }
}
