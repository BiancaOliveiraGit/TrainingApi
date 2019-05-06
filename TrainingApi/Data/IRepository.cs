using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrainingApi.Data
{
    public interface IRepository
    {
        #region Client
        Client GetClientById(int id, ILogger<Client> logger);
        Client GetClientByObjectIdentifier(string objectIdentifier, ILogger<Client> logger);
        IEnumerable<Client> GetClients(ILogger<Client> logger);
        Client PostNewClient(Client newClient, ILogger<Client> logger);
        Client UpdateClient(int id, Client updateClient, ILogger<Client> logger);
        #endregion

        #region VideoLibrary
        VideoLibrary GetVideoById(int id, ILogger<VideoLibrary> logger);
        IEnumerable<VideoLibrary> GetVideoLibraries(ILogger<VideoLibrary> logger);
        VideoLibrary PostNewVideo(VideoLibrary newVideo, ILogger<VideoLibrary> logger);
        VideoLibrary UpdateVideo(int id, VideoLibrary updateVideo, ILogger<VideoLibrary> logger);
        #endregion

        #region Exercise
        Exercise GetExerciseById(int id, ILogger<Exercise> logger);
        IEnumerable<Exercise> GetExercises(ILogger<Exercise> logger);
        Exercise PostNewExercise(Exercise newExercise, ILogger<Exercise> logger);
        Exercise UpdateExercise(int id, Exercise updateExercise, ILogger<Exercise> logger);
        #endregion

        #region ClientExercise
        ClientExercise GetClientExerciseById(int id, ILogger<ClientExercise> logger);
        IEnumerable<ClientExercise> GetClientExercises(ILogger<ClientExercise> logger);
        ClientExercise PostNewClientExercise(ClientExercise newClientExercise, ILogger<ClientExercise> logger);
        ClientExercise UpdateClientExercise(int id, ClientExercise updateClientExercise, ILogger<ClientExercise> logger);
        #endregion

        #region WorkoutPlans
        WorkoutPlan GetWorkoutPlanById(int id, ILogger<WorkoutPlan> logger);
        IEnumerable<WorkoutPlan> GetWorkoutPlans(ILogger<WorkoutPlan> logger);
        WorkoutPlan PostNewWorkoutPlan(WorkoutPlan newWorkoutPlan, ILogger<WorkoutPlan> logger);
        WorkoutPlan UpdateWorkoutPlan(int id, WorkoutPlan updateWorkoutPlan, ILogger<WorkoutPlan> logger);
        #endregion

        #region ClientWorkouts
        ClientWorkout GetClientWorkoutById(int id, ILogger<ClientWorkout> logger);
        IEnumerable<ClientWorkout> GetClientWorkouts(ILogger<ClientWorkout> logger);
        IEnumerable<ClientWorkout> GetClientWorkoutByClientId(int id, ILogger<ClientWorkout> logger);
        ClientWorkout PostNewClientWorkout(AddClientWorkoutDto newClientWorkout, ILogger<ClientWorkout> logger);
        ClientWorkout UpdateClientWorkout(int id, ClientWorkout updateClientWorkout, ILogger<ClientWorkout> logger);
        #endregion

        #region WorkoutExercise
        WorkoutExercise GetWorkoutExerciseById(int id, ILogger<WorkoutExercise> logger);
        IEnumerable<WorkoutExercise> GetWorkoutExercises(ILogger<WorkoutExercise> logger);
        WorkoutExercise PostNewWorkoutExercise(WorkoutExercise newWorkoutExercise, ILogger<WorkoutExercise> logger);
        WorkoutExercise UpdateWorkoutExercise(int id, WorkoutExercise updateWorkoutExercise, ILogger<WorkoutExercise> logger);
        #endregion

        #region Category
        Category GetCategoryById(int id, ILogger<Category> logger);
        IEnumerable<Category> GetCategories(ILogger<Category> logger);
        Category PostNewCategory(Category newCategory, ILogger<Category> logger);
        Category UpdateCategory(int id, Category updateCategory, ILogger<Category> logger);
        #endregion
    }
}
