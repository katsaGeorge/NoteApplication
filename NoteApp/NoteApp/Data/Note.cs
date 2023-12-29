using System;
using System.Collections.Generic;

namespace NoteApp.Data;

public partial class Note
{
    public int Id { get; set; }

    public string? Subject { get; set; }

    public string? Text { get; set; }

    public int UserId { get; set; }

    public DateTime? Date { get; set; }

    public virtual User User { get; set; } = null!;
}
