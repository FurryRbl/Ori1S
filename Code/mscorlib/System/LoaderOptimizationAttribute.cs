using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Used to set the default loader optimization policy for the main method of an executable application.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014B RID: 331
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(true)]
	public sealed class LoaderOptimizationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A value equivalent to a <see cref="T:System.LoaderOptimization" /> constant. </param>
		// Token: 0x060011CA RID: 4554 RVA: 0x0004721C File Offset: 0x0004541C
		public LoaderOptimizationAttribute(byte value)
		{
			this.lo = (LoaderOptimization)value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.LoaderOptimizationAttribute" /> class to the specified value.</summary>
		/// <param name="value">A <see cref="T:System.LoaderOptimization" /> constant. </param>
		// Token: 0x060011CB RID: 4555 RVA: 0x0004722C File Offset: 0x0004542C
		public LoaderOptimizationAttribute(LoaderOptimization value)
		{
			this.lo = value;
		}

		/// <summary>Gets the current <see cref="T:System.LoaderOptimization" /> value for this instance.</summary>
		/// <returns>A <see cref="T:System.LoaderOptimization" /> constant.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0004723C File Offset: 0x0004543C
		public LoaderOptimization Value
		{
			get
			{
				return this.lo;
			}
		}

		// Token: 0x04000525 RID: 1317
		private LoaderOptimization lo;
	}
}
