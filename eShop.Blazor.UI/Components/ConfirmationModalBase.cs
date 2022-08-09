using Microsoft.AspNetCore.Components;

namespace eShop.Blazor.UI.Components
{
    public class ConfirmationModalBase : ComponentBase
    {
        protected bool ShowModal { get; set; }

        public void Show()
        {
            ShowModal = true;
            StateHasChanged();
        }

        public void Hide()
        {
            ShowModal = false;
            StateHasChanged();
        }

        [Parameter]
        public EventCallback<bool> ShowModalCallback { get; set; }
    }
}
