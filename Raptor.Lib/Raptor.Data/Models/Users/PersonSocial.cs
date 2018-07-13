using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Raptor.Data.Models.Users
{
    public class SocialProfile
    {
        public int SocialProfileId { get; set; }

        [ForeignKey(nameof(Person))]
        public int BusinessEntityId { get; set; }

        public string FacebookUrl { get; set; }

        public string TwitterUrl { get; set; }

        public string InstagramUrl { get; set; }

        public string YoutubeUrl { get; set; }

        public virtual Person Person { get; set; }
    }
}
