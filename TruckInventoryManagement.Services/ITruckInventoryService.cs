using System.Collections.Generic;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;

namespace TruckInventoryManagement.Services
{
    public interface ITruckInventoryService
    {
        Task<TruckInventoryResponseDto> AddTruckToInventory(AddTruckToInventoryRequestDto requestDto);
        Task<List<TruckInventoryModel>> GetFilteredTruckInventoryList(string filter);
        Task<List<TruckInventoryModel>> GetTruckInventoryList();
        Task<TruckInventoryResponseDto> RemoveTruckFromInventory(string chassisNumber);
        Task<TruckInventoryResponseDto> UpdateTruckDetails(EditTruckRequestDto requestDto);
    }
}