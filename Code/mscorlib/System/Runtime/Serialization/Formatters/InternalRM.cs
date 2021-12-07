using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Logs tracing messages when the .NET Framework serialization infrastructure is compiled.</summary>
	// Token: 0x02000513 RID: 1299
	[ComVisible(true)]
	public sealed class InternalRM
	{
		/// <summary>Prints SOAP trace messages.</summary>
		/// <param name="messages"></param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Runtime.Remoting" />
		/// </PermissionSet>
		// Token: 0x06003394 RID: 13204 RVA: 0x000A6C9C File Offset: 0x000A4E9C
		[Conditional("_LOGGING")]
		public static void InfoSoap(params object[] messages)
		{
			throw new NotImplementedException();
		}

		/// <summary>Checks if SOAP tracing is enabled.</summary>
		/// <returns>true, if tracing is enabled; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Runtime.Remoting" />
		/// </PermissionSet>
		// Token: 0x06003395 RID: 13205 RVA: 0x000A6CA4 File Offset: 0x000A4EA4
		public static bool SoapCheckEnabled()
		{
			throw new NotImplementedException();
		}
	}
}
