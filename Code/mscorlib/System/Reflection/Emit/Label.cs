using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a label in the instruction stream. Label is used in conjunction with the <see cref="T:System.Reflection.Emit.ILGenerator" /> class.</summary>
	// Token: 0x020002E5 RID: 741
	[ComVisible(true)]
	[Serializable]
	public struct Label
	{
		// Token: 0x060025C7 RID: 9671 RVA: 0x00085DF4 File Offset: 0x00083FF4
		internal Label(int val)
		{
			this.label = val;
		}

		/// <summary>Checks if the given object is an instance of Label and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of Label and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this Label instance. </param>
		// Token: 0x060025C8 RID: 9672 RVA: 0x00085E00 File Offset: 0x00084000
		public override bool Equals(object obj)
		{
			bool flag = obj is Label;
			if (flag)
			{
				Label label = (Label)obj;
				flag = (this.label == label.label);
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.Label" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.Label" /> to compare to the current instance.</param>
		// Token: 0x060025C9 RID: 9673 RVA: 0x00085E38 File Offset: 0x00084038
		public bool Equals(Label obj)
		{
			return this.label == obj.label;
		}

		/// <summary>Generates a hash code for this instance.</summary>
		/// <returns>Returns a hash code for this instance.</returns>
		// Token: 0x060025CA RID: 9674 RVA: 0x00085E4C File Offset: 0x0008404C
		public override int GetHashCode()
		{
			return this.label.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060025CB RID: 9675 RVA: 0x00085E5C File Offset: 0x0008405C
		public static bool operator ==(Label a, Label b)
		{
			return a.Equals(b);
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.Label" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.Label" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060025CC RID: 9676 RVA: 0x00085E68 File Offset: 0x00084068
		public static bool operator !=(Label a, Label b)
		{
			return !(a == b);
		}

		// Token: 0x04000E39 RID: 3641
		internal int label;
	}
}
