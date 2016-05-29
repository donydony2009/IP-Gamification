using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using System.Web.WebPages.Html;

namespace DAKI.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserBuysPrize> UserBuysPrize { get; set; }
        public DbSet<Prize> Prizes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<PersonHasJobInDep> PersonHasJobInDeps { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<UserHasBadge> UserBadges { get; set; }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int CurrentPoints { get; set; }
        public int Points { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable  <DateTime> BirthDate { get; set; }
        public string Address { get; set; }

    }

    [Table("UserBuysPrize")]
    public class UserBuysPrize
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int PrizeId { get; set; }
        public DateTime Date { get; set; }
    }

    [Table("Prize")]
    public class Prize
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int PrizeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public int Limit { get; set; }
    }
    [Table("Badge")]
    public class Badge
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BadgeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public int NecessaryPoints { get; set; }
    }

    [Table("UserHasBadge")]
    public class UserHasBadge
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int BadgeId { get; set; }
        public int UserId { get; set; }
    }

    public class BadgesModel
    {
        public int BadgeId { get; set; }
        public IEnumerable<Badge> Badges { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ShopModel
    {
        public int CurrentPoints { get; set; }
        public IEnumerable<Prize> Prizes { get; set; }
    }
    
    public class SkillModel
    {
        public string UserName { get; set; }
        public IEnumerable<Skills> Skills { get; set; }
    }

    [Table("Skills")]
    public class Skills
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }
        public string Name { get; set; }
    }

    [Table("UserHasSkill")]
    public class UserSkill
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SkillId { get; set; }
        public int UserId { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
