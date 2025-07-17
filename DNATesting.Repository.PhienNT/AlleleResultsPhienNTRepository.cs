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
    public class AlleleResultsPhienNtRepository : GenericRepository<AlleleResultsPhienNt>
    {
        public AlleleResultsPhienNtRepository() { }
        public AlleleResultsPhienNtRepository(Se18Prn232Se1730G3DnatestingSystemContext context) => _context = context;

        public async Task<List<AlleleResultsPhienNt>> GetAllAsync()
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .ToListAsync();
            return results ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<AlleleResultsPhienNt> GetByIdAsync(int id)
        {
            var result = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .FirstOrDefaultAsync(ar => ar.PhienNtid == id);
            return result ?? new AlleleResultsPhienNt();
        }

        public async Task<List<AlleleResultsPhienNt>> GetByTestIdAsync(int testId)
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .Where(ar => ar.TestId == testId)
                .ToListAsync();
            return results ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<List<AlleleResultsPhienNt>> GetByProfileIdAsync(int profileId)
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .Where(ar => ar.ProfileThinhLcid == profileId)
                .ToListAsync();
            return results ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<List<AlleleResultsPhienNt>> SearchAsync(int testId, int profileId, string role)
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .Where(ar => (testId == 0 || ar.TestId == testId)
                          && (profileId == 0 || ar.ProfileThinhLcid == profileId)
                          && (string.IsNullOrEmpty(role) || ar.Role.Contains(role)))
                .ToListAsync();
            return results ?? new List<AlleleResultsPhienNt>();
        }

        public async Task<PaginationResult<List<AlleleResultsPhienNt>>> SearchWithPagingAsync(int testId, int profileId, string role, int page, int pageSize)
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .Where(ar => (testId == 0 || ar.TestId == testId)
                          && (profileId == 0 || ar.ProfileThinhLcid == profileId)
                          && (string.IsNullOrEmpty(role) || ar.Role.Contains(role)))
                .ToListAsync();
            var totalItems = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            results = results.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new PaginationResult<List<AlleleResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = results
            };
            return result;
        }

        public async Task<PaginationResult<List<AlleleResultsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            var results = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .ToListAsync();
            var totalItems = results.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            results = results.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<AlleleResultsPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = results
            };
            return result;
        }

        public async Task<List<AlleleResultsPhienNt>> GetOutliersAsync()
        {
            var outliers = await _context.AlleleResultsPhienNts
                .Include(ar => ar.Locus)
                .Include(ar => ar.ProfileThinhLc)
                .Include(ar => ar.Test)
                .Where(ar => ar.IsOutlier == true)
                .ToListAsync();
            return outliers ?? new List<AlleleResultsPhienNt>();
        }
    }
}