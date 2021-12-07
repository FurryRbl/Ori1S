using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent an event.</summary>
	// Token: 0x020002D6 RID: 726
	[ComVisible(true)]
	[Serializable]
	public struct EventToken
	{
		// Token: 0x060024F2 RID: 9458 RVA: 0x00083094 File Offset: 0x00081294
		internal EventToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of EventToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of EventToken and equals the current instance; otherwise, false.</returns>
		/// <param name="obj">The object to be compared with this instance. </param>
		// Token: 0x060024F4 RID: 9460 RVA: 0x000830BC File Offset: 0x000812BC
		public override bool Equals(object obj)
		{
			bool flag = obj is EventToken;
			if (flag)
			{
				EventToken eventToken = (EventToken)obj;
				flag = (this.tokValue == eventToken.tokValue);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.EventToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to the current instance.</param>
		// Token: 0x060024F5 RID: 9461 RVA: 0x000830F4 File Offset: 0x000812F4
		public bool Equals(EventToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this event.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x060024F6 RID: 9462 RVA: 0x00083108 File Offset: 0x00081308
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this event.</summary>
		/// <returns>Read-only. Retrieves the metadata token for this event.</returns>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x00083110 File Offset: 0x00081310
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060024F8 RID: 9464 RVA: 0x00083118 File Offset: 0x00081318
		public static bool operator ==(EventToken a, EventToken b)
		{
			return object.Equals(a, b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.EventToken" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.EventToken" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060024F9 RID: 9465 RVA: 0x0008312C File Offset: 0x0008132C
		public static bool operator !=(EventToken a, EventToken b)
		{
			return !object.Equals(a, b);
		}

		// Token: 0x04000DE0 RID: 3552
		internal int tokValue;

		/// <summary>The default EventToken with <see cref="P:System.Reflection.Emit.EventToken.Token" /> value 0.</summary>
		// Token: 0x04000DE1 RID: 3553
		public static readonly EventToken Empty = default(EventToken);
	}
}
