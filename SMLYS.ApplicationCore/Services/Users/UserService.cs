using SMLYS.ApplicationCore.Domain.User;
using SMLYS.ApplicationCore.DTOs.Common;
using SMLYS.ApplicationCore.DTOs.User;
using SMLYS.ApplicationCore.Entities.DoctorAggregate;
using SMLYS.ApplicationCore.Entities.UserAggregate;
using SMLYS.ApplicationCore.Interfaces.Repository;
using SMLYS.ApplicationCore.Interfaces.Services.Users;
using SMLYS.ApplicationCore.Specifications.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SMLYS.ApplicationCore.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<SiteUser> _siteUserRepository;
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly UserHandler _userHandler;

        public UserService(IRepository<SiteUser> siteUserRepository, IRepository<Doctor> doctorRepository, UserHandler userHandler)
        {
            _siteUserRepository = siteUserRepository;
            _doctorRepository = doctorRepository;
            _userHandler = userHandler;
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

        public Result RegisterUser(SiteUserModel siteUserModel)
        {
            var userContext = _userHandler.GetUserContext();
            var result = new Result();
            if (userContext == null)
            {
                result.Message = "Session expired. ";
                return result;
            }

            try
            {
                var doctor = new Doctor()
                {
                    Active = true,
                    Age = 1,
                    ClinicId = userContext.ClinicId,
                    CreatedBy = userContext.SiteUserId,
                    CreatedDateUtc = DateTime.UtcNow,
                    Email = siteUserModel.Email,
                    FirstName = siteUserModel.FirstName,
                    Gender = 1,
                    LastName = siteUserModel.LastName,
                    Title = "Dr."
                };

                SiteUser siteUser = new SiteUser() {
                    FirstName = siteUserModel.FirstName,
                    LastName = siteUserModel.LastName,
                    Email = siteUserModel.Email,
                    UserId = siteUserModel.UserId,
                    SiteUserLevelId = siteUserModel.SiteUserLevelId,
                    Active= true,
                    ClinicId = userContext.ClinicId,
                    Doctor = doctor
                };

                _siteUserRepository.Add(siteUser);

                result.Success = true;
                result.Message = "Create user success. ";
            }
            catch (Exception ex)
            {
                result.Message = "Add user failed: " + ex.Message;
            }

            return result;
        }

        public List<SiteUserModel> SearchSiteUsers()
        {

            var searchSiteUserSpecification = new SearchSiteUserSpecification();

            var data = _siteUserRepository.List(searchSiteUserSpecification).ToList();

            var result = data.Select(p => new SiteUserModel {
                Email = p.Email,
                FirstName = p.FirstName,
                LastName = p.LastName,
                SiteUserLevelId = p.SiteUserLevelId,
                SiteUserLevelName = p.SiteUserLevel.Name
            }).ToList();

            return result;
        }
    }
}
