namespace Presentation.Modelos;

public record Sticker
{
    public string room { get; set; }
    public string user { get; set; }
    public IFormFile sticker { get; set; }
}