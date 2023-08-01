namespace AndrewsWebsite;

public class Space
{
    public Space()
    {
        Letter = null;
    }
    public string? Letter { get; set; }

    public Space Copy()
    {
        return new Space { Letter = Letter };
    }
}
