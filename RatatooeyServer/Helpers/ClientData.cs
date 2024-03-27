using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.Helpers
{
	public class ClientData
	{
		public string OS { get; set; }
		public string Hostname { get; set; }
		public string AV { get; set; }
		
		public IPEndPoint IP { get; set; }

	}

}
