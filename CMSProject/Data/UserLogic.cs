using CMSProject.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CMSProject.Data
{
    public class UserLogic(CmsbbddContext context) : Controller
    {

        private readonly CmsbbddContext _context = context;

        /// <summary>
        /// Validación de usuarios, comprueba si el usuario que está introduciendo los datos existe o no.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserTable? ValidateUser(string email, string password)
        {
            var result = _context.UserTables.Where(x => x.Email == email).FirstOrDefault();
            if (result == null)
                return null;


            //Comprobación si la contraseña ya esta hasheada o no (provisional hasta tener todas las contraseñas cifradas)
            if (!String.IsNullOrEmpty(result.Salt))
            {
                var user = _context.UserTables
                    .Include(u => u.IdRoleNavigation)
                    .Where(c => c.Email == email && CheckHash(password, result.Password!, result.Salt))
                    .FirstOrDefault();

                return (user == null) ? null : user;
            }
            else
            {
                var user = _context.UserTables
                    .Include(u => u.IdRoleNavigation)
                    .Where(c => c.Email == email && c.Password == password)
                    .FirstOrDefault();

                return (user == null) ? null : user;
            }


        }

        /// <summary>
        /// Hasheo de la contraseña. Se encarga de cifrar la contraseña.
        /// Genera la nueva contraseña utilizando el tipo de cifrado SHA256.
        /// Con respecto al Salt, lo genera basandose en un numero aleatorio si no existe.
        /// </summary>
        /// <param name="password">Contraseña a cifrar</param>
        /// <param name="existedSalt">En el caso de que exista, el salt existente de la contraseña</param>
        /// <returns>(HashedPassword) La contraseña cifrada y el Salt</returns>
        public static HashedPassword Hash(string password)
        {
            byte[] salt = new byte[128 / 8];

            //Comprobacion si salt existe. En caso de que sea nulo o esté vacio,
            //va a generar uno nuevo utilizando un numero aleatorio. Si ya existe,
            //va a mantenerlo y no se modifica
            //if (String.IsNullOrEmpty(existedSalt))
            //{
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            //}
            //else
            //    salt = Convert.FromBase64String(existedSalt);

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return new HashedPassword() { Password = hashed, Salt = Convert.ToBase64String(salt) };
        }

        /// <summary>
        /// Comprueba si la contraseña que se está ingresando es la misma que la que se encuentra cifrada
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <param name="salt"></param>
        /// <returns>(bool) 1 si la comprobación es correcta y 0 si falla la comprobación</returns>
        public static bool CheckHash(string? password, string hash, string salt)
        {
            if (String.IsNullOrEmpty(password))
                return false;

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Convert.FromBase64String(salt),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed == hash.Trim();
        }

    }

    public class HashedPassword
    {
        public string? Password { get; set; }

        public string? Salt { get; set; }
    }










}

