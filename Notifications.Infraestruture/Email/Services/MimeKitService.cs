﻿using Notifications.Application.Email.Contracts;
using Notifications.Application.Models.Email;

namespace Notifications.Infraestruture.Email.Services
{
    public class MimeKitService : IEmailService
    {
        public Task<bool> SendEmail(EmailToSendDto email, OutlookCredentials remitente)
        {
            throw new NotImplementedException();
        }
    }
}