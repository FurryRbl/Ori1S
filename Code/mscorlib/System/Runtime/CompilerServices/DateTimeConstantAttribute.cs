using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Persists an 8-byte <see cref="T:System.DateTime" /> constant for a field or parameter.</summary>
	// Token: 0x0200032A RID: 810
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter, Inherited = false)]
	[Serializable]
	public sealed class DateTimeConstantAttribute : CustomConstantAttribute
	{
		/// <summary>Initializes a new instance of the DateTimeConstantAttribute class with the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <param name="ticks">The number of 100-nanosecond ticks that represent the date and time of this instance. </param>
		// Token: 0x0600289F RID: 10399 RVA: 0x00091D60 File Offset: 0x0008FF60
		public DateTimeConstantAttribute(long ticks)
		{
			this.ticks = ticks;
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x00091D70 File Offset: 0x0008FF70
		internal long Ticks
		{
			get
			{
				return this.ticks;
			}
		}

		/// <summary>Gets the number of 100-nanosecond ticks that represent the date and time of this instance.</summary>
		/// <returns>The number of 100-nanosecond ticks that represent the date and time of this instance.</returns>
		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00091D78 File Offset: 0x0008FF78
		public override object Value
		{
			get
			{
				return this.ticks;
			}
		}

		// Token: 0x04001087 RID: 4231
		private long ticks;
	}
}
