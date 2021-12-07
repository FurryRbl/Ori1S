using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x0200015C RID: 348
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">true if the object is immutable; otherwise, false. </param>
		// Token: 0x06000C92 RID: 3218 RVA: 0x000201C0 File Offset: 0x0001E3C0
		public ImmutableObjectAttribute(bool immutable)
		{
			this.immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>true if the object is immutable; otherwise, false.</returns>
		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x000201F4 File Offset: 0x0001E3F4
		public bool Immutable
		{
			get
			{
				return this.immutable;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000C95 RID: 3221 RVA: 0x000201FC File Offset: 0x0001E3FC
		public override bool Equals(object obj)
		{
			return obj is ImmutableObjectAttribute && (obj == this || ((ImmutableObjectAttribute)obj).Immutable == this.immutable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06000C96 RID: 3222 RVA: 0x00020228 File Offset: 0x0001E428
		public override int GetHashCode()
		{
			return this.immutable.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000C97 RID: 3223 RVA: 0x00020238 File Offset: 0x0001E438
		public override bool IsDefaultAttribute()
		{
			return this.immutable == ImmutableObjectAttribute.Default.Immutable;
		}

		// Token: 0x04000373 RID: 883
		private bool immutable;

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x04000374 RID: 884
		public static readonly ImmutableObjectAttribute Default = new ImmutableObjectAttribute(false);

		/// <summary>Specifies that an object has at least one editable subproperty. This static field is read-only.</summary>
		// Token: 0x04000375 RID: 885
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Specifies that an object has no subproperties that can be edited. This static field is read-only.</summary>
		// Token: 0x04000376 RID: 886
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);
	}
}
