using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an object. This class cannot be inherited.</summary>
	// Token: 0x02000043 RID: 67
	public sealed class DefaultValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether an object can be validated, based on type.</summary>
		/// <returns>true for all types being validated. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x06000283 RID: 643 RVA: 0x00008104 File Offset: 0x00006304
		public override bool CanValidate(Type type)
		{
			return true;
		}

		/// <summary>Determines whether the value of an object is valid. </summary>
		/// <param name="value">The object value.</param>
		// Token: 0x06000284 RID: 644 RVA: 0x00008108 File Offset: 0x00006308
		public override void Validate(object value)
		{
		}
	}
}
