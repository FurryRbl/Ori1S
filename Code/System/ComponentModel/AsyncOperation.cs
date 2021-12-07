using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Tracks the lifetime of an asynchronous operation.</summary>
	// Token: 0x020000C8 RID: 200
	public sealed class AsyncOperation
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x00019964 File Offset: 0x00017B64
		internal AsyncOperation(SynchronizationContext ctx, object state)
		{
			this.ctx = ctx;
			this.state = state;
			ctx.OperationStarted();
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00019980 File Offset: 0x00017B80
		~AsyncOperation()
		{
			if (!this.done && this.ctx != null)
			{
				this.ctx.OperationCompleted();
			}
		}

		/// <summary>Gets the <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</summary>
		/// <returns>The <see cref="T:System.Threading.SynchronizationContext" /> object that was passed to the constructor.</returns>
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x000199D8 File Offset: 0x00017BD8
		public SynchronizationContext SynchronizationContext
		{
			get
			{
				return this.ctx;
			}
		}

		/// <summary>Gets or sets an object used to uniquely identify an asynchronous operation.</summary>
		/// <returns>The state object passed to the asynchronous method invocation.</returns>
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008A3 RID: 2211 RVA: 0x000199E0 File Offset: 0x00017BE0
		public object UserSuppliedState
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008A4 RID: 2212 RVA: 0x000199E8 File Offset: 0x00017BE8
		public void OperationCompleted()
		{
			if (this.done)
			{
				throw new InvalidOperationException("This task is already completed. Multiple call to OperationCompleted is not allowed.");
			}
			this.ctx.OperationCompleted();
			this.done = true;
		}

		/// <summary>Invokes a delegate on the thread or context appropriate for the application model.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.ComponentModel.AsyncOperation.PostOperationCompleted(System.Threading.SendOrPostCallback,System.Object)" /> method has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060008A5 RID: 2213 RVA: 0x00019A20 File Offset: 0x00017C20
		public void Post(SendOrPostCallback d, object arg)
		{
			if (this.done)
			{
				throw new InvalidOperationException("This task is already completed. Multiple call to Post is not allowed.");
			}
			this.ctx.Post(d, arg);
		}

		/// <summary>Ends the lifetime of an asynchronous operation.</summary>
		/// <param name="d">A <see cref="T:System.Threading.SendOrPostCallback" /> object that wraps the delegate to be called when the operation ends. </param>
		/// <param name="arg">An argument for the delegate contained in the <paramref name="d" /> parameter. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.ComponentModel.AsyncOperation.OperationCompleted" /> has been called previously for this task. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="d" /> is null. </exception>
		// Token: 0x060008A6 RID: 2214 RVA: 0x00019A48 File Offset: 0x00017C48
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			if (this.done)
			{
				throw new InvalidOperationException("This task is already completed. Multiple call to PostOperationCompleted is not allowed.");
			}
			this.Post(d, arg);
			this.OperationCompleted();
		}

		// Token: 0x0400023D RID: 573
		private SynchronizationContext ctx;

		// Token: 0x0400023E RID: 574
		private object state;

		// Token: 0x0400023F RID: 575
		private bool done;
	}
}
