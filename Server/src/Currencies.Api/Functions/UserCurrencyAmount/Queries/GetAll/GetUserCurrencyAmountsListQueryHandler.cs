﻿using Currencies.Contracts.Helpers;
using Currencies.Contracts.Interfaces;
using Currencies.Contracts.ModelDtos.User.CurrencyAmount;
using MediatR;

namespace Currencies.Api.Functions.UserCurrencyAmount.Queries.GetAll;

public class GetUserCurrencyAmountsListQueryHandler : IRequestHandler<GetUserCurrencyAmountsListQuery, PageResult<UserCurrencyAmountDto>?>
{
    private readonly IUserCurrencyAmountService _userCurrencyAmountService;

    public GetUserCurrencyAmountsListQueryHandler(IUserCurrencyAmountService userCurrencyAmountService)
    {
        _userCurrencyAmountService = userCurrencyAmountService;
    }

    public async Task<PageResult<UserCurrencyAmountDto>?> Handle(GetUserCurrencyAmountsListQuery request, CancellationToken cancellationToken)
    {
        return await _userCurrencyAmountService.GetAllUserCurrencyAmountsAsync(request.Filter, cancellationToken);
    }
}
