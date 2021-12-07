using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes a Microsoft intermediate language (MSIL) instruction.</summary>
	// Token: 0x020002EF RID: 751
	[ComVisible(true)]
	public struct OpCode
	{
		// Token: 0x060026A7 RID: 9895 RVA: 0x00089024 File Offset: 0x00087224
		internal OpCode(int p, int q)
		{
			this.op1 = (byte)(p & 255);
			this.op2 = (byte)(p >> 8 & 255);
			this.push = (byte)(p >> 16 & 255);
			this.pop = (byte)(p >> 24 & 255);
			this.size = (byte)(q & 255);
			this.type = (byte)(q >> 8 & 255);
			this.args = (byte)(q >> 16 & 255);
			this.flow = (byte)(q >> 24 & 255);
		}

		/// <summary>Returns the generated hash code for this Opcode.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x060026A8 RID: 9896 RVA: 0x000890B4 File Offset: 0x000872B4
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		/// <summary>Tests whether the given object is equal to this Opcode.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of Opcode and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x060026A9 RID: 9897 RVA: 0x000890C4 File Offset: 0x000872C4
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpCode))
			{
				return false;
			}
			OpCode opCode = (OpCode)obj;
			return opCode.op1 == this.op1 && opCode.op2 == this.op2;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.OpCode" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to the current instance.</param>
		// Token: 0x060026AA RID: 9898 RVA: 0x00089110 File Offset: 0x00087310
		public bool Equals(OpCode obj)
		{
			return obj.op1 == this.op1 && obj.op2 == this.op2;
		}

		/// <summary>Returns this Opcode as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name of this Opcode.</returns>
		// Token: 0x060026AB RID: 9899 RVA: 0x00089144 File Offset: 0x00087344
		public override string ToString()
		{
			return this.Name;
		}

		/// <summary>The name of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The name of the MSIL instruction.</returns>
		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060026AC RID: 9900 RVA: 0x0008914C File Offset: 0x0008734C
		public string Name
		{
			get
			{
				if (this.op1 == 255)
				{
					return OpCodeNames.names[(int)this.op2];
				}
				return OpCodeNames.names[256 + (int)this.op2];
			}
		}

		/// <summary>The size of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The size of the MSIL instruction.</returns>
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060026AD RID: 9901 RVA: 0x00089180 File Offset: 0x00087380
		public int Size
		{
			get
			{
				return (int)this.size;
			}
		}

		/// <summary>The type of Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of Microsoft intermediate language (MSIL) instruction.</returns>
		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060026AE RID: 9902 RVA: 0x00089188 File Offset: 0x00087388
		public OpCodeType OpCodeType
		{
			get
			{
				return (OpCodeType)this.type;
			}
		}

		/// <summary>The operand type of an Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The operand type of an MSIL instruction.</returns>
		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060026AF RID: 9903 RVA: 0x00089190 File Offset: 0x00087390
		public OperandType OperandType
		{
			get
			{
				return (OperandType)this.args;
			}
		}

		/// <summary>The flow control characteristics of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The type of flow control.</returns>
		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060026B0 RID: 9904 RVA: 0x00089198 File Offset: 0x00087398
		public FlowControl FlowControl
		{
			get
			{
				return (FlowControl)this.flow;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pops the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pops the stack.</returns>
		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x000891A0 File Offset: 0x000873A0
		public StackBehaviour StackBehaviourPop
		{
			get
			{
				return (StackBehaviour)this.pop;
			}
		}

		/// <summary>How the Microsoft intermediate language (MSIL) instruction pushes operand onto the stack.</summary>
		/// <returns>Read-only. The way the MSIL instruction pushes operand onto the stack.</returns>
		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x000891A8 File Offset: 0x000873A8
		public StackBehaviour StackBehaviourPush
		{
			get
			{
				return (StackBehaviour)this.push;
			}
		}

		/// <summary>The value of the immediate operand of the Microsoft intermediate language (MSIL) instruction.</summary>
		/// <returns>Read-only. The value of the immediate operand of the MSIL instruction.</returns>
		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x000891B0 File Offset: 0x000873B0
		public short Value
		{
			get
			{
				if (this.size == 1)
				{
					return (short)this.op2;
				}
				return (short)((int)this.op1 << 8 | (int)this.op2);
			}
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> is equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060026B4 RID: 9908 RVA: 0x000891D8 File Offset: 0x000873D8
		public static bool operator ==(OpCode a, OpCode b)
		{
			return a.op1 == b.op1 && a.op2 == b.op2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Emit.OpCode" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> is not equal to <paramref name="b" />; otherwise, false.</returns>
		/// <param name="a">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="b" />.</param>
		/// <param name="b">The <see cref="T:System.Reflection.Emit.OpCode" /> to compare to <paramref name="a" />.</param>
		// Token: 0x060026B5 RID: 9909 RVA: 0x0008920C File Offset: 0x0008740C
		public static bool operator !=(OpCode a, OpCode b)
		{
			return a.op1 != b.op1 || a.op2 != b.op2;
		}

		// Token: 0x04000E80 RID: 3712
		internal byte op1;

		// Token: 0x04000E81 RID: 3713
		internal byte op2;

		// Token: 0x04000E82 RID: 3714
		private byte push;

		// Token: 0x04000E83 RID: 3715
		private byte pop;

		// Token: 0x04000E84 RID: 3716
		private byte size;

		// Token: 0x04000E85 RID: 3717
		private byte type;

		// Token: 0x04000E86 RID: 3718
		private byte args;

		// Token: 0x04000E87 RID: 3719
		private byte flow;
	}
}
