using System;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler.</summary>
	// Token: 0x02000141 RID: 321
	public class DoWorkEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DoWorkEventArgs" /> class.</summary>
		/// <param name="argument">Specifies an argument for an asynchronous operation.</param>
		// Token: 0x06000BDA RID: 3034 RVA: 0x0001F05C File Offset: 0x0001D25C
		public DoWorkEventArgs(object argument)
		{
			this.arg = argument;
		}

		/// <summary>Gets a value that represents the argument of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the argument of an asynchronous operation.</returns>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001F06C File Offset: 0x0001D26C
		public object Argument
		{
			get
			{
				return this.arg;
			}
		}

		/// <summary>Gets or sets a value that represents the result of an asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.Object" /> representing the result of an asynchronous operation.</returns>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x0001F074 File Offset: 0x0001D274
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0001F07C File Offset: 0x0001D27C
		public object Result
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x0400035C RID: 860
		private object arg;

		// Token: 0x0400035D RID: 861
		private object result;
	}
}
