namespace DatingApp.API.DTOs
{
    public class MemberFilterOption
    {
        public string? KeySearch { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; } = 20;
    }
}