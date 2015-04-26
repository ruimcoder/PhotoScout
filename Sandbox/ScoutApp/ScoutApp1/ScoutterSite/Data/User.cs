namespace ScoutterSite.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long Id { get; set; }

        [StringLength(255)]
        public string UserEmail { get; set; }

        [StringLength(50)]
        public string UserAlias { get; set; }

        [StringLength(2048)]
        public string UserPassword { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        [StringLength(255)]
        public string CreatedBy { get; set; }

        [StringLength(255)]
        public string ModifiedBy { get; set; }

        [StringLength(128)]
        public string Country { get; set; }
    }
}
