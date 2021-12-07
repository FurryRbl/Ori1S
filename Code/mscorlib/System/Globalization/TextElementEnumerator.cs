using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Enumerates the text elements of a string. </summary>
	// Token: 0x02000229 RID: 553
	[ComVisible(true)]
	[Serializable]
	public class TextElementEnumerator : IEnumerator
	{
		// Token: 0x06001C67 RID: 7271 RVA: 0x00068B14 File Offset: 0x00066D14
		internal TextElementEnumerator(string str, int startpos)
		{
			this.index = -1;
			this.startpos = startpos;
			this.str = str.Substring(startpos);
			this.element = null;
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>An object containing the current text element in the string.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00068B4C File Offset: 0x00066D4C
		public object Current
		{
			get
			{
				if (this.element == null)
				{
					throw new InvalidOperationException();
				}
				return this.element;
			}
		}

		/// <summary>Gets the index of the text element that the enumerator is currently positioned over.</summary>
		/// <returns>The index of the text element that the enumerator is currently positioned over.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001C69 RID: 7273 RVA: 0x00068B68 File Offset: 0x00066D68
		public int ElementIndex
		{
			get
			{
				if (this.element == null)
				{
					throw new InvalidOperationException();
				}
				return this.elementindex + this.startpos;
			}
		}

		/// <summary>Gets the current text element in the string.</summary>
		/// <returns>A new string containing the current text element in the string being read.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first text element of the string or after the last text element. </exception>
		// Token: 0x06001C6A RID: 7274 RVA: 0x00068B88 File Offset: 0x00066D88
		public string GetTextElement()
		{
			if (this.element == null)
			{
				throw new InvalidOperationException();
			}
			return this.element;
		}

		/// <summary>Advances the enumerator to the next text element of the string.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next text element; false if the enumerator has passed the end of the string.</returns>
		// Token: 0x06001C6B RID: 7275 RVA: 0x00068BA4 File Offset: 0x00066DA4
		public bool MoveNext()
		{
			this.elementindex = this.index + 1;
			if (this.elementindex < this.str.Length)
			{
				this.element = StringInfo.GetNextTextElement(this.str, this.elementindex);
				this.index += this.element.Length;
				return true;
			}
			this.element = null;
			return false;
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first text element in the string.</summary>
		// Token: 0x06001C6C RID: 7276 RVA: 0x00068C10 File Offset: 0x00066E10
		public void Reset()
		{
			this.element = null;
			this.index = -1;
		}

		// Token: 0x04000A9F RID: 2719
		private int index;

		// Token: 0x04000AA0 RID: 2720
		private int elementindex;

		// Token: 0x04000AA1 RID: 2721
		private int startpos;

		// Token: 0x04000AA2 RID: 2722
		private string str;

		// Token: 0x04000AA3 RID: 2723
		private string element;
	}
}
