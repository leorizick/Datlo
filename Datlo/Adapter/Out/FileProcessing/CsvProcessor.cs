using Datlo.Application.Ports.Out;
using Datlo.Domain.Entities;
using System.Globalization;

namespace Datlo.Adapter.Out.FileProcessing
{
    public class CsvProcessor : IProcessFile
    {
        private const int BatchSize = 5000;

        public async Task ProcessStreamAsync(Stream fileStream, Func<IList<ImportedDataModel>, Task> onBatch)
        {
            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
            };

            using var reader = new StreamReader(fileStream);
            using var csv = new CsvHelper.CsvReader(reader, CultureInfo.InvariantCulture);

            await csv.ReadAsync();
            csv.ReadHeader();

            var lote = new List<ImportedDataModel>(BatchSize);

            while (await csv.ReadAsync())
            {
                ImportedDataModel result = null;

                int line = csv.Context.Parser.Row;
                try
                {
                    var name = csv.GetField<string>("Name");
                    var email = csv.GetField<string>("Email");
                    var valueStr = csv.GetField<string>("LastPaymentValue");

                    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || !decimal.TryParse(valueStr, out decimal value))
                    {
                        result = new ImportedDataModel
                        {
                            Name = name,
                            Email = email,
                            DataProcessamento = DateTime.UtcNow,
                            Status = Status.INVALID_DATA,
                            LastPaymentValue = 0,
                            Line = line,
                        };
                    }
                    else
                    {
                        result = new ImportedDataModel
                        {
                            Name = name.Trim().ToUpper(),
                            Email = email,
                            DataProcessamento = DateTime.UtcNow,
                            LastPaymentValue = value,
                            Status = Status.VALID,
                            Line = line
                        };
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR LINE: {csv.Context.Parser.Row}: {ex.Message}");

                    result = new ImportedDataModel
                    {
                        Name = "ERROR",
                        Email = "ERROR",
                        DataProcessamento = DateTime.UtcNow,
                        LastPaymentValue = 0,
                        Status = Status.ERROR_PARSING,
                        Line = line,
                    };
                }

                lote.Add(result);
                if (lote.Count >= BatchSize)
                {
                    await onBatch.Invoke(lote);
                    lote.Clear();
                }
            }
            if (lote.Any())
            {
                await onBatch.Invoke(lote);
            }
        }
    }
}
