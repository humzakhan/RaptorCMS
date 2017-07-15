using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class PostCategory
    {
        [Key]
        public int PostCategoryId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        public string Slug { get; set; }

        public string Description { get; set; }
        public int ParentId { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
