using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public class ClientWorkout
    {
        [Key]
        public int ClientWorkoutId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

       // [ForeignKey("ClientExercise")]
        public int ClientExerciseId { get; set; }

        ////[NotMapped]
        //[ForeignKey(nameof(ClientExerciseId))]
        //public IEnumerable<ClientExercise> ClientExercises { get; set; }

        [ForeignKey("WorkoutPlan")]
        public int WorkoutPlanId { get; set; }

        // [NotMapped]
        [ForeignKey(nameof(WorkoutPlanId))]
        public WorkoutPlan WorkoutPlan { get; set; }

        [Range(1, 14, ErrorMessage = "The Frenquency Range should be between 1 and 14 days")]
        public int Frequency { get; set; }


    }
}
