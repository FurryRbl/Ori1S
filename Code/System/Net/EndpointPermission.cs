using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Defines an endpoint that is authorized by a <see cref="T:System.Net.SocketPermission" /> instance.</summary>
	// Token: 0x02000302 RID: 770
	[Serializable]
	public class EndpointPermission
	{
		// Token: 0x06001A67 RID: 6759 RVA: 0x0004A110 File Offset: 0x00048310
		internal EndpointPermission(string hostname, int port, TransportType transport)
		{
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			this.hostname = hostname;
			this.port = port;
			this.transport = transport;
			this.resolved = false;
			this.hasWildcard = false;
			this.addresses = null;
		}

		/// <summary>Gets the DNS host name or IP address of the server that is associated with this endpoint.</summary>
		/// <returns>A string that contains the DNS host name or IP address of the server.</returns>
		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0004A174 File Offset: 0x00048374
		public string Hostname
		{
			get
			{
				return this.hostname;
			}
		}

		/// <summary>Gets the network port number that is associated with this endpoint.</summary>
		/// <returns>The network port number that is associated with this request, or <see cref="F:System.Net.SocketPermission.AllPorts" />.</returns>
		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0004A17C File Offset: 0x0004837C
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		/// <summary>Gets the transport type that is associated with this endpoint.</summary>
		/// <returns>One of the <see cref="T:System.Net.TransportType" /> values.</returns>
		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0004A184 File Offset: 0x00048384
		public TransportType Transport
		{
			get
			{
				return this.transport;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.SocketPermission" /> instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">The specified <see cref="T:System.Object" /></param>
		// Token: 0x06001A6C RID: 6764 RVA: 0x0004A18C File Offset: 0x0004838C
		public override bool Equals(object obj)
		{
			EndpointPermission endpointPermission = obj as EndpointPermission;
			return endpointPermission != null && this.port == endpointPermission.port && this.transport == endpointPermission.transport && string.Compare(this.hostname, endpointPermission.hostname, true) == 0;
		}

		/// <summary>Serves as a hash function for a particular <see cref="T:System.Net.SocketPermission" /> instance. </summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001A6D RID: 6765 RVA: 0x0004A1E0 File Offset: 0x000483E0
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Net.EndpointPermission" /> instance.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001A6E RID: 6766 RVA: 0x0004A1F0 File Offset: 0x000483F0
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.hostname,
				"#",
				this.port,
				"#",
				(int)this.transport
			});
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x0004A240 File Offset: 0x00048440
		internal bool IsSubsetOf(EndpointPermission perm)
		{
			if (perm == null)
			{
				return false;
			}
			if (perm.port != -1 && this.port != perm.port)
			{
				return false;
			}
			if (perm.transport != TransportType.All && this.transport != perm.transport)
			{
				return false;
			}
			this.Resolve();
			perm.Resolve();
			if (this.hasWildcard)
			{
				return perm.hasWildcard && this.IsSubsetOf(this.hostname, perm.hostname);
			}
			if (this.addresses == null)
			{
				return false;
			}
			if (perm.hasWildcard)
			{
				foreach (IPAddress ipaddress in this.addresses)
				{
					if (this.IsSubsetOf(ipaddress.ToString(), perm.hostname))
					{
						return true;
					}
				}
			}
			if (perm.addresses == null)
			{
				return false;
			}
			foreach (IPAddress ipaddress2 in perm.addresses)
			{
				if (this.IsSubsetOf(this.hostname, ipaddress2.ToString()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x0004A368 File Offset: 0x00048568
		private bool IsSubsetOf(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return false;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return false;
				}
				if (num != 256)
				{
					if (num != num2 && num2 != 256)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x0004A3F0 File Offset: 0x000485F0
		internal EndpointPermission Intersect(EndpointPermission perm)
		{
			if (perm == null)
			{
				return null;
			}
			int num;
			if (this.port == perm.port)
			{
				num = this.port;
			}
			else if (this.port == -1)
			{
				num = perm.port;
			}
			else
			{
				if (perm.port != -1)
				{
					return null;
				}
				num = this.port;
			}
			TransportType transportType;
			if (this.transport == perm.transport)
			{
				transportType = this.transport;
			}
			else if (this.transport == TransportType.All)
			{
				transportType = perm.transport;
			}
			else
			{
				if (perm.transport != TransportType.All)
				{
					return null;
				}
				transportType = this.transport;
			}
			string text = this.IntersectHostname(perm);
			if (text == null)
			{
				return null;
			}
			if (!this.hasWildcard)
			{
				return this;
			}
			if (!perm.hasWildcard)
			{
				return perm;
			}
			return new EndpointPermission(text, num, transportType)
			{
				hasWildcard = true,
				resolved = true
			};
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x0004A4E8 File Offset: 0x000486E8
		private string IntersectHostname(EndpointPermission perm)
		{
			if (this.hostname == perm.hostname)
			{
				return this.hostname;
			}
			this.Resolve();
			perm.Resolve();
			string text = null;
			if (this.hasWildcard)
			{
				if (perm.hasWildcard)
				{
					text = this.Intersect(this.hostname, perm.hostname);
				}
				else if (perm.addresses != null)
				{
					for (int i = 0; i < perm.addresses.Length; i++)
					{
						text = this.Intersect(this.hostname, perm.addresses[i].ToString());
						if (text != null)
						{
							break;
						}
					}
				}
			}
			else if (this.addresses != null)
			{
				for (int j = 0; j < this.addresses.Length; j++)
				{
					string addr = this.addresses[j].ToString();
					if (perm.hasWildcard)
					{
						text = this.Intersect(addr, perm.hostname);
					}
					else if (perm.addresses != null)
					{
						for (int k = 0; k < perm.addresses.Length; k++)
						{
							text = this.Intersect(addr, perm.addresses[k].ToString());
							if (text != null)
							{
								break;
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x0004A638 File Offset: 0x00048838
		private string Intersect(string addr1, string addr2)
		{
			string[] array = addr1.Split(EndpointPermission.dot_char);
			string[] array2 = addr2.Split(EndpointPermission.dot_char);
			string[] array3 = new string[7];
			for (int i = 0; i < 4; i++)
			{
				int num = this.ToNumber(array[i]);
				if (num == -1)
				{
					return null;
				}
				int num2 = this.ToNumber(array2[i]);
				if (num2 == -1)
				{
					return null;
				}
				if (num == 256)
				{
					array3[i << 1] = ((num2 != 256) ? (string.Empty + num2) : "*");
				}
				else if (num2 == 256)
				{
					array3[i << 1] = ((num != 256) ? (string.Empty + num) : "*");
				}
				else
				{
					if (num != num2)
					{
						return null;
					}
					array3[i << 1] = string.Empty + num;
				}
			}
			array3[1] = (array3[3] = (array3[5] = "."));
			return string.Concat(array3);
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x0004A760 File Offset: 0x00048960
		private int ToNumber(string value)
		{
			if (value == "*")
			{
				return 256;
			}
			int length = value.Length;
			if (length < 1 || length > 3)
			{
				return -1;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if ('0' > c || c > '9')
				{
					return -1;
				}
				num = checked(num * 10 + (int)(c - '0'));
			}
			return (num > 255) ? -1 : num;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x0004A7EC File Offset: 0x000489EC
		internal void Resolve()
		{
			if (this.resolved)
			{
				return;
			}
			bool flag = false;
			bool flag2 = false;
			this.addresses = null;
			string[] array = this.hostname.Split(EndpointPermission.dot_char);
			if (array.Length != 4)
			{
				flag = true;
			}
			else
			{
				for (int i = 0; i < 4; i++)
				{
					int num = this.ToNumber(array[i]);
					if (num == -1)
					{
						flag = true;
						break;
					}
					if (num == 256)
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.hasWildcard = false;
				try
				{
					this.addresses = Dns.GetHostAddresses(this.hostname);
				}
				catch (System.Net.Sockets.SocketException)
				{
				}
			}
			else
			{
				this.hasWildcard = flag2;
				if (!flag2)
				{
					this.addresses = new IPAddress[1];
					this.addresses[0] = IPAddress.Parse(this.hostname);
				}
			}
			this.resolved = true;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x0004A8E8 File Offset: 0x00048AE8
		internal void UndoResolve()
		{
			this.resolved = false;
		}

		// Token: 0x04001051 RID: 4177
		private static char[] dot_char = new char[]
		{
			'.'
		};

		// Token: 0x04001052 RID: 4178
		private string hostname;

		// Token: 0x04001053 RID: 4179
		private int port;

		// Token: 0x04001054 RID: 4180
		private TransportType transport;

		// Token: 0x04001055 RID: 4181
		private bool resolved;

		// Token: 0x04001056 RID: 4182
		private bool hasWildcard;

		// Token: 0x04001057 RID: 4183
		private IPAddress[] addresses;
	}
}
