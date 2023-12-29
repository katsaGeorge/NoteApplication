namespace NoteApp.DTO
{
    public class NoteCreateDTO
    {
        public string? Subject { set; get; }
        public string? Text { set; get; }

        public DateTime? date { set; get; }
        public int ? UserID { set; get; }
    }
}
