using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MeetMusic.Context;
using MeetMusic.ExceptionMiddleware;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Utils.Hash;

namespace MeetMusic.Services
{
    public class UserService : IUserService
    {
        private readonly IManagementService _managementService;
        private readonly MeetMusicDbContext _context;

        public UserService(IManagementService service, MeetMusicDbContext context)
        {
            _managementService = service;
            _context = context;
        }

        public async Task<UserModel[]> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToArrayAsync();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound,
                        $"No user with the id '{id}' found");
                return user;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<UserMusicModel[]> GetUserTastes(Guid userId)
        {
            try
            {
                var musicTastes = await _context.UserMusicModels.ToArrayAsync();
                return musicTastes.Where(t => t.UserId.Equals(userId)).ToArray();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<double> GetUsersDistance(Guid firstId, Guid secondId)
        {
            var firstUser = await GetUser(firstId);
            var secondUser = await GetUser(secondId);
            return ComputeDistance(firstUser, secondUser);
        }

        public async Task<Guid> CreateUser(UserModel userModelModel)
        {
            try
            {
                userModelModel.Id = Guid.NewGuid();
                userModelModel.Password = PasswordTool.HashPassword(userModelModel.Password);
                await _context.Users.AddAsync(userModelModel);
                await _context.SaveChangesAsync();
                return userModelModel.Id;
            }
            catch (DbUpdateException e)
            {
                if (e.InnerException != null && e.InnerException.Message.Contains("uplicate"))
                    throw new HttpStatusCodeException(StatusCodes.Status409Conflict, e.InnerException.Message);
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, e.Message);
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<Guid> UpdateUser(Guid id, UserModel userModelModel)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound,
                        $"No user with the id '{id}' found");
                _context.Users.Update(CopyUser(userModelModel, user));
                await _context.SaveChangesAsync();
                return user.Id;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task UpdateUserPosition(Guid id, string latitude, string longitude)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound,
                        $"No user with the id '{id}' found");
                user.Latitude = latitude;
                user.Longitude = longitude;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task UpdateUserTastes(Guid userId, UserMusicModel[] models)
        {
            try
            {
                var musicTastes = await _context.UserMusicModels.ToArrayAsync();
                var userMusicTastes = musicTastes.Where(t => t.UserId.Equals(userId)).ToArray();
                if (userMusicTastes.Any())
                {
                    _context.UserMusicModels.RemoveRange(userMusicTastes);
                    await _context.SaveChangesAsync();
                }
                foreach (var model in models)
                {
                    model.UserId = userId;
                    await _context.UserMusicModels.AddAsync(model);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task SynchronizeUserTastes(Guid id, SynchronizedMusicGenresModel[] models)
        {
            try
            {
                var genresList = new List<string>();
                foreach (var model in models)
                {
                    genresList.AddRange(model.Genres);
                }
                var families = await _managementService.GetAllFamilies();
                var genres = await _managementService.GetAllGenres();
                var familiesList = new List<int>();
                foreach (var genre in genresList)
                {
                    var familyId = genres.SingleOrDefault(g => g.Name.Equals(genre))?.FamilyId;
                    if (familyId == null)
                        continue;
                    var family = families.Single(f => f.Id.Equals(familyId)).Id;
                    familiesList.Add(family);
                }
                var familiesCount = familiesList.GroupBy(i => i).ToDictionary(f => f.Key, f => f.Count());
                var sortedFamiliesCount = familiesCount.OrderByDescending(f => f.Value);
                var position = 1;
                var userMusicTastes = new List<UserMusicModel>();
                foreach (var family in sortedFamiliesCount)
                {
                    userMusicTastes.Add(new UserMusicModel
                    {
                        UserId = id,
                        FamilyId = family.Key,
                        Position = position
                    });
                    position++;
                }
                await UpdateUserTastes(id, userMusicTastes.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<MatchModel[]> MatchUser(Guid id, MatchParametersModel model)
        {
            try
            {
                var matchedUsers = new List<MatchModel>();
                var user = await GetUser(id);
                var userTastes = await GetUserTastes(id);
                var topUserTastes = userTastes.OrderBy(t => t.Position).Take(3).ToArray();
                var users = await GetAllUsers();
                foreach (var item in users)
                {
                    if (item.Id.Equals(id))
                        continue;
                    if (model != null)
                        if (item.Id.Equals(id)
                            || model.Gender != 0 && model.Gender != item.Gender
                            || model.Radius > 0 && ComputeDistance(user, item) > model.Radius)
                            continue;

                    var matchScore = 0.0;
                    var tastes = await GetUserTastes(item.Id);
                    var topTastes = tastes.OrderBy(t => t.Position).Take(3).ToArray();
                    if (!topUserTastes.Select(t => t.FamilyId).Any(x => topTastes.Select(t => t.FamilyId).Any(y => y == x)))
                        continue;
                    foreach (var taste in topUserTastes)
                    {
                        var matchedTaste = topTastes.SingleOrDefault(t => t.FamilyId.Equals(taste.FamilyId));
                        if (matchedTaste == null)
                            continue;
                        matchScore += ComputeMatchScore(matchedTaste.Position, taste.Position);
                    }
                    if (matchScore > 0)
                        matchedUsers.Add(new MatchModel
                        {
                            User = item,
                            MatchScore = matchScore
                        });
                }
                return matchedUsers.OrderByDescending(m => m.MatchScore).ToArray();
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user = _context.Users.Find(id);
                if (user == null)
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, $"No user with the id '{id}' found");
                _context.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        public async Task<Guid> AuthenticateUser(AuthModel authModel)
        {
            try
            {
                var users = await _context.Users.ToArrayAsync();
                var user = users.Single(u => u.Username == authModel.Username);
                if (!PasswordTool.ValidatePassword(authModel.Password, user.Password))
                    throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Invalid password");
                return user.Id;
            }
            catch (HttpStatusCodeException)
            {
                throw;
            }
            catch (ArgumentNullException)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "User not found");
            }
            catch (InvalidOperationException)
            {
                throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "User not found");
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        private UserModel CopyUser(UserModel sourceUserModel, UserModel destUserModel)
        {
            destUserModel.FirstName = sourceUserModel.FirstName;
            destUserModel.LastName = sourceUserModel.LastName;
            destUserModel.Email = sourceUserModel.Email;
            destUserModel.Gender = sourceUserModel.Gender;
            destUserModel.AvatarUrl = sourceUserModel.AvatarUrl;
            destUserModel.Phone = sourceUserModel.Phone;
            destUserModel.BirthDate = sourceUserModel.BirthDate;
            destUserModel.Description = sourceUserModel.Description;
            destUserModel.Latitude = sourceUserModel.Longitude;
            destUserModel.Longitude = sourceUserModel.Longitude;

            return destUserModel;
        }

        private double ComputeDistance(UserModel user, UserModel secondUser)
        {
            var distance = GeoTool.Distance(
                double.Parse(user.Latitude.Replace('.', ',')),
                double.Parse(user.Longitude.Replace('.', ',')),
                double.Parse(secondUser.Latitude.Replace('.', ',')),
                double.Parse(secondUser.Longitude.Replace('.', ',')),
                'K'
            );
            return Math.Round(distance, 2);
        }

        private double ComputeMatchScore(int matchedTastePosition, int userTastePosition)
        {
            int scoreValue;
            switch (userTastePosition)
            {
                case 1:
                    scoreValue = 60;
                    break;
                case 2:
                    scoreValue = 40;
                    break;
                case 3:
                    scoreValue = 30;
                    break;
                default:
                    throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, "Something went wrong during tastes ranking");
            }
            return Math.Round((double)scoreValue/matchedTastePosition, 1);
        }
    }
}
