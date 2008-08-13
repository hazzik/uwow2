using System;
using System.Collections.Specialized;
using System.IO;
using System.Text.RegularExpressions;

namespace UWoW.Helper
{
	public class Config
	{
		#region fields

		static Regex rxComment = new Regex(@"#|//.*", RegexOptions.Compiled);
		static Regex rxParamet = new Regex(@"([^=]*)=([^;]*);", RegexOptions.Compiled);

		StringDictionary _options = new StringDictionary();

		#endregion
		#region Methods

		public Boolean Load(string fname)
		{
			FileInfo fi = new FileInfo(fname);
			if (!fi.Exists)
				return false;

			string line = null;
			using (TextReader r = fi.OpenText())
			{
				while ((line = r.ReadLine()) != null)
				{
					if (rxComment.IsMatch(line))
						continue;
					if (rxParamet.IsMatch(line))
					{
						Match m = rxParamet.Match(line);
						_options.Add(
							m.Groups[1].ToString().ToLower().Trim(),
							m.Groups[2].ToString().ToLower().Trim()
							);
					}
				}
			}

			return true;
		}

		public String GetString(string name)
		{
			return _options[name.ToLower()];
		}

		public Int32 GetInt32(string name)
		{
			Int32 result;
			Int32.TryParse(_options[name.ToLower()], out result);
			return result;
		}

		public Single GetSingle(string name)
		{
			Single result;
			Single.TryParse(_options[name.ToLower()], out result);
			return result;
		}

		#endregion
	}
}
