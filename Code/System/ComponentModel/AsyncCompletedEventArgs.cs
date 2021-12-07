using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Provides data for the MethodNameCompleted event.</summary>
	// Token: 0x020000C7 RID: 199
	public class AsyncCompletedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" /> class. </summary>
		/// <param name="error">Any error that occurred during the asynchronous operation.</param>
		/// <param name="cancelled">A value indicating whether the asynchronous operation was canceled.</param>
		/// <param name="userState">The optional user-supplied state object passed to the <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" /> method.</param>
		// Token: 0x0600089B RID: 2203 RVA: 0x000198FC File Offset: 0x00017AFC
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this._error = error;
			this._cancelled = cancelled;
			this._userState = userState;
		}

		/// <summary>Raises a user-supplied exception if an asynchronous operation failed.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Cancelled" /> property is true. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" /> property has been set by the asynchronous operation. The <see cref="P:System.Exception.InnerException" /> property holds a reference to <see cref="P:System.ComponentModel.AsyncCompletedEventArgs.Error" />. </exception>
		// Token: 0x0600089C RID: 2204 RVA: 0x0001991C File Offset: 0x00017B1C
		protected void RaiseExceptionIfNecessary()
		{
			if (this._error != null)
			{
				throw new TargetInvocationException(this._error);
			}
			if (this._cancelled)
			{
				throw new InvalidOperationException("The operation was cancelled");
			}
		}

		/// <summary>Gets a value indicating whether an asynchronous operation has been canceled.</summary>
		/// <returns>true if the background operation has been canceled; otherwise false. The default is false.</returns>
		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600089D RID: 2205 RVA: 0x0001994C File Offset: 0x00017B4C
		public bool Cancelled
		{
			get
			{
				return this._cancelled;
			}
		}

		/// <summary>Gets a value indicating which error occurred during an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Exception" /> instance, if an error occurred during an asynchronous operation; otherwise null.</returns>
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00019954 File Offset: 0x00017B54
		public Exception Error
		{
			get
			{
				return this._error;
			}
		}

		/// <summary>Gets the unique identifier for the asynchronous task.</summary>
		/// <returns>An object reference that uniquely identifies the asynchronous task; otherwise, null if no value has been set.</returns>
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x0001995C File Offset: 0x00017B5C
		public object UserState
		{
			get
			{
				return this._userState;
			}
		}

		// Token: 0x0400023A RID: 570
		private Exception _error;

		// Token: 0x0400023B RID: 571
		private bool _cancelled;

		// Token: 0x0400023C RID: 572
		private object _userState;
	}
}
