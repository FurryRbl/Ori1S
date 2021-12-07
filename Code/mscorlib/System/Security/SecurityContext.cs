using System;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System.Security
{
	/// <summary>Encapsulates and propagates all security-related data for execution contexts transferred across threads. This class cannot be inherited.</summary>
	// Token: 0x02000540 RID: 1344
	public sealed class SecurityContext
	{
		// Token: 0x060034F1 RID: 13553 RVA: 0x000AE8B4 File Offset: 0x000ACAB4
		internal SecurityContext()
		{
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000AE8BC File Offset: 0x000ACABC
		internal SecurityContext(SecurityContext sc)
		{
			this._capture = true;
			this._winid = sc._winid;
			if (sc._stack != null)
			{
				this._stack = sc._stack.CreateCopy();
			}
		}

		/// <summary>Creates a copy of the current security context.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityContext" /> object representing the security context for the current thread.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current security context has been previously used, was marshaled across application domains, or was not acquired through the <see cref="M:System.Security.SecurityContext.Capture" /> method.</exception>
		// Token: 0x060034F3 RID: 13555 RVA: 0x000AE8F4 File Offset: 0x000ACAF4
		public SecurityContext CreateCopy()
		{
			if (!this._capture)
			{
				throw new InvalidOperationException();
			}
			return new SecurityContext(this);
		}

		/// <summary>Captures the security context for the current thread.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityContext" /> object representing the security context for the current thread.</returns>
		// Token: 0x060034F4 RID: 13556 RVA: 0x000AE910 File Offset: 0x000ACB10
		public static SecurityContext Capture()
		{
			SecurityContext securityContext = Thread.CurrentThread.ExecutionContext.SecurityContext;
			if (securityContext.FlowSuppressed)
			{
				return null;
			}
			return new SecurityContext
			{
				_capture = true,
				_winid = WindowsIdentity.GetCurrentToken(),
				_stack = CompressedStack.Capture()
			};
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060034F5 RID: 13557 RVA: 0x000AE960 File Offset: 0x000ACB60
		// (set) Token: 0x060034F6 RID: 13558 RVA: 0x000AE968 File Offset: 0x000ACB68
		internal bool FlowSuppressed
		{
			get
			{
				return this._suppressFlow;
			}
			set
			{
				this._suppressFlow = value;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x060034F7 RID: 13559 RVA: 0x000AE974 File Offset: 0x000ACB74
		// (set) Token: 0x060034F8 RID: 13560 RVA: 0x000AE97C File Offset: 0x000ACB7C
		internal bool WindowsIdentityFlowSuppressed
		{
			get
			{
				return this._suppressFlowWindowsIdentity;
			}
			set
			{
				this._suppressFlowWindowsIdentity = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000AE988 File Offset: 0x000ACB88
		// (set) Token: 0x060034FA RID: 13562 RVA: 0x000AE990 File Offset: 0x000ACB90
		internal CompressedStack CompressedStack
		{
			get
			{
				return this._stack;
			}
			set
			{
				this._stack = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x000AE99C File Offset: 0x000ACB9C
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x000AE9A4 File Offset: 0x000ACBA4
		internal IntPtr IdentityToken
		{
			get
			{
				return this._winid;
			}
			set
			{
				this._winid = value;
			}
		}

		/// <summary>Determines whether the flow of the security context has been suppressed.</summary>
		/// <returns>true if the flow has been suppressed; otherwise, false. </returns>
		// Token: 0x060034FD RID: 13565 RVA: 0x000AE9B0 File Offset: 0x000ACBB0
		public static bool IsFlowSuppressed()
		{
			return Thread.CurrentThread.ExecutionContext.SecurityContext.FlowSuppressed;
		}

		/// <summary>Determines whether the flow of the Windows identity portion of the current security context has been suppressed.</summary>
		/// <returns>true if the flow has been suppressed; otherwise, false. </returns>
		// Token: 0x060034FE RID: 13566 RVA: 0x000AE9C8 File Offset: 0x000ACBC8
		public static bool IsWindowsIdentityFlowSuppressed()
		{
			return Thread.CurrentThread.ExecutionContext.SecurityContext.WindowsIdentityFlowSuppressed;
		}

		/// <summary>Restores the flow of the security context across asynchronous threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The security context is null or an empty string.</exception>
		// Token: 0x060034FF RID: 13567 RVA: 0x000AE9E0 File Offset: 0x000ACBE0
		public static void RestoreFlow()
		{
			SecurityContext securityContext = Thread.CurrentThread.ExecutionContext.SecurityContext;
			if (!securityContext.FlowSuppressed && !securityContext.WindowsIdentityFlowSuppressed)
			{
				throw new InvalidOperationException();
			}
			securityContext.FlowSuppressed = false;
			securityContext.WindowsIdentityFlowSuppressed = false;
		}

		/// <summary>Runs the specified method in the specified security context on the current thread.</summary>
		/// <param name="securityContext">The <see cref="T:System.Security.SecurityContext" /> to set.</param>
		/// <param name="callback">The <see cref="T:System.Threading.ContextCallback" /> delegate that represents the method to run in the specified security context.</param>
		/// <param name="state">The object to pass to the callback method.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="securityContext" /> is null.-or-<paramref name="securityContext" /> was not acquired through a capture operation -or-<paramref name="securityContext" /> has already been used as the argument to a <see cref="M:System.Security.SecurityContext.Run(System.Security.SecurityContext,System.Threading.ContextCallback,System.Object)" /> method call.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003500 RID: 13568 RVA: 0x000AEA28 File Offset: 0x000ACC28
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPrincipal\"/>\n</PermissionSet>\n")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public static void Run(SecurityContext securityContext, ContextCallback callback, object state)
		{
			if (securityContext == null)
			{
				throw new InvalidOperationException(Locale.GetText("Null SecurityContext"));
			}
			SecurityContext securityContext2 = Thread.CurrentThread.ExecutionContext.SecurityContext;
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			try
			{
				if (securityContext2.IdentityToken != IntPtr.Zero)
				{
					Thread.CurrentPrincipal = new WindowsPrincipal(new WindowsIdentity(securityContext2.IdentityToken));
				}
				if (securityContext.CompressedStack != null)
				{
					CompressedStack.Run(securityContext.CompressedStack, callback, state);
				}
				else
				{
					callback(state);
				}
			}
			finally
			{
				if (currentPrincipal != null && securityContext2.IdentityToken != IntPtr.Zero)
				{
					Thread.CurrentPrincipal = currentPrincipal;
				}
			}
		}

		/// <summary>Suppresses the flow of the security context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003501 RID: 13569 RVA: 0x000AEAF4 File Offset: 0x000ACCF4
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public static AsyncFlowControl SuppressFlow()
		{
			Thread currentThread = Thread.CurrentThread;
			currentThread.ExecutionContext.SecurityContext.FlowSuppressed = true;
			currentThread.ExecutionContext.SecurityContext.WindowsIdentityFlowSuppressed = true;
			return new AsyncFlowControl(currentThread, AsyncFlowControlType.Security);
		}

		/// <summary>Suppresses the flow of the Windows identity portion of the current security context across asynchronous threads.</summary>
		/// <returns>An <see cref="T:System.Threading.AsyncFlowControl" /> structure for restoring the flow.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x06003502 RID: 13570 RVA: 0x000AEB30 File Offset: 0x000ACD30
		public static AsyncFlowControl SuppressFlowWindowsIdentity()
		{
			Thread currentThread = Thread.CurrentThread;
			currentThread.ExecutionContext.SecurityContext.WindowsIdentityFlowSuppressed = true;
			return new AsyncFlowControl(currentThread, AsyncFlowControlType.Security);
		}

		// Token: 0x04001639 RID: 5689
		private bool _capture;

		// Token: 0x0400163A RID: 5690
		private IntPtr _winid;

		// Token: 0x0400163B RID: 5691
		private CompressedStack _stack;

		// Token: 0x0400163C RID: 5692
		private bool _suppressFlowWindowsIdentity;

		// Token: 0x0400163D RID: 5693
		private bool _suppressFlow;
	}
}
