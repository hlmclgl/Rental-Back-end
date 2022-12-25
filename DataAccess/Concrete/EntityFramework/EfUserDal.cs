﻿using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Entities.DTO_s;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RentaCarContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RentaCarContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == user.UserId
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }

        public UserDetailDto GetUserDetails(Expression<Func<UserDetailDto, bool>> filter = null)
        {
            using (RentaCarContext context = new RentaCarContext())
            {
                var result = from u in context.Users
                             join c in context.Customers
                            on u.UserId equals c.UserId
                             join f in context.CustomerFindexScores
                             on c.Id equals f.CustomerId
                             select new UserDetailDto
                             {
                                 UserId = u.UserId,
                                 FirstName = u.FirstName,
                                 LastName = u.LastName,
                                 Email = u.Email,
                                 CustomerId = c.Id,
                                 FindexScore = f.FindexScore

                             };
                return result.SingleOrDefault(filter);
            }
        }
    }
}
