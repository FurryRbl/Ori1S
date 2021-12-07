using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a local variable and provides access to local variable metadata.</summary>
	// Token: 0x02000295 RID: 661
	[ComVisible(true)]
	public class LocalVariableInfo
	{
		// Token: 0x06002199 RID: 8601 RVA: 0x0007A5D4 File Offset: 0x000787D4
		internal LocalVariableInfo()
		{
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value indicating whether the object referred to by the local variable is pinned in memory.</summary>
		/// <returns>true if the object referred to by the variable is pinned in memory; otherwise, false.</returns>
		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0007A5DC File Offset: 0x000787DC
		public virtual bool IsPinned
		{
			get
			{
				return this.is_pinned;
			}
		}

		/// <summary>Gets the index of the local variable within the method body.</summary>
		/// <returns>An integer value that represents the order of declaration of the local variable within the method body.</returns>
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x0007A5E4 File Offset: 0x000787E4
		public virtual int LocalIndex
		{
			get
			{
				return (int)this.position;
			}
		}

		/// <summary>Gets the type of the local variable.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of the local variable.</returns>
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0007A5EC File Offset: 0x000787EC
		public virtual Type LocalType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns a user-readable string that describes the local variable.</summary>
		/// <returns>A string that displays information about the local variable, including the type name, index, and pinned status.</returns>
		// Token: 0x0600219D RID: 8605 RVA: 0x0007A5F4 File Offset: 0x000787F4
		public override string ToString()
		{
			if (this.is_pinned)
			{
				return string.Format("{0} ({1}) (pinned)", this.type, this.position);
			}
			return string.Format("{0} ({1})", this.type, this.position);
		}

		// Token: 0x04000C7E RID: 3198
		internal Type type;

		// Token: 0x04000C7F RID: 3199
		internal bool is_pinned;

		// Token: 0x04000C80 RID: 3200
		internal ushort position;
	}
}
