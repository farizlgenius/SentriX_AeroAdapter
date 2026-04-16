using System.Reflection;
using System.Text;

namespace AeroAdapter.DtoGenerator;

public static class DtoGeneratorEngine
{
    public static void GenerateRootDto(Assembly assembly, string rootClassName, string outputDir)
    {
        var rootType = assembly.GetTypes()
            .FirstOrDefault(t => t.IsClass && t.Name == rootClassName);

        if (rootType == null)
            throw new Exception($"Root class {rootClassName} not found in DLL");

        var filePath = Path.Combine(outputDir, rootClassName + "Dto.cs");

        var sb = new StringBuilder();
        sb.AppendLine("namespace Application.Contracts.GeneratedDtos;");
        sb.AppendLine();

        GenerateClass(rootType, sb, 0);

        File.WriteAllText(filePath, sb.ToString());
        Console.WriteLine($"Generated ROOT DTO: {rootClassName}Dto");
    }

    private static void GenerateClass(Type type, StringBuilder sb, int indent)
    {
        var tabs = new string(' ', indent * 4);
        var dtoName = type.Name + "Dto";

        sb.AppendLine($"{tabs}public class {dtoName}");
        sb.AppendLine($"{tabs}{{");

        // fields
        foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            var mappedType = MapType(field.FieldType);
            sb.AppendLine($"{tabs}    public {mappedType} {field.Name} {{ get; set; }}");
        }

        // properties
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            if (!prop.CanRead) continue;

            var mappedType = MapType(prop.PropertyType);
            sb.AppendLine($"{tabs}    public {mappedType} {prop.Name} {{ get; set; }}");
        }

        // nested classes
        var nestedTypes = type.GetNestedTypes(BindingFlags.Public);

        foreach (var nested in nestedTypes)
        {
            sb.AppendLine();
            GenerateClass(nested, sb, indent + 1);
        }

        sb.AppendLine($"{tabs}}}");
    }

    private static string MapType(Type type)
    {
        if (type.IsEnum) return "int";
        if (type == typeof(byte[])) return "byte[]?";

        if (type.IsArray)
        {
            var elementType = MapType(type.GetElementType()!);
            return elementType + "[]?";
        }

        if (type == typeof(int)) return "int";
        if (type == typeof(short)) return "short";
        if (type == typeof(long)) return "long";
        if (type == typeof(byte)) return "byte";
        if (type == typeof(bool)) return "bool";
        if (type == typeof(string)) return "string";
        if (type == typeof(float)) return "float";
        if (type == typeof(double)) return "double";
        if (type == typeof(decimal)) return "decimal";
        if (type == typeof(char)) return "char";
        if(type == typeof(char[])) return "char[]?";

        // nested class reference → nested DTO
        if (type.IsClass)
            return type.Name + "Dto?";

        return "object?";
    }
}