﻿//-------------------------------------------------------------------------------------
//     Copyright (c) 2014, Anthilla S.r.l. (http://www..com)
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

using System.Dynamic;
using antdlib.Auth;
using Nancy;
using Nancy.Security;

namespace Antd.Modules {
    public class TokenMgmtModule : CoreModule {
        public TokenMgmtModule() {
            this.RequiresAuthentication();

            Get["/tkn"] = x => {
                dynamic vmod = new ExpandoObject();
                vmod.Tokens = TokenAuthentication.Show();
                return View["page-tknmgmt", vmod];
            };

            Post["/tkn"] = x => {
                string u = Request.Form.Username;
                string t = Request.Form.Token;
                TokenAuthentication.AssignOtpToken(u, t);
                return Response.AsJson(true);
            };

            Post["/tkn/remove"] = x => {
                string u = Request.Form.Username;
                TokenAuthentication.DeleteRelation(u);
                return Response.AsJson(true);
            };

            Post["/tkn/u2f"] = x => {
                string u = Request.Form.Username;
                string p = Request.Form.Password;
                string t = Request.Form.Token;
                var v = TokenAuthentication.Validate(u, p, t);
                return v ? HttpStatusCode.OK : HttpStatusCode.Forbidden;
            };
        }
    }
}