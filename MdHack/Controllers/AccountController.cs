using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MdHack.Controllers.Models;
using MdHack.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace MdHack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController: ControllerBase
    {
        private readonly AppDb _appDb;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountController(AppDb appDb, IHttpContextAccessor httpContextAccessor)
        {
            _appDb = appDb;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Register
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid registration type</exception>
        [HttpPost("register")]
        [SwaggerOperation("Register")]
        public async Task<AuthModel> Register(RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Login) || string.IsNullOrEmpty(model.Password))
            {
                return null;
            }
            var passwordHash = AuthHelper.Hash(model.Password);
            var user = await _appDb.Users.FirstOrDefaultAsync(a => a.Login == model.Login && a.PasswordHash == passwordHash);
            if (user!=null)
            {
                return new AuthModel(user.Id.ToString(), AuthHelper.GetToken(user.Id.ToString()));
            }
            var newUser = AppUser.WithLogin(model.Login, passwordHash);
            await _appDb.Users.AddAsync(newUser);
            await _appDb.SaveChangesAsync();
            return new AuthModel(newUser.Id.ToString(), AuthHelper.GetToken(newUser.Id.ToString()));
        }

        [HttpGet("avatar")]
        public async Task<IActionResult> Avatar()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            var user = await _appDb.Users.FirstOrDefaultAsync(f => f.Id.ToString() == userId);
            if (user?.Avatar != null)
            {
                return File(user.Avatar, "image/png");
            }
            return File(System.IO.File.OpenRead(System.IO.Path.Combine("wwwroot", "pic15@2x.png")), "image/png");
        }

        [HttpPost("add-push")]
        public async Task AddPush([FromBody] AddPushModel model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid).Value;
            var user = await _appDb.Users.FirstOrDefaultAsync(f => f.Id.ToString() == userId);
            if (user != null)
            {
                user.PushToken = model.PushToken;
                user.PushTokenData = model.PushTokenData;
                await _appDb.SaveChangesAsync();
            }
        }
    }

    public static class AuthHelper
    {
        public static string Hash(string password)
        {
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
                // without dashes
                .Replace("-", string.Empty)
                // make lowercase
                .ToLower();
            return encoded;
        }
        public static string GetToken(string userId)
        {
            var now = DateTime.UtcNow;
            var claims = new[]
            {
                new Claim(ClaimTypes.Sid, userId),
            };
            var jwt = new JwtSecurityToken(
                issuer: "issuer",
                audience: "audience",
                notBefore: now,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(1000)),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-puper-mega-secret")),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }
    }
}