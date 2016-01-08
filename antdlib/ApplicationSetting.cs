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

using System;
using antdlib.Common;
using antdlib.Log;

namespace antdlib {

    public class ApplicationSetting {
        private const string CoreFileName = "antdConfig";

        private static readonly string[] Files = {
                CoreFileName + "Current",
                CoreFileName + "001",
                CoreFileName + "002"
            };

        public static readonly XmlWriter Writer = new XmlWriter(Files);

        #region core CRUD
        public static void Set(string key, string value) {
            Writer.Write(key, value);
        }

        public static string Get(string key) {
            return Writer.ReadValue(key);
        }

        public static void Delete(string key) {
            throw new NotImplementedException();
        }
        #endregion core CRUD

        public static void WriteDefaults() {
            if (Writer.CheckValue("AntdHttpPort") == false) {
                Writer.Write("AntdHttpPort", "8084");
            }
            if (Writer.CheckValue("AntdHttpsPort") == false) {
                Writer.Write("AntdHttpsPort", "443");
            }
            if (Writer.CheckValue(Parameter.LabelAntdDatabase) == false) {
                Writer.Write(Parameter.LabelAntdDatabase, Parameter.AntdCfgDatabase);
            }
            if (Writer.CheckValue("ssl") == false) {
                Writer.Write("ssl", "yes");
            }
            if (Writer.CheckValue("certificate") == false) {
                Writer.Write("certificate", $"{Parameter.AntdCfg}/certificate.pfx");
            }
            if (Writer.CheckValue("ca") == false) {
                Writer.Write("ca", "no");
            }
            if (Writer.CheckValue("ca_path") == false) {
                Writer.Write("ca", Parameter.CertificateAuthority);
            }
            if (Writer.CheckValue("x509") == false) {
                Writer.Write("x509", "");
            }
            if (Writer.CheckValue(Parameter.LabelAuthIsEnabled) == false) {
                Writer.Write(Parameter.LabelAuthIsEnabled, false.ToString());
            }
            if (Writer.CheckValue("protocol") == false) {
                Writer.Write("protocol", "https");
            }
        }

        public static string GetX509() {
            try {
                return Writer.CheckValue("x509") ? Writer.ReadValue("x509") : Parameter.CertificateAuthority;
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return Parameter.CertificateAuthority;
            }
        }

        public static void SetX509(string val) {
            try {
                Writer.Write("x509", val);
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetCaPath() {
            try {
                return Writer.CheckValue("ca_path") ? Writer.ReadValue("ca_path") : Parameter.CertificateAuthority;
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return Parameter.CertificateAuthority;
            }
        }

        public static void SetCaPath(string path) {
            try {
                Writer.Write("ca_path", path);
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetHttpsPort() {
            try {
                return Writer.CheckValue("AntdHttpsPort") ? Writer.ReadValue("AntdHttpsPort") : "443";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "443";
            }
        }

        public static void SetHttpsPort(string port) {
            try {
                Writer.Write("AntdHttpsPort", port);
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetHttpPort() {
            try {
                return Writer.CheckValue("AntdHttpPort") ? Writer.ReadValue("AntdHttpPort") : "8084";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "8084";
            }
        }

        public static void SetHttpPort(string port) {
            try {
                Writer.Write("AntdHttpPort", port);
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetHttpProtocol() {
            try {
                return Writer.CheckValue("protocol") ? Writer.ReadValue("protocol") : "https";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "https";
            }
        }

        public static void SwitchToHttps() {
            try {
                Writer.Write("protocol", "https");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static void SwitchToHttp() {
            try {
                Writer.Write("protocol", "http");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static bool GetT2Fa() {
            try {
                return Writer.CheckValue(Parameter.LabelAuthIsEnabled) && Convert.ToBoolean(Writer.ReadValue(Parameter.LabelAuthIsEnabled));
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return false;
            }
        }

        public static void EnableT2Fa() {
            try {
                Writer.Write(Parameter.LabelAuthIsEnabled, true.ToString());
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static void DisableT2Fa() {
            try {
                Writer.Write(Parameter.LabelAuthIsEnabled, false.ToString());
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetCertificatePath() {
            try {
                return Writer.CheckValue("certificate") ? Writer.ReadValue("certificate") : $"{Parameter.AntdCfg}/certificate.pfx";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return $"{Parameter.AntdCfg}/certificate.pfx";
            }
        }

        public static void SetCertificatePath(string newCert) {
            try {
                Writer.Write("certificate", $"{Parameter.AntdCfg}/certificate.pfx");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetSsl() {
            try {
                return Writer.CheckValue("ssl") ? Writer.ReadValue("ssl") : "yes";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "yes";
            }
        }

        public static void EnableSsl() {
            try {
                Writer.Write("ssl", "yes");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static void DisableSsl() {
            try {
                Writer.Write("ssl", "no");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetCa() {
            try {
                return Writer.CheckValue("ca") ? Writer.ReadValue("ca") : "no";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "no";
            }
        }

        public static void EnableCa() {
            try {
                Writer.Write("ca", "yes");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static void DisableCa() {
            try {
                Writer.Write("ca", "no");
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
            }
        }

        public static string GetDb() {
            try {
                return Writer.CheckValue(Parameter.LabelAntdDatabase) ? Writer.ReadValue(Parameter.LabelAntdDatabase) : Parameter.AntdCfgDatabase;
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return Parameter.AntdCfgDatabase;
            }
        }

        public static string GetHostUri() {
            try {
                if (Writer.CheckValue(Parameter.LabelAntdPort) == false) {
                    return "http://+:7777/";
                }
                return "http://+:" + Writer.ReadValue(Parameter.LabelAntdPort) + "/";
            }
            catch (Exception ex) {
                ConsoleLogger.Warn(ex.Message);
                return "http://+:7777/";
            }
        }
    }
}


//public class ParametersConfig : CoreParametersConfig {
//    public static void Write(string key, string value) {
//        var readValue = Writer.ReadValue(key);
//        if (readValue == null) {
//            var arr = new[] { value };
//            Writer.Write(key, JsonConvert.SerializeObject(arr));
//        }
//        else {
//            AddValue(key, value);
//        }
//    }

//    private static void AddValue(string key, string value) {
//        var readValue = Writer.ReadValue(key);
//        var arr = JsonConvert.DeserializeObject<string[]>(readValue);
//        var list = arr.ToList();
//        list.Add(value);
//        Writer.Write(key, JsonConvert.SerializeObject(list.ToArray()));
//    }

//    public static string Read(string key) {
//        return Writer.ReadValue(key);
//    }

//    public static void Edit(string key, string value) {
//        var arr = new[] { value };
//        Writer.Write(key, JsonConvert.SerializeObject(arr));
//    }