﻿//-------------------------------------------------------------------------------------
//     Copyright (c) 2014, Anthilla S.r.l. (http://www.anthilla.com)
//     All rights reserved.
//
//     Redistribution and use in source and binary forms, with or without
//     modification, are permitted provided that the following conditions are met:
//         * Redistributions of source code must retain the above copyright
//           notice, this list of conditions and the following disclaimer.
//         * Redistributions in binary form must reproduce the above copyright
//           notice, this list of conditions and the following disclaimer in the
//           documentation and/or other materials provided with the distribution.
//         * Neither the name of the Anthilla S.r.l. nor the
//           names of its contributors may be used to endorse or promote products
//           derived from this software without specific prior written permission.
//
//     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
//     ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
//     WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
//     DISCLAIMED. IN NO EVENT SHALL ANTHILLA S.R.L. BE LIABLE FOR ANY
//     DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
//     (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
//     LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
//     ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
//     (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
//     SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
//
//     20141110
//-------------------------------------------------------------------------------------

using antdlib.Mail;
using antdlib.Security;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace antdlib.Auth.T2FA {
    public class Authentication {
        public static void SendNotification(string session, string alias, string to) {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Anthilla Authentication Manager", "authentication.manager@anthilla.com"));
            message.To.Add(new MailboxAddress(alias, to));
            message.Subject = DateTime.Now.ToString("yyyy/MM/dd") + " Anthilla Authentication Token";
            var token = TokenRepository.Create(session);
            message.Body = new TextPart("plain") {
                Text = @"Here's your token: " + token.Value
            };
            using (var client = new SmtpClient()) {
                client.Connect(Smtp.Settings.Url, Convert.ToInt32(Smtp.Settings.Port), false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(Smtp.Settings.Account, Smtp.Settings.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

        public static bool Confirm(string session, string value) {
            var savedToken = TokenRepository.Fetch(session);
            return (value == savedToken);
        }
    }
}
