using System.Security.Cryptography;
using System.Text;

namespace AttCadastro.Utils
{
    public static class Criptografia
    {
        /// <summary>
        /// Gera o hash SHA256 a partir de uma string (usado para armazenar senhas de forma segura)
        /// </summary>
        public static string GerarHashSha256(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(texto));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
