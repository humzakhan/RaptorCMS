using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Raptor.Data.Models.Content
{
    public class TermRelationship
    {
        [Key]
        [Column(Order = 1)]
        public int ObjectId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TaxonomyId { get; set; }
        public Guid RowGuid { get; set; }

        public Taxonomy Taxonomy { get; set; }
    }
}