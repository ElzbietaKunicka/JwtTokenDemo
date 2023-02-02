using System.Security.Cryptography;
using System.Text;
using JwtTokenDemo.DAL;

namespace JwtTokenDemo.BL
{
    public class AccountService : IAccountService
    {
        private readonly IJwtRepository _jwtRepository;
        public AccountService(IJwtRepository jwtRepository) 
        {
            _jwtRepository = jwtRepository;
        }
        public bool Login(string username, string password)
        {
            var account = _jwtRepository.GetAccount(username);

            if(account == null)
            {
                return false;
            }
            return VerifyPasswordHash(password, account.PasswordHash, account.PasswordSalt);
        }

        public Account SignupNewAccount(string username, string password)
        {
            var account = CreateAccount(username, password);
            _jwtRepository.SaveAccount(account);
            return account;
        }

        private Account CreateAccount(string username, string password)
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
            var account = new Account
            {
                UserName = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return account;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordhash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(passwordhash);
        }
    }
}
