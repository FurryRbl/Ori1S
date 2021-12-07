using System;

namespace System.Configuration
{
	/// <summary>Provides dynamic validation of an object.</summary>
	// Token: 0x02000015 RID: 21
	public sealed class CallbackValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.CallbackValidator" /> class.</summary>
		/// <param name="type">The type of object that will be validated.</param>
		/// <param name="callback">The <see cref="T:System.Configuration.ValidatorCallback" /> used as the delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type " />is null.</exception>
		// Token: 0x060000AC RID: 172 RVA: 0x000029D4 File Offset: 0x00000BD4
		public CallbackValidator(Type type, ValidatorCallback callback)
		{
			this.type = type;
			this.callback = callback;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the type parameter matches the type used as the first parameter when creating an instance of <see cref="T:System.Configuration.CallbackValidator" />; otherwise, false. </returns>
		/// <param name="type">The type of object.</param>
		// Token: 0x060000AD RID: 173 RVA: 0x000029EC File Offset: 0x00000BEC
		public override bool CanValidate(Type type)
		{
			return type == this.type;
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x060000AE RID: 174 RVA: 0x000029F8 File Offset: 0x00000BF8
		public override void Validate(object value)
		{
			this.callback(value);
		}

		// Token: 0x04000029 RID: 41
		private Type type;

		// Token: 0x0400002A RID: 42
		private ValidatorCallback callback;
	}
}
