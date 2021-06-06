using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tech.Assessment.Repository.Entity
{
    public abstract class BaseEntity<TKey> 
    {
        public TKey Id { get; set; }

        [Required]
        public long CreatedBy { get; set; }

        [Required]
        [Column(TypeName = "datetime2(3)")]
        public DateTime CreatedOn { get; set; }

        public long? LastModifiedBy { get; set; }

        [Column(TypeName = "datetime2(3)")]
        public DateTime? LastModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public bool? IsActive { get; set; }
    }
}
