﻿
///-------------------------------------------------------------------------------------
/// Copyright (c) 2014 Anthilla S.r.l. (http://www.anthilla.com)
///
/// Licensed under the BSD licenses.
///
/// 141110
///-------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Antd
{
	public class Meminfo
	{
		public static string GetText()
		{
			string meminfoContent = "";
			meminfoContent = LinqFiles.GetFileText("/proc/meminfo");

			string meminfoJson = JsonConvert.SerializeObject(meminfoContent);
			return meminfoJson;
		}

		public static List<MeminfoModel> GetModel()
		{
			string meminfoContent = "";
			meminfoContent = LinqFiles.GetFileText("/proc/meminfo");

			var meminfo = TextToJson.Meminfo(meminfoContent);
			return meminfo;
		}
	}
}

