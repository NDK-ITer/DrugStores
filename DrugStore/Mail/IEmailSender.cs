namespace DrugStore.Mail
{
    public interface IEmailSender
    {
        Task SenEmailAsync(string email,string subject,,string mess);
    }
}
