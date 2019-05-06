using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }
        public string Name { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("VideoLibrary")]
        public int VideoLibraryId { get; set; }
        public bool DoNotUse { get; set; }

        [ForeignKey(nameof(VideoLibraryId))]
        public VideoLibrary VideoLibrary { get; set; }
        public string ImageUrl { get; set; }
    }
}
