using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	/// <summary>Represents the collection used to store Uniform Resource Identifier (URI) prefixes for <see cref="T:System.Net.HttpListener" /> objects.</summary>
	// Token: 0x02000318 RID: 792
	public class HttpListenerPrefixCollection : IEnumerable, ICollection<string>, IEnumerable<string>
	{
		// Token: 0x06001B9A RID: 7066 RVA: 0x0004EE54 File Offset: 0x0004D054
		internal HttpListenerPrefixCollection(HttpListener listener)
		{
			this.listener = listener;
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06001B9B RID: 7067 RVA: 0x0004EE70 File Offset: 0x0004D070
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.prefixes.GetEnumerator();
		}

		/// <summary>Gets the number of prefixes contained in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> that contains the number of prefixes in this collection. </returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001B9C RID: 7068 RVA: 0x0004EE84 File Offset: 0x0004D084
		public int Count
		{
			get
			{
				return this.prefixes.Count;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is read-only.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001B9D RID: 7069 RVA: 0x0004EE94 File Offset: 0x0004D094
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether access to the collection is synchronized (thread-safe).</summary>
		/// <returns>This property always returns false.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0004EE98 File Offset: 0x0004D098
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Adds a Uniform Resource Identifier (URI) prefix to the collection.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that identifies the URI information that is compared in incoming requests. The prefix must be terminated with a forward slash ("/").</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uriPrefix" /> does not use the http:// or https:// scheme. These are the only schemes supported for <see cref="T:System.Net.HttpListener" /> objects. -or-<paramref name="uriPrefix" /> is not a correctly formatted URI prefix. Make sure the string is terminated with a "/".</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception. This exception is thrown if another <see cref="T:System.Net.HttpListener" /> has already added the prefix <paramref name="uriPrefix" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001B9F RID: 7071 RVA: 0x0004EE9C File Offset: 0x0004D09C
		public void Add(string uriPrefix)
		{
			this.listener.CheckDisposed();
			ListenerPrefix.CheckUri(uriPrefix);
			if (this.prefixes.Contains(uriPrefix))
			{
				return;
			}
			this.prefixes.Add(uriPrefix);
			if (this.listener.IsListening)
			{
				EndPointManager.AddPrefix(uriPrefix, this.listener);
			}
		}

		/// <summary>Removes all the Uniform Resource Identifier (URI) prefixes from the collection.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001BA0 RID: 7072 RVA: 0x0004EEF4 File Offset: 0x0004D0F4
		public void Clear()
		{
			this.listener.CheckDisposed();
			this.prefixes.Clear();
			if (this.listener.IsListening)
			{
				EndPointManager.RemoveListener(this.listener);
			}
		}

		/// <summary>Returns a <see cref="T:System.Boolean" /> value that indicates whether the specified prefix is contained in the collection.</summary>
		/// <returns>true if this collection contains the prefix specified by <paramref name="uriPrefix" />; otherwise, false.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the Uniform Resource Identifier (URI) prefix to test.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		// Token: 0x06001BA1 RID: 7073 RVA: 0x0004EF28 File Offset: 0x0004D128
		public bool Contains(string uriPrefix)
		{
			this.listener.CheckDisposed();
			return this.prefixes.Contains(uriPrefix);
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified string array. </summary>
		/// <param name="array">The one dimensional string array that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		// Token: 0x06001BA2 RID: 7074 RVA: 0x0004EF44 File Offset: 0x0004D144
		public void CopyTo(string[] array, int offset)
		{
			this.listener.CheckDisposed();
			this.prefixes.CopyTo(array, offset);
		}

		/// <summary>Copies the contents of an <see cref="T:System.Net.HttpListenerPrefixCollection" /> to the specified array. </summary>
		/// <param name="array">The one dimensional <see cref="T:System.Array" /> that receives the Uniform Resource Identifier (URI) prefix strings in this collection.</param>
		/// <param name="offset">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> has more than one dimension.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">This collection contains more elements than can be stored in <paramref name="array" /> starting at <paramref name="offset" />.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="array" /> cannot store string values.</exception>
		// Token: 0x06001BA3 RID: 7075 RVA: 0x0004EF60 File Offset: 0x0004D160
		public void CopyTo(Array array, int offset)
		{
			this.listener.CheckDisposed();
			((ICollection)this.prefixes).CopyTo(array, offset);
		}

		/// <summary>Returns an object that can be used to iterate through the collection.</summary>
		/// <returns>An object that implements the <see cref="T:System.Collections.IEnumerator" /> interface and provides access to the strings in this collection.</returns>
		// Token: 0x06001BA4 RID: 7076 RVA: 0x0004EF7C File Offset: 0x0004D17C
		public IEnumerator<string> GetEnumerator()
		{
			return this.prefixes.GetEnumerator();
		}

		/// <summary>Removes the specified Uniform Resource Identifier (URI) from the list of prefixes handled by the <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>true if the <paramref name="uriPrefix" /> was found in the <see cref="T:System.Net.HttpListenerPrefixCollection" /> and removed; otherwise false.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.String" /> that contains the URI prefix to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null.</exception>
		/// <exception cref="T:System.Net.HttpListenerException">A Windows function call failed. To determine the cause of the exception, check the exception's error code.</exception>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Net.HttpListener" /> associated with this collection is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0004EF90 File Offset: 0x0004D190
		public bool Remove(string uriPrefix)
		{
			this.listener.CheckDisposed();
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			bool flag = this.prefixes.Remove(uriPrefix);
			if (flag && this.listener.IsListening)
			{
				EndPointManager.RemovePrefix(uriPrefix, this.listener);
			}
			return flag;
		}

		// Token: 0x04001113 RID: 4371
		private List<string> prefixes = new List<string>();

		// Token: 0x04001114 RID: 4372
		private HttpListener listener;
	}
}
