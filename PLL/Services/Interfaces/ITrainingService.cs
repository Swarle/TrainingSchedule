using Microsoft.AspNetCore.Mvc;
using PLL.Data.Entity;

namespace PLL.Services.Interfaces
{
    public interface ITrainingService
    {
        Task<Training> OverviewTrainingAsync();
        Task AddMuscleGroupAsync(string muscleGroupName);
        Task AddExerciseAsync(string exerciseName,string muscleGroupId);
        Task AddSetAsync(string exerciseId,int numberRepetition,
            int weight, string unitName, string muscleGroupId);
        Task UndoAsync();
    }
}
