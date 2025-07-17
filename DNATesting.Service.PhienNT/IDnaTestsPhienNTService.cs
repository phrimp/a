using DNATesting.Repository.PhienNT;
using DNATesting.Repository.PhienNT.ModelExtensions;
using DNATesting.Repository.PhienNT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNATesting.Services.PhienNT
{
    public interface IDnaTestsPhienNtService
    {
        Task<List<DnaTestsPhienNt>> GetAllAsync();
        Task<DnaTestsPhienNt> GetByIdAsync(int id);
        Task<List<DnaTestsPhienNt>> SearchAsync(string testType, bool? isCompleted);
        Task<int> CreateAsync(DnaTestsPhienNt dnaTest);
        Task<int> UpdateAsync(DnaTestsPhienNt dnaTest);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<DnaTestsPhienNt>>> SearchWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize);
        Task<PaginationResult<List<DnaTestsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize);
    }
}