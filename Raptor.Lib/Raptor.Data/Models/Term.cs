﻿using System.ComponentModel.DataAnnotations;

namespace Raptor.Data.Models
{
    public class Term
    {
        [Key]
        public int TermId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
    }
}
