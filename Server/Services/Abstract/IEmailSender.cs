﻿using System.Threading.Tasks;

namespace SfAttendance.Server.Services.Abstract
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(MailType type, EmailModel emailModel, string extraData);
        bool SendEmail(EmailModel emailModel);
    }
}