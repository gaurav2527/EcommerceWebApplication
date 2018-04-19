namespace EcommerceWebApplication.Models.EF
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class UserLogin
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserLogin()
        {
            this.Orders = new HashSet<Order>();
        }

        [Required(ErrorMessage = "This field is required")]
        [DisplayName("Email")]
        [RegularExpression(@"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$", ErrorMessage = "Please enter proper email address ")]
        public string email { get; set; }
        [Required(ErrorMessage = "Password Required")]
        [DisplayName("Password")]
        public string CustomerPassword { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
