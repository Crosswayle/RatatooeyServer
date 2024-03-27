using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.ProtoMessages
{
	[ProtoContract]
	public class InitialData : IMessage
	{
		[ProtoMember(1)]
		public string OS { get; set; }

		[ProtoMember(2)]
		public string ComputerName { get; set; }

		[ProtoMember(3)]
		public string AV { get; set; }

	}
}
