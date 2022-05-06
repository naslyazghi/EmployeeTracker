using Microsoft.AspNetCore.Components;

namespace EmployeeTrackerTest.Pages
{
    public partial class Counter: ComponentBase
    {
        private int currentCount = 0;
        private void IncrementCount()
        {
            currentCount++;
        }
    }
}
