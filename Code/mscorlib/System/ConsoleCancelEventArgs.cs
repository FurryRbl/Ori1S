using System;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.Console.CancelKeyPress" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000111 RID: 273
	[Serializable]
	public sealed class ConsoleCancelEventArgs : EventArgs
	{
		// Token: 0x06000E48 RID: 3656 RVA: 0x0003C95C File Offset: 0x0003AB5C
		internal ConsoleCancelEventArgs(ConsoleSpecialKey key)
		{
			this.specialKey = key;
		}

		/// <summary>Gets or sets a value indicating whether simultaneously pressing the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and <see cref="F:System.ConsoleKey.C" /> console key (CTRL+C) terminates the current process.</summary>
		/// <returns>true if the current process should resume when the event handler concludes; false if the current process should terminate.</returns>
		/// <exception cref="T:System.InvalidOperationException">true was specified in a set operation and the event was caused by simultaneously pressing the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and the BREAK console key (CTRL+BREAK).</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003C96C File Offset: 0x0003AB6C
		// (set) Token: 0x06000E4A RID: 3658 RVA: 0x0003C974 File Offset: 0x0003AB74
		public bool Cancel
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
			}
		}

		/// <summary>Gets the combination of modifier and console keys that interrupted the current process.</summary>
		/// <returns>One of the <see cref="T:System.ConsoleSpecialKey" /> values. There is no default value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000E4B RID: 3659 RVA: 0x0003C980 File Offset: 0x0003AB80
		public ConsoleSpecialKey SpecialKey
		{
			get
			{
				return this.specialKey;
			}
		}

		// Token: 0x040003CC RID: 972
		private bool cancel;

		// Token: 0x040003CD RID: 973
		private ConsoleSpecialKey specialKey;
	}
}
