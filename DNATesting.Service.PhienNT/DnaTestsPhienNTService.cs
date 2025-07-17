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
    public class DnaTestsPhienNtService : IDnaTestsPhienNtService
    {
        private readonly DnaTestsPhienNtRepository _repository;

        public DnaTestsPhienNtService() => _repository = new DnaTestsPhienNtRepository();

        public async Task<int> CreateAsync(DnaTestsPhienNt dnaTest)
        {
            return await _repository.CreateAsync(dnaTest);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dnaTest = _repository.GetById(id);
            return await _repository.RemoveAsync(dnaTest);
        }

        public async Task<List<DnaTestsPhienNt>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> GetAllWithPagingAsync(int page, int pageSize)
        {
            return await _repository.GetAllWithPagingAsync(page, pageSize);
        }

        public async Task<DnaTestsPhienNt> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<List<DnaTestsPhienNt>> SearchAsync(string testType, bool? isCompleted)
        {
            return await _repository.SearchAsync(testType, isCompleted);
        }

        public async Task<PaginationResult<List<DnaTestsPhienNt>>> SearchWithPagingAsync(string testType, bool? isCompleted, int page, int pageSize)
        {
            var paginationResult = await _repository.SearchWithPagingAsync(testType, isCompleted, page, pageSize);
            return paginationResult ?? new PaginationResult<List<DnaTestsPhienNt>>();
        }

        public async Task<int> UpdateAsync(DnaTestsPhienNt dnaTest)
        {
            return await _repository.UpdateAsync(dnaTest);
        }
    }
}