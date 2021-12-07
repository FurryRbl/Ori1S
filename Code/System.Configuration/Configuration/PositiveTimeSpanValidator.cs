using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object. This class cannot be inherited.</summary>
	// Token: 0x0200005C RID: 92
	public class PositiveTimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Determines whether the object type can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> object; otherwise, false. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x0600033E RID: 830 RVA: 0x00009470 File Offset: 0x00007670
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> cannot be resolved as a positive <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x0600033F RID: 831 RVA: 0x00009480 File Offset: 0x00007680
		public override void Validate(object value)
		{
			TimeSpan t = (TimeSpan)value;
			if (t <= new TimeSpan(0L))
			{
				throw new ArgumentException("The time span value must be positive");
			}
		}
	}
}
