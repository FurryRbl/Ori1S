using System;

namespace System.Configuration
{
	/// <summary>Acts as a base class for deriving a validation class so that a value of an object can be verified.</summary>
	// Token: 0x0200003C RID: 60
	public abstract class ConfigurationValidatorBase
	{
		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <returns>true if the <paramref name="type" /> parameter value matches the expected type; otherwise, false. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x06000252 RID: 594 RVA: 0x00007C74 File Offset: 0x00005E74
		public virtual bool CanValidate(Type type)
		{
			return false;
		}

		/// <summary>Determines whether the value of an object is valid. </summary>
		/// <param name="value">The object value.</param>
		// Token: 0x06000253 RID: 595
		public abstract void Validate(object value);
	}
}
