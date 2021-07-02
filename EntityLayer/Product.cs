namespace EntityLayer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public long Id { get; set; }

        public long? CategoryId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public bool? IsActive { get; set; }
    }
}
