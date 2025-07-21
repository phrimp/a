using Grpc.Net.Client;
using DNATesting.GrpcService.PhienNT.Protos;
using DNATesting.GrpcService.PhienNT;
using System;
using System.Threading.Tasks;

Console.WriteLine("==============================================");
Console.WriteLine("=== gRPC Client for DNA Testing PhienNT ===");
Console.WriteLine("==============================================");

// Create a gRPC channel
using var channel = GrpcChannel.ForAddress("https://localhost:7265");
var dnaTestsClient = new DnaTestsPhienNTGRPC.DnaTestsPhienNTGRPCClient(channel);
var lociClient = new LociPhienNTGRPC.LociPhienNTGRPCClient(channel);

while (true)
{
    ShowMainMenu();
    var choice = Console.ReadLine();

    try
    {
        switch (choice)
        {
            case "1":
                await HandleDnaTestsMenu();
                break;
            case "2":
                await HandleLociMenu();
                break;
            case "3":
                Console.WriteLine("Goodbye!");
                return;
            default:
                Console.WriteLine("❌ Invalid option. Please try again.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Error: {ex.Message}");
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}

void ShowMainMenu()
{
    Console.Clear();
    Console.WriteLine("=== Main Menu ===");
    Console.WriteLine("1. DNA Tests Management");
    Console.WriteLine("2. Loci Management");
    Console.WriteLine("3. Exit");
    Console.Write("Choose an option: ");
}

async Task HandleDnaTestsMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== DNA Tests Management ===");
        Console.WriteLine("1. View All Tests");
        Console.WriteLine("2. Get Test by ID");
        Console.WriteLine("3. Search Tests");
        Console.WriteLine("4. Create New Test");
        Console.WriteLine("5. Update Test");
        Console.WriteLine("6. Delete Test");
        Console.WriteLine("7. Back to Main Menu");
        Console.Write("Choose an option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ViewAllDnaTests();
                break;
            case "2":
                await GetDnaTestById();
                break;
            case "3":
                await SearchDnaTests();
                break;
            case "4":
                await CreateDnaTest();
                break;
            case "5":
                await UpdateDnaTest();
                break;
            case "6":
                await DeleteDnaTest();
                break;
            case "7":
                return;
            default:
                Console.WriteLine("❌ Invalid option.");
                break;
        }

        if (choice != "7")
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

async Task HandleLociMenu()
{
    while (true)
    {
        Console.Clear();
        Console.WriteLine("=== Loci Management ===");
        Console.WriteLine("1. View All Loci");
        Console.WriteLine("2. View CODIS Loci Only");
        Console.WriteLine("3. Get Locus by ID");
        Console.WriteLine("4. Get Locus by Name");
        Console.WriteLine("5. Search Loci");
        Console.WriteLine("6. Create New Locus");
        Console.WriteLine("7. Update Locus");
        Console.WriteLine("8. Delete Locus");
        Console.WriteLine("9. Back to Main Menu");
        Console.Write("Choose an option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                await ViewAllLoci();
                break;
            case "2":
                await ViewCodisLoci();
                break;
            case "3":
                await GetLocusById();
                break;
            case "4":
                await GetLocusByName();
                break;
            case "5":
                await SearchLoci();
                break;
            case "6":
                await CreateLocus();
                break;
            case "7":
                await UpdateLocus();
                break;
            case "8":
                await DeleteLocus();
                break;
            case "9":
                return;
            default:
                Console.WriteLine("❌ Invalid option.");
                break;
        }

        if (choice != "9")
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}

// DNA Tests Methods
async Task ViewAllDnaTests()
{
    Console.WriteLine("\n--- All DNA Tests ---");
    var response = await dnaTestsClient.GetAllAsyncAsync(new EmptyRequest());

    if (response.Tests.Count == 0)
    {
        Console.WriteLine("No tests found.");
        return;
    }

    foreach (var test in response.Tests)
    {
        Console.WriteLine($"ID: {test.PhienNtid} | Type: {test.TestType} | Completed: {test.IsCompleted}");
        Console.WriteLine($"  Conclusion: {test.Conclusion}");
        Console.WriteLine($"  Probability: {test.ProbabilityOfRelationship}% | Index: {test.RelationshipIndex}");
        Console.WriteLine();
    }
}

async Task GetDnaTestById()
{
    Console.Write("Enter Test ID: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var response = await dnaTestsClient.GetByIdAsyncAsync(new GetByIdRequest { Id = id });
        Console.WriteLine($"\n✅ Test Found:");
        Console.WriteLine($"ID: {response.PhienNtid}");
        Console.WriteLine($"Type: {response.TestType}");
        Console.WriteLine($"Conclusion: {response.Conclusion}");
        Console.WriteLine($"Probability: {response.ProbabilityOfRelationship}%");
        Console.WriteLine($"Index: {response.RelationshipIndex}");
        Console.WriteLine($"Completed: {response.IsCompleted}");
    }
    else
    {
        Console.WriteLine("❌ Invalid ID format.");
    }
}

async Task SearchDnaTests()
{
    Console.Write("Enter Test Type (or press Enter to skip): ");
    var testType = Console.ReadLine();

    Console.Write("Show only completed tests? (y/n): ");
    var completedInput = Console.ReadLine();
    bool isCompleted = completedInput?.ToLower() == "y";

    Console.Write("Page number (default 1): ");
    int page = int.TryParse(Console.ReadLine(), out page) ? page : 1;

    Console.Write("Page size (default 10): ");
    int pageSize = int.TryParse(Console.ReadLine(), out pageSize) ? pageSize : 10;

    var request = new SearchDnaTestsRequest
    {
        TestType = testType ?? "",
        IsCompleted = isCompleted,
        Page = page,
        PageSize = pageSize
    };

    var response = await dnaTestsClient.SearchAsyncAsync(request);

    Console.WriteLine($"\n--- Search Results ---");
    Console.WriteLine($"Total: {response.TotalItems} | Pages: {response.TotalPages} | Current: {response.CurrentPage}");

    foreach (var test in response.Items)
    {
        Console.WriteLine($"ID: {test.PhienNtid} | Type: {test.TestType} | Completed: {test.IsCompleted}");
    }
}

async Task CreateDnaTest()
{
    Console.WriteLine("\n--- Create New DNA Test ---");
    Console.Write("Test Type: ");
    var testType = Console.ReadLine();

    Console.Write("Conclusion: ");
    var conclusion = Console.ReadLine();

    Console.Write("Probability of Relationship (0-100): ");
    double.TryParse(Console.ReadLine(), out double probability);

    Console.Write("Relationship Index: ");
    double.TryParse(Console.ReadLine(), out double index);

    Console.Write("Is Completed? (y/n): ");
    bool isCompleted = Console.ReadLine()?.ToLower() == "y";

    var test = new DnaTestsPhienNT
    {
        TestType = testType,
        Conclusion = conclusion,
        ProbabilityOfRelationship = probability,
        RelationshipIndex = index,
        IsCompleted = isCompleted,
        CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    };

    var response = await dnaTestsClient.CreateAsyncAsync(test);
    Console.WriteLine($"✅ Test created with ID: {response.Result}");
}

async Task UpdateDnaTest()
{
    Console.Write("Enter Test ID to update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID format.");
        return;
    }

    // First get the existing test
    var existing = await dnaTestsClient.GetByIdAsyncAsync(new GetByIdRequest { Id = id });

    Console.WriteLine("\n--- Update DNA Test ---");
    Console.WriteLine("Press Enter to keep current value, or type new value:");

    Console.Write($"Test Type [{existing.TestType}]: ");
    var testType = Console.ReadLine();
    if (string.IsNullOrEmpty(testType)) testType = existing.TestType;

    Console.Write($"Conclusion [{existing.Conclusion}]: ");
    var conclusion = Console.ReadLine();
    if (string.IsNullOrEmpty(conclusion)) conclusion = existing.Conclusion;

    Console.Write($"Probability [{existing.ProbabilityOfRelationship}]: ");
    var probInput = Console.ReadLine();
    double probability = string.IsNullOrEmpty(probInput) ? existing.ProbabilityOfRelationship : double.Parse(probInput);

    Console.Write($"Index [{existing.RelationshipIndex}]: ");
    var indexInput = Console.ReadLine();
    double index = string.IsNullOrEmpty(indexInput) ? existing.RelationshipIndex : double.Parse(indexInput);

    Console.Write($"Completed [{existing.IsCompleted}] (y/n): ");
    var completedInput = Console.ReadLine();
    bool isCompleted = string.IsNullOrEmpty(completedInput) ? existing.IsCompleted : completedInput.ToLower() == "y";

    var test = new DnaTestsPhienNT
    {
        PhienNtid = id,
        TestType = testType,
        Conclusion = conclusion,
        ProbabilityOfRelationship = probability,
        RelationshipIndex = index,
        IsCompleted = isCompleted,
        CreatedAt = existing.CreatedAt
    };

    var response = await dnaTestsClient.UpdateAsyncAsync(test);
    Console.WriteLine($"✅ Test updated. Result: {response.Result}");
}

async Task DeleteDnaTest()
{
    Console.Write("Enter Test ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.Write($"Are you sure you want to delete test {id}? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            var response = await dnaTestsClient.DeleteAsyncAsync(new DeleteRequest { Id = id });
            Console.WriteLine($"✅ Test deleted. Result: {response.Result}");
        }
        else
        {
            Console.WriteLine("Delete cancelled.");
        }
    }
    else
    {
        Console.WriteLine("❌ Invalid ID format.");
    }
}

// Loci Methods
async Task ViewAllLoci()
{
    Console.WriteLine("\n--- All Loci ---");
    var response = await lociClient.GetAllAsyncAsync(new EmptyRequest());

    if (response.Loci.Count == 0)
    {
        Console.WriteLine("No loci found.");
        return;
    }

    foreach (var locus in response.Loci)
    {
        Console.WriteLine($"ID: {locus.PhienNtid} | Name: {locus.Name} | CODIS: {locus.IsCodis}");
        Console.WriteLine($"  Description: {locus.Description}");
        Console.WriteLine($"  Mutation Rate: {locus.MutationRate}");
        Console.WriteLine();
    }
}

async Task ViewCodisLoci()
{
    Console.WriteLine("\n--- CODIS Loci Only ---");
    var response = await lociClient.GetCodisLociAsyncAsync(new EmptyRequest());

    foreach (var locus in response.Loci)
    {
        Console.WriteLine($"ID: {locus.PhienNtid} | Name: {locus.Name}");
        Console.WriteLine($"  Description: {locus.Description}");
        Console.WriteLine($"  Mutation Rate: {locus.MutationRate}");
        Console.WriteLine();
    }
}

async Task GetLocusById()
{
    Console.Write("Enter Locus ID: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        var response = await lociClient.GetByIdAsyncAsync(new GetByIdRequest { Id = id });
        Console.WriteLine($"\n✅ Locus Found:");
        Console.WriteLine($"ID: {response.PhienNtid}");
        Console.WriteLine($"Name: {response.Name}");
        Console.WriteLine($"CODIS: {response.IsCodis}");
        Console.WriteLine($"Description: {response.Description}");
        Console.WriteLine($"Mutation Rate: {response.MutationRate}");
    }
    else
    {
        Console.WriteLine("❌ Invalid ID format.");
    }
}

async Task GetLocusByName()
{
    Console.Write("Enter Locus Name: ");
    var name = Console.ReadLine();

    if (!string.IsNullOrEmpty(name))
    {
        var response = await lociClient.GetByNameAsyncAsync(new GetByNameRequest { Name = name });
        Console.WriteLine($"\n✅ Locus Found:");
        Console.WriteLine($"ID: {response.PhienNtid}");
        Console.WriteLine($"Name: {response.Name}");
        Console.WriteLine($"CODIS: {response.IsCodis}");
        Console.WriteLine($"Description: {response.Description}");
        Console.WriteLine($"Mutation Rate: {response.MutationRate}");
    }
    else
    {
        Console.WriteLine("❌ Name cannot be empty.");
    }
}

async Task SearchLoci()
{
    Console.Write("Enter Locus Name (or press Enter to skip): ");
    var name = Console.ReadLine();

    Console.Write("Show only CODIS loci? (y/n): ");
    var codisInput = Console.ReadLine();
    bool isCodis = codisInput?.ToLower() == "y";

    Console.Write("Page number (default 1): ");
    int page = int.TryParse(Console.ReadLine(), out page) ? page : 1;

    Console.Write("Page size (default 10): ");
    int pageSize = int.TryParse(Console.ReadLine(), out pageSize) ? pageSize : 10;

    var request = new SearchLociRequest
    {
        Name = name ?? "",
        IsCodis = isCodis,
        Page = page,
        PageSize = pageSize
    };

    var response = await lociClient.SearchAsyncAsync(request);

    Console.WriteLine($"\n--- Search Results ---");
    Console.WriteLine($"Total: {response.TotalItems} | Pages: {response.TotalPages} | Current: {response.CurrentPage}");

    foreach (var locus in response.Items)
    {
        Console.WriteLine($"ID: {locus.PhienNtid} | Name: {locus.Name} | CODIS: {locus.IsCodis}");
    }
}

async Task CreateLocus()
{
    Console.WriteLine("\n--- Create New Locus ---");
    Console.Write("Locus Name: ");
    var name = Console.ReadLine();

    Console.Write("Description: ");
    var description = Console.ReadLine();

    Console.Write("Is CODIS? (y/n): ");
    bool isCodis = Console.ReadLine()?.ToLower() == "y";

    Console.Write("Mutation Rate (e.g., 0.001): ");
    double.TryParse(Console.ReadLine(), out double mutationRate);

    var locus = new LociPhienNT
    {
        Name = name,
        Description = description,
        IsCodis = isCodis,
        MutationRate = mutationRate,
        CreatedAt = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
    };

    var response = await lociClient.CreateAsyncAsync(locus);
    Console.WriteLine($"✅ Locus created with ID: {response.Result}");
}

async Task UpdateLocus()
{
    Console.Write("Enter Locus ID to update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID format.");
        return;
    }

    // First get the existing locus
    var existing = await lociClient.GetByIdAsyncAsync(new GetByIdRequest { Id = id });

    Console.WriteLine("\n--- Update Locus ---");
    Console.WriteLine("Press Enter to keep current value, or type new value:");

    Console.Write($"Name [{existing.Name}]: ");
    var name = Console.ReadLine();
    if (string.IsNullOrEmpty(name)) name = existing.Name;

    Console.Write($"Description [{existing.Description}]: ");
    var description = Console.ReadLine();
    if (string.IsNullOrEmpty(description)) description = existing.Description;

    Console.Write($"CODIS [{existing.IsCodis}] (y/n): ");
    var codisInput = Console.ReadLine();
    bool isCodis = string.IsNullOrEmpty(codisInput) ? existing.IsCodis : codisInput.ToLower() == "y";

    Console.Write($"Mutation Rate [{existing.MutationRate}]: ");
    var rateInput = Console.ReadLine();
    double mutationRate = string.IsNullOrEmpty(rateInput) ? existing.MutationRate : double.Parse(rateInput);

    var locus = new LociPhienNT
    {
        PhienNtid = id,
        Name = name,
        Description = description,
        IsCodis = isCodis,
        MutationRate = mutationRate,
        CreatedAt = existing.CreatedAt
    };

    var response = await lociClient.UpdateAsyncAsync(locus);
    Console.WriteLine($"✅ Locus updated. Result: {response.Result}");
}

async Task DeleteLocus()
{
    Console.Write("Enter Locus ID to delete: ");
    if (int.TryParse(Console.ReadLine(), out int id))
    {
        Console.Write($"Are you sure you want to delete locus {id}? (y/n): ");
        if (Console.ReadLine()?.ToLower() == "y")
        {
            var response = await lociClient.DeleteAsyncAsync(new DeleteRequest { Id = id });
            Console.WriteLine($"✅ Locus deleted. Result: {response.Result}");
        }
        else
        {
            Console.WriteLine("Delete cancelled.");
        }
    }
    else
    {
        Console.WriteLine("❌ Invalid ID format.");
    }
}