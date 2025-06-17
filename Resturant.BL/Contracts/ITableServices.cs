using Resturant.BL.Features.Tables.Requests;
using Resturant.BL.Features.Tables.Responses;
using Resturant.BL.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant.BL.Contracts
{
    public interface ITableService
    {
        Task<Result<AddTableResponse>> AddAsync(AddTableRequest request);
        Task<Result<DeleteTableResponse>> DeleteAsync(DeleteTableRequest request);
        Task<Result<UpdateTableResponse>> UpdateAsync(UpdateTableRequest request);
        Task<Result<GetTableByIdResponse>> GetByIdAsync(GetTableByIdRequest request);
        Task<Result<GetTableByNumberResponse>> GetByNumberAsync(GetTableByNumberRequest request);
        Task<Result<List<GetAllTablesAvailableResponse>>> GetAvailableTablesAsync();

    }

}
