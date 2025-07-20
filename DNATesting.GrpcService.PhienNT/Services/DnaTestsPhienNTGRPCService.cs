using DNATesting.GrpcService.PhienNT.Protos;
using DNATesting.Repository.PhienNT.ModelExtensions;
using DNATesting.Repository.PhienNT;
using DNATesting.Service.PhienNT;
using Grpc.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using SearchDnaTestsRequest = DNATesting.GrpcService.PhienNT.Protos.SearchDnaTestsRequest;

namespace DNATesting.GrpcService.PhienNT.Services
{
    public class DnaTestsPhienNTGRPCService : DnaTestsPhienNTGRPC.DnaTestsPhienNTGRPCBase
    {
        private readonly IServiceProviders _serviceProviders;

        public DnaTestsPhienNTGRPCService(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public override async Task<DnaTestsPhienNTList> GetAllAsync(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var tests = await _serviceProviders.DnaTestsPhienNtService.GetAllAsync();
                var response = new DnaTestsPhienNTList();

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                foreach (var test in tests)
                {
                    var jsonString = JsonSerializer.Serialize(test, options);
                    var grpcModel = JsonSerializer.Deserialize<DnaTestsPhienNT>(jsonString, options);

                    if (grpcModel != null)
                    {
                        response.Tests.Add(grpcModel);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in GetAllAsync: {ex.Message}"));
            }
        }

        public override async Task<DnaTestsPhienNT> GetByIdAsync(GetByIdRequest request, ServerCallContext context)
        {
            try
            {
                var test = await _serviceProviders.DnaTestsPhienNtService.GetByIdAsync(request.Id);

                if (test == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "DNA Test not found"));
                }

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonString = JsonSerializer.Serialize(test, options);
                var grpcModel = JsonSerializer.Deserialize<DnaTestsPhienNT>(jsonString, options);

                return grpcModel ?? new DnaTestsPhienNT();
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in GetByIdAsync: {ex.Message}"));
            }
        }

        public override async Task<DnaTestsPaginationResult> SearchAsync(SearchDnaTestsRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _serviceProviders.DnaTestsPhienNtService.SearchWithPagingAsync(
                    request.TestType,
                    request.IsCompleted,
                    request.Page,
                    request.PageSize);

                var response = new DnaTestsPaginationResult
                {
                    TotalItems = result.TotalItems,
                    TotalPages = result.TotalPages,
                    CurrentPage = result.CurrentPage,
                    PageSize = result.PageSize
                };

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                foreach (var test in result.Items)
                {
                    var jsonString = JsonSerializer.Serialize(test, options);
                    var grpcModel = JsonSerializer.Deserialize<DnaTestsPhienNT>(jsonString, options);

                    if (grpcModel != null)
                    {
                        response.Items.Add(grpcModel);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in SearchAsync: {ex.Message}"));
            }
        }

        public override async Task<MutationResult> CreateAsync(DnaTestsPhienNT request, ServerCallContext context)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var protoJsonString = JsonSerializer.Serialize(request, options);
                var domainModel = JsonSerializer.Deserialize<DNATesting.Repository.PhienNT.DnaTestsPhienNt>(protoJsonString, options);

                if (domainModel == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid DNA test data"));
                }

                var result = await _serviceProviders.DnaTestsPhienNtService.CreateAsync(domainModel);

                return new MutationResult() { Result = result };
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in CreateAsync: {ex.Message}"));
            }
        }

        public override async Task<MutationResult> UpdateAsync(DnaTestsPhienNT request, ServerCallContext context)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var protoJsonString = JsonSerializer.Serialize(request, options);
                var domainModel = JsonSerializer.Deserialize<DNATesting.Repository.PhienNT.DnaTestsPhienNt>(protoJsonString, options);

                if (domainModel == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid DNA test data"));
                }

                var result = await _serviceProviders.DnaTestsPhienNtService.UpdateAsync(domainModel);

                return new MutationResult() { Result = result };
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in UpdateAsync: {ex.Message}"));
            }
        }

        public override async Task<MutationResult> DeleteAsync(DeleteRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _serviceProviders.DnaTestsPhienNtService.DeleteAsync(request.Id);

                return new MutationResult() { Result = result ? 1 : 0 };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in DeleteAsync: {ex.Message}"));
            }
        }
    }
}