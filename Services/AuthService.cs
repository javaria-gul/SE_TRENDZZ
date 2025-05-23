using SE_TRENDZZ.Models;
using SE_TRENDZZ.Helpers;
using System.Linq;
using System.Data.Entity;
using SE_TRENDZZ.Data;


namespace SE_TRENDZZ.Services
{
    public class AuthService
    {
        private static AuthService _instance;
        private static readonly object _lock = new object();

        private SETrendzzDBContext _context;

        // Private constructor for singleton
        private AuthService()
        {
            _context = new SETrendzzDBContext();
        }

        public static AuthService Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = new AuthService();
                    return _instance;
                }
            }
        }

        // Login method
        public User Authenticate(string email, string password)
        {
            var user = _context.Users.Include(u => u.Role)
                                    .FirstOrDefault(u => u.Email == email);

            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                return user;
            }
            return null;
        }

        // Signup method
        public bool Register(string fullName, string email, string password, int roleID)
        {
            // Check if email already exists
            var exists = _context.Users.Any(u => u.Email == email);
            if (exists)
                return false;

            var hashedPassword = PasswordHelper.HashPassword(password);

            var newUser = new User
            {
                FullName = fullName,
                Email = email,
                PasswordHash = hashedPassword,
                RoleID = roleID
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
            return true;
        }
    }
}
