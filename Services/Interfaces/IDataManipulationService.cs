using ChallengeApplication.Models.DTO;
using System.Threading.Tasks;

namespace ChallengeApplication.Services.Interfaces
{
    public interface IDataManipulationService
    {
        Task<ImpressionsDTO> AggregateImpressionsDataAsync();
        Task<AnomaliesDatesDTO> FindAnomaliesDatesAsync();
    }
}
