using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x020001A6 RID: 422
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class. </summary>
		/// <param name="bindable">true to specify that a property is appropriate to bind settings to; otherwise, false.</param>
		// Token: 0x06000ECC RID: 3788 RVA: 0x000265C8 File Offset: 0x000247C8
		public SettingsBindableAttribute(bool bindable)
		{
			this.bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to. </summary>
		/// <returns>true if the property is appropriate to bind settings to; otherwise, false.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x000265F0 File Offset: 0x000247F0
		public bool Bindable
		{
			get
			{
				return this.bindable;
			}
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000ECF RID: 3791 RVA: 0x000265F8 File Offset: 0x000247F8
		public override int GetHashCode()
		{
			return (!this.bindable) ? -1 : 1;
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> objects are equal.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">The value to compare to.</param>
		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002660C File Offset: 0x0002480C
		public override bool Equals(object obj)
		{
			SettingsBindableAttribute settingsBindableAttribute = obj as SettingsBindableAttribute;
			return settingsBindableAttribute != null && this.bindable == settingsBindableAttribute.bindable;
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x04000433 RID: 1075
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x04000434 RID: 1076
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);

		// Token: 0x04000435 RID: 1077
		private bool bindable;
	}
}
