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
    public class LocusMatchResultsPhienNtRepository : GenericRepository<LocusMatchResultsPhienNt>
    {
        public LocusMatchResultsPhienNtRepository() { }
        public LocusMatchResultsPhienNtRepository(Se18Prn232Se1730G3DnatestingSystemContext context) => _context = context;

        public async Task<List<LocusMatchResultsPhienNt>> GetAllAsync()
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .ToListAsync();
            return results ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<LocusMatchResultsPhienNt> GetByIdAsync(int id)
        {
            var result = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .FirstOrDefaultAsync(lmr => lmr.PhienNtid == id);
            return result ?? new LocusMatchResultsPhienNt();
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetByTestIdAsync(int testId)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .Where(lmr => lmr.TestId == testId)
                .ToListAsync();
            return results ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<List<LocusMatchResultsPhienNt>> SearchAsync(int testId, int locusId, bool? isMatch)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .Where(lmr => (testId == 0 || lmr.TestId == testId)
                           && (locusId == 0 || lmr.LocusId == locusId)
                           && (!isMatch.HasValue || lmr.IsMatch == isMatch))
                .ToListAsync();
            return results ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<PaginationResult<List<LocusMatchResultsPhienNt>>> SearchWithPagingAsync(int testId, int locusId, bool? isMatch, int page, int pageSize)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .Where(lmr => (testId == 0 || lmr.TestId == testId)
                           && (locusId == 0 || lmr.LocusId == locusId)
                           && (!isMatch.HasValue || lmr.IsMatch == isMatch))
                .ToListAsync();
            var totalItems = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            results = results.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new PaginationResult<List<LocusMatchResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = results
            };
            return result;
        }

        public async Task<PaginationResult<List<LocusMatchResultsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            var results = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .ToListAsync();
            var totalItems = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            results = results.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<LocusMatchResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = results
            };
            return result;
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetMatchesAsync()
        {
            var matches = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .Where(lmr => lmr.IsMatch == true)
                .ToListAsync();
            return matches ?? new List<LocusMatchResultsPhienNt>();
        }

        public async Task<List<LocusMatchResultsPhienNt>> GetNonMatchesAsync()
        {
            var nonMatches = await _context.LocusMatchResultsPhienNts
                .Include(lmr => lmr.Locus)
                .Include(lmr => lmr.Test)
                .Where(lmr => lmr.IsMatch == false)
                .ToListAsync();
            return nonMatches ?? new List<LocusMatchResultsPhienNt>();
        }
    }
}