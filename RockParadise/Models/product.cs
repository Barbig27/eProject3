//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RockParadise.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public partial class product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public product()
        {
            this.carts = new HashSet<cart>();
        }
        [Display(Name = "ID")]

        public int id { get; set; }
        [Display(Name = "Name Product")]
        public string name { get; set; }
        [Display(Name = "Price")]
        public int price { get; set; }
        [Display(Name = "Company ID")]
        public int company_id { get; set; }
        [Display(Name = "IMG")]
        public byte[] thumbnail { get; set; }

        [Display(Name = "Creat at")]
        public Nullable<System.DateTime> created_at { get; set; }
        [Display(Name = "Discount %")]
        public Nullable<int> discount { get; set; }
        [Display(Name = "Info")]
        [AllowHtml]
        public string info { get; set; }
        public string describe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }
        public virtual company company { get; set; }
    }
}