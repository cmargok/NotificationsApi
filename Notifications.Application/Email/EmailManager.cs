using Notifications.Application.Email.Contracts;
using Notifications.Application.Email.Contracts.Factory;
using Notifications.Application.Models.Email;
using Notifications.Application.Utils;

namespace Notifications.Application.Email
{
    public class EmailManager : IEmailManager
    {
        private readonly IEmailService _emailService;        

        public EmailManager(IEmailStrategy emailFactory)
        {
            _emailService = emailFactory.GetEmailService();
        }

        public async Task<ApiResponse<bool>> SendEmailAsync(EmailToSendDto emailToSend)
        {
            if(!ValidateInput(emailToSend))
                return Response.Error(false, "sent has successfully failed", "there are some problem while the validation request");                      

            var response = await _emailService.SendEmail(emailToSend);

            return Response.Success(true, message: "email sent");
        }  


     

        private static bool ValidateInput(EmailToSendDto emailToSend)
        {
            ArgumentNullException.ThrowIfNull(emailToSend);

            if (emailToSend.EmailsTo.Count <= 0 || emailToSend.EmailsTo.Any(to => string.IsNullOrEmpty(to.Email)))
                return false;

            if (string.IsNullOrEmpty(emailToSend.Subject) && emailToSend.Subject.Length > 2)
                return false;

            if (emailToSend.Html && string.IsNullOrEmpty(emailToSend.HtmlBody))
                return false;

            if (!emailToSend.Html && string.IsNullOrEmpty(emailToSend.Message))
                return false;

            return true;
        }

    }

}
