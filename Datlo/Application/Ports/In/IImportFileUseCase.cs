namespace Datlo.Application.Ports.In
{
    public interface IImportFileUseCase
    {
        Task ExecuteImportAsync(IFormFile file);
    }
}
