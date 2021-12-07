using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
	// Token: 0x020004BF RID: 1215
	public class ProgressChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProgressChangedEventArgs" /> class.</summary>
		/// <param name="progressPercentage">The percentage of an asynchronous task that has been completed.</param>
		/// <param name="userState">A unique user state.</param>
		// Token: 0x06002BBE RID: 11198 RVA: 0x00098BB0 File Offset: 0x00096DB0
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progress = progressPercentage;
			this.state = userState;
		}

		/// <summary>Gets the asynchronous task progress percentage.</summary>
		/// <returns>A percentage value indicating the asynchronous task progress.</returns>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x00098BC8 File Offset: 0x00096DC8
		public int ProgressPercentage
		{
			get
			{
				return this.progress;
			}
		}

		/// <summary>Gets a unique user state.</summary>
		/// <returns>A unique <see cref="T:System.Object" /> indicating the user state.</returns>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002BC0 RID: 11200 RVA: 0x00098BD0 File Offset: 0x00096DD0
		public object UserState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04001B90 RID: 7056
		private int progress;

		// Token: 0x04001B91 RID: 7057
		private object state;
	}
}
