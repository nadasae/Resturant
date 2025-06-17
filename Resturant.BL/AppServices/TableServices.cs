using AutoMapper;
using Resturant.BL.Shared;
using Resturant.BL.Contracts;
using Resturant.BL.Features.Tables.Requests;
using Resturant.BL.Features.Tables.Responses;
//using Resturant.BL.Shared;
using Resturant.Core.Entities.Models;
using Resturant.Core.Interfaces.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Resturant.BL.AppServices
{
    public class TableService : ITableService
    {
        private readonly ITableRepository Tables;
        private readonly IOrderRepository Orders;
        private readonly IMapper _mapper;

        public TableService(
            ITableRepository tables,
            IOrderRepository orders,
            IMapper mapper)
        {
            Tables = tables;
            Orders = orders;
            _mapper = mapper;
        }

        public async Task<Result<AddTableResponse>> AddAsync(AddTableRequest request)
        {
            var table = _mapper.Map<Table>(request);
            table.IsAvailable = true;

            await Tables.AddAsync(table);
            await Tables.SaveChangesAsync();

            var tabMap = _mapper.Map<AddTableResponse>(table);
            return Result<AddTableResponse>.Success(tabMap);
        }

        public async Task<Result<DeleteTableResponse>> DeleteAsync(DeleteTableRequest request)
        {
            var table = await Tables.GetByIdAsync(request.Id);
            if (table == null)
                return Result<DeleteTableResponse>.Fail("Table not found");

            Tables.Delete(table);
            await Tables.SaveChangesAsync();
            var tabMap = _mapper.Map<DeleteTableResponse>(table);
            return Result<DeleteTableResponse>.Success(tabMap);
        }

        public async Task<Result<UpdateTableResponse>> UpdateAsync(UpdateTableRequest request)
        {
            var table = await Tables.GetByIdAsync(request.Id);
            if (table == null)
                return Result<UpdateTableResponse>.Fail("Table not found");

            //table.TableNumber = request.Number;
            //table.Capacity = request.Capacity;
            var tab = _mapper.Map<Table>(request);

            if (request.OrdersIds?.Any() == true)
            {
                var orders = await Task.WhenAll(request.OrdersIds
                    .Select(id => Orders.GetByIdAsync(id)));

                tab.Orders = orders.Where(o => o != null).ToList()!;
            }

            Tables.Update(tab);
            await Tables.SaveChangesAsync();
            var tabMap = _mapper.Map<UpdateTableResponse>(tab);
            return Result<UpdateTableResponse>.Success(tabMap);
        }

        public async Task<Result<GetTableByIdResponse>> GetByIdAsync(GetTableByIdRequest request)
        {
            var table = await Tables.GetByIdAsync(request.Id);
            if (table == null)
                return Result<GetTableByIdResponse>.Fail("Table not found");

            var response = _mapper.Map<GetTableByIdResponse>(table);
            return Result<GetTableByIdResponse>.Success(response);
        }

        public async Task<Result<GetTableByNumberResponse>> GetByNumberAsync(GetTableByNumberRequest request)
        {
            var table = await Tables.GetTableByNumberAsync(request.Number);
            if (table == null)
                return Result<GetTableByNumberResponse>.Fail("Table not found");

            var response = _mapper.Map<GetTableByNumberResponse>(table);
            return Result<GetTableByNumberResponse>.Success(response);
        }

        public async Task<Result<List<GetAllTablesAvailableResponse>>> GetAvailableTablesAsync()
        {
            var tables = await Tables.GetAvailableTablesAsync();
            var response = _mapper.Map<List<GetAllTablesAvailableResponse>>(tables);
            return Result<List<GetAllTablesAvailableResponse>>.Success(response);
        }
    }
}
