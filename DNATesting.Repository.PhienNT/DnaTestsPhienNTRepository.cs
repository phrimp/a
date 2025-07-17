using DNATesting.Repository.PhienNT.Basic;
using DNATesting.Repository.PhienNT.DBContext;
using DNATesting.Repository.PhienNT.ModelExtensions;
using DNATesting.Repository.PhienNT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Repository.PhienNT
{
    public class DnaTestsPhienNtRepository : GenericRepository<DnaTestsPhienNt>
    {
        public DnaTestsPhienNtRepository() { }
        public DnaTestsPhienNtRepository(Se18Prn232Se1730G3DnatestingSystemContext context) => _context = context;

        public async Task<List<DnaTestsPhienNt>> GetAllAsync()
        {
            var tests = await _context.DnaTestsPhienNts
                .Include(t => t.AlleleResultsPhienNts)
                .Include(t => t.LocusMatchResultsPhienNts)
                .ToListAsync();
            return tests ?? new List<DnaTestsPhienNt>();
        }

        public async Task<DnaTestsPhienNt> GetByIdAsync(int id)
        {
            var test = await _context.DnaTestsPhienNts
                .Include(t => t.AlleleResultsPhienNts)
                    .ThenInclude(ar => ar.Locus)
                .Include(t => t.AlleleResultsPhienNts)
                    .ThenInclude(ar => ar.ProfileThinhLc)
                .Include(t => t.LocusMatchResultsPhienNts)
                    .ThenInclude(lmr => lmr.Locus)
                .FirstOrDefaultAsync(t => t.PhienNtid == id);
            return test ?? new DnaTestsPhienNt();
        }

        public async Task<List<DnaTestsPhienNt>> SearchAsync(string testType, bool? isCompleted)
        {
            var tests = await _context.DnaTestsPhienNts
                .Include(t => t.AlleleResultsPhienNts)
                .Include(t => t.LocusMatchResultsPhienNts)
                .Where(t => (string.IsNullOrEmpty(testType) || t.TestType.Contains(testType))
                         && (!isCompleted.HasValue || t.IsCompleted == isCompleted))
                .ToListAsync();
            return tests ?? new List<DnaTestsPhienNt>();
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> SearchWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize)
        {
            var tests = await _context.DnaTestsPhienNts
                .Include(t => t.AlleleResultsPhienNts)
                .Include(t => t.LocusMatchResultsPhienNts)
                .Where(t => (string.IsNullOrEmpty(testType) || t.TestType.Contains(testType))
                         && (!isCompleted.HasValue || t.IsCompleted == isCompleted))
                .ToListAsync();
            var totalItems = tests.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            tests = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new PaginationResult<List<DnaTestsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = tests
            };
            return result;
        }


        public async Task<PaginationResult<List<DnaTestsPhienNt>>> SearchWithPagingAsyncWithRequest(SearchDnaTestsRequest searchDnaTestsRequest)
        {
            return await SearchWithPagingAsync(searchDnaTestsRequest.testType, searchDnaTestsRequest.isCompleted, searchDnaTestsRequest.page, searchDnaTestsRequest.pageSize);
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            var tests = await _context.DnaTestsPhienNts
                .Include(t => t.AlleleResultsPhienNts)
                .Include(t => t.LocusMatchResultsPhienNts)
                .ToListAsync();
            var totalItems = tests.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            tests = tests.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<DnaTestsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = tests
            };
            return result;
        }
    }
}