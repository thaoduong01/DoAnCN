using System.ComponentModel.DataAnnotations;

namespace DACHUYENNGANH.Models
{
    public class test
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
