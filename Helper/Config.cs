using System;
using System.Collections.Generic;
using System.Collections.Specialized;

using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;

namespace Helper
{
	public class Config
	{
		private static Regex m_regexp_dic = new Regex(@"\s*([^\=]+)\s*", RegexOptions.Compiled);
		private static Regex m_regexp_tab = new Regex(@"\s(\S+)\s", RegexOptions.Compiled);

		private StringDictionary m_dictionary = new StringDictionary();

		public bool Load(string fname)
		{	
			using (TextReader tr = new StreamReader(fname))
			{
				string str = string.Empty;
				string key = string.Empty;
				string val = string.Empty;
				string [] strArr1;
				char[] separator = new char[] {' ','\t'};

				while ((str = tr.ReadLine()) != null)
				{
					if (str.StartsWith("#"))
						continue;
					if (string.IsNullOrEmpty(str))
						continue;
					m_regexp_tab.Split(str);
					MatchCollection m = m_regexp_dic.Matches(str);
					

					strArr1 = str.Split('=');
					key = strArr1[0].Trim();
					val = strArr1[1].Trim();
					m_dictionary.Add(key, val);
				}
			}
			return true;
		}

		public bool Save(string fname)
		{
			using (TextWriter tw = new StreamWriter(fname, false))
			{
				foreach (DictionaryEntry s in m_dictionary)
				{
					tw.WriteLine("{0} = {1}", s.Key, s.Value);
				}
			}
			return true;
		}
	}
}
