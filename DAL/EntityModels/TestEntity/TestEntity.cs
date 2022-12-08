using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DAL.Enums;

namespace DAL.EntityModels.TestEntity
{
    public class TestEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Name { get; set; }
    }
}
