using Microsoft.EntityFrameworkCore;
using System.Linq;
using TestProject.Models;

namespace TestProject.Data.UserRepo {

    public interface IUser {
        IQueryable<UserModel> Set();
    }

    public class UserRepo : IUser {
        protected DbContext _ctx;

        public UserRepo(DbContext ctx) {
            _ctx = ctx;
        }

        public IQueryable<UserModel> Set() {
            return _ctx.Set<UserModel>();
        }
    }
}

