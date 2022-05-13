using System.Collections.Generic;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;

namespace TruckInventoryManagement.Data
{
    public interface IInMemoryTruckInventoryRepository
    {
        Task<TruckInventoryResponseDto> AddNewTruckToInventory(AddTruckToInventoryRequestDto addRequest);
        Task<List<TruckInventoryModel>> GetCurrentTruckInventory();
        Task<List<TruckInventoryModel>> GetFilteredTruckInventory(string searchString);
        Task<TruckInventoryResponseDto> RemoveTruckFromInventory(string chassisNumber);
        Task<TruckInventoryResponseDto> UpdateTruckDetails(EditTruckRequestDto editTruckRequestDto);
    }
}