using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x020000C9 RID: 201
	public static class AsyncOperationManager
	{
		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x00019A80 File Offset: 0x00017C80
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x00019A9C File Offset: 0x00017C9C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nFlags=\"NoFlags\"/>\n</PermissionSet>\n")]
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}

		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. </param>
		// Token: 0x060008AA RID: 2218 RVA: 0x00019AA4 File Offset: 0x00017CA4
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return new AsyncOperation(AsyncOperationManager.SynchronizationContext, userSuppliedState);
		}
	}
}
