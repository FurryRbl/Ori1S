using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores channel data for the remoting channels.</summary>
	// Token: 0x02000449 RID: 1097
	[ComVisible(true)]
	[Serializable]
	public class ChannelDataStore : IChannelDataStore
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.ChannelDataStore" /> class with the URIs that the current channel maps to.</summary>
		/// <param name="channelURIs">An array of channel URIs that the current channel maps to. </param>
		// Token: 0x06002E3F RID: 11839 RVA: 0x000999F0 File Offset: 0x00097BF0
		public ChannelDataStore(string[] channelURIs)
		{
			this._channelURIs = channelURIs;
		}

		/// <summary>Gets or sets an array of channel URIs that the current channel maps to.</summary>
		/// <returns>An array of channel URIs that the current channel maps to.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x00099A00 File Offset: 0x00097C00
		// (set) Token: 0x06002E41 RID: 11841 RVA: 0x00099A08 File Offset: 0x00097C08
		public string[] ChannelUris
		{
			get
			{
				return this._channelURIs;
			}
			set
			{
				this._channelURIs = value;
			}
		}

		/// <summary>Gets or sets the data object that is associated with the specified key for the implementing channel.</summary>
		/// <returns>The specified data object for the implementing channel.</returns>
		/// <param name="key">The key that the data object is associated with. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x17000844 RID: 2116
		public object this[object key]
		{
			get
			{
				if (this._extraData == null)
				{
					return null;
				}
				foreach (DictionaryEntry dictionaryEntry in this._extraData)
				{
					if (dictionaryEntry.Key.Equals(key))
					{
						return dictionaryEntry.Value;
					}
				}
				return null;
			}
			set
			{
				if (this._extraData == null)
				{
					this._extraData = new DictionaryEntry[]
					{
						new DictionaryEntry(key, value)
					};
				}
				else
				{
					DictionaryEntry[] array = new DictionaryEntry[this._extraData.Length + 1];
					this._extraData.CopyTo(array, 0);
					array[this._extraData.Length] = new DictionaryEntry(key, value);
					this._extraData = array;
				}
			}
		}

		// Token: 0x040013CD RID: 5069
		private string[] _channelURIs;

		// Token: 0x040013CE RID: 5070
		private DictionaryEntry[] _extraData;
	}
}
