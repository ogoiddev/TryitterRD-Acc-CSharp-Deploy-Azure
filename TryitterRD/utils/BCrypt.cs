using System.Security.Cryptography;
using System.Text;

namespace TryitterRD.Utils
{
    public class MD5Crypt
    {
        public static string GenerateHashMD5(string passToHash)
        {
            MD5 md5Hash = MD5.Create();

            var bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(passToHash));

            StringBuilder stringBuilder = new();

            for (int i = 0; i < bytes.Length; i += 1)
            {
                stringBuilder.Append(bytes[i]);
            }
            return stringBuilder.ToString();
        }
    }
}
