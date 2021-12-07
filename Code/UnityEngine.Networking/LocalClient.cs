using System;
using System.Collections.Generic;

namespace UnityEngine.Networking
{
	// Token: 0x02000010 RID: 16
	internal sealed class LocalClient : NetworkClient
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00004458 File Offset: 0x00002658
		public override void Disconnect()
		{
			ClientScene.HandleClientDisconnect(this.m_Connection);
			if (this.m_Connected)
			{
				this.PostInternalMessage(33);
				this.m_Connected = false;
			}
			this.m_AsyncConnect = NetworkClient.ConnectState.Disconnected;
			this.m_LocalServer.RemoveLocalClient(this.m_Connection);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004498 File Offset: 0x00002698
		internal void InternalConnectLocalServer(bool generateConnectMsg)
		{
			if (this.m_FreeMessages == null)
			{
				this.m_FreeMessages = new Stack<LocalClient.InternalMsg>();
				for (int i = 0; i < 64; i++)
				{
					LocalClient.InternalMsg t = default(LocalClient.InternalMsg);
					this.m_FreeMessages.Push(t);
				}
			}
			this.m_LocalServer = NetworkServer.instance;
			this.m_Connection = new ULocalConnectionToServer(this.m_LocalServer);
			base.SetHandlers(this.m_Connection);
			this.m_Connection.connectionId = this.m_LocalServer.AddLocalClient(this);
			this.m_AsyncConnect = NetworkClient.ConnectState.Connected;
			NetworkClient.SetActive(true);
			base.RegisterSystemHandlers(true);
			if (generateConnectMsg)
			{
				this.PostInternalMessage(32);
			}
			this.m_Connected = true;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000454C File Offset: 0x0000274C
		internal override void Update()
		{
			this.ProcessInternalMessages();
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004554 File Offset: 0x00002754
		internal void AddLocalPlayer(PlayerController localPlayer)
		{
			if (LogFilter.logDev)
			{
				Debug.Log(string.Concat(new object[]
				{
					"Local client AddLocalPlayer ",
					localPlayer.gameObject.name,
					" conn=",
					this.m_Connection.connectionId
				}));
			}
			this.m_Connection.isReady = true;
			this.m_Connection.SetPlayerController(localPlayer);
			NetworkIdentity unetView = localPlayer.unetView;
			if (unetView != null)
			{
				ClientScene.SetLocalObject(unetView.netId, localPlayer.gameObject);
				unetView.SetConnectionToServer(this.m_Connection);
			}
			ClientScene.InternalAddPlayer(unetView, localPlayer.playerControllerId);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00004600 File Offset: 0x00002800
		private void PostInternalMessage(byte[] buffer, int channelId)
		{
			LocalClient.InternalMsg item;
			if (this.m_FreeMessages.Count == 0)
			{
				item = default(LocalClient.InternalMsg);
			}
			else
			{
				item = this.m_FreeMessages.Pop();
			}
			item.buffer = buffer;
			item.channelId = channelId;
			this.m_InternalMsgs.Add(item);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004654 File Offset: 0x00002854
		private void PostInternalMessage(short msgType)
		{
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(msgType);
			networkWriter.FinishMessage();
			this.PostInternalMessage(networkWriter.AsArray(), 0);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00004684 File Offset: 0x00002884
		private void ProcessInternalMessages()
		{
			if (this.m_InternalMsgs.Count == 0)
			{
				return;
			}
			List<LocalClient.InternalMsg> internalMsgs = this.m_InternalMsgs;
			this.m_InternalMsgs = this.m_InternalMsgs2;
			foreach (LocalClient.InternalMsg t in internalMsgs)
			{
				if (this.s_InternalMessage.reader == null)
				{
					this.s_InternalMessage.reader = new NetworkReader(t.buffer);
				}
				else
				{
					this.s_InternalMessage.reader.Replace(t.buffer);
				}
				this.s_InternalMessage.reader.ReadInt16();
				this.s_InternalMessage.channelId = t.channelId;
				this.s_InternalMessage.conn = base.connection;
				this.s_InternalMessage.msgType = this.s_InternalMessage.reader.ReadInt16();
				this.m_Connection.InvokeHandler(this.s_InternalMessage);
				this.m_FreeMessages.Push(t);
				base.connection.lastMessageTime = Time.time;
			}
			this.m_InternalMsgs = internalMsgs;
			this.m_InternalMsgs.Clear();
			foreach (LocalClient.InternalMsg item in this.m_InternalMsgs2)
			{
				this.m_InternalMsgs.Add(item);
			}
			this.m_InternalMsgs2.Clear();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000483C File Offset: 0x00002A3C
		internal void InvokeHandlerOnClient(short msgType, MessageBase msg, int channelId)
		{
			NetworkWriter networkWriter = new NetworkWriter();
			networkWriter.StartMessage(msgType);
			msg.Serialize(networkWriter);
			networkWriter.FinishMessage();
			this.InvokeBytesOnClient(networkWriter.AsArray(), channelId);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00004870 File Offset: 0x00002A70
		internal void InvokeBytesOnClient(byte[] buffer, int channelId)
		{
			this.PostInternalMessage(buffer, channelId);
		}

		// Token: 0x04000038 RID: 56
		private const int k_InitialFreeMessagePoolSize = 64;

		// Token: 0x04000039 RID: 57
		private List<LocalClient.InternalMsg> m_InternalMsgs = new List<LocalClient.InternalMsg>();

		// Token: 0x0400003A RID: 58
		private List<LocalClient.InternalMsg> m_InternalMsgs2 = new List<LocalClient.InternalMsg>();

		// Token: 0x0400003B RID: 59
		private Stack<LocalClient.InternalMsg> m_FreeMessages;

		// Token: 0x0400003C RID: 60
		private NetworkServer m_LocalServer;

		// Token: 0x0400003D RID: 61
		private bool m_Connected;

		// Token: 0x0400003E RID: 62
		private NetworkMessage s_InternalMessage = new NetworkMessage();

		// Token: 0x02000011 RID: 17
		private struct InternalMsg
		{
			// Token: 0x0400003F RID: 63
			internal byte[] buffer;

			// Token: 0x04000040 RID: 64
			internal int channelId;
		}
	}
}
