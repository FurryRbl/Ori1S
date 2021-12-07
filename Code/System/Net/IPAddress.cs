using System;
using System.Globalization;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Provides an Internet Protocol (IP) address.</summary>
	// Token: 0x0200032A RID: 810
	[Serializable]
	public class IPAddress
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as an <see cref="T:System.Int64" />.</summary>
		/// <param name="newAddress">The long value of the IP address. For example, the value 0x2414188f in big-endian format would be the IP address "143.24.20.36". </param>
		// Token: 0x06001CA3 RID: 7331 RVA: 0x000543D0 File Offset: 0x000525D0
		public IPAddress(long addr)
		{
			this.m_Address = addr;
			this.m_Family = System.Net.Sockets.AddressFamily.InterNetwork;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		// Token: 0x06001CA4 RID: 7332 RVA: 0x000543E8 File Offset: 0x000525E8
		public IPAddress(byte[] address)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			int num = address.Length;
			if (num != 16 && num != 4)
			{
				throw new ArgumentException("An invalid IP address was specified.", "address");
			}
			if (num == 16)
			{
				this.m_Numbers = new ushort[8];
				Buffer.BlockCopy(address, 0, this.m_Numbers, 0, 16);
				this.m_Family = System.Net.Sockets.AddressFamily.InterNetworkV6;
				this.m_ScopeId = 0L;
			}
			else
			{
				this.m_Address = (long)((ulong)((ulong)address[3] << 24) + (ulong)((long)((long)address[2] << 16)) + (ulong)((long)((long)address[1] << 8)) + (ulong)((long)address[0]));
				this.m_Family = System.Net.Sockets.AddressFamily.InterNetwork;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.IPAddress" /> class with the address specified as a <see cref="T:System.Byte" /> array and the specified scope identifier.</summary>
		/// <param name="address">The byte array value of the IP address. </param>
		/// <param name="scopeid">The long value of the scope identifier. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeid" /> &lt; 0 or <paramref name="scopeid" /> &gt; 0x00000000FFFFFFFF </exception>
		// Token: 0x06001CA5 RID: 7333 RVA: 0x00054490 File Offset: 0x00052690
		public IPAddress(byte[] address, long scopeId)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address.Length != 16)
			{
				throw new ArgumentException("An invalid IP address was specified.", "address");
			}
			this.m_Numbers = new ushort[8];
			Buffer.BlockCopy(address, 0, this.m_Numbers, 0, 16);
			this.m_Family = System.Net.Sockets.AddressFamily.InterNetworkV6;
			this.m_ScopeId = scopeId;
		}

		// Token: 0x06001CA6 RID: 7334 RVA: 0x000544FC File Offset: 0x000526FC
		internal IPAddress(ushort[] address, long scopeId)
		{
			this.m_Numbers = address;
			for (int i = 0; i < 8; i++)
			{
				this.m_Numbers[i] = (ushort)IPAddress.HostToNetworkOrder((short)this.m_Numbers[i]);
			}
			this.m_Family = System.Net.Sockets.AddressFamily.InterNetworkV6;
			this.m_ScopeId = scopeId;
		}

		// Token: 0x06001CA8 RID: 7336 RVA: 0x000545C4 File Offset: 0x000527C4
		private static short SwapShort(short number)
		{
			return (short)((number >> 8 & 255) | ((int)number << 8 & 65280));
		}

		// Token: 0x06001CA9 RID: 7337 RVA: 0x000545DC File Offset: 0x000527DC
		private static int SwapInt(int number)
		{
			return (number >> 24 & 255) | (number >> 8 & 65280) | (number << 8 & 16711680) | number << 24;
		}

		// Token: 0x06001CAA RID: 7338 RVA: 0x00054604 File Offset: 0x00052804
		private static long SwapLong(long number)
		{
			return (number >> 56 & 255L) | (number >> 40 & 65280L) | (number >> 24 & 16711680L) | (number >> 8 & (long)((ulong)-16777216)) | (number << 8 & 1095216660480L) | (number << 24 & 280375465082880L) | (number << 40 & 71776119061217280L) | number << 56;
		}

		/// <summary>Converts a short value from host byte order to network byte order.</summary>
		/// <returns>A short value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001CAB RID: 7339 RVA: 0x00054670 File Offset: 0x00052870
		public static short HostToNetworkOrder(short host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return IPAddress.SwapShort(host);
		}

		/// <summary>Converts an integer value from host byte order to network byte order.</summary>
		/// <returns>An integer value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001CAC RID: 7340 RVA: 0x00054684 File Offset: 0x00052884
		public static int HostToNetworkOrder(int host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return IPAddress.SwapInt(host);
		}

		/// <summary>Converts a long value from host byte order to network byte order.</summary>
		/// <returns>A long value, expressed in network byte order.</returns>
		/// <param name="host">The number to convert, expressed in host byte order. </param>
		// Token: 0x06001CAD RID: 7341 RVA: 0x00054698 File Offset: 0x00052898
		public static long HostToNetworkOrder(long host)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return host;
			}
			return IPAddress.SwapLong(host);
		}

		/// <summary>Converts a short value from network byte order to host byte order.</summary>
		/// <returns>A short value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001CAE RID: 7342 RVA: 0x000546AC File Offset: 0x000528AC
		public static short NetworkToHostOrder(short network)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return network;
			}
			return IPAddress.SwapShort(network);
		}

		/// <summary>Converts an integer value from network byte order to host byte order.</summary>
		/// <returns>An integer value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001CAF RID: 7343 RVA: 0x000546C0 File Offset: 0x000528C0
		public static int NetworkToHostOrder(int network)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return network;
			}
			return IPAddress.SwapInt(network);
		}

		/// <summary>Converts a long value from network byte order to host byte order.</summary>
		/// <returns>A long value, expressed in host byte order.</returns>
		/// <param name="network">The number to convert, expressed in network byte order. </param>
		// Token: 0x06001CB0 RID: 7344 RVA: 0x000546D4 File Offset: 0x000528D4
		public static long NetworkToHostOrder(long network)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return network;
			}
			return IPAddress.SwapLong(network);
		}

		/// <summary>Converts an IP address string to an <see cref="T:System.Net.IPAddress" /> instance.</summary>
		/// <returns>An <see cref="T:System.Net.IPAddress" /> instance.</returns>
		/// <param name="ipString">A string that contains an IP address in dotted-quad notation for IPv4 and in colon-hexadecimal notation for IPv6. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ipString" /> is null. </exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="ipString" /> is not a valid IP address. </exception>
		// Token: 0x06001CB1 RID: 7345 RVA: 0x000546E8 File Offset: 0x000528E8
		public static IPAddress Parse(string ipString)
		{
			IPAddress result;
			if (IPAddress.TryParse(ipString, out result))
			{
				return result;
			}
			throw new FormatException("An invalid IP address was specified.");
		}

		/// <summary>Determines whether a string is a valid IP address.</summary>
		/// <returns>true if <paramref name="ipString" /> is a valid IP address; otherwise, false.</returns>
		/// <param name="ipString">The string to validate.</param>
		/// <param name="address">The <see cref="T:System.Net.IPAddress" /> version of the string.</param>
		// Token: 0x06001CB2 RID: 7346 RVA: 0x00054710 File Offset: 0x00052910
		public static bool TryParse(string ipString, out IPAddress address)
		{
			if (ipString == null)
			{
				throw new ArgumentNullException("ipString");
			}
			IPAddress ipaddress;
			address = (ipaddress = IPAddress.ParseIPV4(ipString));
			if (ipaddress == null)
			{
				address = (ipaddress = IPAddress.ParseIPV6(ipString));
				if (ipaddress == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001CB3 RID: 7347 RVA: 0x00054754 File Offset: 0x00052954
		private static IPAddress ParseIPV4(string ip)
		{
			int num = ip.IndexOf(' ');
			if (num != -1)
			{
				string[] array = ip.Substring(num + 1).Split(new char[]
				{
					'.'
				});
				if (array.Length > 0)
				{
					string text = array[array.Length - 1];
					if (text.Length == 0)
					{
						return null;
					}
					foreach (char digit in text)
					{
						if (!System.Uri.IsHexDigit(digit))
						{
							return null;
						}
					}
				}
				ip = ip.Substring(0, num);
			}
			if (ip.Length == 0 || ip[ip.Length - 1] == '.')
			{
				return null;
			}
			string[] array2 = ip.Split(new char[]
			{
				'.'
			});
			if (array2.Length > 4)
			{
				return null;
			}
			IPAddress result;
			try
			{
				long num2 = 0L;
				long num3 = 0L;
				for (int j = 0; j < array2.Length; j++)
				{
					string text3 = array2[j];
					if (3 <= text3.Length && text3.Length <= 4 && text3[0] == '0' && (text3[1] == 'x' || text3[1] == 'X'))
					{
						if (text3.Length == 3)
						{
							num3 = (long)((byte)System.Uri.FromHex(text3[2]));
						}
						else
						{
							num3 = (long)((byte)(System.Uri.FromHex(text3[2]) << 4 | System.Uri.FromHex(text3[3])));
						}
					}
					else
					{
						if (text3.Length == 0)
						{
							return null;
						}
						if (text3[0] == '0')
						{
							num3 = 0L;
							for (int k = 1; k < text3.Length; k++)
							{
								if ('0' > text3[k] || text3[k] > '7')
								{
									return null;
								}
								num3 = (num3 << 3) + (long)text3[k] - 48L;
							}
						}
						else if (!long.TryParse(text3, NumberStyles.None, null, out num3))
						{
							return null;
						}
					}
					if (j == array2.Length - 1)
					{
						j = 3;
					}
					else if (num3 > 255L)
					{
						return null;
					}
					int num4 = 0;
					while (num3 > 0L)
					{
						num2 |= (num3 & 255L) << (j - num4 << 3);
						num4++;
						num3 /= 256L;
					}
				}
				result = new IPAddress(num2);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x00054A2C File Offset: 0x00052C2C
		private static IPAddress ParseIPV6(string ip)
		{
			IPv6Address pv6Address;
			if (IPv6Address.TryParse(ip, out pv6Address))
			{
				return new IPAddress(pv6Address.Address, pv6Address.ScopeId);
			}
			return null;
		}

		/// <summary>An Internet Protocol (IP) address.</summary>
		/// <returns>The long value of the IP address.</returns>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06001CB5 RID: 7349 RVA: 0x00054A5C File Offset: 0x00052C5C
		// (set) Token: 0x06001CB6 RID: 7350 RVA: 0x00054A7C File Offset: 0x00052C7C
		[Obsolete("This property is obsolete. Use GetAddressBytes.")]
		public long Address
		{
			get
			{
				if (this.m_Family != System.Net.Sockets.AddressFamily.InterNetwork)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				return this.m_Address;
			}
			set
			{
				if (this.m_Family != System.Net.Sockets.AddressFamily.InterNetwork)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				this.m_Address = value;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06001CB7 RID: 7351 RVA: 0x00054A9C File Offset: 0x00052C9C
		internal long InternalIPv4Address
		{
			get
			{
				return this.m_Address;
			}
		}

		/// <summary>Gets whether the address is an IPv6 link local address.</summary>
		/// <returns>true if the IP address is an IPv6 link local address; otherwise, false.</returns>
		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06001CB8 RID: 7352 RVA: 0x00054AA4 File Offset: 0x00052CA4
		public bool IsIPv6LinkLocal
		{
			get
			{
				if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return false;
				}
				int num = (int)IPAddress.NetworkToHostOrder((short)this.m_Numbers[0]) & 65520;
				return 65152 <= num && num < 65216;
			}
		}

		/// <summary>Gets whether the address is an IPv6 site local address.</summary>
		/// <returns>true if the IP address is an IPv6 site local address; otherwise, false.</returns>
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x06001CB9 RID: 7353 RVA: 0x00054AEC File Offset: 0x00052CEC
		public bool IsIPv6SiteLocal
		{
			get
			{
				if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
				{
					return false;
				}
				int num = (int)IPAddress.NetworkToHostOrder((short)this.m_Numbers[0]) & 65520;
				return 65216 <= num && num < 65280;
			}
		}

		/// <summary>Gets whether the address is an IPv6 multicast global address.</summary>
		/// <returns>true if the IP address is an IPv6 multicast global address; otherwise, false.</returns>
		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x00054B34 File Offset: 0x00052D34
		public bool IsIPv6Multicast
		{
			get
			{
				return this.m_Family != System.Net.Sockets.AddressFamily.InterNetwork && ((ushort)IPAddress.NetworkToHostOrder((short)this.m_Numbers[0]) & 65280) == 65280;
			}
		}

		/// <summary>Gets or sets the IPv6 address scope identifier.</summary>
		/// <returns>A long integer that specifies the scope of the address.</returns>
		/// <exception cref="T:System.Net.Sockets.SocketException">AddressFamily = InterNetwork. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="scopeId" /> &lt; 0- or -<paramref name="scopeId" /> &gt; 0x00000000FFFFFFFF  </exception>
		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x00054B64 File Offset: 0x00052D64
		// (set) Token: 0x06001CBC RID: 7356 RVA: 0x00054B84 File Offset: 0x00052D84
		public long ScopeId
		{
			get
			{
				if (this.m_Family != System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				return this.m_ScopeId;
			}
			set
			{
				if (this.m_Family != System.Net.Sockets.AddressFamily.InterNetworkV6)
				{
					throw new Exception("The attempted operation is not supported for the type of object referenced");
				}
				this.m_ScopeId = value;
			}
		}

		/// <summary>Provides a copy of the <see cref="T:System.Net.IPAddress" /> as an array of bytes.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		// Token: 0x06001CBD RID: 7357 RVA: 0x00054BA8 File Offset: 0x00052DA8
		public byte[] GetAddressBytes()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetworkV6)
			{
				byte[] array = new byte[16];
				Buffer.BlockCopy(this.m_Numbers, 0, array, 0, 16);
				return array;
			}
			return new byte[]
			{
				(byte)(this.m_Address & 255L),
				(byte)(this.m_Address >> 8 & 255L),
				(byte)(this.m_Address >> 16 & 255L),
				(byte)(this.m_Address >> 24)
			};
		}

		/// <summary>Gets the address family of the IP address.</summary>
		/// <returns>Returns <see cref="F:System.Net.Sockets.AddressFamily.InterNetwork" /> for IPv4 or <see cref="F:System.Net.Sockets.AddressFamily.InterNetworkV6" /> for IPv6.</returns>
		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x00054C28 File Offset: 0x00052E28
		public System.Net.Sockets.AddressFamily AddressFamily
		{
			get
			{
				return this.m_Family;
			}
		}

		/// <summary>Indicates whether the specified IP address is the loopback address.</summary>
		/// <returns>true if <paramref name="address" /> is the loopback address; otherwise, false.</returns>
		/// <param name="address">An IP address. </param>
		// Token: 0x06001CBF RID: 7359 RVA: 0x00054C30 File Offset: 0x00052E30
		public static bool IsLoopback(IPAddress addr)
		{
			if (addr.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return (addr.m_Address & 255L) == 127L;
			}
			for (int i = 0; i < 6; i++)
			{
				if (addr.m_Numbers[i] != 0)
				{
					return false;
				}
			}
			return IPAddress.NetworkToHostOrder((short)addr.m_Numbers[7]) == 1;
		}

		/// <summary>Converts an Internet address to its standard notation.</summary>
		/// <returns>A string that contains the IP address in either IPv4 dotted-quad or in IPv6 colon-hexadecimal notation.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CC0 RID: 7360 RVA: 0x00054C90 File Offset: 0x00052E90
		public override string ToString()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return IPAddress.ToString(this.m_Address);
			}
			ushort[] array = this.m_Numbers.Clone() as ushort[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (ushort)IPAddress.NetworkToHostOrder((short)array[i]);
			}
			return new IPv6Address(array)
			{
				ScopeId = this.ScopeId
			}.ToString();
		}

		// Token: 0x06001CC1 RID: 7361 RVA: 0x00054D00 File Offset: 0x00052F00
		private static string ToString(long addr)
		{
			return string.Concat(new string[]
			{
				(addr & 255L).ToString(),
				".",
				(addr >> 8 & 255L).ToString(),
				".",
				(addr >> 16 & 255L).ToString(),
				".",
				(addr >> 24 & 255L).ToString()
			});
		}

		/// <summary>Compares two IP addresses.</summary>
		/// <returns>true if the two addresses are equal; otherwise, false.</returns>
		/// <param name="comparand">An <see cref="T:System.Net.IPAddress" /> instance to compare to the current instance. </param>
		// Token: 0x06001CC2 RID: 7362 RVA: 0x00054D84 File Offset: 0x00052F84
		public override bool Equals(object other)
		{
			IPAddress ipaddress = other as IPAddress;
			if (ipaddress == null)
			{
				return false;
			}
			if (this.AddressFamily != ipaddress.AddressFamily)
			{
				return false;
			}
			if (this.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return this.m_Address == ipaddress.m_Address;
			}
			ushort[] numbers = ipaddress.m_Numbers;
			for (int i = 0; i < 8; i++)
			{
				if (this.m_Numbers[i] != numbers[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Returns a hash value for an IP address.</summary>
		/// <returns>An integer hash value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001CC3 RID: 7363 RVA: 0x00054DFC File Offset: 0x00052FFC
		public override int GetHashCode()
		{
			if (this.m_Family == System.Net.Sockets.AddressFamily.InterNetwork)
			{
				return (int)this.m_Address;
			}
			return IPAddress.Hash(((int)this.m_Numbers[0] << 16) + (int)this.m_Numbers[1], ((int)this.m_Numbers[2] << 16) + (int)this.m_Numbers[3], ((int)this.m_Numbers[4] << 16) + (int)this.m_Numbers[5], ((int)this.m_Numbers[6] << 16) + (int)this.m_Numbers[7]);
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x00054E74 File Offset: 0x00053074
		private static int Hash(int i, int j, int k, int l)
		{
			return i ^ (j << 13 | j >> 19) ^ (k << 26 | k >> 6) ^ (l << 7 | l >> 25);
		}

		// Token: 0x04001204 RID: 4612
		private long m_Address;

		// Token: 0x04001205 RID: 4613
		private System.Net.Sockets.AddressFamily m_Family;

		// Token: 0x04001206 RID: 4614
		private ushort[] m_Numbers;

		// Token: 0x04001207 RID: 4615
		private long m_ScopeId;

		/// <summary>Provides an IP address that indicates that the server must listen for client activity on all network interfaces. This field is read-only.</summary>
		// Token: 0x04001208 RID: 4616
		public static readonly IPAddress Any = new IPAddress(0L);

		/// <summary>Provides the IP broadcast address. This field is read-only.</summary>
		// Token: 0x04001209 RID: 4617
		public static readonly IPAddress Broadcast = IPAddress.Parse("255.255.255.255");

		/// <summary>Provides the IP loopback address. This field is read-only.</summary>
		// Token: 0x0400120A RID: 4618
		public static readonly IPAddress Loopback = IPAddress.Parse("127.0.0.1");

		/// <summary>Provides an IP address that indicates that no network interface should be used. This field is read-only.</summary>
		// Token: 0x0400120B RID: 4619
		public static readonly IPAddress None = IPAddress.Parse("255.255.255.255");

		/// <summary>The <see cref="M:System.Net.Sockets.Socket.Bind(System.Net.EndPoint)" /> method uses the <see cref="F:System.Net.IPAddress.IPv6Any" /> field to indicate that a <see cref="T:System.Net.Sockets.Socket" /> must listen for client activity on all network interfaces.</summary>
		// Token: 0x0400120C RID: 4620
		public static readonly IPAddress IPv6Any = IPAddress.ParseIPV6("::");

		/// <summary>Provides the IP loopback address. This property is read-only.</summary>
		// Token: 0x0400120D RID: 4621
		public static readonly IPAddress IPv6Loopback = IPAddress.ParseIPV6("::1");

		/// <summary>Provides an IP address that indicates that no network interface should be used. This property is read-only.</summary>
		// Token: 0x0400120E RID: 4622
		public static readonly IPAddress IPv6None = IPAddress.ParseIPV6("::");

		// Token: 0x0400120F RID: 4623
		private int m_HashCode;
	}
}
