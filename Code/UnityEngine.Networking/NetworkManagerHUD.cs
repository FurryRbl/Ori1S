using System;
using System.ComponentModel;
using UnityEngine.Networking.Match;

namespace UnityEngine.Networking
{
	// Token: 0x02000047 RID: 71
	[AddComponentMenu("Network/NetworkManagerHUD")]
	[RequireComponent(typeof(NetworkManager))]
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class NetworkManagerHUD : MonoBehaviour
	{
		// Token: 0x06000303 RID: 771 RVA: 0x0000F010 File Offset: 0x0000D210
		private void Awake()
		{
			this.manager = base.GetComponent<NetworkManager>();
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000F020 File Offset: 0x0000D220
		private void Update()
		{
			if (!this.showGUI)
			{
				return;
			}
			if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
			{
				if (Application.platform != RuntimePlatform.WebGLPlayer)
				{
					if (Input.GetKeyDown(KeyCode.S))
					{
						this.manager.StartServer();
					}
					if (Input.GetKeyDown(KeyCode.H))
					{
						this.manager.StartHost();
					}
				}
				if (Input.GetKeyDown(KeyCode.C))
				{
					this.manager.StartClient();
				}
			}
			if (NetworkServer.active && this.manager.IsClientConnected() && Input.GetKeyDown(KeyCode.X))
			{
				this.manager.StopHost();
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000F0F0 File Offset: 0x0000D2F0
		private void OnGUI()
		{
			if (!this.showGUI)
			{
				return;
			}
			int num = 10 + this.offsetX;
			int num2 = 40 + this.offsetY;
			bool flag = this.manager.client == null || this.manager.client.connection == null || this.manager.client.connection.connectionId == -1;
			if (!this.manager.IsClientConnected() && !NetworkServer.active && this.manager.matchMaker == null)
			{
				if (flag)
				{
					if (Application.platform != RuntimePlatform.WebGLPlayer)
					{
						if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "LAN Host(H)"))
						{
							this.manager.StartHost();
						}
						num2 += 24;
					}
					if (GUI.Button(new Rect((float)num, (float)num2, 105f, 20f), "LAN Client(C)"))
					{
						this.manager.StartClient();
					}
					this.manager.networkAddress = GUI.TextField(new Rect((float)(num + 100), (float)num2, 95f, 20f), this.manager.networkAddress);
					num2 += 24;
					if (Application.platform == RuntimePlatform.WebGLPlayer)
					{
						GUI.Box(new Rect((float)num, (float)num2, 200f, 25f), "(  WebGL cannot be server  )");
						num2 += 24;
					}
					else
					{
						if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "LAN Server Only(S)"))
						{
							this.manager.StartServer();
						}
						num2 += 24;
					}
				}
				else
				{
					GUI.Label(new Rect((float)num, (float)num2, 200f, 20f), string.Concat(new object[]
					{
						"Connecting to ",
						this.manager.networkAddress,
						":",
						this.manager.networkPort,
						".."
					}));
					num2 += 24;
					if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Cancel Connection Attempt"))
					{
						this.manager.StopClient();
					}
				}
			}
			else
			{
				if (NetworkServer.active)
				{
					string text = "Server: port=" + this.manager.networkPort;
					if (this.manager.useWebSockets)
					{
						text += " (Using WebSockets)";
					}
					GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), text);
					num2 += 24;
				}
				if (this.manager.IsClientConnected())
				{
					GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), string.Concat(new object[]
					{
						"Client: address=",
						this.manager.networkAddress,
						" port=",
						this.manager.networkPort
					}));
					num2 += 24;
				}
			}
			if (this.manager.IsClientConnected() && !ClientScene.ready)
			{
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Client Ready"))
				{
					ClientScene.Ready(this.manager.client.connection);
					if (ClientScene.localPlayers.Count == 0)
					{
						ClientScene.AddPlayer(0);
					}
				}
				num2 += 24;
			}
			if (NetworkServer.active || this.manager.IsClientConnected())
			{
				if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Stop (X)"))
				{
					this.manager.StopHost();
				}
				num2 += 24;
			}
			if (!NetworkServer.active && !this.manager.IsClientConnected() && flag)
			{
				num2 += 10;
				if (Application.platform == RuntimePlatform.WebGLPlayer)
				{
					GUI.Box(new Rect((float)(num - 5), (float)num2, 220f, 25f), "(WebGL cannot use Match Maker)");
					return;
				}
				if (this.manager.matchMaker == null)
				{
					if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Enable Match Maker (M)"))
					{
						this.manager.StartMatchMaker();
					}
					num2 += 24;
				}
				else
				{
					if (this.manager.matchInfo == null)
					{
						if (this.manager.matches == null)
						{
							if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Create Internet Match"))
							{
								this.manager.matchMaker.CreateMatch(this.manager.matchName, this.manager.matchSize, true, string.Empty, new NetworkMatch.ResponseDelegate<CreateMatchResponse>(this.manager.OnMatchCreate));
							}
							num2 += 24;
							GUI.Label(new Rect((float)num, (float)num2, 100f, 20f), "Room Name:");
							this.manager.matchName = GUI.TextField(new Rect((float)(num + 100), (float)num2, 100f, 20f), this.manager.matchName);
							num2 += 24;
							num2 += 10;
							if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Find Internet Match"))
							{
								this.manager.matchMaker.ListMatches(0, 20, string.Empty, new NetworkMatch.ResponseDelegate<ListMatchResponse>(this.manager.OnMatchList));
							}
							num2 += 24;
						}
						else
						{
							foreach (MatchDesc matchDesc in this.manager.matches)
							{
								if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Join Match:" + matchDesc.name))
								{
									this.manager.matchName = matchDesc.name;
									this.manager.matchSize = (uint)matchDesc.currentSize;
									this.manager.matchMaker.JoinMatch(matchDesc.networkId, string.Empty, new NetworkMatch.ResponseDelegate<JoinMatchResponse>(this.manager.OnMatchJoined));
								}
								num2 += 24;
							}
						}
					}
					if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Change MM server"))
					{
						this.m_ShowServer = !this.m_ShowServer;
					}
					if (this.m_ShowServer)
					{
						num2 += 24;
						if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Local"))
						{
							this.manager.SetMatchHost("localhost", 1337, false);
							this.m_ShowServer = false;
						}
						num2 += 24;
						if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Internet"))
						{
							this.manager.SetMatchHost("mm.unet.unity3d.com", 443, true);
							this.m_ShowServer = false;
						}
						num2 += 24;
						if (GUI.Button(new Rect((float)num, (float)num2, 100f, 20f), "Staging"))
						{
							this.manager.SetMatchHost("staging-mm.unet.unity3d.com", 443, true);
							this.m_ShowServer = false;
						}
					}
					num2 += 24;
					GUI.Label(new Rect((float)num, (float)num2, 300f, 20f), "MM Uri: " + this.manager.matchMaker.baseUri);
					num2 += 24;
					if (GUI.Button(new Rect((float)num, (float)num2, 200f, 20f), "Disable Match Maker"))
					{
						this.manager.StopMatchMaker();
					}
					num2 += 24;
				}
			}
		}

		// Token: 0x04000162 RID: 354
		public NetworkManager manager;

		// Token: 0x04000163 RID: 355
		[SerializeField]
		public bool showGUI = true;

		// Token: 0x04000164 RID: 356
		[SerializeField]
		public int offsetX;

		// Token: 0x04000165 RID: 357
		[SerializeField]
		public int offsetY;

		// Token: 0x04000166 RID: 358
		private bool m_ShowServer;
	}
}
