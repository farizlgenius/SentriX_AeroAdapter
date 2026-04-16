namespace AeroAdapter.DtoGenerator;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("DTO Generator Started...");

        // ===== CONFIG ======
        var dllPath = @"AeroAdapter.Infrastructure\Libs\Windows\HIDAeroWrap64.dll";
        var outputDir = @"AeroAdapter.Application\Contracts";

        // ====== END CONFIG ======

        if(!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        var assembly = System.Reflection.Assembly.LoadFrom(dllPath);
        var types = assembly.GetTypes()
        .Where(t => t.IsClass && t.IsPublic)
        .OrderBy(t => t.FullName)
        .ToList();

       DtoGeneratorEngine.GenerateRootDto(assembly,"SCPReplyMessage",outputDir);
    }
}
