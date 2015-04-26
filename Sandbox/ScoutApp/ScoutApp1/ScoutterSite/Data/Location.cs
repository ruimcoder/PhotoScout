namespace ScoutterSite.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Location")]
    public partial class Location
    {
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string UserId { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        [StringLength(10)]
        public string CretedBy { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public bool Active { get; set; }

        [StringLength(100)]
        public string Coordinates { get; set; }
    }
}
