using Microsoft.EntityFrameworkCore;
using System;

namespace TrainingApi.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuiler)
        {
            modelBuiler.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Shoulders"},
                new Category { CategoryId = 2, Name = "Back" },
                new Category { CategoryId = 3, Name = "Legs" }
                );

            modelBuiler.Entity<Client>().HasData(
                new Client
                {
                    ClientId = 1,
                    FirstName = "Buzz",
                    LastName = "Lightyear",
                    HomeAddress = "2 Galaxy Way Milkyway",
                    Email = "buzz@gmail.com",
                    Mobile = 421055555,
                    ObjectIdentifier = "buzzIdentifierNo3456"
                },
                new Client
                {
                    ClientId = 2,
                    FirstName = "Woody",
                    LastName = "Cowboy",
                    HomeAddress = "5 Ranch Road Earth",
                    Email = "woody@gmail.com",
                    Mobile = 421054444,
                    ObjectIdentifier = "woodyIdentifierNo3457",
                }
                );

            modelBuiler.Entity<VideoLibrary>().HasData(
                new VideoLibrary
                {
                    VideoLibraryId = 1,
                    VideoUrl = "https://www.youtube.com/embed/0z-QQPzQHRE",
                    AltTag = "Lateral Raise",
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DoNotUse = false
                },
                new VideoLibrary
                {
                    VideoLibraryId = 2,
                    VideoUrl = "https://www.youtube.com/embed/2hLRHXZs15Y",
                    AltTag = "Incline front Raise",
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DoNotUse = false
                },
                new VideoLibrary
                {
                    VideoLibraryId = 3,
                    VideoUrl = "https://www.youtube.com/embed/Zli1UXH9ZeE",
                    AltTag = "Band Overhead Press",
                    CreateDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    DoNotUse = false
                }
                );

            modelBuiler.Entity<Exercise>().HasData(
                new Exercise { ExerciseId = 1, Name = "Lateral Raise", CategoryId = 1, VideoLibraryId = 1, DoNotUse = false },
                new Exercise { ExerciseId = 2, Name = "Incline Front Raise", CategoryId = 1, VideoLibraryId = 2, DoNotUse = false },
                new Exercise { ExerciseId = 3, Name = "Band Overhead Press", CategoryId = 1, VideoLibraryId = 3, DoNotUse = false }
                );

            modelBuiler.Entity<WorkoutPlan>().HasData(
                new WorkoutPlan { WorkoutPlanId = 4, Name = "Low Impact Shoulders", DoNotUse = false, ImageUrl = "./images/Shoulders.jpg" },
                new WorkoutPlan { WorkoutPlanId = 5, Name = "High Impact Legs", DoNotUse = false, ImageUrl = "./images/legs.jpg" }
                );

            modelBuiler.Entity<WorkoutExercise>().HasData(
                new WorkoutExercise { WorkoutExerciseId = 1, WorkoutPlanId = 4, ExerciseId = 1, DoNotUse = false},
                new WorkoutExercise { WorkoutExerciseId = 2, WorkoutPlanId = 4, ExerciseId = 2, DoNotUse = false },
                new WorkoutExercise { WorkoutExerciseId = 3, WorkoutPlanId = 4, ExerciseId = 3, DoNotUse = false }
                );


            modelBuiler.Entity<ClientWorkout>().HasData(
                new ClientWorkout { ClientWorkoutId = 1, WorkoutPlanId = 4, Frequency = 2, ClientId = 1, ClientExerciseId = 1 },
                new ClientWorkout { ClientWorkoutId = 2, WorkoutPlanId = 4, Frequency = 3, ClientId = 2, ClientExerciseId = 1 }
                );
            //not using this as yet
            modelBuiler.Entity<ClientExercise>().HasData(
                new ClientExercise { ClientExerciseId = 1, ClientWorkoutId = 1, ExerciseId = 1, IsActive = true},
                new ClientExercise { ClientExerciseId = 2, ClientWorkoutId = 1, ExerciseId = 2, IsActive = true },
                new ClientExercise { ClientExerciseId = 3, ClientWorkoutId = 1, ExerciseId = 3, IsActive = true },
                new ClientExercise { ClientExerciseId = 4, ClientWorkoutId = 2, ExerciseId = 2, IsActive = true },
                new ClientExercise { ClientExerciseId = 5, ClientWorkoutId = 2, ExerciseId = 3, IsActive = true },
                new ClientExercise { ClientExerciseId = 6, ClientWorkoutId = 2, ExerciseId = 1, IsActive = false }
                );

        }
    }
}
