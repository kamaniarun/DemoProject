namespace EntityLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "Category Name is Required")]
        [StringLength(50)]
        public string CategoryName { get; set; }

        public bool? IsActive { get; set; }

        [NotMapped]
        public long SrNo { get; set; }
    }
}
