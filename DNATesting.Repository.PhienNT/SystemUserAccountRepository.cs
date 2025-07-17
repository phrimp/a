using DNATesting.Repository.PhienNT.Basic;
using DNATesting.Repository.PhienNT.DBContext;
using DNATesting.Repository.PhienNT.Basic;
using DNATesting.Repository.PhienNT.DBContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNATesting.Repository.PhienNT.Models;

namespace DNATesting.Repository.PhienNT
{
    public class SystemUserAccountRepository : GenericRepository<SystemUserAccount>
    {
        public SystemUserAccountRepository() { }
        public SystemUserAccountRepository(Se18Prn232Se1730G3DnatestingSystemContext context) => _context = context;
        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            // return await _context.UserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password); -> username
            // return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Email == username && u.Password == password); -> email
            // return await _context.UserAccounts.FirstOrDefaultAsync(u => u.Phone == username && u.Password == password); -> phone
            // return await _context.UserAccounts.FirstOrDefaultAsync(u => u.EmployeeCode == username && u.Password == password);

            return await _context.SystemUserAccounts.FirstOrDefaultAsync(u => u.UserName == username && u.Password == password);
        }
    }
}
