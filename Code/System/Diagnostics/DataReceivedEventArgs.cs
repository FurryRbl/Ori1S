using System;

namespace System.Diagnostics
{
	/// <summary>Provides data for the <see cref="E:System.Diagnostics.Process.OutputDataReceived" /> and <see cref="E:System.Diagnostics.Process.ErrorDataReceived" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000214 RID: 532
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0002F4B8 File Offset: 0x0002D6B8
		internal DataReceivedEventArgs(string data)
		{
			this.data = data;
		}

		/// <summary>Gets the line of characters that was written to a redirected <see cref="T:System.Diagnostics.Process" /> output stream.</summary>
		/// <returns>The line that was written by an associated <see cref="T:System.Diagnostics.Process" /> to its redirected <see cref="P:System.Diagnostics.Process.StandardOutput" /> or <see cref="P:System.Diagnostics.Process.StandardError" /> stream.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0002F4C8 File Offset: 0x0002D6C8
		public string Data
		{
			get
			{
				return this.data;
			}
		}

		// Token: 0x04000517 RID: 1303
		private string data;
	}
}
