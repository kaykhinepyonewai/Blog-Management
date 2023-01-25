using System;

namespace MOON.Entities.Role
{
    /// <summary>
    /// Defines the <see cref="RoleEntity" />.
    /// </summary>
    public class RoleEntity
    {
        /// <summary>
        /// Gets or sets the Role id.
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Gets or sets the Role.
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// Gets or sets the CreatedAt.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleEntity"/> class.
        /// </summary>
        public RoleEntity()
        {
            InitializedObjectValue();
        }
        /// <summary>
        /// The InitializedObjectValue.
        /// </summary>
        internal void InitializedObjectValue()
        {
            this.RoleId = 0;
            this.Role = String.Empty;
            this.CreatedAt = DateTime.Now;
        }
    }
}
