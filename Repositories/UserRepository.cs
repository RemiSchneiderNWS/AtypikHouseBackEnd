using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using System.Security.Claims;

namespace Repositories;

public class UserRepository : Repository<User>
{
 
    public UserRepository(AppDbContext ctx) : base(ctx)
    {
        Set = ctx.Users;
    }

   public User Login(LogUser userLog) 
   {
       
       User user = Set.Where(user => (user.Mail == userLog.Mail) && (user.Password == userLog.Password)).FirstOrDefault();
       
       return (user);
   }
}