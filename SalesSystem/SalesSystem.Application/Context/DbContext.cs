﻿//using SalesSystem.Application.Context;
//using SalesSystem.Application.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SalesSytem.Data.Context
//{
//    public class DbContext
//    {
//        private IDbStrategy _dbStrategy;

//        public DbContext GetStrategy()
//        {
//            _dbStrategy = new SqlServerConnection();

//            return this;
//        }

//        public IDbConnection GetDbContext(string connectionString)
//        {
//            return _dbStrategy.GetConnection(connectionString);
//        }
//    }
//}
