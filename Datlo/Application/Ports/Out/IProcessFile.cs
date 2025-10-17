using Datlo.Domain.Entities;

namespace Datlo.Application.Ports.Out
{
    public interface IProcessFile
    {
        Task ProcessStreamAsync(Stream fileStream, Func<IList<ImportedDataModel>, Task> onBatch);
    }
}
