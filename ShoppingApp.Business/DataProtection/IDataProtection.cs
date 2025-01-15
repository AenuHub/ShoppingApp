namespace ShoppingApp.Business.DataProtection
{
    public interface IDataProtection
    {
        string Encrypt(string value);
        string Decrypt(string value);
    }
}
