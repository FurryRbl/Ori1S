using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Enables communication with a debugger. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DC RID: 476
	[MonoTODO("The Debugger class is not functional")]
	[ComVisible(true)]
	public sealed class Debugger
	{
		/// <summary>Gets a value that indicates whether a debugger is attached to the process.</summary>
		/// <returns>true if a debugger is attached; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600185A RID: 6234 RVA: 0x0005D588 File Offset: 0x0005B788
		public static bool IsAttached
		{
			get
			{
				return Debugger.IsAttached_internal();
			}
		}

		// Token: 0x0600185B RID: 6235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAttached_internal();

		/// <summary>Signals a breakpoint to an attached debugger.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to break into the debugger. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600185C RID: 6236 RVA: 0x0005D590 File Offset: 0x0005B790
		public static void Break()
		{
		}

		/// <summary>Checks to see if logging is enabled by an attached debugger.</summary>
		/// <returns>true if a debugger is attached and logging is enabled; otherwise, false. The attached debugger is the registered managed debugger in the DbgManagedDebugger registry key. For more information on this key, see Enabling JIT-attach Debugging.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185D RID: 6237 RVA: 0x0005D594 File Offset: 0x0005B794
		public static bool IsLogging()
		{
			return false;
		}

		/// <summary>Launches and attaches a debugger to the process.</summary>
		/// <returns>true if the startup is successful or if the debugger is already attached; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to start the debugger. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600185E RID: 6238 RVA: 0x0005D598 File Offset: 0x0005B798
		[MonoTODO("Not implemented")]
		public static bool Launch()
		{
			throw new NotImplementedException();
		}

		/// <summary>Posts a message for the attached debugger.</summary>
		/// <param name="level">A description of the importance of the message. </param>
		/// <param name="category">The category of the message. </param>
		/// <param name="message">The message to show. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600185F RID: 6239 RVA: 0x0005D5A0 File Offset: 0x0005B7A0
		public static void Log(int level, string category, string message)
		{
		}

		/// <summary>Represents the default category of message with a constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008DE RID: 2270
		public static readonly string DefaultCategory = string.Empty;
	}
}
