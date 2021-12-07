using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores a collection of headers used in the channel sinks.</summary>
	// Token: 0x02000467 RID: 1127
	[ComVisible(true)]
	[MonoTODO("Serialization format not compatible with .NET")]
	[Serializable]
	public class TransportHeaders : ITransportHeaders
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.TransportHeaders" /> class.</summary>
		// Token: 0x06002EB3 RID: 11955 RVA: 0x0009ABE4 File Offset: 0x00098DE4
		public TransportHeaders()
		{
			this.hash_table = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
		}

		/// <summary>Gets or sets a transport header that is associated with the given key.</summary>
		/// <returns>A transport header that is associated with the given key, or null if the key was not found.</returns>
		/// <param name="key">The <see cref="T:System.String" /> that the requested header is associated with. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x1700085E RID: 2142
		public object this[object key]
		{
			get
			{
				return this.hash_table[key];
			}
			set
			{
				this.hash_table[key] = value;
			}
		}

		/// <summary>Returns an enumerator of the stored transport headers.</summary>
		/// <returns>An enumerator of the stored transport headers.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06002EB6 RID: 11958 RVA: 0x0009AC24 File Offset: 0x00098E24
		public IEnumerator GetEnumerator()
		{
			return this.hash_table.GetEnumerator();
		}

		// Token: 0x040013E4 RID: 5092
		private Hashtable hash_table;
	}
}
