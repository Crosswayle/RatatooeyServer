using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.ProtoMessages
{
	[ProtoContract]
	public class MsgBox : IMessage
	{
		[ProtoMember(1)]
		public string Text { get; set; }


	}
}
