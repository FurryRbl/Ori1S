using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Principal.IdentityReference" /> objects and provides a means of converting sets of <see cref="T:System.Security.Principal.IdentityReference" />-derived objects to <see cref="T:System.Security.Principal.IdentityReference" />-derived types. </summary>
	// Token: 0x0200065F RID: 1631
	[ComVisible(false)]
	public class IdentityReferenceCollection : IEnumerable, ICollection<IdentityReference>, IEnumerable<IdentityReference>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class with zero items in the collection.</summary>
		// Token: 0x06003E14 RID: 15892 RVA: 0x000D5A08 File Offset: 0x000D3C08
		public IdentityReferenceCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> class by using the specified initial size.</summary>
		/// <param name="capacity">The initial number of items in the collection. The value of <paramref name="capacity" /> is a hint only; it is not necessarily the maximum number of items created.</param>
		// Token: 0x06003E15 RID: 15893 RVA: 0x000D5A1C File Offset: 0x000D3C1C
		public IdentityReferenceCollection(int capacity)
		{
			this._list = new ArrayList(capacity);
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06003E16 RID: 15894 RVA: 0x000D5A30 File Offset: 0x000D3C30
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of items in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>The number of <see cref="T:System.Security.Principal.IdentityReference" /> objects in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06003E17 RID: 15895 RVA: 0x000D5A38 File Offset: 0x000D3C38
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is read-only.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003E18 RID: 15896 RVA: 0x000D5A48 File Offset: 0x000D3C48
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Sets or gets the node at the specified index of the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.IdentityReference" /> at the specified index in the collection. If <paramref name="index" /> is greater than or equal to the number of nodes in the collection, the return value is null.</returns>
		/// <param name="index">The zero-based index in the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</param>
		// Token: 0x17000BBA RID: 3002
		public IdentityReference this[int index]
		{
			get
			{
				if (index >= this._list.Count)
				{
					return null;
				}
				return (IdentityReference)this._list[index];
			}
			set
			{
				this._list[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Principal.IdentityReference" /> object to the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to add to the collection.</param>
		// Token: 0x06003E1B RID: 15899 RVA: 0x000D5A90 File Offset: 0x000D3C90
		public void Add(IdentityReference identity)
		{
			this._list.Add(identity);
		}

		/// <summary>Clears all <see cref="T:System.Security.Principal.IdentityReference" /> objects from the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		// Token: 0x06003E1C RID: 15900 RVA: 0x000D5AA0 File Offset: 0x000D3CA0
		public void Clear()
		{
			this._list.Clear();
		}

		/// <summary>Indicates whether the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection contains the specified <see cref="T:System.Security.Principal.IdentityReference" /> object.</summary>
		/// <returns>true if the collection contains the specified object.</returns>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to check for.</param>
		// Token: 0x06003E1D RID: 15901 RVA: 0x000D5AB0 File Offset: 0x000D3CB0
		public bool Contains(IdentityReference identity)
		{
			foreach (object obj in this._list)
			{
				IdentityReference identityReference = (IdentityReference)obj;
				if (identityReference.Equals(identity))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection to an <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array, starting at the specified index.</summary>
		/// <param name="array">An <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> array object to which the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> where the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection is to be copied.</param>
		// Token: 0x06003E1E RID: 15902 RVA: 0x000D5B30 File Offset: 0x000D3D30
		public void CopyTo(IdentityReference[] array, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can be used to iterate through the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</summary>
		/// <returns>An enumerator for the <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection.</returns>
		// Token: 0x06003E1F RID: 15903 RVA: 0x000D5B38 File Offset: 0x000D3D38
		public IEnumerator<IdentityReference> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes the specified <see cref="T:System.Security.Principal.IdentityReference" /> object from the collection.</summary>
		/// <returns>true if the specified object was removed from the collection.</returns>
		/// <param name="identity">The <see cref="T:System.Security.Principal.IdentityReference" /> object to remove.</param>
		// Token: 0x06003E20 RID: 15904 RVA: 0x000D5B40 File Offset: 0x000D3D40
		public bool Remove(IdentityReference identity)
		{
			foreach (object obj in this._list)
			{
				IdentityReference identityReference = (IdentityReference)obj;
				if (identityReference.Equals(identity))
				{
					this._list.Remove(identityReference);
					return true;
				}
			}
			return false;
		}

		/// <summary>Converts the objects in the collection to the specified type. Calling this method is the same as calling <see cref="M:System.Security.Principal.IdentityReferenceCollection.Translate(System.Type,System.Boolean)" /> with the second parameter set to false, which means that exceptions will not be thrown for items that fail conversion.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E21 RID: 15905 RVA: 0x000D5BCC File Offset: 0x000D3DCC
		public IdentityReferenceCollection Translate(Type targetType)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts the objects in the collection to the specified type and uses the specified fault tolerance to handle or ignore errors associated with a type not having a conversion mapping.</summary>
		/// <returns>A <see cref="T:System.Security.Principal.IdentityReferenceCollection" /> collection that represents the converted contents of the original collection.</returns>
		/// <param name="targetType">The type to which items in the collection are being converted.</param>
		/// <param name="forceSuccess">A Boolean value that determines how conversion errors are handled.If <paramref name="forceSuccess" /> is true, conversion errors due to a mapping not being found for the translation result in a failed conversion and exceptions being thrown.If <paramref name="forceSuccess" /> is false, types that failed to convert due to a mapping not being found for the translation are copied without being converted into the collection being returned.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06003E22 RID: 15906 RVA: 0x000D5BD4 File Offset: 0x000D3DD4
		public IdentityReferenceCollection Translate(Type targetType, bool forceSuccess)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001AB4 RID: 6836
		private ArrayList _list;
	}
}
