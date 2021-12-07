using System;

namespace System.Diagnostics
{
	/// <summary>Identifies the level type for a switch. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000252 RID: 594
	[MonoLimitation("This attribute is not considered in trace support.")]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class SwitchLevelAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SwitchLevelAttribute" /> class, specifying the type that determines whether a trace should be written.</summary>
		/// <param name="switchLevelType">The <see cref="T:System.Type" /> that determines whether a trace should be written.</param>
		// Token: 0x060014E2 RID: 5346 RVA: 0x000374A8 File Offset: 0x000356A8
		public SwitchLevelAttribute(Type switchLevelType)
		{
			if (switchLevelType == null)
			{
				throw new ArgumentNullException("switchLevelType");
			}
			this.type = switchLevelType;
		}

		/// <summary>Gets or sets the type that determines whether a trace should be written.</summary>
		/// <returns>The <see cref="T:System.Type" /> that determines whether a trace should be written.</returns>
		/// <exception cref="T:System.ArgumentNullException">The set operation failed because the value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x060014E3 RID: 5347 RVA: 0x000374C8 File Offset: 0x000356C8
		// (set) Token: 0x060014E4 RID: 5348 RVA: 0x000374D0 File Offset: 0x000356D0
		public Type SwitchLevelType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.type = value;
			}
		}

		// Token: 0x04000657 RID: 1623
		private Type type;
	}
}
