using System;

namespace System.Threading
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Application.ThreadException" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004AC RID: 1196
	public class ThreadExceptionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ThreadExceptionEventArgs" /> class.</summary>
		/// <param name="t">The <see cref="T:System.Exception" /> that occurred. </param>
		// Token: 0x06002AF5 RID: 10997 RVA: 0x00093A3C File Offset: 0x00091C3C
		public ThreadExceptionEventArgs(Exception t)
		{
			this.exception = t;
		}

		/// <summary>Gets the <see cref="T:System.Exception" /> that occurred.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x00093A4C File Offset: 0x00091C4C
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04001B1D RID: 6941
		private Exception exception;
	}
}
