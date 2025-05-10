namespace Application.Data.DTOs;

public class FileUploadDto
{
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
    public Stream Content { get; init; } = Stream.Null;
}