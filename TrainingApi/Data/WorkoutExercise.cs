using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TrainingApi.Data
{
    public class WorkoutExercise
    {
        [Key]
        public int WorkoutExerciseId { get; set; }

        [ForeignKey("WorkoutPlan")]
        public int WorkoutPlanId { get; set; }

        [ForeignKey("Exercise")]
        public int ExerciseId { get; set; }

        [ForeignKey(nameof(ExerciseId))]
        public Exercise Exercise { get; set; }

        public bool DoNotUse { get; set; }
    }
}
