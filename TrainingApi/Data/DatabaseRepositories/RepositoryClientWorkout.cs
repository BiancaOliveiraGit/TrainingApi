using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TrainingApi.ErrorMiddleware;
using System.Net;
using Microsoft.Extensions.Logging;

namespace TrainingApi.Data
{
    public partial class Repository 
    {
        public ClientWorkout GetClientWorkoutById(int id, ILogger<ClientWorkout> logger)
        {
            var item = new ClientWorkout();
            try
            {
                item = _appDbContext.ClientWorkouts.Where(w => w.ClientWorkoutId == id)
                                       // .Include(i => i.ClientExercises)
                                        .Include(i => i.WorkoutPlan)
                                        .Select(s => s).FirstOrDefault();
                
                return item;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientWorkoutById: {id}");
            }
            return item;
        }

        public IEnumerable<ClientWorkout>GetClientWorkoutByClientId(int id, ILogger<ClientWorkout> logger)
        {
            var items = new List<ClientWorkout>();
            try
            {
                items = _appDbContext.ClientWorkouts.Where(w => w.ClientId == id)
                                      //  .Include(i => i.ClientExercises)
                                        .Include(i => i.WorkoutPlan)
                                        .Select(s => s).ToList();

                return items;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientWorkoutByClientId: {id}");
            }
            return items;
        }

        public IEnumerable<ClientWorkout> GetClientWorkouts(ILogger<ClientWorkout> logger)
        {
            var list = new List<ClientWorkout>();
            try
            {
                list = _appDbContext.ClientWorkouts
                                      //  .Include(i => i.ClientExercises)
                                        .Include(i => i.WorkoutPlan)
                                        .Select(s => s).ToList();
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error in GetClientWorkouts");
            }
            return list;
        }

        public ClientWorkout PostNewClientWorkout(AddClientWorkoutDto newClientWorkout, ILogger<ClientWorkout> logger)
        {
            try
            {
                //create new ClientWorkout object
                ClientWorkout clientWorkout = new ClientWorkout()
                {
                    ClientId = newClientWorkout.ClientId,
                    Frequency = newClientWorkout.Frequency,
                    WorkoutPlanId = newClientWorkout.WorkoutPlanId,
                };

                //check that workoutplan exists
                var existingWorkout = _appDbContext.WorkoutPlans.Where(w => w.WorkoutPlanId == newClientWorkout.WorkoutPlanId)
                                                     .Select(s => s).FirstOrDefault();

                if (existingWorkout == null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("Workout Plan id: {0} does not exist in the system", newClientWorkout.WorkoutPlanId));

                clientWorkout.WorkoutPlan = existingWorkout;

                //get client 
                //check that ClientWorkout doesn't exist
                var exists = _appDbContext.ClientWorkouts.Where(w => w.WorkoutPlanId == newClientWorkout.WorkoutPlanId
                                                                    && w.ClientId == newClientWorkout.ClientId
                                                                    && w.Frequency == newClientWorkout.Frequency)
                                                          .Select(s => s).FirstOrDefault();
                if (exists != null)
                  throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("ClientWorkout id {0} for {1} Workout Plan with the same frequency already exists", exists.ClientWorkoutId, existingWorkout.Name));

                var item = _appDbContext.Add(clientWorkout);
                item.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var isOk = _appDbContext.SaveChanges();

                return item.Entity;               
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in PostNewClientWorkout for WorkOutPlanID = {newClientWorkout.WorkoutPlanId}");
                throw e;
            }
        }

        public ClientWorkout UpdateClientWorkout(int id, ClientWorkout updateClientWorkout, ILogger<ClientWorkout> logger)
        {
            try
            {
                //get exercise object
                var existingWorkout = _appDbContext.WorkoutPlans.Where(w => w.WorkoutPlanId == updateClientWorkout.WorkoutPlanId)
                                                              .Select(s => s).FirstOrDefault();

                if (existingWorkout == null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "This Workout Plan Doesn't Exist in system");


                //check that ClientWorkout exists
                var existingClientWorkout = _appDbContext.ClientWorkouts.Where(w => w.ClientWorkoutId == updateClientWorkout.ClientWorkoutId)
                                                  .Select(s => s).FirstOrDefault();
                if (existingClientWorkout != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("ClientWorkoutID {0},- {1} Doesn't Exist in system", updateClientWorkout.ClientWorkoutId, existingWorkout.Name));

                //update ClientWorkout
                existingClientWorkout.Frequency = updateClientWorkout.Frequency;
                var isOk = _appDbContext.SaveChanges();

                return existingClientWorkout;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in UpdateCategory: {updateClientWorkout.ClientWorkoutId} - {updateClientWorkout.WorkoutPlan.Name}");
            }
            return updateClientWorkout;
        }
    }
}
