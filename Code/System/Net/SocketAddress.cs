using System;
using System.Net.Sockets;

namespace System.Net
{
	/// <summary>Stores serialized information from <see cref="T:System.Net.EndPoint" /> derived classes.</summary>
	// Token: 0x020003E8 RID: 1000
	public class SocketAddress
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class using the specified address family and buffer size.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value. </param>
		/// <param name="size">The number of bytes to allocate for the underlying buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="size" /> is less than 2. These 2 bytes are needed to store <paramref name="family" />. </exception>
		// Token: 0x0600229A RID: 8858 RVA: 0x00065260 File Offset: 0x00063460
		public SocketAddress(System.Net.Sockets.AddressFamily family, int size)
		{
			if (size < 2)
			{
				throw new ArgumentOutOfRangeException("size is too small");
			}
			this.data = new byte[size];
			this.data[0] = (byte)family;
			this.data[1] = (byte)(family >> 8);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.SocketAddress" /> class for the given address family.</summary>
		/// <param name="family">An <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value. </param>
		// Token: 0x0600229B RID: 8859 RVA: 0x000652A8 File Offset: 0x000634A8
		public SocketAddress(System.Net.Sockets.AddressFamily family) : this(family, 32)
		{
		}

		/// <summary>Gets the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated value of the current <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>One of the <see cref="T:System.Net.Sockets.AddressFamily" /> enumerated values.</returns>
		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x0600229C RID: 8860 RVA: 0x000652B4 File Offset: 0x000634B4
		public System.Net.Sockets.AddressFamily Family
		{
			get
			{
				return (System.Net.Sockets.AddressFamily)((int)this.data[0] + ((int)this.data[1] << 8));
			}
		}

		/// <summary>Gets the underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</summary>
		/// <returns>The underlying buffer size of the <see cref="T:System.Net.SocketAddress" />.</returns>
		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x0600229D RID: 8861 RVA: 0x000652CC File Offset: 0x000634CC
		public int Size
		{
			get
			{
				return this.data.Length;
			}
		}

		/// <summary>Gets or sets the specified index element in the underlying buffer.</summary>
		/// <returns>The value of the specified index element in the underlying buffer.</returns>
		/// <param name="offset">The array index element of the desired information. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The specified index does not exist in the buffer. </exception>
		// Token: 0x17000A09 RID: 2569
		public byte this[int offset]
		{
			get
			{
				return this.data[offset];
			}
			set
			{
				this.data[offset] = value;
			}
		}

		/// <summary>Returns information about the socket address.</summary>
		/// <returns>A string that contains information about the <see cref="T:System.Net.SocketAddress" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060022A0 RID: 8864 RVA: 0x000652F0 File Offset: 0x000634F0
		public override string ToString()
		{
			string text = ((System.Net.Sockets.AddressFamily)this.data[0]).ToString();
			int num = this.data.Length;
			string text2 = string.Concat(new object[]
			{
				text,
				":",
				num,
				":{"
			});
			for (int i = 2; i < num; i++)
			{
				int num2 = (int)this.data[i];
				text2 += num2;
				if (i < num - 1)
				{
					text2 += ",";
				}
			}
			return text2 + "}";
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Net.SocketAddress" /> instance.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="comparand">The specified <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Net.SocketAddress" /> instance.</param>
		// Token: 0x060022A1 RID: 8865 RVA: 0x00065390 File Offset: 0x00063590
		public override bool Equals(object obj)
		{
			SocketAddress socketAddress = obj as SocketAddress;
			if (socketAddress != null && socketAddress.data.Length == this.data.Length)
			{
				byte[] array = socketAddress.data;
				for (int i = 0; i < this.data.Length; i++)
				{
					if (array[i] != this.data[i])
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Serves as a hash function for a particular type, suitable for use in hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060022A2 RID: 8866 RVA: 0x000653F4 File Offset: 0x000635F4
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < this.data.Length; i++)
			{
				num += (int)this.data[i] + i;
			}
			return num;
		}

		// Token: 0x0400152F RID: 5423
		private byte[] data;
	}
}
