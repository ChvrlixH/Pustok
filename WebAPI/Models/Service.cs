using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Models.Common;

namespace WebAPI.Models
{
    public class Service : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Image { get; set; } = null!;
        
        [NotMapped]
        public override bool IsDeleted { get; set; }
    }
}
