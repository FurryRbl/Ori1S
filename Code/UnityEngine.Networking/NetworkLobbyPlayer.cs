using System;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.SceneManagement;

namespace UnityEngine.Networking
{
	// Token: 0x02000044 RID: 68
	[DisallowMultipleComponent]
	[AddComponentMenu("Network/NetworkLobbyPlayer")]
	public class NetworkLobbyPlayer : NetworkBehaviour
	{
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000274 RID: 628 RVA: 0x0000CE8C File Offset: 0x0000B08C
		// (set) Token: 0x06000275 RID: 629 RVA: 0x0000CE94 File Offset: 0x0000B094
		public byte slot
		{
			get
			{
				return this.m_Slot;
			}
			set
			{
				this.m_Slot = value;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000276 RID: 630 RVA: 0x0000CEA0 File Offset: 0x0000B0A0
		// (set) Token: 0x06000277 RID: 631 RVA: 0x0000CEA8 File Offset: 0x0000B0A8
		public bool readyToBegin
		{
			get
			{
				return this.m_ReadyToBegin;
			}
			set
			{
				this.m_ReadyToBegin = value;
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		private void Start()
		{
			Object.DontDestroyOnLoad(base.gameObject);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public override void OnStartClient()
		{
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				networkLobbyManager.lobbySlots[(int)this.m_Slot] = this;
				this.m_ReadyToBegin = false;
				this.OnClientEnterLobby();
			}
			else
			{
				Debug.LogError("LobbyPlayer could not find a NetworkLobbyManager. The LobbyPlayer requires a NetworkLobbyManager object to function. Make sure that there is one in the scene.");
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CF14 File Offset: 0x0000B114
		public void SendReadyToBeginMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyPlayer SendReadyToBeginMessage");
			}
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				LobbyReadyToBeginMessage lobbyReadyToBeginMessage = new LobbyReadyToBeginMessage();
				lobbyReadyToBeginMessage.slotId = (byte)base.playerControllerId;
				lobbyReadyToBeginMessage.readyState = true;
				networkLobbyManager.client.Send(43, lobbyReadyToBeginMessage);
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CF74 File Offset: 0x0000B174
		public void SendNotReadyToBeginMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyPlayer SendReadyToBeginMessage");
			}
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				LobbyReadyToBeginMessage lobbyReadyToBeginMessage = new LobbyReadyToBeginMessage();
				lobbyReadyToBeginMessage.slotId = (byte)base.playerControllerId;
				lobbyReadyToBeginMessage.readyState = false;
				networkLobbyManager.client.Send(43, lobbyReadyToBeginMessage);
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000CFD4 File Offset: 0x0000B1D4
		public void SendSceneLoadedMessage()
		{
			if (LogFilter.logDebug)
			{
				Debug.Log("NetworkLobbyPlayer SendSceneLoadedMessage");
			}
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				IntegerMessage msg = new IntegerMessage((int)base.playerControllerId);
				networkLobbyManager.client.Send(44, msg);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000D028 File Offset: 0x0000B228
		private void OnLevelWasLoaded()
		{
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				string name = SceneManager.GetSceneAt(0).name;
				if (name == networkLobbyManager.lobbyScene)
				{
					return;
				}
			}
			if (base.isLocalPlayer)
			{
				this.SendSceneLoadedMessage();
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000D080 File Offset: 0x0000B280
		public void RemovePlayer()
		{
			if (base.isLocalPlayer && !this.m_ReadyToBegin)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log("NetworkLobbyPlayer RemovePlayer");
				}
				ClientScene.RemovePlayer(base.GetComponent<NetworkIdentity>().playerControllerId);
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public virtual void OnClientEnterLobby()
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		public virtual void OnClientExitLobby()
		{
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000D0D0 File Offset: 0x0000B2D0
		public virtual void OnClientReady(bool readyState)
		{
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		public override bool OnSerialize(NetworkWriter writer, bool initialState)
		{
			writer.WritePackedUInt32(1U);
			writer.Write(this.m_Slot);
			writer.Write(this.m_ReadyToBegin);
			return true;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D104 File Offset: 0x0000B304
		public override void OnDeserialize(NetworkReader reader, bool initialState)
		{
			if (reader.ReadPackedUInt32() == 0U)
			{
				return;
			}
			this.m_Slot = reader.ReadByte();
			this.m_ReadyToBegin = reader.ReadBoolean();
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000D138 File Offset: 0x0000B338
		private void OnGUI()
		{
			if (!this.ShowLobbyGUI)
			{
				return;
			}
			NetworkLobbyManager networkLobbyManager = NetworkManager.singleton as NetworkLobbyManager;
			if (networkLobbyManager)
			{
				if (!networkLobbyManager.showLobbyGUI)
				{
					return;
				}
				string name = SceneManager.GetSceneAt(0).name;
				if (name != networkLobbyManager.lobbyScene)
				{
					return;
				}
			}
			Rect position = new Rect((float)(100 + this.m_Slot * 100), 200f, 90f, 20f);
			if (base.isLocalPlayer)
			{
				string text;
				if (this.m_ReadyToBegin)
				{
					text = "(Ready)";
				}
				else
				{
					text = "(Not Ready)";
				}
				GUI.Label(position, text);
				if (this.m_ReadyToBegin)
				{
					position.y += 25f;
					if (GUI.Button(position, "STOP"))
					{
						this.SendNotReadyToBeginMessage();
					}
				}
				else
				{
					position.y += 25f;
					if (GUI.Button(position, "START"))
					{
						this.SendReadyToBeginMessage();
					}
					position.y += 25f;
					if (GUI.Button(position, "Remove"))
					{
						ClientScene.RemovePlayer(base.GetComponent<NetworkIdentity>().playerControllerId);
					}
				}
			}
			else
			{
				GUI.Label(position, "Player [" + base.netId + "]");
				position.y += 25f;
				GUI.Label(position, "Ready [" + this.m_ReadyToBegin + "]");
			}
		}

		// Token: 0x0400012F RID: 303
		[SerializeField]
		public bool ShowLobbyGUI = true;

		// Token: 0x04000130 RID: 304
		private byte m_Slot;

		// Token: 0x04000131 RID: 305
		private bool m_ReadyToBegin;
	}
}
