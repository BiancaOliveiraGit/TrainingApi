namespace TrainingApi.Data
{
    public class AddClientWorkoutDto
    {
        public int ClientId { get; set; }

        public int WorkoutPlanId { get; set; }

        public int Frequency { get; set; }
    }
}
