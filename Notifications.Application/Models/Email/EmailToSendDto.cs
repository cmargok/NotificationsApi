﻿using System.ComponentModel.DataAnnotations;

namespace Notifications.Application.Models.Email
{
    public class EmailToSendDto
    {    
        public List<To> EmailsTo { get; set; } = [];
        public string Subject{ get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public bool Html { get; set; } = false;
        public string HtmlBody { get; set; } = string.Empty;

        public class To
        {
            public string DisplayName { get; set; } = string.Empty;

            [EmailAddress]
            public string Email { get; set; } = string.Empty;
        }
    }

    
}