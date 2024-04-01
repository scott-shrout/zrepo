namespace DEPUI.Models.Navigation
{
    public class NavigationItem
    {
        public string? Name { get; set; }
        public string? Href { get; set; }
        public NavigationMatchType MatchType { get; set; }
        public string? IconPath { get; set; }
    }
}
