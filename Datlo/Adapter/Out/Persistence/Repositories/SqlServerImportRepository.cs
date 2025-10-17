using Datlo.Adapter.Out.Persistence.Internal;
using Datlo.Application.Ports.Out;
using Datlo.Configuration.Database;
using Datlo.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Datlo.Adapter.Out.Persistence.Repositories
{
    public class SqlServerImportRepository : IImportRepository
    {
        private readonly AppDbContext _context;

        public SqlServerImportRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SaveBatchAsync(IEnumerable<ImportedDataModel> dataBatch)
        {
            var connectionString = _context.Database.GetDbConnection().ConnectionString;

            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            using var bulkCopy = new SqlBulkCopy(connection);
            bulkCopy.DestinationTableName = "ImportedData";

            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.Name), "Name");
            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.Email), "Email");
            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.LastPaymentValue), "LastPaymentValue");
            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.DataProcessamento), "DataProcessamento");
            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.Status), "Status");
            bulkCopy.ColumnMappings.Add(nameof(ImportedDataModel.Line), "Line");

            using var reader = new ImportedDataDataReader(dataBatch);

            await bulkCopy.WriteToServerAsync(reader);
        }


    }
}
