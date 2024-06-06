using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace MotoOpinion
{
    public class Recursos
    {

        public static String cifrarPasswordSHA256(String Password) {
            using var sha256 = SHA256.Create();
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(Password));

            var sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(bytes[i].ToString("x2"));
            }
            return sb.ToString();
        }

       
    }
}
