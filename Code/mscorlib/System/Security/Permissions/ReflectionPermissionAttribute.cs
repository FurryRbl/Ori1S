using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.ReflectionPermission" /> to be applied to code using declarative security. </summary>
	// Token: 0x02000615 RID: 1557
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.ReflectionPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003B4A RID: 15178 RVA: 0x000CB7DC File Offset: 0x000C99DC
		public ReflectionPermissionAttribute(SecurityAction action) : base(action)
		{
		}

		/// <summary>Gets or sets the current allowed uses of reflection.</summary>
		/// <returns>One or more of the <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> values combined using a bitwise OR.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt is made to set this property to an invalid value. See <see cref="T:System.Security.Permissions.ReflectionPermissionFlag" /> for the valid values. </exception>
		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000CB7E8 File Offset: 0x000C99E8
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000CB7F0 File Offset: 0x000C99F0
		public ReflectionPermissionFlag Flags
		{
			get
			{
				return this.flags;
			}
			set
			{
				this.flags = value;
				this.memberAccess = ((this.flags & ReflectionPermissionFlag.MemberAccess) == ReflectionPermissionFlag.MemberAccess);
				this.reflectionEmit = ((this.flags & ReflectionPermissionFlag.ReflectionEmit) == ReflectionPermissionFlag.ReflectionEmit);
				this.typeInfo = ((this.flags & ReflectionPermissionFlag.TypeInformation) == ReflectionPermissionFlag.TypeInformation);
			}
		}

		/// <summary>Gets or sets a value that indicates whether invocation of operations on non-public members is allowed.</summary>
		/// <returns>true if invocation of operations on non-public members is allowed; otherwise, false.</returns>
		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000CB838 File Offset: 0x000C9A38
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x000CB840 File Offset: 0x000C9A40
		public bool MemberAccess
		{
			get
			{
				return this.memberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.MemberAccess;
				}
				else
				{
					this.flags -= 2;
				}
				this.memberAccess = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether use of certain features in <see cref="N:System.Reflection.Emit" />, such as emitting debug symbols, is allowed.</summary>
		/// <returns>true if use of the affected features is allowed; otherwise, false.</returns>
		// Token: 0x17000B31 RID: 2865
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000CB87C File Offset: 0x000C9A7C
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x000CB884 File Offset: 0x000C9A84
		public bool ReflectionEmit
		{
			get
			{
				return this.reflectionEmit;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.ReflectionEmit;
				}
				else
				{
					this.flags -= 4;
				}
				this.reflectionEmit = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether restricted invocation of non-public members is allowed. Restricted invocation means that the grant set of the assembly that contains the non-public member that is being invoked must be equal to, or a subset of, the grant set of the invoking assembly. </summary>
		/// <returns>true if restricted invocation of non-public members is allowed; otherwise, false.</returns>
		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000CB8C0 File Offset: 0x000C9AC0
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x000CB8D0 File Offset: 0x000C9AD0
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.flags & ReflectionPermissionFlag.RestrictedMemberAccess) == ReflectionPermissionFlag.RestrictedMemberAccess;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.RestrictedMemberAccess;
				}
				else
				{
					this.flags -= 8;
				}
			}
		}

		/// <summary>Gets or sets a value that indicates whether reflection on members that are not visible is allowed.</summary>
		/// <returns>true if reflection on members that are not visible is allowed; otherwise, false.</returns>
		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000CB8FC File Offset: 0x000C9AFC
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x000CB904 File Offset: 0x000C9B04
		[Obsolete("not enforced in 2.0+")]
		public bool TypeInformation
		{
			get
			{
				return this.typeInfo;
			}
			set
			{
				if (value)
				{
					this.flags |= ReflectionPermissionFlag.TypeInformation;
				}
				else
				{
					this.flags--;
				}
				this.typeInfo = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.ReflectionPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.ReflectionPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003B55 RID: 15189 RVA: 0x000CB940 File Offset: 0x000C9B40
		public override IPermission CreatePermission()
		{
			ReflectionPermission result;
			if (base.Unrestricted)
			{
				result = new ReflectionPermission(PermissionState.Unrestricted);
			}
			else
			{
				result = new ReflectionPermission(this.flags);
			}
			return result;
		}

		// Token: 0x040019C6 RID: 6598
		private ReflectionPermissionFlag flags;

		// Token: 0x040019C7 RID: 6599
		private bool memberAccess;

		// Token: 0x040019C8 RID: 6600
		private bool reflectionEmit;

		// Token: 0x040019C9 RID: 6601
		private bool typeInfo;
	}
}
