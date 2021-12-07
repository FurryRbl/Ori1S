using System;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	/// <summary>Provides configuration and statistical information for a network interface.</summary>
	// Token: 0x020003A9 RID: 937
	public abstract class NetworkInterface
	{
		// Token: 0x060020A0 RID: 8352
		[DllImport("libc")]
		private static extern int uname(IntPtr buf);

		/// <summary>Returns objects that describe the network interfaces on the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.NetworkInterface" /> array that contains objects that describe the available network interfaces, or an empty array if no interfaces are detected.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">A Windows system function call failed. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Net.NetworkInformation.NetworkInformationPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Access="Read" />
		/// </PermissionSet>
		// Token: 0x060020A1 RID: 8353 RVA: 0x0005FFE8 File Offset: 0x0005E1E8
		[MonoTODO("Only works on Linux and Windows")]
		public static NetworkInterface[] GetAllNetworkInterfaces()
		{
			if (NetworkInterface.runningOnUnix)
			{
				bool flag = false;
				IntPtr intPtr = Marshal.AllocHGlobal(8192);
				if (NetworkInterface.uname(intPtr) == 0)
				{
					string a = Marshal.PtrToStringAnsi(intPtr);
					if (a == "Darwin")
					{
						flag = true;
					}
				}
				Marshal.FreeHGlobal(intPtr);
				try
				{
					if (flag)
					{
						return MacOsNetworkInterface.ImplGetAllNetworkInterfaces();
					}
					return LinuxNetworkInterface.ImplGetAllNetworkInterfaces();
				}
				catch (SystemException ex)
				{
					throw ex;
				}
				catch
				{
					return new NetworkInterface[0];
				}
			}
			if (Environment.OSVersion.Version >= NetworkInterface.windowsVer51)
			{
				return Win32NetworkInterface2.ImplGetAllNetworkInterfaces();
			}
			return new NetworkInterface[0];
		}

		/// <summary>Indicates whether any network connection is available.</summary>
		/// <returns>true if a network connection is available; otherwise, false.</returns>
		// Token: 0x060020A2 RID: 8354 RVA: 0x000600D0 File Offset: 0x0005E2D0
		[MonoTODO("Always returns true")]
		public static bool GetIsNetworkAvailable()
		{
			return true;
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x000600D4 File Offset: 0x0005E2D4
		internal static string ReadLine(string path)
		{
			string result;
			using (FileStream fileStream = File.OpenRead(path))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					result = streamReader.ReadLine();
				}
			}
			return result;
		}

		/// <summary>Gets the index of the IPv4 loopback interface.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the index for the IPv4 loopback interface.</returns>
		/// <exception cref="T:System.Net.NetworkInformation.NetworkInformationException">This property is not valid on computers running only Ipv6.</exception>
		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x00060154 File Offset: 0x0005E354
		[MonoTODO("Only works on Linux. Returns 0 on other systems.")]
		public static int LoopbackInterfaceIndex
		{
			get
			{
				if (NetworkInterface.runningOnUnix)
				{
					try
					{
						return UnixNetworkInterface.IfNameToIndex("lo");
					}
					catch
					{
						return 0;
					}
					return 0;
				}
				return 0;
			}
		}

		/// <summary>Returns an object that describes the configuration of this network interface.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPInterfaceProperties" /> object that describes this network interface.</returns>
		// Token: 0x060020A5 RID: 8357
		public abstract IPInterfaceProperties GetIPProperties();

		/// <summary>Gets the IPv4 statistics.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.IPv4InterfaceStatistics" /> object.</returns>
		// Token: 0x060020A6 RID: 8358
		public abstract IPv4InterfaceStatistics GetIPv4Statistics();

		/// <summary>Returns the Media Access Control (MAC) or physical address for this adapter.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkInformation.PhysicalAddress" /> object that contains the physical address.</returns>
		// Token: 0x060020A7 RID: 8359
		public abstract PhysicalAddress GetPhysicalAddress();

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the interface supports the specified protocol.</summary>
		/// <returns>true if the specified protocol is supported; otherwise, false.</returns>
		/// <param name="networkInterfaceComponent">A <see cref="T:System.Net.NetworkInformation.NetworkInterfaceComponent" /> value.</param>
		// Token: 0x060020A8 RID: 8360
		public abstract bool Supports(NetworkInterfaceComponent networkInterfaceComponent);

		/// <summary>Gets the description of the interface.</summary>
		/// <returns>A <see cref="T:System.String" /> that describes this interface.</returns>
		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x060020A9 RID: 8361
		public abstract string Description { get; }

		/// <summary>Gets the identifier of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the identifier.</returns>
		// Token: 0x1700091F RID: 2335
		// (get) Token: 0x060020AA RID: 8362
		public abstract string Id { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is set to only receive data packets.</summary>
		/// <returns>true if the interface only receives network traffic; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x060020AB RID: 8363
		public abstract bool IsReceiveOnly { get; }

		/// <summary>Gets the name of the network adapter.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the adapter name.</returns>
		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x060020AC RID: 8364
		public abstract string Name { get; }

		/// <summary>Gets the interface type.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkInformation.NetworkInterfaceType" /> value that specifies the network interface type.</returns>
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x060020AD RID: 8365
		public abstract NetworkInterfaceType NetworkInterfaceType { get; }

		/// <summary>Gets the current operational state of the network connection.</summary>
		/// <returns>One of the <see cref="T:System.Net.NetworkInformation.OperationalStatus" /> values.</returns>
		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x060020AE RID: 8366
		public abstract OperationalStatus OperationalStatus { get; }

		/// <summary>Gets the speed of the network interface.</summary>
		/// <returns>A <see cref="T:System.Int64" /> value that specifies the speed in bits per second.</returns>
		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x060020AF RID: 8367
		public abstract long Speed { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the network interface is enabled to receive multicast packets.</summary>
		/// <returns>true if the interface receives multicast packets; otherwise, false.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">This property is not valid on computers running operating systems earlier than Windows XP. </exception>
		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x060020B0 RID: 8368
		public abstract bool SupportsMulticast { get; }

		// Token: 0x040013DE RID: 5086
		private static Version windowsVer51 = new Version(5, 1);

		// Token: 0x040013DF RID: 5087
		internal static readonly bool runningOnUnix = Environment.OSVersion.Platform == PlatformID.Unix;
	}
}
