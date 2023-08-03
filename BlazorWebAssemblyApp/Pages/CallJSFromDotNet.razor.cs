using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Runtime.Serialization;

namespace BlazorWebAssemblyApp.Pages
{
    public partial class CallJSFromDotNet
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
    }
}
