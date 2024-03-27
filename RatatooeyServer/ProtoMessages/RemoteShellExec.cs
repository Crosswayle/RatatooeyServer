using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.ProtoMessages
{
	[ProtoContract]
	public class RemoteShellExec : IMessage
	{
		[ProtoMember(1)]
		public string Command { get; set; }
	}
}
