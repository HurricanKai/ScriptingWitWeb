using System;
using Nancy;
using System.Collections.Generic;
using Newtonsoft.Json;
using Engine;
using Engine.Languages;

namespace Engine.Web.Nancy
{
	public class Status : NancyModule
	{
		public Status() : base("/Status")
		{
			Get["/"] = _ => new Services();
		}
	}

	public class Services : NancyModule
	{
		public Services() : base("/Services")
		{
			Get["/"] = _ => "/Soon";
			Get["/Web"] = para => "Well, you are seeing this. so it is online ... i think";
			Get["/Lua"] = luapara => "Hmm. \"throw new NotImplementedException();\"";
			Get["/Start/{language}"] = para =>
				Service.Start(Languages.Languages.GetLanguage((string)para.language)).ToString();
			
		}
	}
	public class ServicesAny : NancyModule
	{
		public ServicesAny() : base("/Services/Any")
		{
			Get["/"] = _ => FormatStatus(GetStatusOfAny());
			Get["/Json"] = _ => FormatAsJson(GetStatusOfAny());
		}

		private dynamic FormatAsJson(Dictionary<Service, bool> dictionary)
		{
			string n = System.Environment.NewLine;
			string returner = "{" + n;
			foreach (var v in dictionary)
			{
				//TODO: Make Nicer :D
				/*
"LuaEngine1": {
"state": "true",
"Language": "Lua"
},
				 */
				returner += $"\"{v.Key.name}\": " + " {" + n;
				returner += $"\"state\": \"{v.Value.ToString()}\"," + n;
				returner += $"\"Language\": \"{v.Key.language.ToString()}\"" + n;
				returner += "},";
			}
			returner = returner.Remove(returner.Length - 1, 1);
			returner += n + "}";
			return returner;
		}

		private string FormatAsJson(Service s, bool b)
		{
			return FormatAsJson(new Dictionary<Service, bool> { { s, b } });
		}

		private string FormatStatus(Dictionary<Service, bool> dictionary)
		{
			string returner = String.Empty;
			foreach (var v in dictionary)
			{
				string status = String.Empty;
				if (v.Value)
				{
					status = "online";
				}
				else
				{
					status = "offline";
				}
				returner += $"\r\n{v.Key.name} is currently {status}";
			}
			return returner;
		}

		private string FormatStatus(Service s, bool b)
		{
			return FormatStatus(new Dictionary<Service, bool> { { s, b } });
		}

		private Dictionary<Service, bool> GetStatusOfAny()
		{
			var returner =  new Dictionary<Service, bool>();

			foreach (Service s in Service.GetAllServices())
			{
				returner.Add(s, s.GetStatus());
			}

			return returner;
		}


	}
}