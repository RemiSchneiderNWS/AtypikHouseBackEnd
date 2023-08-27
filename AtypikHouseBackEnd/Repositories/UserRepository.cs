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
        string lowerCaseMail = userLog.Mail.ToLowerInvariant();
        User user = Set.FirstOrDefault(user => user.Mail == lowerCaseMail && user.Password == userLog.Password);
        return user;
        
   }
    public bool UserExist(string mail)
    {
        User user  = Set.FirstOrDefault( user => user.Mail == mail);
        if(user == null)
        {
            return  false;
        }
        return true;
    }
}