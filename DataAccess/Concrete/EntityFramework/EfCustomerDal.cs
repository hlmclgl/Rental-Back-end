﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTO_s;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCustomerDal : EfEntityRepositoryBase<Customer, RentaCarContext>, ICustomerDal
    {
        public List<CustomerDetailDto> GetCustomerDetails(Expression<Func<Customer, bool>> filter = null)
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from c in context.Customers
                             join u in context.Users
                             on c.UserId equals u.UserId
                             join f in context.CustomerFindexScores
                             on c.Id equals f.CustomerId
                             select new CustomerDetailDto
                             {
                                 UserId = u.UserId,
                                 CustomerName = c.CustomerName,
                                 Email = u.Email,
                                 FindexScore = f.FindexScore

                             };
                return result.ToList();
            }
        }
    }
}
