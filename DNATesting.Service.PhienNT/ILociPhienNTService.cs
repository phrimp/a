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
    public interface ILociPhienNtService
    {
        Task<List<LociPhienNt>> GetAllAsync();
        Task<LociPhienNt> GetByIdAsync(int id);
        Task<LociPhienNt> GetByNameAsync(string name);
        Task<List<LociPhienNt>> SearchAsync(string name, bool? isCodis);
        Task<List<LociPhienNt>> GetCodisLociAsync();
        Task<int> CreateAsync(LociPhienNt locus);
        Task<int> UpdateAsync(LociPhienNt locus);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<LociPhienNt>>> SearchWithPagingAsync(string name, bool? isCodis, int page, int pageSize);
        Task<PaginationResult<List<LociPhienNt>>> GetAllWithPagingAsync(int page, int pageSize);
    }
}