using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication.ViewModel
{
    public class PasswordEncoder
    {
        private string mergePasswordAndSalt(string password, string salt)
        {
            if (salt == "") {
                return password;
            }

            if( salt.Contains('{') || salt.Contains('}') ) 
                throw new Exception("Cannot use { or } in salt.");

            return password + '{' + salt + '}';
        }

        public string encodePassword(string raw, string salt)
        {
            string salted = this.mergePasswordAndSalt(raw, salt);

            string digest = this.SHA512(salted);

            // "stretch" hash
            for (int i = 1; i < 5000; ++i)
                digest = this.SHA512(digest + salted);

            return System.Convert.ToBase64String( System.Text.Encoding.UTF8.GetBytes(digest.ToCharArray()) );
        }

        private string SHA512(string chaine)
        {
            byte[] data = System.Text.Encoding.UTF8.GetBytes( chaine.ToCharArray() );
            byte[] result = (new SHA512Managed()).ComputeHash(data);

            return System.Text.Encoding.UTF8.GetString(result);
        }
    }
}
