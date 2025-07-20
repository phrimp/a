using DNATesting.Services.PhienNT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Service.PhienNT
{
    public interface IServiceProvider
    {
        SystemUserAccountService UserAccountService { get; }
        DnaTestsPhienNtService DnaTestsPhienNtService { get; }
        LociPhienNtService LociPhienNtService { get;  }
    }

    public class ServiceProvider : IServiceProvider
    {
        SystemUserAccountService _userAccountService;
        DnaTestsPhienNtService _dnaTestsPhienNtService;
        LociPhienNtService _lociPhienNtService;

        public ServiceProvider() { }

        public SystemUserAccountService UserAccountService
        {
            get { return _userAccountService ??= new SystemUserAccountService(); }
        }

        public DnaTestsPhienNtService DnaTestsPhienNtService
        {
            get { return _dnaTestsPhienNtService ??= new DnaTestsPhienNtService(); }
        }

        public LociPhienNtService LociPhienNtService
        {
            get { return _lociPhienNtService ??= new LociPhienNtService(); }
        }
    }
}
