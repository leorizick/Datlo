using Datlo.Domain.Entities;
using System.Data;

namespace Datlo.Adapter.Out.Persistence.Internal
{
    public class ImportedDataDataReader : IDataReader
    {
        private readonly IEnumerator<ImportedDataModel> _enumerator;

        private static readonly string[] Colunas = new[]
        {
            nameof(ImportedDataModel.Name),
            nameof(ImportedDataModel.Email),
            nameof(ImportedDataModel.LastPaymentValue),
            nameof(ImportedDataModel.DataProcessamento),
            nameof(ImportedDataModel.Status),
            nameof(ImportedDataModel.Line)
        };

        public ImportedDataDataReader(IEnumerable<ImportedDataModel> source)
        {
            _enumerator = source.GetEnumerator();
        }

        public bool Read() => _enumerator.MoveNext();

        public object GetValue(int i)
        {
            var current = _enumerator.Current;
            return i switch
            {
                0 => current.Name,
                1 => current.Email,
                2 => current.LastPaymentValue,
                3 => current.DataProcessamento,
                4 => current.Status,
                5 => current.Line,
                _ => throw new IndexOutOfRangeException()
            };
        }

        public string GetName(int i) => Colunas[i];
        public int FieldCount => Colunas.Length;

        public int Depth => throw new NotImplementedException();

        public bool IsClosed => throw new NotImplementedException();

        public int RecordsAffected => throw new NotImplementedException();

        public object this[string name] => throw new NotImplementedException();

        public object this[int i] => throw new NotImplementedException();

        public int GetOrdinal(string name) => Array.IndexOf(Colunas, name);
        public bool IsDBNull(int i) => GetValue(i) == null || GetValue(i) == DBNull.Value;

        public void Dispose() => _enumerator.Dispose();
        public void Close() { }
        public Type GetFieldType(int i) => GetValue(i)?.GetType() ?? typeof(object);

        public DataTable? GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[]? buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }
    }
}
