using System;


namespace MOON.Entities.User
{
    /// <summary>
    /// Defines the <see cref="UserEntity" />.
    /// </summary>
    public class UserEntity
    {
        /// <summary>
        /// Gets or sets the User id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Gets or sets the Role id.
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        /// 
        public string Username { get; set; }
        /// <summary>
        /// Gets or sets First Name.
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Gets or sets Second Name.
        /// </summary>
        public string SecondName { get; set; }
        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Gets or sets the ConfirmPassword.
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets Address.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Gets or sets the Mobile.
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// Gets or sets the Gender.
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Gets or sets the Profile.
        /// </summary>
        public string Profile { get; set; }
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the UpdatedAt.
        /// </summary>
        public DateTime UpdatedAt { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="UserEntity"/> class.
        /// </summary>
        public UserEntity()
        {
            InitializedObjectValue();
        }

        /// <summary>
        /// The InitializedObjectValue.
        /// </summary>
        internal void InitializedObjectValue()
        {
            this.UserId = 0;
            this.RoleId = 0;
            this.Username = string.Empty;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.Address = string.Empty;
            this.Password = string.Empty;
            this.ConfirmPassword = string.Empty;
            this.Email = string.Empty;
            this.Mobile = string.Empty;
            this.Gender = string.Empty;
            this.Profile = string.Empty;
            this.CreatedAt = DateTime.Now;
            this.UpdatedAt = DateTime.Now;
        }
    }
}
