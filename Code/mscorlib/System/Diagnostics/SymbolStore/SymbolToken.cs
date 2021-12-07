using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure is an object representation of a token that represents symbolic information.</summary>
	// Token: 0x020001F3 RID: 499
	[ComVisible(true)]
	public struct SymbolToken
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure when given a value.</summary>
		/// <param name="val">The value to be used for the token. </param>
		// Token: 0x060018F9 RID: 6393 RVA: 0x0005E0B8 File Offset: 0x0005C2B8
		public SymbolToken(int val)
		{
			this._val = val;
		}

		/// <summary>Determines whether <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The object to check. </param>
		// Token: 0x060018FA RID: 6394 RVA: 0x0005E0C4 File Offset: 0x0005C2C4
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && ((SymbolToken)obj).GetToken() == this._val;
		}

		/// <summary>Determines whether <paramref name="obj" /> is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> to check.</param>
		// Token: 0x060018FB RID: 6395 RVA: 0x0005E0F4 File Offset: 0x0005C2F4
		public bool Equals(SymbolToken obj)
		{
			return obj.GetToken() == this._val;
		}

		/// <summary>Generates the hash code for the current token.</summary>
		/// <returns>The hash code for the current token.</returns>
		// Token: 0x060018FC RID: 6396 RVA: 0x0005E108 File Offset: 0x0005C308
		public override int GetHashCode()
		{
			return this._val.GetHashCode();
		}

		/// <summary>Gets the value of the current token.</summary>
		/// <returns>The value of the current token.</returns>
		// Token: 0x060018FD RID: 6397 RVA: 0x0005E118 File Offset: 0x0005C318
		public int GetToken()
		{
			return this._val;
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> objects are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		/// <param name="b">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		// Token: 0x060018FE RID: 6398 RVA: 0x0005E120 File Offset: 0x0005C320
		public static bool operator ==(SymbolToken a, SymbolToken b)
		{
			return a.Equals(b);
		}

		/// <summary>Returns a value indicating whether two <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		/// <param name="b">A <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure.</param>
		// Token: 0x060018FF RID: 6399 RVA: 0x0005E12C File Offset: 0x0005C32C
		public static bool operator !=(SymbolToken a, SymbolToken b)
		{
			return !a.Equals(b);
		}

		// Token: 0x0400090D RID: 2317
		private int _val;
	}
}
