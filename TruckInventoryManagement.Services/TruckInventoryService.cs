using System.Collections.Generic;
using System.Threading.Tasks;
using TruckInventoryManagement.Data;
using TruckInventoryManagement.Models;

namespace TruckInventoryManagement.Services
{
    public class TruckInventoryService : ITruckInventoryService
    {
        private readonly IInMemoryTruckInventoryRepository _truckInventoryRespository;

        public TruckInventoryService(IInMemoryTruckInventoryRepository truckInventoryRespository)
        {
            _truckInventoryRespository = truckInventoryRespository;
        }
        public async Task<List<TruckInventoryModel>> GetTruckInventoryList()
            => await _truckInventoryRespository.GetCurrentTruckInventory();

        public async Task<TruckInventoryResponseDto> AddTruckToInventory(AddTruckToInventoryRequestDto requestDto)
            => await _truckInventoryRespository.AddNewTruckToInventory(requestDto);

        public async Task<TruckInventoryResponseDto> RemoveTruckFromInventory(string chassisNumber)
            => await _truckInventoryRespository.RemoveTruckFromInventory(chassisNumber);

        public async Task<List<TruckInventoryModel>> GetFilteredTruckInventoryList(string filter)
            => await _truckInventoryRespository.GetFilteredTruckInventory(filter);

        public async Task<TruckInventoryResponseDto> UpdateTruckDetails(EditTruckRequestDto requestDto)
            => await _truckInventoryRespository.UpdateTruckDetails(requestDto);
    }
}
