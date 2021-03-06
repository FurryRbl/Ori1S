using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x02000419 RID: 1049
	[ComVisible(true)]
	[Serializable]
	public class WebHeaderCollection : System.Collections.Specialized.NameValueCollection, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x060025B6 RID: 9654 RVA: 0x00074FF0 File Offset: 0x000731F0
		public WebHeaderCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />. </exception>
		// Token: 0x060025B7 RID: 9655 RVA: 0x00074FF8 File Offset: 0x000731F8
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			try
			{
				int @int = serializationInfo.GetInt32("Count");
				for (int i = 0; i < @int; i++)
				{
					this.Add(serializationInfo.GetString(i.ToString()), serializationInfo.GetString((@int + i).ToString()));
				}
			}
			catch (SerializationException)
			{
				int @int = serializationInfo.GetInt32("count");
				for (int j = 0; j < @int; j++)
				{
					this.Add(serializationInfo.GetString("k" + j), serializationInfo.GetString("v" + j));
				}
			}
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x000750C4 File Offset: 0x000732C4
		internal WebHeaderCollection(bool internallyCreated)
		{
			this.internallyCreated = internallyCreated;
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x000750D4 File Offset: 0x000732D4
		static WebHeaderCollection()
		{
			WebHeaderCollection.restricted = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			WebHeaderCollection.restricted.Add("accept", true);
			WebHeaderCollection.restricted.Add("connection", true);
			WebHeaderCollection.restricted.Add("content-length", true);
			WebHeaderCollection.restricted.Add("content-type", true);
			WebHeaderCollection.restricted.Add("date", true);
			WebHeaderCollection.restricted.Add("expect", true);
			WebHeaderCollection.restricted.Add("host", true);
			WebHeaderCollection.restricted.Add("if-modified-since", true);
			WebHeaderCollection.restricted.Add("range", true);
			WebHeaderCollection.restricted.Add("referer", true);
			WebHeaderCollection.restricted.Add("transfer-encoding", true);
			WebHeaderCollection.restricted.Add("user-agent", true);
			WebHeaderCollection.restricted.Add("proxy-connection", true);
			WebHeaderCollection.restricted_response = new Dictionary<string, bool>(StringComparer.InvariantCultureIgnoreCase);
			WebHeaderCollection.restricted_response.Add("Content-Length", true);
			WebHeaderCollection.restricted_response.Add("Transfer-Encoding", true);
			WebHeaderCollection.restricted_response.Add("WWW-Authenticate", true);
			WebHeaderCollection.multiValue = new Hashtable(CaseInsensitiveHashCodeProvider.DefaultInvariant, CaseInsensitiveComparer.DefaultInvariant);
			WebHeaderCollection.multiValue.Add("accept", true);
			WebHeaderCollection.multiValue.Add("accept-charset", true);
			WebHeaderCollection.multiValue.Add("accept-encoding", true);
			WebHeaderCollection.multiValue.Add("accept-language", true);
			WebHeaderCollection.multiValue.Add("accept-ranges", true);
			WebHeaderCollection.multiValue.Add("allow", true);
			WebHeaderCollection.multiValue.Add("authorization", true);
			WebHeaderCollection.multiValue.Add("cache-control", true);
			WebHeaderCollection.multiValue.Add("connection", true);
			WebHeaderCollection.multiValue.Add("content-encoding", true);
			WebHeaderCollection.multiValue.Add("content-language", true);
			WebHeaderCollection.multiValue.Add("expect", true);
			WebHeaderCollection.multiValue.Add("if-match", true);
			WebHeaderCollection.multiValue.Add("if-none-match", true);
			WebHeaderCollection.multiValue.Add("proxy-authenticate", true);
			WebHeaderCollection.multiValue.Add("public", true);
			WebHeaderCollection.multiValue.Add("range", true);
			WebHeaderCollection.multiValue.Add("transfer-encoding", true);
			WebHeaderCollection.multiValue.Add("upgrade", true);
			WebHeaderCollection.multiValue.Add("vary", true);
			WebHeaderCollection.multiValue.Add("via", true);
			WebHeaderCollection.multiValue.Add("warning", true);
			WebHeaderCollection.multiValue.Add("www-authenticate", true);
			WebHeaderCollection.multiValue.Add("set-cookie", true);
			WebHeaderCollection.multiValue.Add("set-cookie2", true);
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x060025BA RID: 9658 RVA: 0x00075480 File Offset: 0x00073680
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.The length of <paramref name="value" /> is greater than 65535.-or- The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.-or- <paramref name="header" /> is a restricted header that should be set with a property.-or- The value part of <paramref name="header" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535. </exception>
		// Token: 0x060025BB RID: 9659 RVA: 0x0007548C File Offset: 0x0007368C
		public void Add(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException("no colon found", "header");
			}
			this.Add(header.Substring(0, num), header.Substring(num + 1));
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="name" /> is a restricted header that must be set with a property setting.-or- <paramref name="value" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		// Token: 0x060025BC RID: 9660 RVA: 0x000754E4 File Offset: 0x000736E4
		public override void Add(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("This header must be modified with the appropiate property.");
			}
			this.AddWithoutValidate(name, value);
		}

		/// <summary>Inserts a header into the collection without checking whether the header is on the restricted header list.</summary>
		/// <param name="headerName">The header to add to the collection. </param>
		/// <param name="headerValue">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="headerValue" /> contains invalid characters. </exception>
		// Token: 0x060025BD RID: 9661 RVA: 0x0007552C File Offset: 0x0007372C
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			if (!WebHeaderCollection.IsHeaderName(headerName))
			{
				throw new ArgumentException("invalid header name: " + headerName, "headerName");
			}
			if (headerValue == null)
			{
				headerValue = string.Empty;
			}
			else
			{
				headerValue = headerValue.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(headerValue))
			{
				throw new ArgumentException("invalid header value: " + headerValue, "headerValue");
			}
			base.Add(headerName, headerValue);
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="header">The header to return. </param>
		// Token: 0x060025BE RID: 9662 RVA: 0x000755A0 File Offset: 0x000737A0
		public override string[] GetValues(string header)
		{
			if (header == null)
			{
				throw new ArgumentNullException("header");
			}
			string[] values = base.GetValues(header);
			if (values == null || values.Length == 0)
			{
				return null;
			}
			return values;
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="index">The header index to return.</param>
		// Token: 0x060025BF RID: 9663 RVA: 0x000755D8 File Offset: 0x000737D8
		public override string[] GetValues(int index)
		{
			string[] values = base.GetValues(index);
			if (values == null || values.Length == 0)
			{
				return null;
			}
			return values;
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request.</summary>
		/// <returns>true if the header is restricted; otherwise false.</returns>
		/// <param name="headerName">The header to test. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x060025C0 RID: 9664 RVA: 0x00075600 File Offset: 0x00073800
		public static bool IsRestricted(string headerName)
		{
			if (headerName == null)
			{
				throw new ArgumentNullException("headerName");
			}
			if (headerName == string.Empty)
			{
				throw new ArgumentException("empty string", "headerName");
			}
			if (!WebHeaderCollection.IsHeaderName(headerName))
			{
				throw new ArgumentException("Invalid character in header");
			}
			return WebHeaderCollection.restricted.ContainsKey(headerName);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request or the response.</summary>
		/// <returns>true if the header is restricted; otherwise, false.</returns>
		/// <param name="headerName">The header to test.</param>
		/// <param name="response">Does the Framework test the response or the request?</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x060025C1 RID: 9665 RVA: 0x00075660 File Offset: 0x00073860
		public static bool IsRestricted(string headerName, bool response)
		{
			if (string.IsNullOrEmpty(headerName))
			{
				throw new ArgumentNullException("headerName");
			}
			if (!WebHeaderCollection.IsHeaderName(headerName))
			{
				throw new ArgumentException("Invalid character in header");
			}
			if (response)
			{
				return WebHeaderCollection.restricted_response.ContainsKey(headerName);
			}
			return WebHeaderCollection.restricted.ContainsKey(headerName);
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x060025C2 RID: 9666 RVA: 0x000756B8 File Offset: 0x000738B8
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null<see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> contains invalid characters. </exception>
		// Token: 0x060025C3 RID: 9667 RVA: 0x000756BC File Offset: 0x000738BC
		public override void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("restricted header");
			}
			base.Remove(name);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> or <paramref name="value" /> contain invalid characters. </exception>
		// Token: 0x060025C4 RID: 9668 RVA: 0x000756F8 File Offset: 0x000738F8
		public override void Set(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this.internallyCreated && WebHeaderCollection.IsRestricted(name))
			{
				throw new ArgumentException("restricted header");
			}
			if (!WebHeaderCollection.IsHeaderName(name))
			{
				throw new ArgumentException("invalid header name");
			}
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(value))
			{
				throw new ArgumentException("invalid header value");
			}
			base.Set(name, value);
		}

		/// <summary>Converts the <see cref="T:System.Net.WebHeaderCollection" /> to a byte array..</summary>
		/// <returns>A <see cref="T:System.Byte" /> array holding the header collection.</returns>
		// Token: 0x060025C5 RID: 9669 RVA: 0x00075788 File Offset: 0x00073988
		public byte[] ToByteArray()
		{
			return Encoding.UTF8.GetBytes(this.ToString());
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0007579C File Offset: 0x0007399C
		internal string ToStringMultiValue()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				string key = this.GetKey(i);
				if (WebHeaderCollection.IsMultiValue(key))
				{
					foreach (string value in this.GetValues(i))
					{
						stringBuilder.Append(key).Append(": ").Append(value).Append("\r\n");
					}
				}
				else
				{
					stringBuilder.Append(key).Append(": ").Append(this.Get(i)).Append("\r\n");
				}
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		/// <summary>Obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x060025C7 RID: 9671 RVA: 0x00075864 File Offset: 0x00073A64
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			int count = base.Count;
			for (int i = 0; i < count; i++)
			{
				stringBuilder.Append(this.GetKey(i)).Append(": ").Append(this.Get(i)).Append("\r\n");
			}
			return stringBuilder.Append("\r\n").ToString();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x060025C8 RID: 9672 RVA: 0x000758D0 File Offset: 0x00073AD0
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			int count = base.Count;
			serializationInfo.AddValue("Count", count);
			for (int i = 0; i < count; i++)
			{
				serializationInfo.AddValue(i.ToString(), this.GetKey(i));
				serializationInfo.AddValue((count + i).ToString(), this.Get(i));
			}
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x060025C9 RID: 9673 RVA: 0x00075930 File Offset: 0x00073B30
		public override string[] AllKeys
		{
			get
			{
				return base.AllKeys;
			}
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x060025CA RID: 9674 RVA: 0x00075938 File Offset: 0x00073B38
		public override int Count
		{
			get
			{
				return base.Count;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060025CB RID: 9675 RVA: 0x00075940 File Offset: 0x00073B40
		public override System.Collections.Specialized.NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		/// <summary>Get the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x060025CC RID: 9676 RVA: 0x00075948 File Offset: 0x00073B48
		public override string Get(int index)
		{
			return base.Get(index);
		}

		/// <summary>Get the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		/// <param name="name">The name of the Web header.</param>
		// Token: 0x060025CD RID: 9677 RVA: 0x00075954 File Offset: 0x00073B54
		public override string Get(string name)
		{
			return base.Get(name);
		}

		/// <summary>Get the header name at the specified position in the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x060025CE RID: 9678 RVA: 0x00075960 File Offset: 0x00073B60
		public override string GetKey(int index)
		{
			return base.GetKey(index);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x060025CF RID: 9679 RVA: 0x0007596C File Offset: 0x00073B6C
		public void Add(HttpRequestHeader header, string value)
		{
			this.Add(this.RequestHeaderToString(header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025D0 RID: 9680 RVA: 0x0007597C File Offset: 0x00073B7C
		public void Remove(HttpRequestHeader header)
		{
			this.Remove(this.RequestHeaderToString(header));
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x060025D1 RID: 9681 RVA: 0x0007598C File Offset: 0x00073B8C
		public void Set(HttpRequestHeader header, string value)
		{
			this.Set(this.RequestHeaderToString(header), value);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x060025D2 RID: 9682 RVA: 0x0007599C File Offset: 0x00073B9C
		public void Add(HttpResponseHeader header, string value)
		{
			this.Add(this.ResponseHeaderToString(header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060025D3 RID: 9683 RVA: 0x000759AC File Offset: 0x00073BAC
		public void Remove(HttpResponseHeader header)
		{
			this.Remove(this.ResponseHeaderToString(header));
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x060025D4 RID: 9684 RVA: 0x000759BC File Offset: 0x00073BBC
		public void Set(HttpResponseHeader header, string value)
		{
			this.Set(this.ResponseHeaderToString(header), value);
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x000759CC File Offset: 0x00073BCC
		private string RequestHeaderToString(HttpRequestHeader value)
		{
			switch (value)
			{
			case HttpRequestHeader.CacheControl:
				return "cache-control";
			case HttpRequestHeader.Connection:
				return "connection";
			case HttpRequestHeader.Date:
				return "date";
			case HttpRequestHeader.KeepAlive:
				return "keep-alive";
			case HttpRequestHeader.Pragma:
				return "pragma";
			case HttpRequestHeader.Trailer:
				return "trailer";
			case HttpRequestHeader.TransferEncoding:
				return "transfer-encoding";
			case HttpRequestHeader.Upgrade:
				return "upgrade";
			case HttpRequestHeader.Via:
				return "via";
			case HttpRequestHeader.Warning:
				return "warning";
			case HttpRequestHeader.Allow:
				return "allow";
			case HttpRequestHeader.ContentLength:
				return "content-length";
			case HttpRequestHeader.ContentType:
				return "content-type";
			case HttpRequestHeader.ContentEncoding:
				return "content-encoding";
			case HttpRequestHeader.ContentLanguage:
				return "content-language";
			case HttpRequestHeader.ContentLocation:
				return "content-location";
			case HttpRequestHeader.ContentMd5:
				return "content-md5";
			case HttpRequestHeader.ContentRange:
				return "content-range";
			case HttpRequestHeader.Expires:
				return "expires";
			case HttpRequestHeader.LastModified:
				return "last-modified";
			case HttpRequestHeader.Accept:
				return "accept";
			case HttpRequestHeader.AcceptCharset:
				return "accept-charset";
			case HttpRequestHeader.AcceptEncoding:
				return "accept-encoding";
			case HttpRequestHeader.AcceptLanguage:
				return "accept-language";
			case HttpRequestHeader.Authorization:
				return "authorization";
			case HttpRequestHeader.Cookie:
				return "cookie";
			case HttpRequestHeader.Expect:
				return "expect";
			case HttpRequestHeader.From:
				return "from";
			case HttpRequestHeader.Host:
				return "host";
			case HttpRequestHeader.IfMatch:
				return "if-match";
			case HttpRequestHeader.IfModifiedSince:
				return "if-modified-since";
			case HttpRequestHeader.IfNoneMatch:
				return "if-none-match";
			case HttpRequestHeader.IfRange:
				return "if-range";
			case HttpRequestHeader.IfUnmodifiedSince:
				return "if-unmodified-since";
			case HttpRequestHeader.MaxForwards:
				return "max-forwards";
			case HttpRequestHeader.ProxyAuthorization:
				return "proxy-authorization";
			case HttpRequestHeader.Referer:
				return "referer";
			case HttpRequestHeader.Range:
				return "range";
			case HttpRequestHeader.Te:
				return "te";
			case HttpRequestHeader.Translate:
				return "translate";
			case HttpRequestHeader.UserAgent:
				return "user-agent";
			default:
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the specified request header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header value.</returns>
		/// <param name="header">The request header value.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AB2 RID: 2738
		public string this[HttpRequestHeader hrh]
		{
			get
			{
				return this.Get(this.RequestHeaderToString(hrh));
			}
			set
			{
				this.Add(this.RequestHeaderToString(hrh), value);
			}
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00075BA8 File Offset: 0x00073DA8
		private string ResponseHeaderToString(HttpResponseHeader value)
		{
			switch (value)
			{
			case HttpResponseHeader.CacheControl:
				return "cache-control";
			case HttpResponseHeader.Connection:
				return "connection";
			case HttpResponseHeader.Date:
				return "date";
			case HttpResponseHeader.KeepAlive:
				return "keep-alive";
			case HttpResponseHeader.Pragma:
				return "pragma";
			case HttpResponseHeader.Trailer:
				return "trailer";
			case HttpResponseHeader.TransferEncoding:
				return "transfer-encoding";
			case HttpResponseHeader.Upgrade:
				return "upgrade";
			case HttpResponseHeader.Via:
				return "via";
			case HttpResponseHeader.Warning:
				return "warning";
			case HttpResponseHeader.Allow:
				return "allow";
			case HttpResponseHeader.ContentLength:
				return "content-length";
			case HttpResponseHeader.ContentType:
				return "content-type";
			case HttpResponseHeader.ContentEncoding:
				return "content-encoding";
			case HttpResponseHeader.ContentLanguage:
				return "content-language";
			case HttpResponseHeader.ContentLocation:
				return "content-location";
			case HttpResponseHeader.ContentMd5:
				return "content-md5";
			case HttpResponseHeader.ContentRange:
				return "content-range";
			case HttpResponseHeader.Expires:
				return "expires";
			case HttpResponseHeader.LastModified:
				return "last-modified";
			case HttpResponseHeader.AcceptRanges:
				return "accept-ranges";
			case HttpResponseHeader.Age:
				return "age";
			case HttpResponseHeader.ETag:
				return "etag";
			case HttpResponseHeader.Location:
				return "location";
			case HttpResponseHeader.ProxyAuthenticate:
				return "proxy-authenticate";
			case HttpResponseHeader.RetryAfter:
				return "RetryAfter";
			case HttpResponseHeader.Server:
				return "server";
			case HttpResponseHeader.SetCookie:
				return "set-cookie";
			case HttpResponseHeader.Vary:
				return "vary";
			case HttpResponseHeader.WwwAuthenticate:
				return "www-authenticate";
			default:
				throw new InvalidOperationException();
			}
		}

		/// <summary>Gets or sets the specified response header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header.</returns>
		/// <param name="header">The response header value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AB3 RID: 2739
		public string this[HttpResponseHeader hrh]
		{
			get
			{
				return this.Get(this.ResponseHeaderToString(hrh));
			}
			set
			{
				this.Add(this.ResponseHeaderToString(hrh), value);
			}
		}

		/// <summary>Removes all headers from the collection.</summary>
		// Token: 0x060025DB RID: 9691 RVA: 0x00075D14 File Offset: 0x00073F14
		public override void Clear()
		{
			base.Clear();
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x060025DC RID: 9692 RVA: 0x00075D1C File Offset: 0x00073F1C
		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00075D24 File Offset: 0x00073F24
		internal void SetInternal(string header)
		{
			int num = header.IndexOf(':');
			if (num == -1)
			{
				throw new ArgumentException("no colon found", "header");
			}
			this.SetInternal(header.Substring(0, num), header.Substring(num + 1));
		}

		// Token: 0x060025DE RID: 9694 RVA: 0x00075D68 File Offset: 0x00073F68
		internal void SetInternal(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			if (!WebHeaderCollection.IsHeaderValue(value))
			{
				throw new ArgumentException("invalid header value");
			}
			if (WebHeaderCollection.IsMultiValue(name))
			{
				base.Add(name, value);
			}
			else
			{
				base.Remove(name);
				base.Set(name, value);
			}
		}

		// Token: 0x060025DF RID: 9695 RVA: 0x00075DCC File Offset: 0x00073FCC
		internal void RemoveAndAdd(string name, string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			else
			{
				value = value.Trim();
			}
			base.Remove(name);
			base.Set(name, value);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x00075DF8 File Offset: 0x00073FF8
		internal void RemoveInternal(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			base.Remove(name);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x00075E14 File Offset: 0x00074014
		internal static bool IsMultiValue(string headerName)
		{
			return headerName != null && !(headerName == string.Empty) && WebHeaderCollection.multiValue.ContainsKey(headerName);
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x00075E3C File Offset: 0x0007403C
		internal static bool IsHeaderValue(string value)
		{
			int length = value.Length;
			for (int i = 0; i < length; i++)
			{
				char c = value[i];
				if (c == '\u007f')
				{
					return false;
				}
				if (c < ' ' && c != '\r' && c != '\n' && c != '\t')
				{
					return false;
				}
				if (c == '\n' && ++i < length)
				{
					c = value[i];
					if (c != ' ' && c != '\t')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00075EC4 File Offset: 0x000740C4
		internal static bool IsHeaderName(string name)
		{
			if (name == null || name.Length == 0)
			{
				return false;
			}
			int length = name.Length;
			for (int i = 0; i < length; i++)
			{
				char c = name[i];
				if (c > '~' || !WebHeaderCollection.allowed_chars[(int)c])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001766 RID: 5990
		private static readonly Hashtable restricted;

		// Token: 0x04001767 RID: 5991
		private static readonly Hashtable multiValue;

		// Token: 0x04001768 RID: 5992
		private static readonly Dictionary<string, bool> restricted_response;

		// Token: 0x04001769 RID: 5993
		private bool internallyCreated;

		// Token: 0x0400176A RID: 5994
		private static bool[] allowed_chars = new bool[]
		{
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			false,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			false,
			true,
			true,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			false,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			false,
			false,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			true,
			false,
			true,
			false
		};
	}
}
