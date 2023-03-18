using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;
using Core.Interfaces;

namespace Infrastructure.Data.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // public async Task<AppUser> CreateUser(AppUser user)
        // {
        //     await _unitOfWork.Repository<AppUser>().Add(user);
        //     var result = await _unitOfWork.Complete();
        //     if (result <= 0) return null;
        //     // return branch
        //     return user;
        // }
    }
}