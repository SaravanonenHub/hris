using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.IEntries
{
    public interface IRequestService<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetRequestDetails(int id);
    }
}
