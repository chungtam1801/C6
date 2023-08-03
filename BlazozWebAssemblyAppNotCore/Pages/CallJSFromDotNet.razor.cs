using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazozWebAssemblyAppNotCore.Model;

namespace BlazozWebAssemblyAppNotCore.Pages
{
    public partial class CallJSFromDotNet
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        private IJSObjectReference _jsModule;
        public string _registrationResult { get; set; }
        private string _detailMessage;

        protected override async void OnInitialized()
        {
            _jsModule = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./scripts/jsExample.js");
        }
        private async Task ShowAlertWindow()
        {
            await _jsModule.InvokeVoidAsync("showAlert",new {Name = "Nguyen Chung Tam",Age = 20 });
        }
        private async Task RegisterEmail()
        {
            _registrationResult = await _jsModule.InvokeAsync<string>("emailRegistration", "Please provide your email");
        }
        private async Task ExtractEmailInfo()
        {
            var emailDetail = await _jsModule.InvokeAsync<EmailDetail>("splitEmailDetail", "Please provide your email");
            if (emailDetail != null) _detailMessage = $"Name: {emailDetail.Name}, Server: {emailDetail.Server}, Domain: {emailDetail.Domain}";
            else _detailMessage = "Email is not provided";
        }
    }
}
