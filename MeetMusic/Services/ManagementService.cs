using System;
using System.Linq;
using System.Threading.Tasks;
using MeetMusic.Context;
using MeetMusic.ExceptionMiddleware;
using MeetMusic.Interfaces;
using MeetMusicModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MeetMusic.Services
{
    public class ManagementService : IManagementService
    {
        private readonly MeetMusicDbContext _context;

        public ManagementService(MeetMusicDbContext context)
        {
            _context = context;
        }

        public async Task UpdateFamilies(MusicFamilyUpdateModel[] model)
        {
            try
            {
                //Updates music families
                var familyNames = model.Select(i => i.FamilyName).ToHashSet();
                var existingFamilyNames = await _context.MusicFamilies.Select(f => f.Name).ToArrayAsync();
                foreach (var family in familyNames)
                {
                    if (!existingFamilyNames.Contains(family))
                        await _context.MusicFamilies.AddAsync(new MusicFamilyModel { Name = family });
                }

                await _context.SaveChangesAsync();

                var existingFamilies = await _context.MusicFamilies.ToArrayAsync();
                //Updates music genres and associate their families
                var genres = model.Select(i => i).ToHashSet();
                var existingGenres = await _context.MusicGenres.ToArrayAsync();
                foreach (var genre in genres)
                {
                    //If genre allready exist update family id
                    if (existingGenres.Any(g => g.Name.Equals(genre.FamilyName)))
                    {
                        existingGenres.Single(g => g.Name.Equals(genre.FamilyName)).FamilyId =
                            existingFamilies.Single(f => f.Name.Equals(genre.FamilyName)).Id;
                    }
                    else
                    {
                        await _context.MusicGenres.AddAsync(new MusicGenreModel
                        {
                            Name = genre.GenreName,
                            FamilyId = existingFamilies.Single(f => f.Name.Equals(genre.FamilyName)).Id
                        });
                    }
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new HttpStatusCodeException(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
