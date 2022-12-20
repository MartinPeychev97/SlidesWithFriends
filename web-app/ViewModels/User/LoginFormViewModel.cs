namespace web_app.ViewModels.User
{
    using System.ComponentModel.DataAnnotations;
    using static DAL.Constants.SlidesUserConstants;
    public class LoginFormViewModel
    {
        [Required]
        [StringLength(MaxUserNameLength, MinimumLength = MinUserNameLength)]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(MaxPasswordLength, MinimumLength = MinPasswordLength)]
        public string Password { get; set; }
    }
}