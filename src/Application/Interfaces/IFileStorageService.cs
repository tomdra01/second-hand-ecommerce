namespace Application.Interfaces;

public interface IFileStorageService
{
    Task UploadFileAsync(string objectName, Stream stream, string contentType);
    string GetFileUrl(string objectName);
}