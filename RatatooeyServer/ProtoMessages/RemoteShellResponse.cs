using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.ProtoMessages
{
	[ProtoContract]
	public class RemoteShellResponse : IMessage
	{
		[ProtoMember(1)]
		public string Output { get; set; }

		[ProtoMember(2)]
		public bool IsError { get; set; }
	}
}
