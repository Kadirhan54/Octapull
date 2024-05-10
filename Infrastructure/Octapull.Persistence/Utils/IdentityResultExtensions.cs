﻿using Microsoft.AspNetCore.Identity;
using Octapull.Application.Models;

namespace Octapull.Persistence.Utils
{
    public static class IdentityResultExtensions
    {
        public static Result ToApplicationResult(this IdentityResult result)
        {
            return result.Succeeded
                ? Result.Success()
                : Result.Failure(result.Errors.Select(e => e.Description));
        }
    }
}
