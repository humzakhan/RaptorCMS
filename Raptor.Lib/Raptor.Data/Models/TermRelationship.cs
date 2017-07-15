﻿using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class TermRelationship
    {
        [Key]
        public int ObjectId { get; set; }
        public int TaxonomyId { get; set; }

        public Taxonomy Taxonomy { get; set; }
    }
}