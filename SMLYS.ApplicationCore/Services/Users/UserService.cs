using SMLYS.ApplicationCore.DTOs.User;
using SMLYS.ApplicationCore.Entities.UserAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Users;
using SMLYS.ApplicationCore.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMLYS.ApplicationCore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<SiteUser> _siteUserRepository;

        public UserService(IRepository<SiteUser> siteUserRepository)
        {
            _siteUserRepository = siteUserRepository;
        }

        public UserContext GetUserContextAsync(string userId)
        {
            try
            {
                var userContextSpefification = new UserContextSpefification();
                userContextSpefification.AddUserId(userId);

                var siteUser = _siteUserRepository.GetSingleBySpec(userContextSpefification);

                var result = new UserContext
                {
                    ClinicId = siteUser.ClinicId,
                    ClinicName = siteUser.Clinic.Name,
                    DoctorId = siteUser.DoctorId,
                    DoctorName = siteUser.Doctor != null ? $"{siteUser.Doctor.FirstName} {siteUser.Doctor.LastName}" : string.Empty,
                    SiteUserId = siteUser.Id,
                    SiteUserName = $"{siteUser.FirstName} {siteUser.LastName}",
                    SiteUserLevelId = siteUser.SiteUserLevelId,
                    SiteUserLevelName = siteUser.SiteUserLevel.Name
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot find the site user. ");
            }
        }
    }
}
