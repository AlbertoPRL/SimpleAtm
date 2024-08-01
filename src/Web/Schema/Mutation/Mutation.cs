﻿using SimpleAtm.Application.Common.Interfaces;
using SimpleAtm.Web.Schema.Mutation.User;

namespace SimpleAtm.Web.Schema.Mutation;

public class Mutation
{
    public async Task<CreateUserAccountPayload> CreateAccount(CreateUserAccountInput input, [Service] IIdentityService identityService)
    {
        var response = await identityService.CreateUserAsync(input.UserName, input.Password);
        if(response.Result.Succeeded)
        {
            return new CreateUserAccountPayload(200, response.Result.Errors, true, response.UserId);
        }
        else
        {
            return new CreateUserAccountPayload(400, response.Result.Errors, false, null);
        }
    }
}
