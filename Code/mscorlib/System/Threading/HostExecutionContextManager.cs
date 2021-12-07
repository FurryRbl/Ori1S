﻿using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides the functionality that allows a common language runtime host to participate in the flow, or migration, of the execution context.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006A0 RID: 1696
	public class HostExecutionContextManager
	{
		/// <summary>Captures the host execution context from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host execution context of the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004088 RID: 16520 RVA: 0x000DF220 File Offset: 0x000DD420
		[MonoTODO]
		public virtual HostExecutionContext Capture()
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores the host execution context to its prior state.</summary>
		/// <param name="previousState">The previous context state to revert to.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previousState" /> is null.-or-<paramref name="previousState" /> was not created on the current thread.-or-<paramref name="previousState" /> is not the last state for the <see cref="T:System.Threading.HostExecutionContext" />.</exception>
		// Token: 0x06004089 RID: 16521 RVA: 0x000DF228 File Offset: 0x000DD428
		[MonoTODO]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public virtual void Revert(object previousState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current host execution context to the specified host execution context.</summary>
		/// <returns>An object for restoring the <see cref="T:System.Threading.HostExecutionContext" /> to its previous state.</returns>
		/// <param name="hostExecutionContext">The <see cref="T:System.Threading.HostExecutionContext" /> to be set.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="hostExecutionContext" /> was not acquired through a capture operation. -or- <paramref name="hostExecutionContext" /> has been the argument to a previous <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" />  method call.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="Infrastructure" />
		/// </PermissionSet>
		// Token: 0x0600408A RID: 16522 RVA: 0x000DF230 File Offset: 0x000DD430
		[MonoTODO]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"Infrastructure\"/>\n</PermissionSet>\n")]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			throw new NotImplementedException();
		}
	}
}
