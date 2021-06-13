namespace BS.Domain.Interfaces
{
    public interface IDataConcurrency
    {
        public byte[] RowVersion { get; set; }
    }
}
