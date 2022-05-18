using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;
using TruckInventoryManagement.Services;

namespace TruckInventoryManagement.Components
{

    public partial class RemoveTruck
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        [Inject] public ITruckInventoryService TruckInventoryService { get; set; }

        public string ChassisNumber { get; set; }

        protected void CloseWindow()
        {
            ModalInstance.CancelAsync();
        }

        protected async Task RemoveTruckFromInventory()
        {
            
            var response = await TruckInventoryService.RemoveTruckFromInventory(ChassisNumber);

            await ModalInstance.CloseAsync(ModalResult.Ok(response));
        }
    }
}
