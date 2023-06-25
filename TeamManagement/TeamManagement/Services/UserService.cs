using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TeamManagement.Data.Enums;
using TeamManagement.Data.Etintities;
using TeamManagement.Data;
using TeamManagement.Services.Dtos;

namespace TeamManagement.Services
{
    public class UserService
    {
        private readonly string securityKey;
        private readonly UnitOfWork unitOfWork;

        private int PBKDF2IterCount = 1000;
        private int PBKDF2SubkeyLength = 256 / 8;
        private int SaltSize = 128 / 8;


        public UserService(IConfiguration config, UnitOfWork unitOfWork)
        {
            securityKey = config["JWT:SecurityKey"];
            this.unitOfWork = unitOfWork;
        }

        public string Login(UserLoginDto loginData)
        {
            if (loginData == null)
            {
                return "Invalid client request!";
            }

            var user = unitOfWork.Users.GetByEmail(loginData.Email);
            if (user == null)
            {
                return "User not found!";
            }

            if (!VerifyHashedPassword(user.PasswordHash, loginData.Password))
            {
                return $"Invalid password!";
            }



            var token = GetToken(user);
            if (token == null)
            {
                return "Token is null!";
            }

            return token;
        }

        public User? Register(UserRegisterDto registerData)
        {
            if (registerData == null)
            {
                return null;
            }

            if (unitOfWork.Users.GetByEmail(registerData.Email) != null)
            {
                return null;
            }

            var hashedPassword = HashPassword(registerData.Password);

            var user = new User
            {
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                Email = registerData.Email,
                PasswordHash = hashedPassword,
                Role = registerData.Role
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();

            if (user.Role == Role.TeamOwner)
            {
                CreateTeamOwner(user);
            }

            return user;
        }

        public string GetToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var emailClaim = new Claim(ClaimTypes.Email, user.Email);
            var roleClaim = new Claim(ClaimTypes.Role, user.Role.ToString());
            var userIdClaim = new Claim("userId", user.Id.ToString());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = "Backend",
                Audience = "Frontend",
                Subject = new ClaimsIdentity(new[] { roleClaim, emailClaim, userIdClaim }),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = credentials
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var tokenString = jwtTokenHandler.WriteToken(token);

            return tokenString;
        }

        public bool ValidateToken(string tokenString)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                IssuerSigningKey = key,
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
            };

            if (!jwtTokenHandler.CanReadToken(tokenString.Replace("Bearer ", string.Empty)))
            {
                Console.WriteLine("Invalid Token");
                return false;
            }

            jwtTokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var validatedToken);
            return validatedToken != null;
        }

        public string HashPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentNullException("Password can't be null");
            }

            byte[] salt;
            byte[] subkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, SaltSize, PBKDF2IterCount))
            {
                salt = deriveBytes.Salt;
                subkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }

            var outputBytes = new byte[1 + SaltSize + PBKDF2SubkeyLength];
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, PBKDF2SubkeyLength);
            return Convert.ToBase64String(outputBytes);
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);

            if (hashedPasswordBytes.Length != (1 + SaltSize + PBKDF2SubkeyLength) || hashedPasswordBytes[0] != 0x00)
            {
                return false;
            }

            var salt = new byte[SaltSize];
            Buffer.BlockCopy(hashedPasswordBytes, 1, salt, 0, SaltSize);
            var storedSubkey = new byte[PBKDF2SubkeyLength];
            Buffer.BlockCopy(hashedPasswordBytes, 1 + SaltSize, storedSubkey, 0, PBKDF2SubkeyLength);

            byte[] generatedSubkey;
            using (var deriveBytes = new Rfc2898DeriveBytes(password, salt, PBKDF2IterCount))
            {
                generatedSubkey = deriveBytes.GetBytes(PBKDF2SubkeyLength);
            }
            return ByteArraysEqual(storedSubkey, generatedSubkey);
        }

        private bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }

            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }

        private void CreateTeamOwner(User user)
        {
            var teamOwner = new TeamOwner
            {
                UserId = user.Id
            };
            unitOfWork.TeamOwners.Insert(teamOwner);
            unitOfWork.SaveChanges();
        }
    }
}
