using System.ComponentModel.DataAnnotations;

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