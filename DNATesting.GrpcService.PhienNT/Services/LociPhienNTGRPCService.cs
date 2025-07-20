using DNATesting.GrpcService.PhienNT.Protos;
using DNATesting.Repository.PhienNT;
using DNATesting.Service.PhienNT;
using Grpc.Core;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DNATesting.GrpcService.PhienNT.Services
{
    public class LociPhienNTGRPCService : LociPhienNTGRPC.LociPhienNTGRPCBase
    {
        private readonly IServiceProviders _serviceProviders;

        public LociPhienNTGRPCService(IServiceProviders serviceProviders)
        {
            _serviceProviders = serviceProviders;
        }

        public override async Task<LociPhienNTList> GetAllAsync(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var loci = await _serviceProviders.LociPhienNtService.GetAllAsync();
                var response = new LociPhienNTList();

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                foreach (var locus in loci)
                {
                    var jsonString = JsonSerializer.Serialize(locus, options);
                    var grpcModel = JsonSerializer.Deserialize<LociPhienNT>(jsonString, options);

                    if (grpcModel != null)
                    {
                        response.Loci.Add(grpcModel);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in GetAllAsync: {ex.Message}"));
            }
        }

        public override async Task<LociPhienNT> GetByIdAsync(GetByIdRequest request, ServerCallContext context)
        {
            try
            {
                var locus = await _serviceProviders.LociPhienNtService.GetByIdAsync(request.Id);

                if (locus == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Locus not found"));
                }

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonString = JsonSerializer.Serialize(locus, options);
                var grpcModel = JsonSerializer.Deserialize<LociPhienNT>(jsonString, options);

                return grpcModel ?? new LociPhienNT();
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

        public override async Task<LociPhienNT> GetByNameAsync(GetByNameRequest request, ServerCallContext context)
        {
            try
            {
                var locus = await _serviceProviders.LociPhienNtService.GetByNameAsync(request.Name);

                if (locus == null)
                {
                    throw new RpcException(new Status(StatusCode.NotFound, "Locus not found"));
                }

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var jsonString = JsonSerializer.Serialize(locus, options);
                var grpcModel = JsonSerializer.Deserialize<LociPhienNT>(jsonString, options);

                return grpcModel ?? new LociPhienNT();
            }
            catch (RpcException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in GetByNameAsync: {ex.Message}"));
            }
        }

        public override async Task<LociPaginationResult> SearchAsync(SearchLociRequest request, ServerCallContext context)
        {
            try
            {
                var result = await _serviceProviders.LociPhienNtService.SearchWithPagingAsync(
                    request.Name,
                    request.IsCodis,
                    request.Page,
                    request.PageSize);

                var response = new LociPaginationResult
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

                foreach (var locus in result.Items)
                {
                    var jsonString = JsonSerializer.Serialize(locus, options);
                    var grpcModel = JsonSerializer.Deserialize<LociPhienNT>(jsonString, options);

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

        public override async Task<LociPhienNTList> GetCodisLociAsync(EmptyRequest request, ServerCallContext context)
        {
            try
            {
                var loci = await _serviceProviders.LociPhienNtService.GetCodisLociAsync();
                var response = new LociPhienNTList();

                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                foreach (var locus in loci)
                {
                    var jsonString = JsonSerializer.Serialize(locus, options);
                    var grpcModel = JsonSerializer.Deserialize<LociPhienNT>(jsonString, options);

                    if (grpcModel != null)
                    {
                        response.Loci.Add(grpcModel);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in GetCodisLociAsync: {ex.Message}"));
            }
        }

        public override async Task<MutationResult> CreateAsync(LociPhienNT request, ServerCallContext context)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var protoJsonString = JsonSerializer.Serialize(request, options);
                var domainModel = JsonSerializer.Deserialize<DNATesting.Repository.PhienNT.LociPhienNt>(protoJsonString, options);

                if (domainModel == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid locus data"));
                }

                var result = await _serviceProviders.LociPhienNtService.CreateAsync(domainModel);

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

        public override async Task<MutationResult> UpdateAsync(LociPhienNT request, ServerCallContext context)
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    ReferenceHandler = ReferenceHandler.IgnoreCycles,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
                };

                var protoJsonString = JsonSerializer.Serialize(request, options);
                var domainModel = JsonSerializer.Deserialize<DNATesting.Repository.PhienNT.LociPhienNt>(protoJsonString, options);

                if (domainModel == null)
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid locus data"));
                }

                var result = await _serviceProviders.LociPhienNtService.UpdateAsync(domainModel);

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
                var result = await _serviceProviders.LociPhienNtService.DeleteAsync(request.Id);

                return new MutationResult() { Result = result ? 1 : 0 };
            }
            catch (Exception ex)
            {
                throw new RpcException(new Status(StatusCode.Internal, $"Error in DeleteAsync: {ex.Message}"));
            }
        }
    }
}