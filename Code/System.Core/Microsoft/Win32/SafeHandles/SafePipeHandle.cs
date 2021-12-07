using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000067 RID: 103
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
	public sealed class SafePipeHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x00018FBC File Offset: 0x000171BC
		public SafePipeHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			this.handle = preexistingHandle;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00018FCC File Offset: 0x000171CC
		protected override bool ReleaseHandle()
		{
			bool result;
			try
			{
				Marshal.FreeHGlobal(this.handle);
				result = true;
			}
			catch (ArgumentException)
			{
				result = false;
			}
			return result;
		}
	}
}
