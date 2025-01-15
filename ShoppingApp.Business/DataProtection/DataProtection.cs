using Microsoft.AspNetCore.DataProtection;

namespace ShoppingApp.Business.DataProtection
{
    public class DataProtection : IDataProtection
    {
        private readonly IDataProtector _protector;

        public DataProtection(IDataProtectionProvider protector)
        {
            _protector = protector.CreateProtector("ShoppingApp");
        }

        public string Decrypt(string value)
        {
            return _protector.Unprotect(value);
        }

        public string Encrypt(string value)
        {
            return _protector.Protect(value);
        }
    }
}
