﻿using DancePlatform.Infrastructure.Models.Identity;
using DancePlatform.Application.Specifications.Base;

namespace DancePlatform.Infrastructure.Specifications
{
    public class UserFilterSpecification : HeroSpecification<DanceFairAndSquareUser>
    {
        public UserFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p => p.FirstName.Contains(searchString) || p.LastName.Contains(searchString) || p.Email.Contains(searchString) || p.PhoneNumber.Contains(searchString) || p.UserName.Contains(searchString);
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}