using SalesSystem.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.Application.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(Interfaces.IUserRepository cashierRepository)
        {
            Cashiers = cashierRepository;
        }
        public Interfaces.IUserRepository Cashiers { get ; set; }
    }
}
