using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazozWebAssemblyAppNotCore.Pages
{
    public partial class CallDotNetFromJS
    {
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [JSInvokable]
        public static string CalculateSquareRoot(int number)
        {
            var result = Math.Sqrt(number);
            return $"The square root of {number} is {result}";
        }
    }
}
