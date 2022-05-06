using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OH.Common.EmployeeTrackerTest.ViewModels;
using OH.UI.EmployeeTrackerTest.Helpers;

namespace EmployeeTrackerTest.Pages
{
    public partial class FetchData:ComponentBase
    {
        [Inject] private IWeatherForecastHelper WeatherForecastHelper { get; set; }
        private List<WeatherForecastViewModel> _forecasts;

        protected override async Task OnInitializedAsync()
        {
            var (forecasts, errorMessage) = await WeatherForecastHelper.GetRandomWeatherForecastAsync();
            _forecasts = forecasts;
        }
    }
}
