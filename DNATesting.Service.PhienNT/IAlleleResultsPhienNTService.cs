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
    public interface IAlleleResultsPhienNtService
    {
        Task<List<AlleleResultsPhienNt>> GetAllAsync();
        Task<AlleleResultsPhienNt> GetByIdAsync(int id);
        Task<List<AlleleResultsPhienNt>> GetByTestIdAsync(int testId);
        Task<List<AlleleResultsPhienNt>> GetByProfileIdAsync(int profileId);
        Task<List<AlleleResultsPhienNt>> SearchAsync(int testId, int profileId, string role);
        Task<List<AlleleResultsPhienNt>> GetOutliersAsync();
        Task<int> CreateAsync(AlleleResultsPhienNt alleleResult);
        Task<int> UpdateAsync(AlleleResultsPhienNt alleleResult);
        Task<bool> DeleteAsync(int id);
        Task<PaginationResult<List<AlleleResultsPhienNt>>> SearchWithPagingAsync(int testId, int profileId, string role, int page, int pageSize);
        Task<PaginationResult<List<AlleleResultsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize);
    }
}