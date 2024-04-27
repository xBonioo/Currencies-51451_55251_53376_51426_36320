﻿using Currencies.Contracts.Helpers;
using Currencies.Contracts.Interfaces;
using Currencies.Contracts.ModelDtos.ExchangeRate;
using Currencies.Contracts.ModelDtos.Role;
using Currencies.DataAccess.Services;
using MediatR;

namespace Currencies.Api.Functions.ExchangeRate.Queries.GetAll;

public class GetExchangeRateListQueryHandler : IRequestHandler<GetExchangeRateListQuery, PageResult<ExchangeRateDto>?>
{
    private readonly IExchangeRateService _exchangeRateService;

    public GetExchangeRateListQueryHandler(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    public async Task<PageResult<ExchangeRateDto>?> Handle(GetExchangeRateListQuery request, CancellationToken cancellationToken)
    {
        return await _exchangeRateService.GetAllExchangeRateAsync(request.Filter, cancellationToken);
    }
}