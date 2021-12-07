using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine.Networking.NetworkSystem;

namespace UnityEngine.Networking
{
	// Token: 0x0200003C RID: 60
	public class NetworkCRC
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x000092F8 File Offset: 0x000074F8
		internal static NetworkCRC singleton
		{
			get
			{
				if (NetworkCRC.s_Singleton == null)
				{
					NetworkCRC.s_Singleton = new NetworkCRC();
				}
				return NetworkCRC.s_Singleton;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00009314 File Offset: 0x00007514
		public Dictionary<string, int> scripts
		{
			get
			{
				return this.m_Scripts;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000931C File Offset: 0x0000751C
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00009328 File Offset: 0x00007528
		public static bool scriptCRCCheck
		{
			get
			{
				return NetworkCRC.singleton.m_ScriptCRCCheck;
			}
			set
			{
				NetworkCRC.singleton.m_ScriptCRCCheck = value;
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00009338 File Offset: 0x00007538
		public static void ReinitializeScriptCRCs(Assembly callingAssembly)
		{
			NetworkCRC.singleton.m_Scripts.Clear();
			foreach (Type type in callingAssembly.GetTypes())
			{
				if (type.BaseType == typeof(NetworkBehaviour))
				{
					MethodInfo method = type.GetMethod(".cctor", BindingFlags.Static);
					if (method != null)
					{
						method.Invoke(null, new object[0]);
					}
				}
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000093AC File Offset: 0x000075AC
		public static void RegisterBehaviour(string name, int channel)
		{
			NetworkCRC.singleton.m_Scripts[name] = channel;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000093C0 File Offset: 0x000075C0
		internal static bool Validate(CRCMessageEntry[] scripts, int numChannels)
		{
			return NetworkCRC.singleton.ValidateInternal(scripts, numChannels);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000093D0 File Offset: 0x000075D0
		private bool ValidateInternal(CRCMessageEntry[] remoteScripts, int numChannels)
		{
			if (this.m_Scripts.Count != remoteScripts.Length)
			{
				if (LogFilter.logWarn)
				{
					Debug.LogWarning("Network configuration mismatch detected. The number of networked scripts on the client does not match the number of networked scripts on the server. This could be caused by lazy loading of scripts on the client. This warning can be disabled by the checkbox in NetworkManager Script CRC Check.");
				}
				this.Dump(remoteScripts);
				return false;
			}
			foreach (CRCMessageEntry crcmessageEntry in remoteScripts)
			{
				if (LogFilter.logDebug)
				{
					Debug.Log(string.Concat(new object[]
					{
						"Script: ",
						crcmessageEntry.name,
						" Channel: ",
						crcmessageEntry.channel
					}));
				}
				if (this.m_Scripts.ContainsKey(crcmessageEntry.name))
				{
					int num = this.m_Scripts[crcmessageEntry.name];
					if (num != (int)crcmessageEntry.channel)
					{
						if (LogFilter.logError)
						{
							Debug.LogError(string.Concat(new object[]
							{
								"HLAPI CRC Channel Mismatch. Script: ",
								crcmessageEntry.name,
								" LocalChannel: ",
								num,
								" RemoteChannel: ",
								crcmessageEntry.channel
							}));
						}
						this.Dump(remoteScripts);
						return false;
					}
				}
				if ((int)crcmessageEntry.channel >= numChannels)
				{
					if (LogFilter.logError)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"HLAPI CRC channel out of range! Script: ",
							crcmessageEntry.name,
							" Channel: ",
							crcmessageEntry.channel
						}));
					}
					this.Dump(remoteScripts);
					return false;
				}
			}
			return true;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00009564 File Offset: 0x00007764
		private void Dump(CRCMessageEntry[] remoteScripts)
		{
			foreach (string text in this.m_Scripts.Keys)
			{
				Debug.Log(string.Concat(new object[]
				{
					"CRC Local Dump ",
					text,
					" : ",
					this.m_Scripts[text]
				}));
			}
			foreach (CRCMessageEntry crcmessageEntry in remoteScripts)
			{
				Debug.Log(string.Concat(new object[]
				{
					"CRC Remote Dump ",
					crcmessageEntry.name,
					" : ",
					crcmessageEntry.channel
				}));
			}
		}

		// Token: 0x040000E2 RID: 226
		internal static NetworkCRC s_Singleton;

		// Token: 0x040000E3 RID: 227
		private Dictionary<string, int> m_Scripts = new Dictionary<string, int>();

		// Token: 0x040000E4 RID: 228
		private bool m_ScriptCRCCheck;
	}
}
