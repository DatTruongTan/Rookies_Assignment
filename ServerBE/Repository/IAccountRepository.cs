using Microsoft.AspNetCore.Identity;
using ServerBE.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerBE.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> SignUpAsync(SignUpModel registerModel);
        Task<string> LoginAsync(SignUpModel registerModel);
    }
}
