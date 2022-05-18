﻿using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using TruckInventoryManagement.Components;
using TruckInventoryManagement.Models;
using TruckInventoryManagement.Services;

namespace TruckInventoryManagement.Pages
{
    public partial class Index
    {
        [Inject] public ITruckInventoryService TruckInventoryService { get; set; }
        [Inject] public IModalService ModalService { get; set; }

        public string FilterString { get; set; }
        public string Message { get; set; }
        public bool Error { get; set; }

        private List<TruckInventoryModel> _currentTruckInventory = new();

        protected override async Task OnInitializedAsync()
        {
            _currentTruckInventory = await TruckInventoryService.GetTruckInventoryList();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender)
            {
                Message = string.Empty;
                Error = false;
            }
        }

        protected async void OnSearchClick()
        {
            _currentTruckInventory = await TruckInventoryService.GetFilteredTruckInventoryList(FilterString);
        }

        protected async void OnAddTruckClick()
        {
            var title = "Add Truck to Inventory";
            var options = GetModelOptions();
            var response = ModalService.Show<AddTruck>(title, options);
            var modalResult = await response.Result;

            if (modalResult != null && modalResult.Data != null)
            {
                var truckResponseDto = (TruckInventoryResponseDto)modalResult.Data;

                Error = !truckResponseDto.Successful;
                Message = truckResponseDto.Message;
                _currentTruckInventory = await TruckInventoryService.GetTruckInventoryList();
                StateHasChanged();
            }
        }

        protected async void OnEditTruckClick(TruckInventoryModel truck)
        {
            var title = "Edit Truck";
            ModalParameters parameters = new ModalParameters();
            parameters.Add("CurrentChassisNumber", truck.ChassisNumber);
            parameters.Add("CurrentCustomer", truck.Customer);
            parameters.Add("CurrentModelFamily", truck.ModelFamily);
            parameters.Add("CurrentModelNumber", truck.ModelNumber);
            var options = GetModelOptions();
            var response = ModalService.Show<EditTruck>(title, parameters, options);
            var modalResult = await response.Result;

            if (modalResult != null && modalResult.Data != null)
            {
                var truckResponseDto = (TruckInventoryResponseDto)modalResult.Data;

                Error = !truckResponseDto.Successful;
                Message = truckResponseDto.Message;
                _currentTruckInventory = await TruckInventoryService.GetTruckInventoryList();
                StateHasChanged();
            }
        }

       protected async void OnRemoveTruckClick(string ChassisNumberForDelete)
        {
            var title = "Remove Truck from Inventory?";
            var options = GetModelOptions();
            var response = ModalService.Show<RemoveTruck>(title, options);
            var modalResult = await response.Result;

            if (modalResult != null && modalResult.Data != null)
            {
                var truckResponseDto = (TruckInventoryResponseDto)modalResult.Data;

                Error = !truckResponseDto.Successful;
                Message = truckResponseDto.Message;
                await TruckInventoryService.RemoveTruckFromInventory(ChassisNumberForDelete);
                _currentTruckInventory = await TruckInventoryService.GetTruckInventoryList();
                StateHasChanged();
            }
        }

        private ModalOptions GetModelOptions()
        {
            return new ModalOptions()
            {
                HideCloseButton = true,
                DisableBackgroundCancel = true
            };
        }
    }
}
