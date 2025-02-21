using System.Security.Cryptography;
using System.Text;

namespace BudgetManagement.Core.Helpers;

public static class StringHelper
{
    public static string GetPasswordHash(this string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var builder = new StringBuilder();
        foreach (var b in bytes)
        {
            builder.Append(b.ToString("x2"));
        }

        return builder.ToString();
    }
}
