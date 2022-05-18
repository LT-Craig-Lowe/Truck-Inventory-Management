using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TruckInventoryManagement.Models;

namespace TruckInventoryManagement.Data
{
    public class InMemoryTruckInventoryRepository : IInMemoryTruckInventoryRepository
    {
        private List<TruckInventoryModel> _truckInventory = new();

        public InMemoryTruckInventoryRepository()
        {
            InitialiseData();
        }

        public async Task<List<TruckInventoryModel>> GetCurrentTruckInventory() => _truckInventory;

        public async Task<TruckInventoryResponseDto> AddNewTruckToInventory(AddTruckToInventoryRequestDto addRequest)
        {
            var responseDto = new TruckInventoryResponseDto();

            if (string.IsNullOrWhiteSpace(addRequest.ChassisNumber) 
                || string.IsNullOrWhiteSpace(addRequest.ModelNumber) 
                || string.IsNullOrWhiteSpace(addRequest.ModelFamily) 
                || string.IsNullOrWhiteSpace(addRequest.Customer))
            {
                responseDto.Successful = false;
                responseDto.Message = $"Not all fields have information entered. Please ensure each of the fields have been filled in.";
                return responseDto;
            }

            //Insert a new record, if the chassis number is unique.
            if (_truckInventory.Any(truck => truck.ChassisNumber == addRequest.ChassisNumber))
            {
                responseDto.Successful = false;
                responseDto.Message = $"A truck with chassis number {addRequest.ChassisNumber} already exists in the inventory. Please enter a unique vehicle chassis number.";
                return responseDto;
            }

            _truckInventory.Add(CreateTruckInventoryModelFromRequestDto(addRequest));
            responseDto.Successful = true;
            responseDto.Message = $"Truck {addRequest.ChassisNumber} added successfully for customer {addRequest.Customer}";
            return responseDto;
        }

        //Remove the truck from inventory, prompting user confirmation first
        public async Task<TruckInventoryResponseDto> RemoveTruckFromInventory(string chassisNumber)
        {
            var responseDto = new TruckInventoryResponseDto();
            if (_truckInventory.Any(truck => truck.ChassisNumber == chassisNumber))
            {
                var truckToRemove = _truckInventory.FirstOrDefault(truck => truck.ChassisNumber == chassisNumber);
                _truckInventory.Remove(truckToRemove);
                responseDto.Successful = true;
                responseDto.Message = $"Successfully removed truck {chassisNumber} from the current inventory.";
            }
            else
            {
                responseDto.Successful = false;
                responseDto.Message = $"No truck found for chassis number {chassisNumber}.";
            }
            return responseDto;
        }

        public async Task<List<TruckInventoryModel>> GetFilteredTruckInventory(string searchString)
        {
            if(string.IsNullOrEmpty(searchString))
            {
                //No search criteria - return entire List as no further processing required
                return _truckInventory.ToList();
            }

            var filteredByChassisResults = _truckInventory.Where(truck => truck.ChassisNumber.StartsWith(searchString) || truck.ChassisNumber.EndsWith(searchString) || truck.ChassisNumber.Contains(searchString));
            var filteredByModelFamily = _truckInventory.Where(truck => truck.ModelFamily.StartsWith(searchString) || truck.ModelFamily.EndsWith(searchString) || truck.ModelFamily.Contains(searchString));
            var filteredByModelNumber = _truckInventory.Where(truck => truck.ModelNumber.StartsWith(searchString) || truck.ModelNumber.EndsWith(searchString) || truck.ModelNumber.Contains(searchString));
            var filteredByCustomer = _truckInventory.Where(truck => truck.Customer.StartsWith(searchString) || truck.Customer.EndsWith(searchString) || truck.Customer.Contains(searchString));

            var filteredResults = new List<TruckInventoryModel>();

            //Catch exceptions related to the results
            try
            {
                filteredResults.AddRange(filteredByChassisResults);
                filteredResults.AddRange(filteredByModelFamily);
                filteredResults.AddRange(filteredByModelNumber);
                filteredResults.AddRange(filteredByCustomer);
            }

            catch (Exception)
            {
                //TODO: Handle any errors when an exception is thrown (likely from a lack of a search criteria)
                filteredResults = _truckInventory.ToList();
            }

            var distinctResults = filteredResults.ToList();
            return distinctResults;
        }

        public async Task<TruckInventoryResponseDto> UpdateTruckDetails(EditTruckRequestDto editTruckRequestDto)
        {
            var responseDto = new TruckInventoryResponseDto();

            var truck = _truckInventory.FirstOrDefault(truck => truck.ChassisNumber == editTruckRequestDto.ChassisNumber);

            if (truck == null)
            {
                responseDto.Successful = false;
                responseDto.Message = $"No truck found for chassis number {editTruckRequestDto.ChassisNumber}. Failed to update truck details.";
                return responseDto;
            }

            truck.ModelFamily = editTruckRequestDto.ModelFamily;
            truck.ModelNumber = editTruckRequestDto.ModelNumber;
            truck.Customer = editTruckRequestDto.Customer;
            responseDto.Successful = true;
            responseDto.Message = $"Truck details for Chassis Number: {truck.ChassisNumber} successfully updated.";
            return responseDto;
        }

        private void InitialiseData()
        {
            _truckInventory.AddRange(
                new TruckInventoryModel[] {
                    new TruckInventoryModel
                    {
                        ChassisNumber = "L513456",
                        ModelFamily = "LF",
                        ModelNumber = "FA 56280G19 LH",
                        Customer = "Mondelez"
                    },
                    new TruckInventoryModel
                    {
                        ChassisNumber = "L510139",
                        ModelFamily = "LF",
                        ModelNumber = "FA 36250G12 LH",
                        Customer = "Mondelez"
                    },
                   new TruckInventoryModel
                    {
                        ChassisNumber = "L510106",
                        ModelFamily = "LF",
                        ModelNumber = "FA 46230I16 RH",
                        Customer = "Royal Mail"
                   }
                });
        }

        private TruckInventoryModel CreateTruckInventoryModelFromRequestDto(AddTruckToInventoryRequestDto addRequest)
        {
            return new TruckInventoryModel
            {
                ChassisNumber = addRequest.ChassisNumber,
                ModelFamily = addRequest.ModelFamily,
                ModelNumber = addRequest.ModelNumber,
                Customer = addRequest.Customer
            };
        }
    }
}
