using System;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;

namespace MdHack.Model
{
    public class AppDb : DbContext
    {
        public AppDb(DbContextOptions options) : base(options)
        {

        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<ProductStatus> ProductStatuses { get; set; }
    }

    public class AppUser
    {
        public AppUser()
        {
        }

        public static AppUser WithLogin(string login, string passwordHash)
        {
            return new AppUser()
            {
                Id = Guid.NewGuid(),
                Login = login,
                PasswordHash = passwordHash
            };
        }

        public static AppUser WithFace(string faceId, string faceData)
        {
            return new AppUser()
            {
                Id = Guid.NewGuid(),
                FaceId = faceId,
                FaceData = faceData,
            };
        }

        public Guid Id { get; set; }
        public string FaceId { get; set; }
        public string FaceData { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string PushToken { get; set; }
        public string PushTokenData { get; set; }
        public string Passport { get; set; }
        public byte[] Avatar { get; set; }
    }

    public class ProductStatus
    {
        public Guid Id { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? User { get; set; }
        public string StatusString { get; set; }
        public string Data { get; set; }
        public int? Status { get; set; }
    }
}