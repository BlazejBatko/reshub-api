using ResHub.Models;

namespace ResHub.Interfaces
{
    public interface IResidence
    {
        Task<List<ResidenceModel>> GetResidences(); // Get all records
        Task<ResidenceModel> AddResidence(ResidenceModel residence); // Post new record

        Task<ResidenceModel> GetResidenceDetails(Guid id); // Get record by id

        Task<ResidenceModel> UpdateResidence(ResidenceModel residence); // Patch record

        Task<(bool, string)> DeleteResidence(ResidenceModel residence); // Delete record
    }
}
