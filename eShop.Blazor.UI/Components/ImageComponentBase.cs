using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Components
{
    public class ImageComponentBase : ComponentBase
    {
        [Inject]
        protected IImageService ImageService { get; set; }
        [Parameter]
        public long ImageId { get; set; }

        [Parameter]
        public string ImagePath { get; set; }
        [Parameter]
        public EventCallback OnImageDeleted { get; set; }
        protected async Task OnDeleteImage()
        {
            if (ImageId == 0)
            {
                await OnImageDeleted.InvokeAsync();
                return;
            }
            await ImageService.DeleteImageAsync(ImageId);
            await OnImageDeleted.InvokeAsync();
        }


    }
}
