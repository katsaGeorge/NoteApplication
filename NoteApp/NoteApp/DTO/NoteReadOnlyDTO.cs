namespace NoteApp.DTO
{
    public class NoteReadOnlyDTO : BaseDTO
    {
        public string? Subject { set; get; }
        public string? Text { set; get; }
    }
}
