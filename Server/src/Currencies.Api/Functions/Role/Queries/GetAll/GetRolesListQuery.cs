﻿using MediatR;
using Currencies.Contracts.ModelDtos.Role;
using Currencies.Contracts.Response;

namespace Currencies.Api.Functions.Role.Queries.GetAll;

public class GetRolesListQuery : IRequest<PageResult<RoleDto>>
{
    public FilterRoleDto Filter;

    public GetRolesListQuery(FilterRoleDto filter)
    {
        Filter = filter;
    }
}