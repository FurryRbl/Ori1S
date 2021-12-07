using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x020001A4 RID: 420
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunWorkerCompletedEventArgs" /> class.</summary>
		/// <param name="result">The result of an asynchronous operation.</param>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		// Token: 0x06000EC4 RID: 3780 RVA: 0x00026540 File Offset: 0x00024740
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled) : base(error, cancelled, null)
		{
			this.result = result;
		}

		/// <summary>Gets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> is not null. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x00026554 File Offset: 0x00024754
		public object Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		/// <summary>Gets a value that represents the user state.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the user state.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00026564 File Offset: 0x00024764
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public new object UserState
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000432 RID: 1074
		private object result;
	}
}
