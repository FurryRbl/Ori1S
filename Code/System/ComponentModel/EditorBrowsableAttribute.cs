using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a property or method is viewable in an editor. This class cannot be inherited.</summary>
	// Token: 0x02000143 RID: 323
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> set to the default state.</summary>
		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001F158 File Offset: 0x0001D358
		public EditorBrowsableAttribute()
		{
			this.state = EditorBrowsableState.Always;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with an <see cref="T:System.ComponentModel.EditorBrowsableState" />.</summary>
		/// <param name="state">The <see cref="T:System.ComponentModel.EditorBrowsableState" /> to set <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> to. </param>
		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F168 File Offset: 0x0001D368
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.state = state;
		}

		/// <summary>Gets the browsable state of the property or method.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EditorBrowsableState" /> that is the browsable state of the property or method.</returns>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BE9 RID: 3049 RVA: 0x0001F178 File Offset: 0x0001D378
		public EditorBrowsableState State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorBrowsableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000BEA RID: 3050 RVA: 0x0001F180 File Offset: 0x0001D380
		public override bool Equals(object obj)
		{
			return obj is EditorBrowsableAttribute && (obj == this || ((EditorBrowsableAttribute)obj).State == this.state);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001F1AC File Offset: 0x0001D3AC
		public override int GetHashCode()
		{
			return this.state.GetHashCode();
		}

		// Token: 0x04000360 RID: 864
		private EditorBrowsableState state;
	}
}
