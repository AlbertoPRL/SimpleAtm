﻿using Microsoft.AspNetCore.Identity;

namespace SimpleAtm.Infrastructure.Identity;
public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() : base()
    {
    }

    public ApplicationRole(string roleName) : base(roleName)
    {
    }
}
