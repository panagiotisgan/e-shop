using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Threading.Tasks;
using System;
using eShop.Blazor.UI.Dto_Model;

namespace eShop.Blazor.UI.Components
{
    public class CustomButtonComponentBase : ComponentBase
    {
        [Inject]
        IUserService userService { get; set; }
        [Parameter]
        public EventCallback<User> OnUpdateActive { get; set; }
        [Parameter]
        public User SelectedUser { get; set; }

        public string label = string.Empty;
        ButtonStyle buttonStyle = ButtonStyle.Light;
        public string btnClass = "";
        protected override void OnParametersSet()
        {
            label = SelectedUser.ButtonLabel;
            //buttonStyle = user.StyleOfButton;
            btnClass = SelectedUser.ButtonClass;
            base.OnParametersSet();
        }

        public async Task OnUpdateActivity(User user)
        {
            Console.WriteLine(user.Id.ToString());
            //await userService.SetUserAccountState(user);
            //await grid.Reload();
            await OnUpdateActive.InvokeAsync(user);
            StateHasChanged();
        }
    }
}
