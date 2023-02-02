namespace JwtTokenDemo.DAL
{
    public class JwtRepository : IJwtRepository
    {
        private readonly JwtDemoDbContext _context;
        public JwtRepository(JwtDemoDbContext context) 
        {
            _context = context;
        }

        //public Account Getaccount(string username)
        //{
        //    return _context.Accounts.FirstOrDefault(a => a.UserName == username);
        //}

        public Account GetAccount(string username)
        {
            return _context.Accounts.FirstOrDefault(a => a.UserName == username);
        }

        //public void Saveaccount(Account account)
        //{
        //    _context.Accounts.Add(account);
        //    _context.SaveChanges();
        //}

        public void SaveAccount(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
    }
}
