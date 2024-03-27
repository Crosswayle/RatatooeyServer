using ProtoBuf;
using RatatooeyServer.ProtoMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatatooeyServer.Helpers
{
	public class PayloadReader : MemoryStream
	{
		private readonly Stream _innerStream;
		public bool LeaveInnerStreamOpen { get; }
		public PayloadReader(byte[] payload, int length, bool leaveInnerStreamOpen)
		{
			_innerStream = new MemoryStream(payload, 0, length, false, true);
			LeaveInnerStreamOpen = leaveInnerStreamOpen;
		}

		public PayloadReader(Stream stream, bool leaveInnerStreamOpen)
		{
			_innerStream = stream;
			LeaveInnerStreamOpen = leaveInnerStreamOpen;
		}

		public IMessage ReadMessage()
		{
			/* Length prefix is ignored here and already handled in Client class,
             * it would cause to much trouble to check here for split or not fully
             * received packets.
             */
			IMessage message = Serializer.Deserialize<IMessage>(_innerStream);
			return message;
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (LeaveInnerStreamOpen)
				{
					_innerStream.Flush();
				}
				else
				{
					_innerStream.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}
	}
}
