using DNATesting.Repository.PhienNT;
using DNATesting.Repository.PhienNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Services.PhienNT
{
    public class SystemUserAccountService
    {
        private readonly SystemUserAccountRepository _userAccountRepository;
        public SystemUserAccountService() => _userAccountRepository = new SystemUserAccountRepository();

        public async Task<SystemUserAccount> GetUserAccount(string username, string password)
        {
            return await _userAccountRepository.GetUserAccount(username, password) ;
        }
    }
}
