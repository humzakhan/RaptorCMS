using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models.Content
{
    public class Taxonomy
    {
        [Key]
        public int TaxonomyId { get; set; }
        public int TermId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }

        public Term Term { get; set; }
    }
}