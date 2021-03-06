using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;

namespace System.Net.NetworkInformation
{
	/// <summary>Allows an application to determine whether a remote computer is accessible over the network.</summary>
	// Token: 0x020003B2 RID: 946
	[MonoTODO("IPv6 support is missing")]
	public class Ping : System.ComponentModel.Component, IDisposable
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x00061418 File Offset: 0x0005F618
		static Ping()
		{
			if (Environment.OSVersion.Platform == PlatformID.Unix)
			{
				Ping.CheckLinuxCapabilities();
				if (!Ping.canSendPrivileged && WindowsIdentity.GetCurrent().Name == "root")
				{
					Ping.canSendPrivileged = true;
				}
			}
			else
			{
				Ping.canSendPrivileged = true;
			}
		}

		/// <summary>Occurs when an asynchronous operation to send an Internet Control Message Protocol (ICMP) echo message and receive the corresponding ICMP echo reply message completes or is canceled.</summary>
		// Token: 0x14000051 RID: 81
		// (add) Token: 0x060020F0 RID: 8432 RVA: 0x0006147C File Offset: 0x0005F67C
		// (remove) Token: 0x060020F1 RID: 8433 RVA: 0x00061498 File Offset: 0x0005F698
		public event PingCompletedEventHandler PingCompleted;

		/// <summary>Releases all resources used by instances of the <see cref="T:System.Net.NetworkInformation.Ping" /> class.</summary>
		// Token: 0x060020F2 RID: 8434 RVA: 0x000614B4 File Offset: 0x0005F6B4
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060020F3 RID: 8435
		[DllImport("libc")]
		private static extern int capget(ref Ping.cap_user_header_t header, ref Ping.cap_user_data_t data);

