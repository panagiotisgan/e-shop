namespace eShop.Blazor.UI.Helpers
{
    public class PagingLink
    {
        public int Page { get; set; }
        public string Text { get; set; }
        public bool Enabled { get; set; }
        public bool Active { get; set; }
        public PagingLink(int page, string text, bool enable)
        {
            Page = page;
            Text = text;
            Enabled = enable;
        }
    }
}
