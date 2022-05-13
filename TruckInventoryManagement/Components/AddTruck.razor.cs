using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;
using TruckInventoryManagement.Services;

namespace TruckInventoryManagement.Components
{
    public partial class AddTruck
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        [Inject] public ITruckInventoryService TruckInventoryService { get; set; }


        public string ChassisNumber { get; set; }
        public string ModelFamily { get; set; }
        public string ModelNumber { get; set; }
        public string Customer { get; set; }


        protected void CloseWindow()
        {
            ModalInstance.CancelAsync();
        }

        protected async Task AddTruckToInventory()
        {
            var request = new AddTruckToInventoryRequestDto
            {
                ChassisNumber = ChassisNumber,
                ModelFamily = ModelFamily,
                ModelNumber = ModelNumber,
                Customer = Customer
            };

            var response = await TruckInventoryService.AddTruckToInventory(request);

            await ModalInstance.CloseAsync(ModalResult.Ok(response));
        }
    }
}
