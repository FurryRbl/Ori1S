using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Marks the program elements that are no longer in use. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003F RID: 63
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	[Serializable]
	public sealed class ObsoleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with default properties.</summary>
		// Token: 0x06000650 RID: 1616 RVA: 0x00014A10 File Offset: 0x00012C10
		public ObsoleteAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a specified workaround message.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		// Token: 0x06000651 RID: 1617 RVA: 0x00014A18 File Offset: 0x00012C18
		public ObsoleteAttribute(string message)
		{
			this._message = message;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ObsoleteAttribute" /> class with a workaround message and a Boolean value indicating whether the obsolete element usage is considered an error.</summary>
		/// <param name="message">The text string that describes alternative workarounds. </param>
		/// <param name="error">The Boolean value that indicates whether the obsolete element usage is considered an error. </param>
		// Token: 0x06000652 RID: 1618 RVA: 0x00014A28 File Offset: 0x00012C28
		public ObsoleteAttribute(string message, bool error)
		{
			this._message = message;
			this._error = error;
		}

		/// <summary>Gets the workaround message, including a description of the alternative program elements.</summary>
		/// <returns>The workaround text string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x00014A40 File Offset: 0x00012C40
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		/// <summary>Gets a Boolean value indicating whether the compiler will treat usage of the obsolete program element as an error.</summary>
		/// <returns>true if the obsolete element usage is considered an error; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00014A48 File Offset: 0x00012C48
		public bool IsError
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x04000083 RID: 131
		private string _message;

		// Token: 0x04000084 RID: 132
		private bool _error;
	}
}
