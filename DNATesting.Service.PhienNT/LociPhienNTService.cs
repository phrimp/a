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
    public class LociPhienNtService : ILociPhienNtService
    {
        private readonly LociPhienNtRepository _repository;

        public LociPhienNtService() => _repository = new LociPhienNtRepository();

        public async Task<int> CreateAsync(LociPhienNt locus)
        {
            return await _repository.CreateAsync(locus);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var locus = _repository.GetById(id);
            return await _repository.RemoveAsync(locus);
        }

        public async Task<List<LociPhienNt>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginationResult<List<LociPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            return await _repository.GetAllWithPagingAsync(page, pageSize);
        }

        public async Task<LociPhienNt> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<LociPhienNt> GetByNameAsync(string name)
        {
            return await _repository.GetByNameAsync(name);
        }

        public async Task<List<LociPhienNt>> GetCodisLociAsync()
        {
            return await _repository.GetCodisLociAsync();
        }

        public async Task<List<LociPhienNt>> SearchAsync(string name, bool? isCodis)
        {
            return await _repository.SearchAsync(name, isCodis);
        }

        public async Task<PaginationResult<List<LociPhienNt>>> SearchWithPagingAsync(string name, bool? isCodis, int page, int pageSize)
        {
            var paginationResult = await _repository.SearchWithPagingAsync(name, isCodis, page, pageSize);
            return paginationResult ?? new PaginationResult<List<LociPhienNt>>();
        }

        public async Task<int> UpdateAsync(LociPhienNt locus)
        {
            return await _repository.UpdateAsync(locus);
        }
    }
}