﻿using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> object also contains information about the type of object to which the rule applies, the type of child object that can inherit the rule, how the rule is inherited by child objects, and how that inheritance is propagated.</summary>
	// Token: 0x02000583 RID: 1411
	public abstract class ObjectAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.ObjectAccessRule" /> class with the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.  It must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="accessMask">The access mask of this rule. The access mask is a 32-bit collection of anonymous bits, the meaning of which is defined by the individual integrators.</param>
		/// <param name="isInherited">true if this rule is inherited from a parent container.</param>
		/// <param name="inheritanceFlags">Specifies the inheritance properties of the access rule.</param>
		/// <param name="propagationFlags">Specifies whether inherited access rules are automatically propagated. The propagation flags are ignored if <paramref name="inheritanceFlags" /> is set to <see cref="F:System.Security.AccessControl.InheritanceFlags.None" />.</param>
		/// <param name="objectType">The type of object to which the rule applies.</param>
		/// <param name="inheritedObjectType">The type of child object that can inherit the rule.</param>
		/// <param name="type">Specifies whether this rule allows or denies access.</param>
		/// <exception cref="T:System.ArgumentException">The value of the <paramref name="identity" /> parameter cannot be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />, or the <paramref name="type" /> parameter contains an invalid value.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="accessMask" /> parameter is 0, or the <paramref name="inheritanceFlags" /> or <paramref name="propagationFlags" /> parameters contain unrecognized flag values.</exception>
		// Token: 0x060036A8 RID: 13992 RVA: 0x000B231C File Offset: 0x000B051C
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type) : base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
			this.object_type = objectType;
			this.inherited_object_type = inheritedObjectType;
		}

		/// <summary>Gets the type of child object that can inherit the <see cref="System.Security.AccessControl.ObjectAccessRule" /> object.</summary>
		/// <returns>The type of child object that can inherit the <see cref="System.Security.AccessControl.ObjectAccessRule" /> object.</returns>
		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x000B2340 File Offset: 0x000B0540
		public Guid InheritedObjectType
		{
			get
			{
				return this.inherited_object_type;
			}
		}

		/// <summary>Gets flags that specify if the <see cref="P:System.Security.AccessControl.ObjectAccessRule.ObjectType" /> and <see cref="P:System.Security.AccessControl.ObjectAccessRule.InheritedObjectType" /> properties of the <see cref="System.Security.AccessControl.ObjectAccessRule" /> object contain valid values.</summary>
		/// <returns>
		///   <see cref="F:System.Security.AccessControl.ObjectAceFlags.ObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAccessRule.ObjectType" /> property contains a valid value. <see cref="F:System.Security.AccessControl.ObjectAceFlags.InheritedObjectAceTypePresent" /> specifies that the <see cref="P:System.Security.AccessControl.ObjectAccessRule.InheritedObjectType" /> property contains a valid value. These values can be combined with a logical OR.</returns>
		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060036AA RID: 13994 RVA: 0x000B2348 File Offset: 0x000B0548
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				ObjectAceFlags objectAceFlags = ObjectAceFlags.None;
				if (this.object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.ObjectAceTypePresent;
				}
				if (this.inherited_object_type != Guid.Empty)
				{
					objectAceFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				}
				return objectAceFlags;
			}
		}

		/// <summary>Gets the type of object to which the <see cref="System.Security.AccessControl.ObjectAccessRule" /> applies.</summary>
		/// <returns>The type of object to which the <see cref="System.Security.AccessControl.ObjectAccessRule" /> applies.</returns>
		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x000B238C File Offset: 0x000B058C
		public Guid ObjectType
		{
			get
			{
				return this.object_type;
			}
		}

		// Token: 0x04001730 RID: 5936
		private Guid object_type;

		// Token: 0x04001731 RID: 5937
		private Guid inherited_object_type;
	}
}
