using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class PositiveTimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets an instance of the <see cref="T:System.Configuration.PositiveTimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance. </returns>
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000341 RID: 833 RVA: 0x000094BC File Offset: 0x000076BC
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new PositiveTimeSpanValidator();
				}
				return this.instance;
			}
		}

		// Token: 0x040000FF RID: 255
		private ConfigurationValidatorBase instance;
	}
}
