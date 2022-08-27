using eShop.Blazor.UI.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Components
{
    public partial class PaginationComponentBase : ComponentBase
    {
        [Parameter]
        public PaginationMetaDataResult Metadata { get; set; }
        [Parameter]
        public int Spread { get; set; }
        [Parameter]
        public EventCallback<int> SelectedPage { get; set; }

        protected List<PagingLink> _links;

        protected override void OnParametersSet()
        {
            CreatePaginationLinks();
        }
        private void CreatePaginationLinks()
        {
            //_links = new List<PagingLink>();
            _links = new List<PagingLink>();
            _links.Add(new PagingLink(Metadata.CurrentPage - 1, "Previous", Metadata.HasPrevious));
            for (int i = 1; i <= Metadata.TotalPages; i++)
            {
                if (i >= Metadata.CurrentPage - Spread && i <= Metadata.CurrentPage + Spread)
                {
                    _links.Add(new PagingLink(i, i.ToString(), true) { Active = Metadata.CurrentPage == i });
                }
            }
            _links.Add(new PagingLink(Metadata.CurrentPage + 1, "Next", Metadata.HasNext));
        }
        protected async Task OnSelectedPage(PagingLink link)
        {
            if (link.Page == Metadata.CurrentPage || !link.Enabled)
                return;
            Metadata.CurrentPage = link.Page;
            await SelectedPage.InvokeAsync(link.Page);
        }
    }
}
