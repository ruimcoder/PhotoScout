namespace ScoutterSite.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LocationImage")]
    public partial class LocationImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(10)]
        public string ImageTitle { get; set; }

        [StringLength(10)]
        public string FileName { get; set; }

        [StringLength(10)]
        public string OriginalFileName { get; set; }

        public DateTime? DateCreated { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }

        [StringLength(10)]
        public string ModifiedBy { get; set; }

        public bool? Active { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public string Notes { get; set; }

        public DateTime? DateModified { get; set; }
    }
}
