using System;
using System.Net.Sockets;
using System.Net;
using RatatooeyServer.Settings;
using System.Collections.Generic;
using System.Threading;
using RatatooeyServer.ProtoMessages;
using ProtoBuf;
using System.IO;
using RatatooeyServer.Helpers;
using System.Linq;



namespace RatatooeyServer.Server
{
	public class Server
	{
		private Socket _handle;
		private SocketAsyncEventArgs _socketAccept;
		private readonly List<Client> _clients = new List<Client>();
		private FormServer serverForm;



		public delegate void ClientStateEventHandler();
		public event ClientStateEventHandler ClientState; 
		public void OnClientState(Client client, bool connected)
		{
			if (!connected)
				RemoveClient(client);
		}

		// Define a delegate for the event handler
		public delegate void SendMessageEventHandler(TcpClient client, IMessage message);

		// Define the event using the delegate
		public event SendMessageEventHandler SendMessage;
		public void OnSendMessage(Client client, IMessage message)
		{
			try
			{
				using (MemoryStream ms = new MemoryStream())
				{
					Serializer.Serialize(ms, message);
					byte[] payload = ms.ToArray();
					byte[] length = BitConverter.GetBytes(payload.Length);
					client.Stream.Write(length, 0, length.Length);
					client.Stream.Write(payload, 0, payload.Length);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error sending message: {ex.Message}");
			}
		}

		public uint KEEP_ALIVE_INTERVAL { get { return 25000; } } // 25s
		public uint KEEP_ALIVE_TIME { get { return 25000; } } // 25s

		private SocketError m_SocketError;
		public SocketError SocketError
		{
			get
			{
				return m_SocketError;
			}
			set
			{
				m_SocketError = value;
			}
		}

		public Server(FormServer serverForm)
		{
			var ff = TypeRegistry.GetPacketTypes(typeof(IMessage)).ToArray();
			TypeRegistry.AddTypesToSerializer(typeof(IMessage), TypeRegistry.GetPacketTypes(typeof(IMessage)).ToArray());
			this.serverForm = serverForm;
		}

        public void Start()
		{
			_handle = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_handle.Bind(new IPEndPoint(IPAddress.Any, Config.PORT));
			_handle.Listen(1000);

			_socketAccept = new SocketAsyncEventArgs();
			_socketAccept.Completed += ProcessClient;

			Console.WriteLine("Server started. Waiting for connections...");

			//Start accepting clients
			if (!_handle.AcceptAsync(_socketAccept))
			{
				ThreadPool.QueueUserWorkItem(HandleClientCommunication, _socketAccept.AcceptSocket);
			}
		}

		private void ProcessClient(object s, SocketAsyncEventArgs e)
		{
			if (e.SocketError == SocketError.Success)
			{
				ThreadPool.QueueUserWorkItem(HandleClientCommunication, e.AcceptSocket);
			}
			else
			{
				Console.WriteLine($"Socket error occurred: {e.SocketError}");
			}

			// Accept the next client
			e.AcceptSocket = null; // Reset the accept socket
			if (!_handle.AcceptAsync(e))
			{
				ThreadPool.QueueUserWorkItem(state => ProcessClient(this, e), null); // Retry accepting
			}
		}


		public void Disconnect()
		{
			if (_handle != null)
			{
				_handle.Close();
				_handle = null;
			}
		}

		private void HandleClientCommunication(object clientSocketObj)
		{
			Socket clientSocket = (Socket)clientSocketObj;
			TcpClient tcpClient = new TcpClient();
			tcpClient.Client = clientSocket;

			//NetworkStream stream = client.GetStream();
			IPEndPoint endPoint = (IPEndPoint)clientSocket.RemoteEndPoint;
			Client newClient = new Client(tcpClient, endPoint, this);


			newClient.StartReceiving();
		}
		

		public void AddClient(Client client)
		{
			_clients.Add(client);
			serverForm.AddClientToList(client);
		}

		public void RemoveClient(Client client)
		{
			_clients.Remove(client);
			serverForm.RemoveClientList(client);
		}

	}
}
