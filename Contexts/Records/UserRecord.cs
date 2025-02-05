namespace RoofStockBackend.Contexts.Records
{
    public record UserRecord(int Id, string login, string password, DateTime creation_date);
}