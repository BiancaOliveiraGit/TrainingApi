using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TrainingApi.ErrorMiddleware;

namespace TrainingApi.Data
{
    public partial class Repository 
    {
        public ClientExercise GetClientExerciseById(int id, ILogger<ClientExercise> logger)
        {
            var item = new ClientExercise();
            try
            {
                item = _appDbContext.ClientExercises.Where(w => w.ClientExerciseId == id)
                                                        .Include(i => i.Exercise)
                                                        .Select(s => s).FirstOrDefault();
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientExerciseById: {id}");
            }
            return item;
        }

        public IEnumerable<ClientExercise> GetClientExercises(ILogger<ClientExercise> logger)
        {
            var list = new List<ClientExercise>();
            try
            {
                list = _appDbContext.ClientExercises.Select(s => s)
                                                        .Include(i => i.Exercise).ToList();
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientExercises");
            }
            return list;
        }

        public ClientExercise PostNewClientExercise(ClientExercise newClientExercise, ILogger<ClientExercise> logger)
        {
            try
            {
                //check that ClientExercise doesn't exist
                var exists = _appDbContext.ClientExercises.Where(w => w.ExerciseId == newClientExercise.ExerciseId 
                                                                    && w.ClientWorkoutId == newClientExercise.ClientWorkoutId)
                                                          .Select(s => s).FirstOrDefault();
                if (exists != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("ClientExercise {0} for this Workout Plan already exists", newClientExercise.Exercise.Name));

                var item = _appDbContext.Add(newClientExercise);
                item.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var isOk = _appDbContext.SaveChanges();

                return item.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in PostNewClientExercise: {newClientExercise.Exercise.Name}");
                throw e;
            }
        }

        public ClientExercise UpdateClientExercise(int id, ClientExercise updateClientExercise, ILogger<ClientExercise> logger)
        {
            try
            {
                //get exercise object
                var existingExercise = _appDbContext.Exercises.Where(w => w.ExerciseId == updateClientExercise.ExerciseId)
                                                              .Select(s => s).FirstOrDefault();

                if (existingExercise == null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "This Exercise Doesn't Exist in system");


                //check that ClientExercise exists
                var existingClientExercise = _appDbContext.ClientExercises.Where(w => w.ClientExerciseId == updateClientExercise.ClientExerciseId)
                                                  .Select(s => s).FirstOrDefault();
                if (existingClientExercise != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("ClientExerciseID {0},- {1} Doesn't Exist in system", updateClientExercise.ClientExerciseId, existingExercise.Name));

                //update ClientExercise
                existingClientExercise.Exercise = existingExercise;
                existingClientExercise.ExerciseId = existingExercise.ExerciseId;
                existingClientExercise.IsActive = updateClientExercise.IsActive;
                // existingClientExercise.ClientWorkoutId = updateClientExercise.ClientWorkoutId;    //not allowing to update

                var isOk = _appDbContext.SaveChanges();

                return existingClientExercise;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in UpdateClientExercise: {updateClientExercise.ClientExerciseId} - {updateClientExercise.Exercise.Name}");
            }
            return updateClientExercise;
        }
    }
}
