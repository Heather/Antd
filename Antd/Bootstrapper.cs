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

using System.Collections.Generic;
using antdlib.Auth;
using Antd2;
using Nancy;
using Nancy.Authentication.Forms;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.TinyIoc;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using System;

namespace Antd {

    public class Bootstrapper : DefaultNancyBootstrapper {

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context) {
            base.ConfigureRequestContainer(container, context);
            container.Register<IUserMapper, UserDatabase>();
        }

        protected override void ConfigureConventions(NancyConventions conv) {
            base.ConfigureConventions(conv);
            conv.StaticContentsConventions.Clear();
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("Content", @"/Content/", TimeSpan.FromDays(365)));
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("Scripts", @"/Scripts/", TimeSpan.FromDays(365)));
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("novnc", @"/novnc/", TimeSpan.FromDays(365)));
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("Fonts", @"/Fonts/", TimeSpan.FromDays(365)));
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("repo", @"/Resources/", TimeSpan.FromDays(365)));
            conv.StaticContentsConventions.Add(RequestHandling.AddDirectoryWithExpiresHeader("repo/ssh", @"/Resources/ssh/", TimeSpan.FromDays(365)));
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines) {
            base.ApplicationStartup(container, pipelines);
            pipelines.RegisterCompressionCheck();
        }

        protected override void RequestStartup(TinyIoCContainer requestContainer, IPipelines pipelines, NancyContext context) {
            var formsAuthConfiguration = new FormsAuthenticationConfiguration {
                RedirectUrl = "/login",
                UserMapper = requestContainer.Resolve<IUserMapper>()
            };
            FormsAuthentication.Enable(pipelines, formsAuthConfiguration);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container) {
            base.ConfigureApplicationContainer(container);
            container
                .Register<IEnumerable<ISuperSimpleViewEngineMatcher>>(
                    (c, p) => new List<ISuperSimpleViewEngineMatcher> {
                        new AntdViewEngine()
                    });
        }

        //protected override DiagnosticsConfiguration DiagnosticsConfiguration => new DiagnosticsConfiguration { Password = "@Nancy12e3" };
    }
}