		// Token: 0x060020F4 RID: 8436 RVA: 0x000614B8 File Offset: 0x0005F6B8
		private static void CheckLinuxCapabilities()
		{
			try
			{
				Ping.cap_user_header_t cap_user_header_t = default(Ping.cap_user_header_t);
				Ping.cap_user_data_t cap_user_data_t = default(Ping.cap_user_data_t);
				cap_user_header_t.version = 537333798U;
				int num = -1;
				try
				{
					num = Ping.capget(ref cap_user_header_t, ref cap_user_data_t);
				}
				catch (Exception)
				{
				}
				if (num != -1)
				{
					Ping.canSendPrivileged = ((cap_user_data_t.effective & 8192U) != 0U);
				}
			}
			catch
			{
				Ping.canSendPrivileged = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Net.NetworkInformation.Ping.PingCompleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Net.NetworkInformation.PingCompletedEventArgs" />  object that contains event data.</param>
		// Token: 0x060020F5 RID: 8437 RVA: 0x00061564 File Offset: 0x0005F764
		protected void OnPingCompleted(PingCompletedEventArgs e)
		{
			if (this.PingCompleted != null)
			{
				this.PingCompleted(this, e);
			}
			this.user_async_state = null;
			this.worker = null;
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or describes the reason for the failure if no message was received.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020F6 RID: 8438 RVA: 0x00061598 File Offset: 0x0005F798
		public PingReply Send(IPAddress address)
		{
			return this.Send(address, 4000);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation. </summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x060020F7 RID: 8439 RVA: 0x000615A8 File Offset: 0x0005F7A8
		public PingReply Send(IPAddress address, int timeout)
		{
			return this.Send(address, timeout, Ping.default_buffer);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x060020F8 RID: 8440 RVA: 0x000615B8 File Offset: 0x0005F7B8
		public PingReply Send(IPAddress address, int timeout, byte[] buffer)
		{
			return this.Send(address, timeout, buffer, new PingOptions());
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060020F9 RID: 8441 RVA: 0x000615C8 File Offset: 0x0005F7C8
		public PingReply Send(string hostNameOrAddress)
		{
			return this.Send(hostNameOrAddress, 4000);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This method allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x060020FA RID: 8442 RVA: 0x000615D8 File Offset: 0x0005F7D8
		public PingReply Send(string hostNameOrAddress, int timeout)
		{
			return this.Send(hostNameOrAddress, timeout, Ping.default_buffer);
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x060020FB RID: 8443 RVA: 0x000615E8 File Offset: 0x0005F7E8
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer)
		{
			return this.Send(hostNameOrAddress, timeout, buffer, new PingOptions());
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message if one was received, or provides the reason for the failure if no message was received.</returns>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is a zero length string.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x060020FC RID: 8444 RVA: 0x000615F8 File Offset: 0x0005F7F8
		public PingReply Send(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options)
		{
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostNameOrAddress);
			return this.Send(hostAddresses[0], timeout, buffer, options);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0006161C File Offset: 0x0005F81C
		private static IPAddress GetNonLoopbackIP()
		{
			foreach (IPAddress ipaddress in Dns.GetHostByName(Dns.GetHostName()).AddressList)
			{
				if (!IPAddress.IsLoopback(ipaddress))
				{
					return ipaddress;
				}
			}
			throw new InvalidOperationException("Could not resolve non-loopback IP address for localhost");
		}

		/// <summary>Attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" /> and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PingReply" /> object that provides information about the ICMP echo reply message, if one was received, or provides the reason for the failure, if no message was received. The method will return <see cref="F:System.Net.NetworkInformation.IPStatus.PacketTooBig" /> if the packet exceeds the Maximum Transmission Unit (MTU).</returns>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null, or the <paramref name="buffer" /> size is greater than 65500 bytes.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x060020FE RID: 8446 RVA: 0x00061668 File Offset: 0x0005F868
		public PingReply Send(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (timeout < 0)
			{
				throw new ArgumentOutOfRangeException("timeout", "timeout must be non-negative integer");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (buffer.Length > 65500)
			{
				throw new ArgumentException("buffer");
			}
			if (Ping.canSendPrivileged)
			{
				return this.SendPrivileged(address, timeout, buffer, options);
			}
			return this.SendUnprivileged(address, timeout, buffer, options);
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x000616E8 File Offset: 0x0005F8E8
		private PingReply SendPrivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			IPEndPoint ipendPoint = new IPEndPoint(address, 0);
			IPEndPoint ipendPoint2 = new IPEndPoint(Ping.GetNonLoopbackIP(), 0);
			PingReply result;
			using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Raw, System.Net.Sockets.ProtocolType.Icmp))
			{
				if (options != null)
				{
					socket.DontFragment = options.DontFragment;
					socket.Ttl = (short)options.Ttl;
				}
				socket.SendTimeout = timeout;
				socket.ReceiveTimeout = timeout;
				Ping.IcmpMessage icmpMessage = new Ping.IcmpMessage(8, 0, 1, 0, buffer);
				byte[] array = icmpMessage.GetBytes();
				socket.SendBufferSize = array.Length;
				socket.SendTo(array, array.Length, System.Net.Sockets.SocketFlags.None, ipendPoint);
				DateTime now = DateTime.Now;
				array = new byte[100];
				int num;
				long num3;
				Ping.IcmpMessage icmpMessage2;
				for (;;)
				{
					EndPoint endPoint = ipendPoint2;
					num = 0;
					int num2 = socket.ReceiveFrom_nochecks_exc(array, 0, 100, System.Net.Sockets.SocketFlags.None, ref endPoint, false, out num);
					if (num != 0)
					{
						break;
					}
					num3 = (long)(DateTime.Now - now).TotalMilliseconds;
					int num4 = (int)(array[0] & 15) << 2;
					int size = num2 - num4;
					if (!((IPEndPoint)endPoint).Address.Equals(ipendPoint.Address))
					{
						long num5 = (long)timeout - num3;
						if (num5 <= 0L)
						{
							goto Block_7;
						}
						socket.ReceiveTimeout = (int)num5;
					}
					else
					{
						icmpMessage2 = new Ping.IcmpMessage(array, num4, size);
						if (icmpMessage2.Identifier == 1 && icmpMessage2.Type != 8)
						{
							goto IL_1C9;
						}
						long num6 = (long)timeout - num3;
						if (num6 <= 0L)
						{
							goto Block_9;
						}
						socket.ReceiveTimeout = (int)num6;
					}
				}
				if (num == 10060)
				{
					return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				}
				throw new NotSupportedException(string.Format("Unexpected socket error during ping request: {0}", num));
				Block_7:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				Block_9:
				return new PingReply(null, new byte[0], options, 0L, IPStatus.TimedOut);
				IL_1C9:
				result = new PingReply(address, icmpMessage2.Data, options, num3, icmpMessage2.IPStatus);
			}
			return result;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00061914 File Offset: 0x0005FB14
		private PingReply SendUnprivileged(IPAddress address, int timeout, byte[] buffer, PingOptions options)
		{
			DateTime now = DateTime.Now;
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			string arguments = this.BuildPingArgs(address, timeout, options);
			long roundtripTime = 0L;
			process.StartInfo.FileName = "/bin/ping";
			process.StartInfo.Arguments = arguments;
			process.StartInfo.CreateNoWindow = true;
			process.StartInfo.UseShellExecute = false;
			process.StartInfo.RedirectStandardOutput = true;
			process.StartInfo.RedirectStandardError = true;
			DateTime utcNow = DateTime.UtcNow;
			try
			{
				process.Start();
				roundtripTime = (long)(DateTime.Now - now).TotalMilliseconds;
				if (!process.WaitForExit(timeout) || (process.HasExited && process.ExitCode == 2))
				{
					return new PingReply(address, buffer, options, roundtripTime, IPStatus.TimedOut);
				}
				if (process.ExitCode == 1)
				{
					return new PingReply(address, buffer, options, roundtripTime, IPStatus.TtlExpired);
				}
			}
			catch (Exception)
			{
				return new PingReply(address, buffer, options, roundtripTime, IPStatus.Unknown);
			}
			finally
			{
				if (process != null)
				{
					if (!process.HasExited)
					{
						process.Kill();
					}
					process.Dispose();
				}
			}
			return new PingReply(address, buffer, options, roundtripTime, IPStatus.Success);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06002101 RID: 8449 RVA: 0x00061A84 File Offset: 0x0005FC84
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(address, 4000, Ping.default_buffer, new PingOptions(), userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.Net.IPAddress,System.Int32,System.Byte[],System.Object)" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06002102 RID: 8450 RVA: 0x00061AA0 File Offset: 0x0005FCA0
		public void SendAsync(IPAddress address, int timeout, object userToken)
		{
			this.SendAsync(address, 4000, Ping.default_buffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to the <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002103 RID: 8451 RVA: 0x00061AB4 File Offset: 0x0005FCB4
		public void SendAsync(IPAddress address, object userToken)
		{
			this.SendAsync(address, 4000, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="buffer">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06002104 RID: 8452 RVA: 0x00061AC4 File Offset: 0x0005FCC4
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, buffer, new PingOptions(), userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP packet.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="buffer">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06002105 RID: 8453 RVA: 0x00061AD8 File Offset: 0x0005FCD8
		public void SendAsync(string hostNameOrAddress, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			IPAddress address = Dns.GetHostEntry(hostNameOrAddress).AddressList[0];
			this.SendAsync(address, timeout, buffer, options, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="timeout">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" /> is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="hostNameOrAddress" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		// Token: 0x06002106 RID: 8454 RVA: 0x00061B00 File Offset: 0x0005FD00
		public void SendAsync(string hostNameOrAddress, int timeout, object userToken)
		{
			this.SendAsync(hostNameOrAddress, timeout, Ping.default_buffer, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message to the specified computer, and receive a corresponding ICMP echo reply message from that computer.</summary>
		/// <param name="hostNameOrAddress">A <see cref="T:System.String" /> that identifies the computer that is the destination for the ICMP echo message. The value specified for this parameter can be a host name or a string representation of an IP address.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hostNameOrAddress" /> is null or is an empty string ("").</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="M:System.Net.NetworkInformation.Ping.SendAsync(System.String,System.Object)" />  method is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="hostNameOrAddress" /> could not be resolved to a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.SocketPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002107 RID: 8455 RVA: 0x00061B10 File Offset: 0x0005FD10
		public void SendAsync(string hostNameOrAddress, object userToken)
		{
			this.SendAsync(hostNameOrAddress, 4000, userToken);
		}

		/// <summary>Asynchronously attempts to send an Internet Control Message Protocol (ICMP) echo message with the specified data buffer to the computer that has the specified <see cref="T:System.Net.IPAddress" />, and receive a corresponding ICMP echo reply message from that computer. This overload allows you to specify a time-out value for the operation and control fragmentation and Time-to-Live values for the ICMP echo message packet.</summary>
		/// <param name="address">An <see cref="T:System.Net.IPAddress" /> that identifies the computer that is the destination for the ICMP echo message.</param>
		/// <param name="timeout">A <see cref="T:System.Byte" /> array that contains data to be sent with the ICMP echo message and returned in the ICMP echo reply message. The array cannot contain more than 65,500 bytes.</param>
		/// <param name="buffer">An <see cref="T:System.Int32" /> value that specifies the maximum number of milliseconds (after sending the echo message) to wait for the ICMP echo reply message.</param>
		/// <param name="options">A <see cref="T:System.Net.NetworkInformation.PingOptions" />  object used to control fragmentation and Time-to-Live values for the ICMP echo message packet.</param>
		/// <param name="userToken">An object that is passed to the method invoked when the asynchronous operation completes.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="buffer" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is less than zero.</exception>
		/// <exception cref="T:System.InvalidOperationException">A call to <see cref="Overload:System.Net.NetworkInformation.Ping.SendAsync" />  is in progress.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="address" /> is an IPv6 address and the local computer is running an operating system earlier than Windows 2000. </exception>
		/// <exception cref="T:System.Net.NetworkInformation.PingException">An exception was thrown while sending or receiving the ICMP messages. See the inner exception for the exact exception that was thrown.</exception>
		/// <exception cref="T:System.Net.Sockets.SocketException">
		///   <paramref name="address" /> is not a valid IP address.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object has been disposed.</exception>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="buffer" /> exceeds 65500 bytes.</exception>
		// Token: 0x06002108 RID: 8456 RVA: 0x00061B20 File Offset: 0x0005FD20
		public void SendAsync(IPAddress address, int timeout, byte[] buffer, PingOptions options, object userToken)
		{
			if (this.worker != null)
			{
				throw new InvalidOperationException("Another SendAsync operation is in progress");
			}
			this.worker = new System.ComponentModel.BackgroundWorker();
			this.worker.DoWork += delegate(object o, System.ComponentModel.DoWorkEventArgs ea)
			{
				try
				{
					this.user_async_state = ea.Argument;
					ea.Result = this.Send(address, timeout, buffer, options);
				}
				catch (Exception result)
				{
					ea.Result = result;
				}
			};
			this.worker.WorkerSupportsCancellation = true;
			this.worker.RunWorkerCompleted += delegate(object o, System.ComponentModel.RunWorkerCompletedEventArgs ea)
			{
				this.OnPingCompleted(new PingCompletedEventArgs(ea.Error, ea.Cancelled, this.user_async_state, ea.Result as PingReply));
			};
			this.worker.RunWorkerAsync(userToken);
		}

		/// <summary>Cancels all pending asynchronous requests to send an Internet Control Message Protocol (ICMP) echo message and receives a corresponding ICMP echo reply message.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002109 RID: 8457 RVA: 0x00061BC0 File Offset: 0x0005FDC0
		public void SendAsyncCancel()
		{
			if (this.worker == null)
			{
				throw new InvalidOperationException("SendAsync operation is not in progress");
			}
			this.worker.CancelAsync();
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00061BE4 File Offset: 0x0005FDE4
		private string BuildPingArgs(IPAddress address, int timeout, PingOptions options)
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			StringBuilder stringBuilder = new StringBuilder();
			uint num = Convert.ToUInt32(Math.Floor((double)(timeout + 1000) / 1000.0));
			bool flag = Environment.OSVersion.Platform == PlatformID.MacOSX;
			if (!flag)
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -w {1} -t {2} -M ", new object[]
				{
					1,
					num,
					options.Ttl
				});
			}
			else
			{
				stringBuilder.AppendFormat(invariantCulture, "-q -n -c {0} -t {1} -o -m {2} ", new object[]
				{
					1,
					num,
					options.Ttl
				});
			}
			if (!flag)
			{
				stringBuilder.Append((!options.DontFragment) ? "dont " : "do ");
			}
			else if (options.DontFragment)
			{
				stringBuilder.Append("-D ");
			}
			stringBuilder.Append(address.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x0400141D RID: 5149
		private const int DefaultCount = 1;

		// Token: 0x0400141E RID: 5150
		private const string PingBinPath = "/bin/ping";

		// Token: 0x0400141F RID: 5151
		private const int default_timeout = 4000;

		// Token: 0x04001420 RID: 5152
		private const int identifier = 1;

		// Token: 0x04001421 RID: 5153
		private const uint linux_cap_version = 537333798U;

		// Token: 0x04001422 RID: 5154
		private static readonly byte[] default_buffer = new byte[0];

		// Token: 0x04001423 RID: 5155
		private static bool canSendPrivileged;

		// Token: 0x04001424 RID: 5156
		private System.ComponentModel.BackgroundWorker worker;

		// Token: 0x04001425 RID: 5157
		private object user_async_state;

		// Token: 0x020003B3 RID: 947
		private struct cap_user_header_t
		{
			// Token: 0x04001427 RID: 5159
			public uint version;

			// Token: 0x04001428 RID: 5160
			public int pid;
		}

		// Token: 0x020003B4 RID: 948
		private struct cap_user_data_t
		{
			// Token: 0x04001429 RID: 5161
			public uint effective;

			// Token: 0x0400142A RID: 5162
			public uint permitted;

			// Token: 0x0400142B RID: 5163
			public uint inheritable;
		}

		// Token: 0x020003B5 RID: 949
		private class IcmpMessage
		{
			// Token: 0x0600210B RID: 8459 RVA: 0x00061CF0 File Offset: 0x0005FEF0
			public IcmpMessage(byte[] bytes, int offset, int size)
			{
				this.bytes = new byte[size];
				Buffer.BlockCopy(bytes, offset, this.bytes, 0, size);
			}

			// Token: 0x0600210C RID: 8460 RVA: 0x00061D14 File Offset: 0x0005FF14
			public IcmpMessage(byte type, byte code, short identifier, short sequence, byte[] data)
			{
				this.bytes = new byte[data.Length + 8];
				this.bytes[0] = type;
				this.bytes[1] = code;
				this.bytes[4] = (byte)(identifier & 255);
				this.bytes[5] = (byte)(identifier >> 8);
				this.bytes[6] = (byte)(sequence & 255);
				this.bytes[7] = (byte)(sequence >> 8);
				Buffer.BlockCopy(data, 0, this.bytes, 8, data.Length);
				ushort num = Ping.IcmpMessage.ComputeChecksum(this.bytes);
				this.bytes[2] = (byte)(num & 255);
				this.bytes[3] = (byte)(num >> 8);
			}

			// Token: 0x1700093A RID: 2362
			// (get) Token: 0x0600210D RID: 8461 RVA: 0x00061DC0 File Offset: 0x0005FFC0
			public byte Type
			{
				get
				{
					return this.bytes[0];
				}
			}

			// Token: 0x1700093B RID: 2363
			// (get) Token: 0x0600210E RID: 8462 RVA: 0x00061DCC File Offset: 0x0005FFCC
			public byte Code
			{
				get
				{
					return this.bytes[1];
				}
			}

			// Token: 0x1700093C RID: 2364
			// (get) Token: 0x0600210F RID: 8463 RVA: 0x00061DD8 File Offset: 0x0005FFD8
			public byte Identifier
			{
				get
				{
					return (byte)((int)this.bytes[4] + ((int)this.bytes[5] << 8));
				}
			}

			// Token: 0x1700093D RID: 2365
			// (get) Token: 0x06002110 RID: 8464 RVA: 0x00061DF0 File Offset: 0x0005FFF0
			public byte Sequence
			{
				get
				{
					return (byte)((int)this.bytes[6] + ((int)this.bytes[7] << 8));
				}
			}

			// Token: 0x1700093E RID: 2366
			// (get) Token: 0x06002111 RID: 8465 RVA: 0x00061E08 File Offset: 0x00060008
			public byte[] Data
			{
				get
				{
					byte[] array = new byte[this.bytes.Length - 8];
					Buffer.BlockCopy(this.bytes, 0, array, 0, array.Length);
					return array;
				}
			}

			// Token: 0x06002112 RID: 8466 RVA: 0x00061E38 File Offset: 0x00060038
			public byte[] GetBytes()
			{
				return this.bytes;
			}

			// Token: 0x06002113 RID: 8467 RVA: 0x00061E40 File Offset: 0x00060040
			private static ushort ComputeChecksum(byte[] data)
			{
				uint num = 0U;
				for (int i = 0; i < data.Length; i += 2)
				{
					ushort num2 = (ushort)((i + 1 >= data.Length) ? 0 : data[i + 1]);
					num2 = (ushort)(num2 << 8);
					num2 += (ushort)data[i];
					num += (uint)num2;
				}
				num = (num >> 16) + (num & 65535U);
				return (ushort)(~(ushort)num);
			}

			// Token: 0x1700093F RID: 2367
			// (get) Token: 0x06002114 RID: 8468 RVA: 0x00061E9C File Offset: 0x0006009C
			public IPStatus IPStatus
			{
				get
				{
					byte type = this.Type;
					switch (type)
					{
					case 0:
						return IPStatus.Success;
					default:
						switch (type)
						{
						case 8:
							return IPStatus.Success;
						case 11:
						{
							byte code = this.Code;
							if (code == 0)
							{
								return IPStatus.TimeExceeded;
							}
							if (code == 1)
							{
								return IPStatus.TtlReassemblyTimeExceeded;
							}
							break;
						}
						case 12:
							return IPStatus.ParameterProblem;
						}
						break;
					case 3:
						switch (this.Code)
						{
						case 0:
							return IPStatus.DestinationNetworkUnreachable;
						case 1:
							return IPStatus.DestinationHostUnreachable;
						case 2:
							return IPStatus.DestinationProhibited;
						case 3:
							return IPStatus.DestinationPortUnreachable;
						case 4:
							return IPStatus.BadOption;
						case 5:
							return IPStatus.BadRoute;
						}
						break;
					case 4:
						return IPStatus.SourceQuench;
					}
					return IPStatus.Unknown;
				}
			}

			// Token: 0x0400142C RID: 5164
			private byte[] bytes;
		}
	}
}
