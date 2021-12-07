using System;
using System.Xml.XPath;

namespace System.Xml
{
	/// <summary>Provides text manipulation methods that are used by several classes.</summary>
	// Token: 0x020000EC RID: 236
	public abstract class XmlCharacterData : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlCharacterData" /> class.</summary>
		/// <param name="data"></param>
		/// <param name="doc"></param>
		// Token: 0x06000880 RID: 2176 RVA: 0x0002F42C File Offset: 0x0002D62C
		protected internal XmlCharacterData(string data, XmlDocument doc) : base(doc)
		{
			if (data == null)
			{
				data = string.Empty;
			}
			this.data = data;
		}

		/// <summary>Contains the data of the node.</summary>
		/// <returns>The data of the node.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000881 RID: 2177 RVA: 0x0002F44C File Offset: 0x0002D64C
		// (set) Token: 0x06000882 RID: 2178 RVA: 0x0002F454 File Offset: 0x0002D654
		public virtual string Data
		{
			get
			{
				return this.data;
			}
			set
			{
				string oldValue = this.data;
				this.OwnerDocument.onNodeChanging(this, this.ParentNode, oldValue, value);
				this.data = value;
				this.OwnerDocument.onNodeChanged(this, this.ParentNode, oldValue, value);
			}
		}

		/// <summary>Gets or sets the concatenated values of the node and all the children of the node.</summary>
		/// <returns>The concatenated values of the node and all the children of the node.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x0002F498 File Offset: 0x0002D698
		// (set) Token: 0x06000884 RID: 2180 RVA: 0x0002F4A0 File Offset: 0x0002D6A0
		public override string InnerText
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		/// <summary>Gets the length of the data, in characters.</summary>
		/// <returns>The length, in characters, of the string in the <see cref="P:System.Xml.XmlCharacterData.Data" /> property. The length may be zero; that is, CharacterData nodes can be empty.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		public virtual int Length
		{
			get
			{
				return (this.data == null) ? 0 : this.data.Length;
			}
		}

		/// <summary>Gets or sets the value of the node.</summary>
		/// <returns>The value of the node.</returns>
		/// <exception cref="T:System.ArgumentException">Node is read-only. </exception>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000886 RID: 2182 RVA: 0x0002F4CC File Offset: 0x0002D6CC
		// (set) Token: 0x06000887 RID: 2183 RVA: 0x0002F4D4 File Offset: 0x0002D6D4
		public override string Value
		{
			get
			{
				return this.data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0002F4E0 File Offset: 0x0002D6E0
		internal override XPathNodeType XPathNodeType
		{
			get
			{
				return XPathNodeType.Text;
			}
		}

		/// <summary>Appends the specified string to the end of the character data of the node.</summary>
		/// <param name="strData">The string to insert into the existing string. </param>
		// Token: 0x06000889 RID: 2185 RVA: 0x0002F4E4 File Offset: 0x0002D6E4
		public virtual void AppendData(string strData)
		{
			string oldValue = this.data;
			string newValue = this.data += strData;
			this.OwnerDocument.onNodeChanging(this, this.ParentNode, oldValue, newValue);
			this.data = newValue;
			this.OwnerDocument.onNodeChanged(this, this.ParentNode, oldValue, newValue);
		}

		/// <summary>Removes a range of characters from the node.</summary>
		/// <param name="offset">The position within the string to start deleting. </param>
		/// <param name="count">The number of characters to delete. </param>
		// Token: 0x0600088A RID: 2186 RVA: 0x0002F540 File Offset: 0x0002D740
		public virtual void DeleteData(int offset, int count)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be non-negative and must not be greater than the length of this instance.");
			}
			int count2 = this.data.Length - offset;
			if (offset + count < this.data.Length)
			{
				count2 = count;
			}
			string oldValue = this.data;
			string newValue = this.data.Remove(offset, count2);
			this.OwnerDocument.onNodeChanging(this, this.ParentNode, oldValue, newValue);
			this.data = newValue;
			this.OwnerDocument.onNodeChanged(this, this.ParentNode, oldValue, newValue);
		}

		/// <summary>Inserts the specified string at the specified character offset.</summary>
		/// <param name="offset">The position within the string to insert the supplied string data. </param>
		/// <param name="strData">The string data that is to be inserted into the existing string. </param>
		// Token: 0x0600088B RID: 2187 RVA: 0x0002F5CC File Offset: 0x0002D7CC
		public virtual void InsertData(int offset, string strData)
		{
			if (offset < 0 || offset > this.data.Length)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be non-negative and must not be greater than the length of this instance.");
			}
			string oldValue = this.data;
			string newValue = this.data.Insert(offset, strData);
			this.OwnerDocument.onNodeChanging(this, this.ParentNode, oldValue, newValue);
			this.data = newValue;
			this.OwnerDocument.onNodeChanged(this, this.ParentNode, oldValue, newValue);
		}

		/// <summary>Replaces the specified number of characters starting at the specified offset with the specified string.</summary>
		/// <param name="offset">The position within the string to start replacing. </param>
		/// <param name="count">The number of characters to replace. </param>
		/// <param name="strData">The new data that replaces the old string data. </param>
		// Token: 0x0600088C RID: 2188 RVA: 0x0002F648 File Offset: 0x0002D848
		public virtual void ReplaceData(int offset, int count, string strData)
		{
			if (offset < 0 || offset > this.data.Length)
			{
				throw new ArgumentOutOfRangeException("offset", "Must be non-negative and must not be greater than the length of this instance.");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", "Must be non-negative.");
			}
			if (strData == null)
			{
				throw new ArgumentNullException("strData", "Must be non-null.");
			}
			string oldValue = this.data;
			string text = this.data.Substring(0, offset) + strData;
			if (offset + count < this.data.Length)
			{
				text += this.data.Substring(offset + count);
			}
			this.OwnerDocument.onNodeChanging(this, this.ParentNode, oldValue, text);
			this.data = text;
			this.OwnerDocument.onNodeChanged(this, this.ParentNode, oldValue, text);
		}

		/// <summary>Retrieves a substring of the full string from the specified range.</summary>
		/// <returns>The substring corresponding to the specified range.</returns>
		/// <param name="offset">The position within the string to start retrieving. An offset of zero indicates the starting point is at the start of the data. </param>
		/// <param name="count">The number of characters to retrieve. </param>
		// Token: 0x0600088D RID: 2189 RVA: 0x0002F71C File Offset: 0x0002D91C
		public virtual string Substring(int offset, int count)
		{
			if (this.data.Length < offset + count)
			{
				return this.data.Substring(offset);
			}
			return this.data.Substring(offset, count);
		}

		// Token: 0x040004BB RID: 1211
		private string data;
	}
}
