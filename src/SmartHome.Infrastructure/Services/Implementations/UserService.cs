using System;
using System.Linq;
using SmartHome.Core.Models;
using SmartHome.Core.Services.Abstractions;
using SmartHome.Infrastructure.DbContexts.Abstractions;

namespace SmartHome.Infrastructure.Services.Implementations
{
    /// <inheritdoc cref="IUserService" />
    public class UserService : IUserService, IDisposable
    {
        private readonly ISmartHomeDbContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="UserService"/> class with a context.
        /// </summary>
        /// <param name="context">SmartHome database context</param>
        public UserService(ISmartHomeDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public User GetUser(string username)
        {
            return _context.Users.SingleOrDefault(u => u.IsEnabled && u.Username == username);
        }

        /// <inheritdoc />
        public User GetUser(Guid userId)
        {
            return _context.Users.SingleOrDefault(u => u.IsEnabled && u.Id == userId);
        }

        /// <inheritdoc />
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}