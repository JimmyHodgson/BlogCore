using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BlogCore.Models.Common
{
    [DataContract]
    public partial class User : IdentityUser
    {
        [Required]
        [DataMember]
        public bool Status { get; set; } = true;
        [Column(TypeName = "datetime")]
        [DataMember]
        [Display(Name = "Created Date")]
        public DateTime CreateDate { get; set; } = DateTime.Now;
        [Column(TypeName = "datetime")]
        [DataMember]
        [Display(Name = "Modified Date")]
        public DateTime? ModifyDate { get; set; }
        [Column(TypeName = "datetime")]
        [DataMember]
        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }
        [Display(Name = "First name")]
        [DataMember]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Bio { get; set; }
        [DataMember]
        public string Title { get; set; }
        //overrides
        [DataMember]
        [Display(Name ="Phone number")]
        public override string PhoneNumber { get => base.PhoneNumber; set => base.PhoneNumber = value; }
        [DataMember]
        [Required]
        [EmailAddress]
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
