using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public class ClientExercise
    {
        [Key]
        public int ClientExerciseId { get; set; }

        [ForeignKey("ClientWorkout")]
        public int ClientWorkoutId { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

       // [NotMapped]
        [ForeignKey(nameof(ExerciseId))]
        public virtual Exercise Exercise { get; set; }

        public bool IsActive { get; set; }
    }
}
