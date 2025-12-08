using System;
using System.Security.Cryptography;
using System.Text;

namespace gestion_etablissement.Services
{
    /// <summary>
    /// Service de hachage et vérification des mots de passe
    /// Utilise BCrypt (recommandé) avec fallback sur PBKDF2
    /// </summary>
    public static class PasswordHasher
    {
        // Configuration BCrypt
        private const int BCRYPT_WORK_FACTOR = 12; // 2^12 = 4096 itérations

        // Configuration PBKDF2 (fallback)
        private const int PBKDF2_ITERATIONS = 100000;
        private const int PBKDF2_SALT_SIZE = 32;
        private const int PBKDF2_HASH_SIZE = 32;

        /// <summary>
        /// Hash un mot de passe avec BCrypt (RECOMMANDÉ)
        /// Nécessite le package: BCrypt.Net-Next
        /// </summary>
        /// <param name="password">Mot de passe en clair</param>
        /// <returns>Hash BCrypt (60 caractères)</returns>
        public static string HashPasswordBCrypt(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Le mot de passe ne peut pas être vide", nameof(password));

            try
            {
                // Utilise BCrypt.Net-Next
                return BCrypt.Net.BCrypt.HashPassword(password, BCRYPT_WORK_FACTOR);
            }
            catch (TypeInitializationException)
            {
                // Si BCrypt n'est pas installé, utiliser PBKDF2
                return HashPasswordPBKDF2(password);
            }
        }

        /// <summary>
        /// Vérifie un mot de passe avec son hash BCrypt
        /// </summary>
        /// <param name="password">Mot de passe en clair</param>
        /// <param name="hashedPassword">Hash stocké en base</param>
        /// <returns>True si le mot de passe correspond</returns>
        public static bool VerifyPasswordBCrypt(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            try
            {
                // Détecte automatiquement le format BCrypt
                if (hashedPassword.StartsWith("$2"))
                {
                    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
                }
                else
                {
                    // Format PBKDF2
                    return VerifyPasswordPBKDF2(password, hashedPassword);
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Hash un mot de passe avec PBKDF2 (FALLBACK sans dépendances externes)
        /// Format: Base64(salt):Base64(hash)
        /// </summary>
        /// <param name="password">Mot de passe en clair</param>
        /// <returns>Hash PBKDF2</returns>
        public static string HashPasswordPBKDF2(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Le mot de passe ne peut pas être vide", nameof(password));

            // Générer un salt aléatoire
            byte[] salt = RandomNumberGenerator.GetBytes(PBKDF2_SALT_SIZE);

            // Générer le hash avec PBKDF2
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                PBKDF2_ITERATIONS,
                HashAlgorithmName.SHA256
            );

            byte[] hash = pbkdf2.GetBytes(PBKDF2_HASH_SIZE);

            // Retourner au format: Base64(salt):Base64(hash)
            return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Vérifie un mot de passe avec PBKDF2
        /// </summary>
        public static bool VerifyPasswordPBKDF2(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            try
            {
                // Extraire salt et hash
                var parts = hashedPassword.Split(':');
                if (parts.Length != 2)
                    return false;

                byte[] salt = Convert.FromBase64String(parts[0]);
                byte[] storedHash = Convert.FromBase64String(parts[1]);

                // Recalculer le hash avec le même salt
                using var pbkdf2 = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    PBKDF2_ITERATIONS,
                    HashAlgorithmName.SHA256
                );

                byte[] computedHash = pbkdf2.GetBytes(PBKDF2_HASH_SIZE);

                // Comparaison sécurisée (résistant au timing attack)
                return CryptographicOperations.FixedTimeEquals(computedHash, storedHash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Méthode principale - Choisit automatiquement la meilleure méthode
        /// </summary>
        public static string HashPassword(string password)
        {
            try
            {
                return HashPasswordBCrypt(password);
            }
            catch
            {
                return HashPasswordPBKDF2(password);
            }
        }

        /// <summary>
        /// Méthode principale de vérification - Compatible avec tous les formats
        /// </summary>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            // Détecte le format automatiquement
            if (hashedPassword.StartsWith("$2"))
            {
                return VerifyPasswordBCrypt(password, hashedPassword);
            }
            else if (hashedPassword.Contains(":"))
            {
                return VerifyPasswordPBKDF2(password, hashedPassword);
            }
            else
            {
                // Texte clair (migration) - DÉPRÉCIÉ
                return password == hashedPassword;
            }
        }

        /// <summary>
        /// Vérifie si un hash doit être mis à jour (obsolète ou faible)
        /// </summary>
        public static bool NeedsRehash(string hashedPassword)
        {
            // Si c'est en texte clair ou PBKDF2, devrait passer à BCrypt
            return !hashedPassword.StartsWith("$2");
        }
    }
}