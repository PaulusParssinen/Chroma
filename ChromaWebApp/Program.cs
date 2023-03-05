using System.Security.Cryptography;
using System.Text;

using Chroma;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("", static ([AsParameters] RenderRequest request, IConfiguration configuration) =>
    {
        // Disallow path traversal
        string sprite = request.Sprite.Replace("..", string.Empty, StringComparison.OrdinalIgnoreCase);        
        string fileNameUnique = $"{sprite}{request.Small}{request.State}{request.Direction}{request.Color}{request.Shadow}{request.Bg}{request.Canvas}{request.Crop}{request.Icon}";
        string hashedUniqueName = Convert.ToHexString(SHA1.HashData(Encoding.UTF8.GetBytes(fileNameUnique)));

        string furniAssetsDirectory = configuration["AssetsDirectory"] ?? "./swfs/hof_furni/";
        string furniExportDirectory = configuration["ExportDirectory"] ?? "./furni_export/";

        var dir = Directory.CreateDirectory(Path.Combine(furniExportDirectory, sprite, "export"));
        string uniqueFilePath = Path.Combine(furniExportDirectory, sprite, "export", $"{hashedUniqueName}.png");

        if (!File.Exists(uniqueFilePath))
        {
            if (string.IsNullOrEmpty(sprite)) return null;

            var furni = new ChromaFurniture(Path.Combine(furniAssetsDirectory, sprite + ".swf"),
                isSmallFurni: request.Small, renderState: request.State,
                renderDirection: request.Direction, colourId: request.Color,
                renderShadows: request.Shadow, renderBackground: request.Bg,
                renderCanvasColour: request.Canvas, cropImage: request.Crop, renderIcon: request.Icon);
            furni.Run();
            var bytes = furni.CreateImage();

            if (bytes != null)
            {
                File.WriteAllBytes(uniqueFilePath, bytes);
            }
            else
            {
                File.WriteAllBytes(uniqueFilePath, Array.Empty<byte>());

            }

            return Results.File(bytes, "image/png");
        }
        else
        {
            return Results.File(File.ReadAllBytes(uniqueFilePath), "image/png");
        }

        return Results.NotFound();
});

app.Run();

internal readonly record struct RenderRequest(
    string Sprite, 
    bool Small = false,
    int State = 0, 
    int Direction = 0, 
    int Rotation = 0, 
    int Color = 0,
    bool Bg = false, bool Crop = true, bool Shadow = true, 
    string Canvas = "FEFEFE", bool Icon = false)
{ }
