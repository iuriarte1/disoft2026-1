namespace Octopath_Traveler.Data;

public class FolderHandler
{
    public List<string> GetFolderNames(string ruta)
    {
        var archivos = Directory.GetFiles(ruta);
        var nombres = new List<string>();
        foreach (var archivo in archivos)
        {
            nombres.Add(Path.GetFileName(archivo));
        }
        return nombres;
    }
}