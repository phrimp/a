using DNATesting.Repository.PhienNT.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Repository.PhienNT
{
    public interface IUnitOfWork : IDisposable
    {
        SystemUserAccountRepository UserAccountRepository { get; }
        DnaTestsPhienNtRepository DnaTestsPhienNtRepository { get; }
        LociPhienNtRepository LociPhienNtRepository { get; }
        AlleleResultsPhienNtRepository AlleleResultsPhienNtRepository { get; }
        LocusMatchResultsPhienNtRepository LocusMatchResultsPhienNtRepository{ get; }

        int SaveChangesWithTransaction();
        Task<int> SaveChangesWithTransactionAsync();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly Se18Prn232Se1730G3DnatestingSystemContext _context;
        private SystemUserAccountRepository _userAccountRepository;
        private LocusMatchResultsPhienNtRepository _locusMatchResultsPhienNtRepository;
        private DnaTestsPhienNtRepository _dnaTestsPhienNtRepository;
        private LociPhienNtRepository _lociPhienNtRepository;
        private AlleleResultsPhienNtRepository _alleleResultsPhienNtRepository;

        public UnitOfWork() => _context ??= new Se18Prn232Se1730G3DnatestingSystemContext();

        public SystemUserAccountRepository UserAccountRepository
        {
            get { return _userAccountRepository ??= new SystemUserAccountRepository(_context); }
        }

        public DnaTestsPhienNtRepository DnaTestsPhienNtRepository
        {
            get { return _dnaTestsPhienNtRepository ??= new DnaTestsPhienNtRepository(_context); }
        }

        public LociPhienNtRepository LociPhienNtRepository
        {
            get { return _lociPhienNtRepository ??= new LociPhienNtRepository(_context);  }
        }

        public AlleleResultsPhienNtRepository AlleleResultsPhienNtRepository
        {
            get { return _alleleResultsPhienNtRepository ??= new AlleleResultsPhienNtRepository(_context); }
        }

        public LocusMatchResultsPhienNtRepository LocusMatchResultsPhienNtRepository => throw new NotImplementedException();

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChangesWithTransaction()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = _context.SaveChanges();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {                  
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }

        public async Task<int> SaveChangesWithTransactionAsync()
        {
            int result = -1;

            using (var dbContextTransaction = _context.Database.BeginTransaction())
            {
                try
                {
                    result = await _context.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception)
                {
                    //Log Exception Handling message                      
                    result = -1;
                    dbContextTransaction.Rollback();
                }
            }

            return result;
        }
    }
}
