using Microsoft.EntityFrameworkCore;
using ResHub.Interfaces;
using ResHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResHub.Services
{
    public class Residence : IResidence
    {
        private readonly AppDbContext db;

        public Residence(AppDbContext appDbContext)
        {
            db = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));
        }

        public async Task<List<ResidenceModel>> GetResidences()
        {
            try
            {
                return await db.Residences.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching residences from the database.", ex);
            }
        }

        public async Task<ResidenceModel> AddResidence(ResidenceModel residence)
        {
            try
            {
                db.Residences.Add(residence);
                await db.SaveChangesAsync();
                return residence;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding residence to the database.", ex);
            }
        }

        public async Task<ResidenceModel> GetResidenceDetails(Guid id)
        {
            try
            {
                var residence = await db.Residences.FirstOrDefaultAsync(r => r.Id == id);

                if (residence == null)
                {
                    throw new Exception($"Residence with id {id} not found.");
                }

                return residence;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching residence details for id {id}.", ex);
            }
        }

        public async Task<ResidenceModel> UpdateResidence(ResidenceModel residence)
        {
            try
            {
                db.Entry(residence).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return residence;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating residence with id {residence.Id}.", ex);
            }
        }

        public async Task<(bool, string)> DeleteResidence(ResidenceModel residence)
        {
            try
            {
                db.Residences.Remove(residence);
                await db.SaveChangesAsync();
                return (true, $"Residence with id {residence.Id} deleted successfully.");
            }
            catch (Exception ex)
            {
                return (false, $"Error deleting residence with id {residence.Id}: {ex.Message}");
            }
        }
    }
}
