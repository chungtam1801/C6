using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.Pages
{
    public class DatabindingDemoBase : ComponentBase
    {
        protected string Name { get; set; } = "Tom";
        protected string Color { get; set; } = "background-color:white";
        protected void UpdateColorStyle(ChangeEventArgs e)
        {
            Color = "background-color:" + e.Value.ToString();
        }
    }
}
