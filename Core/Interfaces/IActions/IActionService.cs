using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.IActions
{
    public interface IActionService<T> where T : class
    {
        Task<T> CreateAction(T action);

    }
}