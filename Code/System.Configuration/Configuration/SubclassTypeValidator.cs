using System;

namespace System.Configuration
{
	/// <summary>Validates that an object is a derived class of a specified type.</summary>
	// Token: 0x02000078 RID: 120
	public sealed class SubclassTypeValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.SubclassTypeValidator" /> class. </summary>
		/// <param name="baseClass">The base class to validate against.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="baseClass" /> is null.</exception>
		// Token: 0x06000413 RID: 1043 RVA: 0x0000B944 File Offset: 0x00009B44
		public SubclassTypeValidator(Type baseClass)
		{
			this.baseClass = baseClass;
		}

		/// <summary>Determines whether an object can be validated based on type.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a type that can be validated; otherwise, false. </returns>
		/// <param name="type">The object type.</param>
		// Token: 0x06000414 RID: 1044 RVA: 0x0000B954 File Offset: 0x00009B54
		public override bool CanValidate(Type type)
		{
			return type == typeof(Type);
		}

		/// <summary>Determines whether the value of an object is valid. </summary>
		/// <param name="value">The object value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="value" /> is not of a <see cref="T:System.Type" /> that can be derived from <paramref name="baseClass" /> as defined in the constructor.</exception>
		// Token: 0x06000415 RID: 1045 RVA: 0x0000B964 File Offset: 0x00009B64
		public override void Validate(object value)
		{
			Type c = (Type)value;
			if (!this.baseClass.IsAssignableFrom(c))
			{
				throw new ArgumentException("The value must be a subclass");
			}
		}

		// Token: 0x04000146 RID: 326
		private Type baseClass;
	}
}
