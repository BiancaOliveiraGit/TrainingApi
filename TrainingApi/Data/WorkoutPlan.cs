using System.ComponentModel.DataAnnotations;

namespace TrainingApi.Data
{
    public class WorkoutPlan
    {
        [Key]
        public int WorkoutPlanId { get; set; }
        public string Name { get; set; }
        public bool DoNotUse { get; set; }
        public string ImageUrl { get; set; }
    }
}
