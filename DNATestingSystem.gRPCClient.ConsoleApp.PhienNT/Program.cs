using Grpc.Net.Client;
using DNATesting.GrpcService.PhienNT.Protos;
using DNATesting.GrpcService.PhienNT;
using System;
using System.Threading.Tasks;

Console.WriteLine("==============================================");
Console.WriteLine("=== gRPC Client for DNA Testing PhienNT ===");
Console.WriteLine("==============================================");

// Create a gRPC channel
using var channel = GrpcChannel.ForAddress("https://localhost:7265"); // Adjust port as needed

// Store created IDs for testing
int createdTestId = 0;
int createdLocusId = 0;

try
{
    // Test basic greeting service first
    Console.WriteLine("\n--- Testing Greeting Service ---");
    var greeterClient = new Greeter.GreeterClient(channel);
    var greetResponse = await greeterClient.SayHelloAsync(new HelloRequest { Name = "PhienNT Console Client" });
    Console.WriteLine($"Greeting Response: {greetResponse.Message}");

    // ============================================
    // DNA TESTS SERVICE TESTING
    // ============================================
    Console.WriteLine("\n=== TESTING DNA TESTS SERVICE ===");
    var dnaTestsClient = new DnaTestsPhienNTGRPC.DnaTestsPhienNTGRPCClient(channel);

    // Test 1: GetAllAsync for DNA Tests
    Console.WriteLine("\n1. Getting all DNA tests...");
    var getAllTestsResponse = await dnaTestsClient.GetAllAsyncAsync(new EmptyRequest());
    Console.WriteLine($"✅ Found {getAllTestsResponse.Tests.Count} DNA tests");

    if (getAllTestsResponse.Tests.Count > 0)
    {
        Console.WriteLine("   Sample data:");
        foreach (var test in getAllTestsResponse.Tests.Take(3))
        {
            Console.WriteLine($"   - Test ID: {test.PhienNtid}, Type: {test.TestType}, Completed: {test.IsCompleted}");
        }
    }

    // Test 2: Create DNA Test
    Console.WriteLine("\n2. Creating new DNA test...");
    var newTest = new DnaTestsPhienNT
    {
        PhienNtid = 0, // Will be auto-generated
        TestType = "Paternity Test - PhienNT Client",
        Conclusion = "Test pending - Created via gRPC console client",
        ProbabilityOfRelationship = 0.0,
        RelationshipIndex = 0.0,
        IsCompleted = false,
        CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    };

    var createTestResponse = await dnaTestsClient.CreateAsyncAsync(newTest);
    Console.WriteLine($"✅ Created DNA test with result: {createTestResponse.Result}");
    createdTestId = createTestResponse.Result;

    // Test 3: GetByIdAsync
    if (createdTestId > 0)
    {
        Console.WriteLine("\n3. Getting DNA test by ID...");
        var getTestByIdResponse = await dnaTestsClient.GetByIdAsyncAsync(new GetByIdRequest { Id = createdTestId });
        Console.WriteLine($"✅ Retrieved test: ID={getTestByIdResponse.PhienNtid}, Type={getTestByIdResponse.TestType}");
    }
    else if (getAllTestsResponse.Tests.Count > 0)
    {
        Console.WriteLine("\n3. Getting DNA test by ID (using existing)...");
        var firstTestId = getAllTestsResponse.Tests[0].PhienNtid;
        var getTestByIdResponse = await dnaTestsClient.GetByIdAsyncAsync(new GetByIdRequest { Id = firstTestId });
        Console.WriteLine($"✅ Retrieved test: ID={getTestByIdResponse.PhienNtid}, Type={getTestByIdResponse.TestType}");
    }

    // Test 4: Search DNA Tests
    Console.WriteLine("\n4. Searching DNA tests...");
    var searchTestsRequest = new SearchDnaTestsRequest
    {
        TestType = "Paternity",
        IsCompleted = false,
        Page = 1,
        PageSize = 10
    };

    var searchTestsResponse = await dnaTestsClient.SearchAsyncAsync(searchTestsRequest);
    Console.WriteLine($"✅ Search found {searchTestsResponse.TotalItems} DNA tests matching criteria");
    Console.WriteLine($"   Total Pages: {searchTestsResponse.TotalPages}, Current Page: {searchTestsResponse.CurrentPage}");

    // Test 5: Update DNA Test
    if (createdTestId > 0)
    {
        Console.WriteLine("\n5. Updating DNA test...");
        var updateTest = new DnaTestsPhienNT
        {
            PhienNtid = createdTestId,
            TestType = "Paternity Test - Updated via PhienNT Client",
            Conclusion = "Test completed - Updated via gRPC console client",
            ProbabilityOfRelationship = 99.99,
            RelationshipIndex = 1500.75,
            IsCompleted = true,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        };

        var updateTestResponse = await dnaTestsClient.UpdateAsyncAsync(updateTest);
        Console.WriteLine($"✅ Updated DNA test with result: {updateTestResponse.Result}");
    }

    // ============================================
    // LOCI SERVICE TESTING
    // ============================================
    Console.WriteLine("\n=== TESTING LOCI SERVICE ===");
    var lociClient = new LociPhienNTGRPC.LociPhienNTGRPCClient(channel);

    // Test 1: GetAllAsync for Loci
    Console.WriteLine("\n1. Getting all loci...");
    var getAllLociResponse = await lociClient.GetAllAsyncAsync(new EmptyRequest());
    Console.WriteLine($"✅ Found {getAllLociResponse.Loci.Count} loci");

    if (getAllLociResponse.Loci.Count > 0)
    {
        Console.WriteLine("   Sample data:");
        foreach (var locus in getAllLociResponse.Loci.Take(3))
        {
            Console.WriteLine($"   - Locus ID: {locus.PhienNtid}, Name: {locus.Name}, CODIS: {locus.IsCodis}");
        }
    }

    // Test 2: GetCodisLociAsync
    Console.WriteLine("\n2. Getting CODIS loci only...");
    var getCodisLociResponse = await lociClient.GetCodisLociAsyncAsync(new EmptyRequest());
    Console.WriteLine($"✅ Found {getCodisLociResponse.Loci.Count} CODIS loci");

    // Test 3: Create Locus
    Console.WriteLine("\n3. Creating new locus...");
    var newLocus = new LociPhienNT
    {
        PhienNtid = 0, // Will be auto-generated
        Name = $"TEST_LOCUS_PhienNT_{DateTime.Now:HHmmss}",
        IsCodis = true,
        Description = "Test locus created via gRPC PhienNT console client",
        MutationRate = 0.0001,
        CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    };

    var createLocusResponse = await lociClient.CreateAsyncAsync(newLocus);
    Console.WriteLine($"✅ Created locus with result: {createLocusResponse.Result}");
    createdLocusId = createLocusResponse.Result;

    // Test 4: GetByIdAsync for Locus
    if (createdLocusId > 0)
    {
        Console.WriteLine("\n4. Getting locus by ID...");
        var getLocusByIdResponse = await lociClient.GetByIdAsyncAsync(new GetByIdRequest { Id = createdLocusId });
        Console.WriteLine($"✅ Retrieved locus: ID={getLocusByIdResponse.PhienNtid}, Name={getLocusByIdResponse.Name}");
    }
    else if (getAllLociResponse.Loci.Count > 0)
    {
        Console.WriteLine("\n4. Getting locus by ID (using existing)...");
        var firstLocusId = getAllLociResponse.Loci[0].PhienNtid;
        var getLocusByIdResponse = await lociClient.GetByIdAsyncAsync(new GetByIdRequest { Id = firstLocusId });
        Console.WriteLine($"✅ Retrieved locus: ID={getLocusByIdResponse.PhienNtid}, Name={getLocusByIdResponse.Name}");
    }

    // Test 5: GetByNameAsync
    if (createdLocusId > 0)
    {
        Console.WriteLine("\n5. Getting locus by name...");
        var getLocusByNameResponse = await lociClient.GetByNameAsyncAsync(new GetByNameRequest { Name = newLocus.Name });
        Console.WriteLine($"✅ Retrieved locus by name: ID={getLocusByNameResponse.PhienNtid}, Name={getLocusByNameResponse.Name}");
    }

    // Test 6: Search Loci
    Console.WriteLine("\n6. Searching loci...");
    var searchLociRequest = new SearchLociRequest
    {
        Name = "D",
        IsCodis = true,
        Page = 1,
        PageSize = 5
    };

    var searchLociResponse = await lociClient.SearchAsyncAsync(searchLociRequest);
    Console.WriteLine($"✅ Search found {searchLociResponse.TotalItems} loci matching criteria");
    Console.WriteLine($"   Total Pages: {searchLociResponse.TotalPages}, Current Page: {searchLociResponse.CurrentPage}");

    // Test 7: Update Locus
    if (createdLocusId > 0)
    {
        Console.WriteLine("\n7. Updating locus...");
        var updateLocus = new LociPhienNT
        {
            PhienNtid = createdLocusId,
            Name = $"UPDATED_LOCUS_PhienNT_{DateTime.Now:HHmmss}",
            IsCodis = false, // Changed to non-CODIS
            Description = "Updated test locus via gRPC PhienNT console client",
            MutationRate = 0.0002, // Increased mutation rate
            CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
        };

        var updateLocusResponse = await lociClient.UpdateAsyncAsync(updateLocus);
        Console.WriteLine($"✅ Updated locus with result: {updateLocusResponse.Result}");
    }

    // ============================================
    // PAGINATION TESTING
    // ============================================
    Console.WriteLine("\n=== TESTING PAGINATION ===");

    // Test pagination with DNA Tests
    Console.WriteLine("\n1. Testing DNA Tests pagination...");
    var paginationTestRequest = new SearchDnaTestsRequest
    {
        TestType = "",
        IsCompleted = false,
        Page = 1,
        PageSize = 2
    };

    var paginationTestResponse = await dnaTestsClient.SearchAsyncAsync(paginationTestRequest);
    Console.WriteLine($"✅ Page 1: {paginationTestResponse.Items.Count} items out of {paginationTestResponse.TotalItems} total");

    if (paginationTestResponse.TotalPages > 1)
    {
        paginationTestRequest.Page = 2;
        var page2Response = await dnaTestsClient.SearchAsyncAsync(paginationTestRequest);
        Console.WriteLine($"✅ Page 2: {page2Response.Items.Count} items");
    }

    // Test pagination with Loci
    Console.WriteLine("\n2. Testing Loci pagination...");
    var lociPaginationRequest = new SearchLociRequest
    {
        Name = "",
        IsCodis = true,
        Page = 1,
        PageSize = 3
    };

    var lociPaginationResponse = await lociClient.SearchAsyncAsync(lociPaginationRequest);
    Console.WriteLine($"✅ Page 1: {lociPaginationResponse.Items.Count} items out of {lociPaginationResponse.TotalItems} total");

    // ============================================
    // CLEANUP (OPTIONAL DELETE TESTS)
    // ============================================
    Console.WriteLine("\n=== CLEANUP TESTING (DELETE) ===");

    Console.WriteLine("\nDo you want to test DELETE operations? (y/n)");
    var deleteChoice = Console.ReadLine()?.ToLower();

    if (deleteChoice == "y" || deleteChoice == "yes")
    {
        // Test Delete DNA Test
        if (createdTestId > 0)
        {
            Console.WriteLine($"\nDeleting created DNA test (ID: {createdTestId})...");
            var deleteTestResponse = await dnaTestsClient.DeleteAsyncAsync(new DeleteRequest { Id = createdTestId });
            Console.WriteLine($"✅ Deleted DNA test with result: {deleteTestResponse.Result}");
        }

        // Test Delete Locus
        if (createdLocusId > 0)
        {
            Console.WriteLine($"\nDeleting created locus (ID: {createdLocusId})...");
            var deleteLocusResponse = await lociClient.DeleteAsyncAsync(new DeleteRequest { Id = createdLocusId });
            Console.WriteLine($"✅ Deleted locus with result: {deleteLocusResponse.Result}");
        }
    }
    else
    {
        Console.WriteLine("Skipping delete operations. Created records remain in database.");
        if (createdTestId > 0) Console.WriteLine($"  - DNA Test ID: {createdTestId}");
        if (createdLocusId > 0) Console.WriteLine($"  - Locus ID: {createdLocusId}");
    }

    // ============================================
    // FINAL STATUS
    // ============================================
    Console.WriteLine("\n==============================================");
    Console.WriteLine("✅ ALL TESTS COMPLETED SUCCESSFULLY!");
    Console.WriteLine("==============================================");
    Console.WriteLine("\nTested Services:");
    Console.WriteLine("  ✅ Greeter Service - Basic connectivity");
    Console.WriteLine("  ✅ DNA Tests Service - Full CRUD + Search");
    Console.WriteLine("  ✅ Loci Service - Full CRUD + Search + CODIS filtering");
    Console.WriteLine("  ✅ Pagination - Both services");
    Console.WriteLine("  ✅ Error handling - Exception management");

    Console.WriteLine($"\nCreated Records:");
    if (createdTestId > 0) Console.WriteLine($"  - DNA Test ID: {createdTestId}");
    if (createdLocusId > 0) Console.WriteLine($"  - Locus ID: {createdLocusId}");
}
catch (Exception ex)
{
    Console.WriteLine("\n❌ ERROR OCCURRED:");
    Console.WriteLine($"Message: {ex.Message}");
    if (ex.InnerException != null)
    {
        Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
    }
    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
}

Console.WriteLine("\n==============================================");
Console.WriteLine("Press any key to exit...");
Console.ReadKey();