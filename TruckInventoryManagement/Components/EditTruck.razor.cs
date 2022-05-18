using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;
using TruckInventoryManagement.Services;

namespace TruckInventoryManagement.Components
{
    public partial class EditTruck
    {
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }
        [Inject] public ITruckInventoryService TruckInventoryService { get; set; }

        public string ChassisNumber { get; set; }
        public string ModelFamily { get; set; }
        public string ModelNumber { get; set; }
        public string Customer { get; set; }

        [Parameter]
        public string CurrentChassisNumber { get; set; }
        [Parameter]
        public string CurrentModelFamily { get; set; }
        [Parameter]
        public string CurrentModelNumber { get; set; }
        [Parameter]
        public string CurrentCustomer { get; set; }

        protected override void OnInitialized()
        {
            ChassisNumber = CurrentChassisNumber;
        }

        protected void CloseWindow()
        {
            ModalInstance.CancelAsync();
        }

        protected async Task UpdateTruckDetails()
        {
            var request = new EditTruckRequestDto
            {
                ChassisNumber = ChassisNumber,
                ModelFamily = ModelFamily,
                ModelNumber = ModelNumber,
                Customer = Customer
            };

            ChassisNumber = request.ChassisNumber;
            StateHasChanged();

            var response = await TruckInventoryService.UpdateTruckDetails(request);

            await ModalInstance.CloseAsync(ModalResult.Ok(response));
        }
    }
}
