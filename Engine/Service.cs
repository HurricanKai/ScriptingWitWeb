using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Languages;

namespace Engine
{
	public class Service
	{
		public Service(SupportedLanguages l)
		{
			this.name = l.ToString();
			this.language = l;
			this.Status = true;
			Services.Add(this);
		}

		private static List<Service> Services = new List<Service>();
		public string name;
		public SupportedLanguages language;
		private bool Status;

		public static List<Engine.Service> GetAllServices()
		{
			return Services;
		}

		public bool GetStatus()
		{
			return Status;
		}

		public static bool Start(SupportedLanguages v)
		{
			try
			{
				new Service(v);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
