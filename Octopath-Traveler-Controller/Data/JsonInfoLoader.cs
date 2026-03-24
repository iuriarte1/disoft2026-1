using System.Text.Json;
using Octopath_Traveler_Model;
namespace Octopath_Traveler.Data;

public class JsonInfoLoader
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public List<Traveler> LoaderTravelers(string filePath)
    {
        string content = ReadFileContent(filePath);
        return JsonSerializer.Deserialize<List<Traveler>>(content, _jsonOptions) ?? new List<Traveler>();
    }

    public List<Beast> LoaderBeasts(string filePath)
    {
        string content = ReadFileContent(filePath);
        return JsonSerializer.Deserialize<List<Beast>>(content, _jsonOptions) ?? new List<Beast>();
    }
    public List<Skill> LoaderSkills(string filePath)
    {
        string jsonContent = ReadFileContent(filePath);
        return JsonSerializer.Deserialize<List<Skill>>(jsonContent, _jsonOptions) ?? new List<Skill>();
    }
    
    private string ReadFileContent(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Archivo no encontrado en la ruta: {filePath}");
        }
        return File.ReadAllText(filePath);
    }
}