using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.Pages
{
    public class GreetingCode:ComponentBase
    {
        public string title = "Hi Blazor Server";
        public string name;
        public string address;
        public string greeting;

        public void Welcome()
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(address))
            {
                greeting = $"Welcome {name} from {address} go to blazor app";
            }
        }
    }
}
