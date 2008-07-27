using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace UWoW
{
	public class PatchInfo
	{
		static Regex searchRegex = new Regex(@"(\S+)", RegexOptions.Compiled);

		bool success;
		string m_platformtag;
		string m_clienttag;
		Version m_versiontag;
		string m_mpqfiletag;

		public PatchInfo(string str)
		{
			try
			{
				if (!searchRegex.IsMatch(str))
					return;
				MatchCollection mc = searchRegex.Matches(str);
				m_platformtag = mc[0].ToString();
				m_clienttag = mc[1].ToString();
				m_versiontag = new Version(mc[2].ToString().Replace("x", int.MaxValue.ToString()));
				m_mpqfiletag = mc[3].ToString();
				success = true;
			}
			catch
			{
				success = false;
			}
		}

		public string Platform
		{ get { return m_platformtag; } }
		public string Client
		{ get { return m_clienttag; } }
		public Version Version
		{ get { return m_versiontag; } }
		public string File
		{ get { return m_mpqfiletag; } }
		public bool Success
		{ get { return success; } }
	}

	public class PatchManager
	{
		private static List<PatchInfo> _list = new List<PatchInfo>();

		public static int Load(string fname)
		{
			string str = string.Empty;

			string archtag = string.Empty;
			string clienttag = string.Empty;
			string versiontag = string.Empty;
			string mpqfiletag = string.Empty;

			string[] strArr1;
			using (TextReader tr = new StreamReader(fname))
				while ((str = tr.ReadLine()) != null)
				{
					str = str.Trim();
					if (string.IsNullOrEmpty(str) || str.StartsWith("#"))
						continue;
					PatchInfo p = new PatchInfo(str);
					if (p.Success)
						_list.Add(p);
				}
			return _list.Count;
		}

		public static int GetPatchFileName(string platform, string client, Version version, string language)
		{
			foreach (PatchInfo pinfo in _list)
			{
				if (platform != pinfo.Platform) continue;
				if (client != pinfo.Client) continue;
				if (version > pinfo.Version) continue;
				{
				}
			}
			return 0;
		}
	}
}
