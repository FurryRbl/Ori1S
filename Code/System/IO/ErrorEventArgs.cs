using System;

namespace System.IO
{
	/// <summary>Provides data for the <see cref="E:System.IO.FileSystemWatcher.Error" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000278 RID: 632
	public class ErrorEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.IO.ErrorEventArgs" /> class.</summary>
		/// <param name="exception">An <see cref="T:System.Exception" /> that represents the error that occurred. </param>
		// Token: 0x06001655 RID: 5717 RVA: 0x0003C550 File Offset: 0x0003A750
		public ErrorEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that represents the error that occurred.</summary>
		/// <returns>An <see cref="T:System.Exception" /> that represents the error that occurred.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001656 RID: 5718 RVA: 0x0003C560 File Offset: 0x0003A760
		public virtual Exception GetException()
		{
			return this.exception;
		}

		// Token: 0x04000705 RID: 1797
		private Exception exception;
	}
}
