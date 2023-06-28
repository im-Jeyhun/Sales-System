using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Cashiers { get; set; }
    }
}
