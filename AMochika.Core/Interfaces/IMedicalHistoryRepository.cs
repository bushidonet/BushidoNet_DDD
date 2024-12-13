using AMochika.Core.Entities;

namespace AMochika.Core.Interfaces;

public interface IMedicalHistoryRepository
{
    Task<MedicalHistory> GetByClienteIdAsync(int clienteId);
    Task AddAsync(MedicalHistory medicalHistory);
    Task<IEnumerable<MedicalHistory>> GetAllAsync();
    Task UpdateAsync(MedicalHistory medicalHistory);
    Task DeleteSoftAsync(int id);
}