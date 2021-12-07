using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a security identifier (SID) and provides marshaling and comparison operations for SIDs.</summary>
	// Token: 0x02000665 RID: 1637
	[ComVisible(false)]
	[MonoTODO("not implemented")]
	public sealed class SecurityIdentifier : IdentityReference, IComparable<SecurityIdentifier>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using the specified security identifier (SID) in Security Descriptor Definition Language (SDDL) format.</summary>
		/// <param name="sddlForm">SDDL string for the SID used to created the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		// Token: 0x06003E3B RID: 15931 RVA: 0x000D5E10 File Offset: 0x000D4010
		public SecurityIdentifier(string sddlForm)
		{
			if (sddlForm == null)
			{
				throw new ArgumentNullException("sddlForm");
			}
			this._value = sddlForm.ToUpperInvariant();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using a specified binary representation of a security identifier (SID).</summary>
		/// <param name="binaryForm">The byte array that represents the SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />. </param>
		// Token: 0x06003E3C RID: 15932 RVA: 0x000D5E38 File Offset: 0x000D4038
		public SecurityIdentifier(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 1)
			{
				throw new ArgumentException("offset");
			}
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using an integer that represents the binary form of a security identifier (SID).</summary>
		/// <param name="binaryForm">An integer that represents the binary form of a SID.</param>
		// Token: 0x06003E3D RID: 15933 RVA: 0x000D5E74 File Offset: 0x000D4074
		public SecurityIdentifier(IntPtr binaryForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class by using the specified well known security identifier (SID) type and domain SID.</summary>
		/// <param name="sidType">A <see cref="T:System.Security.Principal.WellKnownSidType" /> value.This value must not be <see cref="F:System.Security.Principal.WellKnownSidType.WinLogonIdsSid" />.</param>
		/// <param name="domainSid">The domain SID. This value is required for the following <see cref="T:System.Security.Principal.WellKnownSidType" /> values. This parameter is ignored for any other <see cref="T:System.Security.Principal.WellKnownSidType" /> values.- <see cref="F:System.Security.Principal.WellKnownSidType.AccountAdministratorSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountGuestSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountKrbtgtSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainUsersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountDomainGuestsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountComputersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountControllersSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountCertAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountSchemaAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountEnterpriseAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountPolicyAdminsSid" />- <see cref="F:System.Security.Principal.WellKnownSidType.AccountRasAndIasServersSid" /></param>
		// Token: 0x06003E3E RID: 15934 RVA: 0x000D5E84 File Offset: 0x000D4084
		public SecurityIdentifier(WellKnownSidType sidType, SecurityIdentifier domainSid)
		{
			switch (sidType)
			{
			case WellKnownSidType.AccountAdministratorSid:
			case WellKnownSidType.AccountGuestSid:
			case WellKnownSidType.AccountKrbtgtSid:
			case WellKnownSidType.AccountDomainAdminsSid:
			case WellKnownSidType.AccountDomainUsersSid:
			case WellKnownSidType.AccountDomainGuestsSid:
			case WellKnownSidType.AccountComputersSid:
			case WellKnownSidType.AccountControllersSid:
			case WellKnownSidType.AccountCertAdminsSid:
			case WellKnownSidType.AccountSchemaAdminsSid:
			case WellKnownSidType.AccountEnterpriseAdminsSid:
			case WellKnownSidType.AccountPolicyAdminsSid:
			case WellKnownSidType.AccountRasAndIasServersSid:
				if (domainSid == null)
				{
					throw new ArgumentNullException("domainSid");
				}
				break;
			default:
				if (sidType == WellKnownSidType.LogonIdsSid)
				{
					throw new ArgumentException("sidType");
				}
				break;
			}
		}

		/// <summary>Returns the account domain security identifier (SID) portion from the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object if the SID represents a Windows account SID. If the SID does not represent a Windows account SID, this property returns <see cref="T:System.ArgumentNullException" />.</summary>
		/// <returns>The account domain SID portion from the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object if the SID represents a Windows account SID; otherwise, it returns <see cref="T:System.ArgumentNullException" />.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003E40 RID: 15936 RVA: 0x000D5F14 File Offset: 0x000D4114
		public SecurityIdentifier AccountDomainSid
		{
			get
			{
				throw new ArgumentNullException("AccountDomainSid");
			}
		}

		/// <summary>Returns the length, in bytes, of the security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>The length, in bytes, of the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003E41 RID: 15937 RVA: 0x000D5F20 File Offset: 0x000D4120
		public int BinaryLength
		{
			get
			{
				return -1;
			}
		}

		/// <summary>Returns an uppercase Security Descriptor Definition Language (SDDL) string for the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>An uppercase SDDL string for the SID represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06003E42 RID: 15938 RVA: 0x000D5F24 File Offset: 0x000D4124
		public override string Value
		{
			get
			{
				return this._value;
			}
		}

		/// <summary>Compares the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>A signed number indicating the relative values of this instance and <paramref name="sid" />.Return Value Description Less than zero This instance is less than <paramref name="sid" />. Zero This instance is equal to <paramref name="sid" />. Greater than zero This instance is greater than <paramref name="sid" />. </returns>
		/// <param name="sid">The object to compare with the current object.</param>
		// Token: 0x06003E43 RID: 15939 RVA: 0x000D5F2C File Offset: 0x000D412C
		public int CompareTo(SecurityIdentifier sid)
		{
			return this.Value.CompareTo(sid.Value);
		}

		/// <summary>Returns a value that indicates whether this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to a specified object.</summary>
		/// <returns>true if <paramref name="o" /> is an object with the same underlying type and value as this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object; otherwise, false.</returns>
		/// <param name="o">An object to compare with this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object, or null.</param>
		// Token: 0x06003E44 RID: 15940 RVA: 0x000D5F40 File Offset: 0x000D4140
		public override bool Equals(object o)
		{
			return this.Equals(o as SecurityIdentifier);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is equal to the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <returns>true if the value of <paramref name="sid" /> is equal to the value of the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		/// <param name="sid">The object to compare with the current object.</param>
		// Token: 0x06003E45 RID: 15941 RVA: 0x000D5F50 File Offset: 0x000D4150
		public bool Equals(SecurityIdentifier sid)
		{
			return !(sid == null) && sid.Value == this.Value;
		}

		/// <summary>Copies the binary representation of the specified security identifier (SID) represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class to a byte array.</summary>
		/// <param name="binaryForm">The byte array to receive the copied SID.</param>
		/// <param name="offset">The byte offset to use as the starting index in <paramref name="binaryForm" />. </param>
		// Token: 0x06003E46 RID: 15942 RVA: 0x000D5F7C File Offset: 0x000D417C
		public void GetBinaryForm(byte[] binaryForm, int offset)
		{
			if (binaryForm == null)
			{
				throw new ArgumentNullException("binaryForm");
			}
			if (offset < 0 || offset > binaryForm.Length - 1 - this.BinaryLength)
			{
				throw new ArgumentException("offset");
			}
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. The <see cref="M:System.Security.Principal.SecurityIdentifier.GetHashCode" /> method is suitable for hashing algorithms and data structures like a hash table.</summary>
		/// <returns>A hash value for the current <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06003E47 RID: 15943 RVA: 0x000D5FB4 File Offset: 0x000D41B4
		public override int GetHashCode()
		{
			return this.Value.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is a valid Windows account SID.</summary>
		/// <returns>true if the SID represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is a valid Windows account SID; otherwise, false.</returns>
		// Token: 0x06003E48 RID: 15944 RVA: 0x000D5FC4 File Offset: 0x000D41C4
		public bool IsAccountSid()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value that indicates whether the security identifier (SID) represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is from the same domain as the specified SID.</summary>
		/// <returns>true if the SID represented by this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object is in the same domain as the <paramref name="sid" /> SID; otherwise, false.</returns>
		/// <param name="sid">The SID to compare with this <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		// Token: 0x06003E49 RID: 15945 RVA: 0x000D5FCC File Offset: 0x000D41CC
		public bool IsEqualDomainSid(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a value that indicates whether the specified type is a valid translation type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class.</summary>
		/// <returns>true if <paramref name="targetType" /> is a valid translation type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> class; otherwise, false.</returns>
		/// <param name="targetType">The type being queried for validity to serve as a conversion from <see cref="T:System.Security.Principal.SecurityIdentifier" />. The following target types are valid:- <see cref="T:System.Security.Principal.NTAccount" />- <see cref="T:System.Security.Principal.SecurityIdentifier" /></param>
		// Token: 0x06003E4A RID: 15946 RVA: 0x000D5FD4 File Offset: 0x000D41D4
		public override bool IsValidTargetType(Type targetType)
		{
			return targetType == typeof(SecurityIdentifier) || targetType == typeof(NTAccount);
		}

		/// <summary>Returns a value that indicates whether the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object matches the specified well known security identifier (SID) type. </summary>
		/// <returns>true if <paramref name="type" /> is the SID type for the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Security.Principal.WellKnownSidType" /> value to compare with the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</param>
		// Token: 0x06003E4B RID: 15947 RVA: 0x000D5FFC File Offset: 0x000D41FC
		public bool IsWellKnown(WellKnownSidType type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the security identifier (SID), in Security Descriptor Definition Language (SDDL) format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object. An example of the SDDL format is S-1-5-9. </summary>
		/// <returns>The SID, in SDDL format, for the account represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</returns>
		// Token: 0x06003E4C RID: 15948 RVA: 0x000D6004 File Offset: 0x000D4204
		public override string ToString()
		{
			return this.Value;
		}

		/// <summary>Translates the account name represented by the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object into another <see cref="T:System.Security.Principal.IdentityReference" />-derived type.</summary>
		/// <returns>The converted identity.</returns>
		/// <param name="targetType">The target type for the conversion from <see cref="T:System.Security.Principal.SecurityIdentifier" />. The target type must be a type that is considered valid by the <see cref="M:System.Security.Principal.SecurityIdentifier.IsValidTargetType(System.Type)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="targetType " />is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="targetType " />is not an <see cref="T:System.Security.Principal.IdentityReference" /> type.</exception>
		/// <exception cref="T:System.Security.Principal.IdentityNotMappedException">Some or all identity references could not be translated.</exception>
		/// <exception cref="T:System.SystemException">A Win32 error code was returned.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E4D RID: 15949 RVA: 0x000D600C File Offset: 0x000D420C
		public override IdentityReference Translate(Type targetType)
		{
			if (targetType == typeof(SecurityIdentifier))
			{
				return this;
			}
			return null;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are equal. They are considered equal if they have the same canonical representation as the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if they are both null. </summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The left operand to use for the equality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the equality comparison. This parameter can be null.</param>
		// Token: 0x06003E4E RID: 15950 RVA: 0x000D6024 File Offset: 0x000D4224
		public static bool operator ==(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right == null;
			}
			return right != null && left.Value == right.Value;
		}

		/// <summary>Compares two <see cref="T:System.Security.Principal.SecurityIdentifier" /> objects to determine whether they are not equal. They are considered not equal if they have different canonical name representations than the one returned by the <see cref="P:System.Security.Principal.SecurityIdentifier.Value" /> property or if one of the objects is null and the other is not.</summary>
		/// <returns>true if <paramref name="left" /> and <paramref name="right" /> are not equal; otherwise, false.</returns>
		/// <param name="left">The left operand to use for the inequality comparison. This parameter can be null.</param>
		/// <param name="right">The right operand to use for the inequality comparison. This parameter can be null.</param>
		// Token: 0x06003E4F RID: 15951 RVA: 0x000D6058 File Offset: 0x000D4258
		public static bool operator !=(SecurityIdentifier left, SecurityIdentifier right)
		{
			if (left == null)
			{
				return right != null;
			}
			return right == null || left.Value != right.Value;
		}

		// Token: 0x04001ABA RID: 6842
		private string _value;

		/// <summary>Returns the maximum size, in bytes, of the binary representation of the security identifier.</summary>
		/// <returns>The maximum size, in bytes, of the binary representation of the security identifier.</returns>
		// Token: 0x04001ABB RID: 6843
		public static readonly int MaxBinaryLength;

		/// <summary>Returns the minimum size, in bytes, of the binary representation of the security identifier.</summary>
		/// <returns>The minimum size, in bytes, of the binary representation of the security identifier.</returns>
		// Token: 0x04001ABC RID: 6844
		public static readonly int MinBinaryLength;
	}
}
