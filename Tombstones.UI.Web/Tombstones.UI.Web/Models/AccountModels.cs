using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;
using System.Web.Security;

namespace Tombstones.UI.Web.Models
{

    public class ChangePasswordModel
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
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
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

    public class UserModel
    {
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime LastAuthenicationDate { get; set; }

        public bool DoesPasswordMatch(string password)
        {
            var hashedPassword = GetHashOfString(password);

            return PasswordHash.CompareTo(hashedPassword) == 0;
        }

        private string GetHashOfString(string input)
        {
            var hasher = System.Security.Cryptography.SHA256.Create();
            var encoder = new ASCIIEncoding();
            hasher.Initialize();
            var hashData = hasher.ComputeHash(encoder.GetBytes(input));

            return BytesToBase64(hashData);
            
        }

        private string StringToBase64(string input)
        {
            var encoder = new ASCIIEncoding();
            byte[] bytes = encoder.GetBytes(input);
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
        private string BytesToBase64(byte[] bytes)
        {
            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
