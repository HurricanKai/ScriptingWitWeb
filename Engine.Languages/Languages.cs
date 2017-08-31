using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Languages
{
	public enum SupportedLanguages
	{
		Lua, Pyhton, Null
	}
	public class Languages
	{

		public static SupportedLanguages GetLanguage(string language)
		{
			switch (language.ToLower())
			{
				case "lua":
					return SupportedLanguages.Lua;
				case "python":
					return SupportedLanguages.Pyhton;
			}
			return SupportedLanguages.Null;
		}
	}
}
