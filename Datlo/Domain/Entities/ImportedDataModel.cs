namespace Datlo.Domain.Entities
{
    public class ImportedDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal LastPaymentValue { get; set; }

        public DateTime DataProcessamento { get; set; }
        public Status Status { get; set; }

        public int Line { get; set; }
    }


    public enum Status
    {
        VALID = 0,
        INVALID_DATA = 1,
        ERROR_PARSING =2,
    }
}
