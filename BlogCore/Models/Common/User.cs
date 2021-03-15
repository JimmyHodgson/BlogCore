using BlogCore.Models.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

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
        public LinkModel Link { get; set; }
        [DataMember]
        public string Bio { get; set; }
        //overrides
        [DataMember]
        [Display(Name ="Phone number")]
        public override string PhoneNumber { get => base.UserName; set => base.UserName = value; }
        [DataMember]
        [Required]
        [EmailAddress]
        public override string Email { get => base.Email; set => base.Email = value; }
    }
}
