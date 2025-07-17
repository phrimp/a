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
    public class LociPhienNtRepository : GenericRepository<LociPhienNt>
    {
        public LociPhienNtRepository() { }
        public LociPhienNtRepository(Se18Prn232Se1730G3DnatestingSystemContext context) => _context = context;

        public async Task<List<LociPhienNt>> GetAllAsync()
        {
            var loci = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .ToListAsync();
            return loci ?? new List<LociPhienNt>();
        }

        public async Task<LociPhienNt> GetByIdAsync(int id)
        {
            var locus = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .FirstOrDefaultAsync(l => l.PhienNtid == id);
            return locus ?? new LociPhienNt();
        }

        public async Task<LociPhienNt> GetByNameAsync(string name)
        {
            var locus = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .FirstOrDefaultAsync(l => l.Name == name);
            return locus ?? new LociPhienNt();
        }

        public async Task<List<LociPhienNt>> SearchAsync(string name, bool? isCodis)
        {
            var loci = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .Where(l => (string.IsNullOrEmpty(name) || l.Name.Contains(name))
                         && (!isCodis.HasValue || l.IsCodis == isCodis))
                .ToListAsync();
            return loci ?? new List<LociPhienNt>();
        }

        public async Task<PaginationResult<List<LociPhienNt>>> SearchWithPagingAsync(string name, bool? isCodis, int page, int pageSize)
        {
            var loci = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .Where(l => (string.IsNullOrEmpty(name) || l.Name.Contains(name))
                         && (!isCodis.HasValue || l.IsCodis == isCodis))
                .ToListAsync();
            var totalItems = loci.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            loci = loci.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var result = new PaginationResult<List<LociPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = loci
            };
            return result;
        }

        public async Task<PaginationResult<List<LociPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            var loci = await _context.LociPhienNts
                .Include(l => l.AlleleResultsPhienNts)
                .Include(l => l.LocusMatchResultsPhienNts)
                .ToListAsync();
            var totalItems = loci.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            loci = loci.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var result = new PaginationResult<List<LociPhienNt>>
            {
                TotalItems = totalItems,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Items = loci
            };
            return result;
        }

        public async Task<List<LociPhienNt>> GetCodisLociAsync()
        {
            var codisLoci = await _context.LociPhienNts
                .Where(l => l.IsCodis == true)
                .ToListAsync();
            return codisLoci ?? new List<LociPhienNt>();
        }
    }
}