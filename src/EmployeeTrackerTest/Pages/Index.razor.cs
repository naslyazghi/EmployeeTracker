
using System.Threading.Tasks;
using System.Timers;
using Blazored.LocalStorage;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;


namespace EmployeeTrackerTest.Pages
{
    public partial class Index : ComponentBase
    {

        [Inject]
        private IToastService ToastService { get; set; }
        [Inject]
        private ILocalStorageService LocalStore { get; set; }
        

        private bool ShowWait { get; set; }
        private string WaitTitle { get; set; }
        private string WaitMessage { get; set; }

        private string ConfirmationTitle { get; set; }
        private string ConfirmationMessage { get; set; }
        private string ConfirmDeleteTitle { get; set; }
        private string ConfirmDeleteMessage { get; set; }

        protected EmployeeTrackerTest.Components.ConfirmDelete DeleteConfirmation { get; set; }
        protected EmployeeTrackerTest.Components.Confirm Confirmation { get; set; }

        private void ShowWaitState(string title, string waitStateMessage)
        {
            WaitMessage = waitStateMessage;
            WaitTitle = title;
            ShowWait = true;
        }

        private void HideWaitState()
        {
            WaitMessage = string.Empty;
            WaitTitle = string.Empty;
            ShowWait = false;
        }

        protected void ShowWaitDialog()
        {
            ShowWaitState("Test Title", "Test Wait Message");
            StateHasChanged();
            var aTimer = new Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            HideWaitState();
            InvokeAsync(() =>
            {
                StateHasChanged();
            });

        }

        protected void ShowConfirmDialog()
        {
            ConfirmationTitle = "Test Confirm Title";
            ConfirmationMessage = "Test Confirm Message";
            Confirmation.Show();
            StateHasChanged();
        }

        protected void ShowDeleteDialog()
        {
            ConfirmationTitle = "Test Delete Title";
            ConfirmationMessage = "Test Delete Message";
            DeleteConfirmation.Show();
            StateHasChanged();
        }
        protected void ShowToast()
        {
            ToastService.ShowError("Example Toast Error");
            StateHasChanged();
        }

        protected void ConfirmDelete_Click(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
               //delete confirmed do stuff here
                StateHasChanged();

            }
        }

        protected void ConfirmCancel_Click(bool confirmed)
        {
            Confirmation.Hide();
            if (confirmed)
            {
                //confirmed do stuff here
            }
            
        }
    }
}
