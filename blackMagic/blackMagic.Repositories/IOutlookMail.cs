namespace blackMagic.Repositories
{
    public interface IOutlookMail
    {
        string UniqueId { get; set; }
        string Subject { get; set; }
        string[] Recipients { get; set; }
        string From { get; set; }
    }
}