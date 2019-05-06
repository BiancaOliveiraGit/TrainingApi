using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public class VideoLibrary
    {
        [Key]
        public int VideoLibraryId { get; set; }
        public string VideoUrl { get; set; }
        public string AltTag { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool DoNotUse { get; set; }
    }
}
