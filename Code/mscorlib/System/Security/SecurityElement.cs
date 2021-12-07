using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Xml;

namespace System.Security
{
	/// <summary>Represents the XML object model for encoding security objects. This class cannot be inherited.</summary>
	// Token: 0x02000543 RID: 1347
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag.</summary>
		/// <param name="tag">The tag name of an XML element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter is invalid in XML. </exception>
		// Token: 0x06003506 RID: 13574 RVA: 0x000AEBB0 File Offset: 0x000ACDB0
		public SecurityElement(string tag) : this(tag, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.SecurityElement" /> class with the specified tag and text.</summary>
		/// <param name="tag">The tag name of the XML element. </param>
		/// <param name="text">The text content within the element. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="tag" /> parameter or <paramref name="text" /> parameter is invalid in XML. </exception>
		// Token: 0x06003507 RID: 13575 RVA: 0x000AEBBC File Offset: 0x000ACDBC
		public SecurityElement(string tag, string text)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (!SecurityElement.IsValidTag(tag))
			{
				throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + tag);
			}
			this.tag = tag;
			this.Text = text;
		}

		// Token: 0x06003508 RID: 13576 RVA: 0x000AEC14 File Offset: 0x000ACE14
		internal SecurityElement(SecurityElement se)
		{
			this.Tag = se.Tag;
			this.Text = se.Text;
			if (se.attributes != null)
			{
				foreach (object obj in se.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					this.AddAttribute(securityAttribute.Name, securityAttribute.Value);
				}
			}
			if (se.children != null)
			{
				foreach (object obj2 in se.children)
				{
					SecurityElement child = (SecurityElement)obj2;
					this.AddChild(child);
				}
			}
		}

		/// <summary>Gets or sets the attributes of an XML element as name/value pairs.</summary>
		/// <returns>The <see cref="T:System.Collections.Hashtable" /> object for the attribute values of the XML element.</returns>
		/// <exception cref="T:System.InvalidCastException">The name or value of the <see cref="T:System.Collections.Hashtable" /> object is invalid. </exception>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x0600350A RID: 13578 RVA: 0x000AEDB0 File Offset: 0x000ACFB0
		// (set) Token: 0x0600350B RID: 13579 RVA: 0x000AEE4C File Offset: 0x000AD04C
		public Hashtable Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					return null;
				}
				Hashtable hashtable = new Hashtable(this.attributes.Count);
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					hashtable.Add(securityAttribute.Name, securityAttribute.Value);
				}
				return hashtable;
			}
			set
			{
				if (value == null || value.Count == 0)
				{
					this.attributes.Clear();
					return;
				}
				if (this.attributes == null)
				{
					this.attributes = new ArrayList();
				}
				else
				{
					this.attributes.Clear();
				}
				IDictionaryEnumerator enumerator = value.GetEnumerator();
				while (enumerator.MoveNext())
				{
					this.attributes.Add(new SecurityElement.SecurityAttribute((string)enumerator.Key, (string)enumerator.Value));
				}
			}
		}

		/// <summary>Gets or sets the array of child elements of the XML element.</summary>
		/// <returns>The ordered child elements of the XML element as security elements.</returns>
		/// <exception cref="T:System.ArgumentException">A child of the XML parent node is null. </exception>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600350C RID: 13580 RVA: 0x000AEEDC File Offset: 0x000AD0DC
		// (set) Token: 0x0600350D RID: 13581 RVA: 0x000AEEE4 File Offset: 0x000AD0E4
		public ArrayList Children
		{
			get
			{
				return this.children;
			}
			set
			{
				if (value != null)
				{
					using (IEnumerator enumerator = value.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current == null)
							{
								throw new ArgumentNullException();
							}
						}
					}
				}
				this.children = value;
			}
		}

		/// <summary>Gets or sets the tag name of an XML element.</summary>
		/// <returns>The tag name of an XML element.</returns>
		/// <exception cref="T:System.ArgumentNullException">The tag is null. </exception>
		/// <exception cref="T:System.ArgumentException">The tag is not valid in XML. </exception>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600350E RID: 13582 RVA: 0x000AEF5C File Offset: 0x000AD15C
		// (set) Token: 0x0600350F RID: 13583 RVA: 0x000AEF64 File Offset: 0x000AD164
		public string Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Tag");
				}
				if (!SecurityElement.IsValidTag(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.tag = value;
			}
		}

		/// <summary>Gets or sets the text within an XML element.</summary>
		/// <returns>The value of the text within an XML element.</returns>
		/// <exception cref="T:System.ArgumentException">The text is not valid in XML. </exception>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06003510 RID: 13584 RVA: 0x000AEFB0 File Offset: 0x000AD1B0
		// (set) Token: 0x06003511 RID: 13585 RVA: 0x000AEFB8 File Offset: 0x000AD1B8
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				if (value != null && !SecurityElement.IsValidText(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML string") + ": " + value);
				}
				this.text = SecurityElement.Unescape(value);
			}
		}

		/// <summary>Adds a name/value attribute to an XML element.</summary>
		/// <param name="name">The name of the attribute. </param>
		/// <param name="value">The value of the attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="name" /> parameter or <paramref name="value" /> parameter is invalid in XML.-or- An attribute with the name specified by the <paramref name="name" /> parameter already exists. </exception>
		// Token: 0x06003512 RID: 13586 RVA: 0x000AF000 File Offset: 0x000AD200
		public void AddAttribute(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.GetAttribute(name) != null)
			{
				throw new ArgumentException(Locale.GetText("Duplicate attribute : " + name));
			}
			if (this.attributes == null)
			{
				this.attributes = new ArrayList();
			}
			this.attributes.Add(new SecurityElement.SecurityAttribute(name, value));
		}

		/// <summary>Adds a child element to the XML element.</summary>
		/// <param name="child">The child element to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="child" /> parameter is null. </exception>
		// Token: 0x06003513 RID: 13587 RVA: 0x000AF07C File Offset: 0x000AD27C
		public void AddChild(SecurityElement child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this.children == null)
			{
				this.children = new ArrayList();
			}
			this.children.Add(child);
		}

		/// <summary>Finds an attribute by name in an XML element.</summary>
		/// <returns>The value associated with the named attribute, or null if no attribute with <paramref name="name" /> exists.</returns>
		/// <param name="name">The name of the attribute for which to search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06003514 RID: 13588 RVA: 0x000AF0C0 File Offset: 0x000AD2C0
		public string Attribute(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			SecurityElement.SecurityAttribute attribute = this.GetAttribute(name);
			return (attribute != null) ? attribute.Value : null;
		}

		/// <summary>Creates and returns an identical copy of the current <see cref="T:System.Security.SecurityElement" /> object.</summary>
		/// <returns>A copy of the current <see cref="T:System.Security.SecurityElement" /> object.</returns>
		// Token: 0x06003515 RID: 13589 RVA: 0x000AF0F8 File Offset: 0x000AD2F8
		[ComVisible(false)]
		public SecurityElement Copy()
		{
			return new SecurityElement(this);
		}

		/// <summary>Compares two XML element objects for equality.</summary>
		/// <returns>true if the tag, attribute names and values, child elements, and text fields in the current XML element are identical to their counterparts in the <paramref name="other" /> parameter; otherwise, false.</returns>
		/// <param name="other">An XML element object to which to compare the current XML element object. </param>
		// Token: 0x06003516 RID: 13590 RVA: 0x000AF100 File Offset: 0x000AD300
		public bool Equal(SecurityElement other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.text != other.text)
			{
				return false;
			}
			if (this.tag != other.tag)
			{
				return false;
			}
			if (this.attributes == null && other.attributes != null && other.attributes.Count != 0)
			{
				return false;
			}
			if (other.attributes == null && this.attributes != null && this.attributes.Count != 0)
			{
				return false;
			}
			if (this.attributes != null && other.attributes != null)
			{
				if (this.attributes.Count != other.attributes.Count)
				{
					return false;
				}
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					SecurityElement.SecurityAttribute attribute = other.GetAttribute(securityAttribute.Name);
					if (attribute == null || securityAttribute.Value != attribute.Value)
					{
						return false;
					}
				}
			}
			if (this.children == null && other.children != null && other.children.Count != 0)
			{
				return false;
			}
			if (other.children == null && this.children != null && this.children.Count != 0)
			{
				return false;
			}
			if (this.children != null && other.children != null)
			{
				if (this.children.Count != other.children.Count)
				{
					return false;
				}
				for (int i = 0; i < this.children.Count; i++)
				{
					if (!((SecurityElement)this.children[i]).Equal((SecurityElement)other.children[i]))
					{
						return false;
					}
				}
			}
			return true;
		}

		/// <summary>Replaces invalid XML characters in a string with their valid XML equivalent.</summary>
		/// <returns>The input string with invalid characters replaced.</returns>
		/// <param name="str">The string within which to escape invalid characters. </param>
		// Token: 0x06003517 RID: 13591 RVA: 0x000AF334 File Offset: 0x000AD534
		public static string Escape(string str)
		{
			if (str == null)
			{
				return null;
			}
			if (str.IndexOfAny(SecurityElement.invalid_chars) == -1)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int length = str.Length;
			for (int i = 0; i < length; i++)
			{
				char c = str[i];
				char c2 = c;
				switch (c2)
				{
				case '"':
					stringBuilder.Append("&quot;");
					break;
				default:
					switch (c2)
					{
					case '<':
						stringBuilder.Append("&lt;");
						goto IL_D9;
					case '>':
						stringBuilder.Append("&gt;");
						goto IL_D9;
					}
					stringBuilder.Append(c);
					break;
				case '&':
					stringBuilder.Append("&amp;");
					break;
				case '\'':
					stringBuilder.Append("&apos;");
					break;
				}
				IL_D9:;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003518 RID: 13592 RVA: 0x000AF42C File Offset: 0x000AD62C
		private static string Unescape(string str)
		{
			if (str == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(str);
			stringBuilder.Replace("&lt;", "<");
			stringBuilder.Replace("&gt;", ">");
			stringBuilder.Replace("&amp;", "&");
			stringBuilder.Replace("&quot;", "\"");
			stringBuilder.Replace("&apos;", "'");
			return stringBuilder.ToString();
		}

		/// <summary>Creates a security element from an XML-encoded string.</summary>
		/// <returns>A <see cref="T:System.Security.SecurityElement" /> created from the XML.</returns>
		/// <param name="xml">The XML-encoded string from which to create the security element.</param>
		/// <exception cref="T:System.Security.XmlSyntaxException">
		///   <paramref name="xml" /> contains one or more single quotation mark characters.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="xml" /> is null.</exception>
		// Token: 0x06003519 RID: 13593 RVA: 0x000AF4A4 File Offset: 0x000AD6A4
		public static SecurityElement FromString(string xml)
		{
			if (xml == null)
			{
				throw new ArgumentNullException("xml");
			}
			if (xml.Length == 0)
			{
				throw new XmlSyntaxException(Locale.GetText("Empty string."));
			}
			SecurityElement result;
			try
			{
				SecurityParser securityParser = new SecurityParser();
				securityParser.LoadXml(xml);
				result = securityParser.ToXml();
			}
			catch (Exception inner)
			{
				string message = Locale.GetText("Invalid XML.");
				throw new XmlSyntaxException(message, inner);
			}
			return result;
		}

		/// <summary>Determines whether a string is a valid attribute name.</summary>
		/// <returns>true if the <paramref name="name" /> parameter is a valid XML attribute name; otherwise, false.</returns>
		/// <param name="name">The attribute name to test for validity. </param>
		// Token: 0x0600351A RID: 13594 RVA: 0x000AF534 File Offset: 0x000AD734
		public static bool IsValidAttributeName(string name)
		{
			return name != null && name.IndexOfAny(SecurityElement.invalid_attr_name_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid attribute value.</summary>
		/// <returns>true if the <paramref name="value" /> parameter is a valid XML attribute value; otherwise, false.</returns>
		/// <param name="value">The attribute value to test for validity. </param>
		// Token: 0x0600351B RID: 13595 RVA: 0x000AF550 File Offset: 0x000AD750
		public static bool IsValidAttributeValue(string value)
		{
			return value != null && value.IndexOfAny(SecurityElement.invalid_attr_value_chars) == -1;
		}

		/// <summary>Determines whether a string is a valid tag.</summary>
		/// <returns>true if the <paramref name="tag" /> parameter is a valid XML tag; otherwise, false.</returns>
		/// <param name="tag">The tag to test for validity. </param>
		// Token: 0x0600351C RID: 13596 RVA: 0x000AF56C File Offset: 0x000AD76C
		public static bool IsValidTag(string tag)
		{
			return tag != null && tag.IndexOfAny(SecurityElement.invalid_tag_chars) == -1;
		}

		/// <summary>Determines whether a string is valid as text within an XML element.</summary>
		/// <returns>true if the <paramref name="text" /> parameter is a valid XML text element; otherwise, false.</returns>
		/// <param name="text">The text to test for validity. </param>
		// Token: 0x0600351D RID: 13597 RVA: 0x000AF588 File Offset: 0x000AD788
		public static bool IsValidText(string text)
		{
			return text != null && text.IndexOfAny(SecurityElement.invalid_text_chars) == -1;
		}

		/// <summary>Finds a child by its tag name.</summary>
		/// <returns>The first child XML element with the specified tag value, or null if no child element with <paramref name="tag" /> exists.</returns>
		/// <param name="tag">The tag for which to search in child elements. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="tag" /> parameter is null. </exception>
		// Token: 0x0600351E RID: 13598 RVA: 0x000AF5A4 File Offset: 0x000AD7A4
		public SecurityElement SearchForChildByTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				SecurityElement securityElement = (SecurityElement)this.children[i];
				if (securityElement.tag == tag)
				{
					return securityElement;
				}
			}
			return null;
		}

		/// <summary>Finds a child by its tag name and returns the contained text.</summary>
		/// <returns>The text contents of the first child element with the specified tag value.</returns>
		/// <param name="tag">The tag for which to search in child elements. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tag" /> is null. </exception>
		// Token: 0x0600351F RID: 13599 RVA: 0x000AF614 File Offset: 0x000AD814
		public string SearchForTextOfTag(string tag)
		{
			if (tag == null)
			{
				throw new ArgumentNullException("tag");
			}
			if (this.tag == tag)
			{
				return this.text;
			}
			if (this.children == null)
			{
				return null;
			}
			for (int i = 0; i < this.children.Count; i++)
			{
				string text = ((SecurityElement)this.children[i]).SearchForTextOfTag(tag);
				if (text != null)
				{
					return text;
				}
			}
			return null;
		}

		/// <summary>Produces a string representation of an XML element and its constituent attributes, child elements, and text.</summary>
		/// <returns>The XML element and its contents.</returns>
		// Token: 0x06003520 RID: 13600 RVA: 0x000AF694 File Offset: 0x000AD894
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.ToXml(ref stringBuilder, 0);
			return stringBuilder.ToString();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000AF6B8 File Offset: 0x000AD8B8
		private void ToXml(ref StringBuilder s, int level)
		{
			s.Append("<");
			s.Append(this.tag);
			if (this.attributes != null)
			{
				s.Append(" ");
				for (int i = 0; i < this.attributes.Count; i++)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)this.attributes[i];
					s.Append(securityAttribute.Name).Append("=\"").Append(SecurityElement.Escape(securityAttribute.Value)).Append("\"");
					if (i != this.attributes.Count - 1)
					{
						s.Append(Environment.NewLine);
					}
				}
			}
			if ((this.text == null || this.text == string.Empty) && (this.children == null || this.children.Count == 0))
			{
				s.Append("/>").Append(Environment.NewLine);
			}
			else
			{
				s.Append(">").Append(SecurityElement.Escape(this.text));
				if (this.children != null)
				{
					s.Append(Environment.NewLine);
					foreach (object obj in this.children)
					{
						SecurityElement securityElement = (SecurityElement)obj;
						securityElement.ToXml(ref s, level + 1);
					}
				}
				s.Append("</").Append(this.tag).Append(">").Append(Environment.NewLine);
			}
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000AF898 File Offset: 0x000ADA98
		internal SecurityElement.SecurityAttribute GetAttribute(string name)
		{
			if (this.attributes != null)
			{
				foreach (object obj in this.attributes)
				{
					SecurityElement.SecurityAttribute securityAttribute = (SecurityElement.SecurityAttribute)obj;
					if (securityAttribute.Name == name)
					{
						return securityAttribute;
					}
				}
			}
			return null;
		}

		// Token: 0x04001642 RID: 5698
		private string text;

		// Token: 0x04001643 RID: 5699
		private string tag;

		// Token: 0x04001644 RID: 5700
		private ArrayList attributes;

		// Token: 0x04001645 RID: 5701
		private ArrayList children;

		// Token: 0x04001646 RID: 5702
		private static readonly char[] invalid_tag_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04001647 RID: 5703
		private static readonly char[] invalid_text_chars = new char[]
		{
			'<',
			'>'
		};

		// Token: 0x04001648 RID: 5704
		private static readonly char[] invalid_attr_name_chars = new char[]
		{
			' ',
			'<',
			'>'
		};

		// Token: 0x04001649 RID: 5705
		private static readonly char[] invalid_attr_value_chars = new char[]
		{
			'"',
			'<',
			'>'
		};

		// Token: 0x0400164A RID: 5706
		private static readonly char[] invalid_chars = new char[]
		{
			'<',
			'>',
			'"',
			'\'',
			'&'
		};

		// Token: 0x02000544 RID: 1348
		internal class SecurityAttribute
		{
			// Token: 0x06003523 RID: 13603 RVA: 0x000AF928 File Offset: 0x000ADB28
			public SecurityAttribute(string name, string value)
			{
				if (!SecurityElement.IsValidAttributeName(name))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute name") + ": " + name);
				}
				if (!SecurityElement.IsValidAttributeValue(value))
				{
					throw new ArgumentException(Locale.GetText("Invalid XML attribute value") + ": " + value);
				}
				this._name = name;
				this._value = SecurityElement.Unescape(value);
			}

			// Token: 0x170009E8 RID: 2536
			// (get) Token: 0x06003524 RID: 13604 RVA: 0x000AF99C File Offset: 0x000ADB9C
			public string Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170009E9 RID: 2537
			// (get) Token: 0x06003525 RID: 13605 RVA: 0x000AF9A4 File Offset: 0x000ADBA4
			public string Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x0400164B RID: 5707
			private string _name;

			// Token: 0x0400164C RID: 5708
			private string _value;
		}
	}
}
