using VisitorManagement2022.DTO;

namespace VisitorManagement2022.Service
{
    public interface IAPI
    {
        Task<Root> WeatherAPI();
    }
}