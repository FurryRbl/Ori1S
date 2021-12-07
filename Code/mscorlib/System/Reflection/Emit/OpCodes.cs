using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Provides field representations of the Microsoft Intermediate Language (MSIL) instructions for emission by the <see cref="T:System.Reflection.Emit.ILGenerator" /> class members (such as <see cref="M:System.Reflection.Emit.ILGenerator.Emit(System.Reflection.Emit.OpCode)" />).</summary>
	// Token: 0x020002F0 RID: 752
	[ComVisible(true)]
	public class OpCodes
	{
		// Token: 0x060026B6 RID: 9910 RVA: 0x00089238 File Offset: 0x00087438
		internal OpCodes()
		{
		}

		/// <summary>Returns true or false if the supplied opcode takes a single byte argument.</summary>
		/// <returns>True or false.</returns>
		/// <param name="inst">An instance of an Opcode object. </param>
		// Token: 0x060026B8 RID: 9912 RVA: 0x0008A3F8 File Offset: 0x000885F8
		public static bool TakesSingleByteArgument(OpCode inst)
		{
			OperandType operandType = inst.OperandType;
			return operandType == OperandType.ShortInlineBrTarget || operandType == OperandType.ShortInlineI || operandType == OperandType.ShortInlineR || operandType == OperandType.ShortInlineVar;
		}

		/// <summary>Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.</summary>
		// Token: 0x04000E88 RID: 3720
		public static readonly OpCode Nop = new OpCode(1179903, 84215041);

		/// <summary>Signals the Common Language Infrastructure (CLI) to inform the debugger that a break point has been tripped.</summary>
		// Token: 0x04000E89 RID: 3721
		public static readonly OpCode Break = new OpCode(1180159, 17106177);

		/// <summary>Loads the argument at index 0 onto the evaluation stack.</summary>
		// Token: 0x04000E8A RID: 3722
		public static readonly OpCode Ldarg_0 = new OpCode(1245951, 84214017);

		/// <summary>Loads the argument at index 1 onto the evaluation stack.</summary>
		// Token: 0x04000E8B RID: 3723
		public static readonly OpCode Ldarg_1 = new OpCode(1246207, 84214017);

		/// <summary>Loads the argument at index 2 onto the evaluation stack.</summary>
		// Token: 0x04000E8C RID: 3724
		public static readonly OpCode Ldarg_2 = new OpCode(1246463, 84214017);

		/// <summary>Loads the argument at index 3 onto the evaluation stack.</summary>
		// Token: 0x04000E8D RID: 3725
		public static readonly OpCode Ldarg_3 = new OpCode(1246719, 84214017);

		/// <summary>Loads the local variable at index 0 onto the evaluation stack.</summary>
		// Token: 0x04000E8E RID: 3726
		public static readonly OpCode Ldloc_0 = new OpCode(1246975, 84214017);

		/// <summary>Loads the local variable at index 1 onto the evaluation stack.</summary>
		// Token: 0x04000E8F RID: 3727
		public static readonly OpCode Ldloc_1 = new OpCode(1247231, 84214017);

		/// <summary>Loads the local variable at index 2 onto the evaluation stack.</summary>
		// Token: 0x04000E90 RID: 3728
		public static readonly OpCode Ldloc_2 = new OpCode(1247487, 84214017);

		/// <summary>Loads the local variable at index 3 onto the evaluation stack.</summary>
		// Token: 0x04000E91 RID: 3729
		public static readonly OpCode Ldloc_3 = new OpCode(1247743, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 0.</summary>
		// Token: 0x04000E92 RID: 3730
		public static readonly OpCode Stloc_0 = new OpCode(17959679, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 1.</summary>
		// Token: 0x04000E93 RID: 3731
		public static readonly OpCode Stloc_1 = new OpCode(17959935, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 2.</summary>
		// Token: 0x04000E94 RID: 3732
		public static readonly OpCode Stloc_2 = new OpCode(17960191, 84214017);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at index 3.</summary>
		// Token: 0x04000E95 RID: 3733
		public static readonly OpCode Stloc_3 = new OpCode(17960447, 84214017);

		/// <summary>Loads the argument (referenced by a specified short form index) onto the evaluation stack.</summary>
		// Token: 0x04000E96 RID: 3734
		public static readonly OpCode Ldarg_S = new OpCode(1249023, 85065985);

		/// <summary>Load an argument address, in short form, onto the evaluation stack.</summary>
		// Token: 0x04000E97 RID: 3735
		public static readonly OpCode Ldarga_S = new OpCode(1380351, 85065985);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index, short form.</summary>
		// Token: 0x04000E98 RID: 3736
		public static readonly OpCode Starg_S = new OpCode(17961215, 85065985);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x04000E99 RID: 3737
		public static readonly OpCode Ldloc_S = new OpCode(1249791, 85065985);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack, short form.</summary>
		// Token: 0x04000E9A RID: 3738
		public static readonly OpCode Ldloca_S = new OpCode(1381119, 85065985);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at <paramref name="index" /> (short form).</summary>
		// Token: 0x04000E9B RID: 3739
		public static readonly OpCode Stloc_S = new OpCode(17961983, 85065985);

		/// <summary>Pushes a null reference (type O) onto the evaluation stack.</summary>
		// Token: 0x04000E9C RID: 3740
		public static readonly OpCode Ldnull = new OpCode(1643775, 84215041);

		/// <summary>Pushes the integer value of -1 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000E9D RID: 3741
		public static readonly OpCode Ldc_I4_M1 = new OpCode(1381887, 84214017);

		/// <summary>Pushes the integer value of 0 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000E9E RID: 3742
		public static readonly OpCode Ldc_I4_0 = new OpCode(1382143, 84214017);

		/// <summary>Pushes the integer value of 1 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000E9F RID: 3743
		public static readonly OpCode Ldc_I4_1 = new OpCode(1382399, 84214017);

		/// <summary>Pushes the integer value of 2 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA0 RID: 3744
		public static readonly OpCode Ldc_I4_2 = new OpCode(1382655, 84214017);

		/// <summary>Pushes the integer value of 3 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA1 RID: 3745
		public static readonly OpCode Ldc_I4_3 = new OpCode(1382911, 84214017);

		/// <summary>Pushes the integer value of 4 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA2 RID: 3746
		public static readonly OpCode Ldc_I4_4 = new OpCode(1383167, 84214017);

		/// <summary>Pushes the integer value of 5 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA3 RID: 3747
		public static readonly OpCode Ldc_I4_5 = new OpCode(1383423, 84214017);

		/// <summary>Pushes the integer value of 6 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA4 RID: 3748
		public static readonly OpCode Ldc_I4_6 = new OpCode(1383679, 84214017);

		/// <summary>Pushes the integer value of 7 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA5 RID: 3749
		public static readonly OpCode Ldc_I4_7 = new OpCode(1383935, 84214017);

		/// <summary>Pushes the integer value of 8 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA6 RID: 3750
		public static readonly OpCode Ldc_I4_8 = new OpCode(1384191, 84214017);

		/// <summary>Pushes the supplied int8 value onto the evaluation stack as an int32, short form.</summary>
		// Token: 0x04000EA7 RID: 3751
		public static readonly OpCode Ldc_I4_S = new OpCode(1384447, 84934913);

		/// <summary>Pushes a supplied value of type int32 onto the evaluation stack as an int32.</summary>
		// Token: 0x04000EA8 RID: 3752
		public static readonly OpCode Ldc_I4 = new OpCode(1384703, 84018433);

		/// <summary>Pushes a supplied value of type int64 onto the evaluation stack as an int64.</summary>
		// Token: 0x04000EA9 RID: 3753
		public static readonly OpCode Ldc_I8 = new OpCode(1450495, 84083969);

		/// <summary>Pushes a supplied value of type float32 onto the evaluation stack as type F (float).</summary>
		// Token: 0x04000EAA RID: 3754
		public static readonly OpCode Ldc_R4 = new OpCode(1516287, 85001473);

		/// <summary>Pushes a supplied value of type float64 onto the evaluation stack as type F (float).</summary>
		// Token: 0x04000EAB RID: 3755
		public static readonly OpCode Ldc_R8 = new OpCode(1582079, 84346113);

		/// <summary>Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.</summary>
		// Token: 0x04000EAC RID: 3756
		public static readonly OpCode Dup = new OpCode(18097663, 84215041);

		/// <summary>Removes the value currently on top of the evaluation stack.</summary>
		// Token: 0x04000EAD RID: 3757
		public static readonly OpCode Pop = new OpCode(17966847, 84215041);

		/// <summary>Exits current method and jumps to specified method.</summary>
		// Token: 0x04000EAE RID: 3758
		public static readonly OpCode Jmp = new OpCode(1189887, 33817857);

		/// <summary>Calls the method indicated by the passed method descriptor.</summary>
		// Token: 0x04000EAF RID: 3759
		public static readonly OpCode Call = new OpCode(437987583, 33817857);

		/// <summary>Calls the method indicated on the evaluation stack (as a pointer to an entry point) with arguments described by a calling convention.</summary>
		// Token: 0x04000EB0 RID: 3760
		public static readonly OpCode Calli = new OpCode(437987839, 34145537);

		/// <summary>Returns from the current method, pushing a return value (if present) from the callee's evaluation stack onto the caller's evaluation stack.</summary>
		// Token: 0x04000EB1 RID: 3761
		public static readonly OpCode Ret = new OpCode(437398271, 117769473);

		/// <summary>Unconditionally transfers control to a target instruction (short form).</summary>
		// Token: 0x04000EB2 RID: 3762
		public static readonly OpCode Br_S = new OpCode(1190911, 983297);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is false, a null reference, or zero.</summary>
		// Token: 0x04000EB3 RID: 3763
		public static readonly OpCode Brfalse_S = new OpCode(51522815, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if <paramref name="value" /> is true, not null, or non-zero.</summary>
		// Token: 0x04000EB4 RID: 3764
		public static readonly OpCode Brtrue_S = new OpCode(51523071, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if two values are equal.</summary>
		// Token: 0x04000EB5 RID: 3765
		public static readonly OpCode Beq_S = new OpCode(34746111, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than or equal to the second value.</summary>
		// Token: 0x04000EB6 RID: 3766
		public static readonly OpCode Bge_S = new OpCode(34746367, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value.</summary>
		// Token: 0x04000EB7 RID: 3767
		public static readonly OpCode Bgt_S = new OpCode(34746623, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value.</summary>
		// Token: 0x04000EB8 RID: 3768
		public static readonly OpCode Ble_S = new OpCode(34746879, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value.</summary>
		// Token: 0x04000EB9 RID: 3769
		public static readonly OpCode Blt_S = new OpCode(34747135, 51314945);

		/// <summary>Transfers control to a target instruction (short form) when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x04000EBA RID: 3770
		public static readonly OpCode Bne_Un_S = new OpCode(34747391, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EBB RID: 3771
		public static readonly OpCode Bge_Un_S = new OpCode(34747647, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EBC RID: 3772
		public static readonly OpCode Bgt_Un_S = new OpCode(34747903, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EBD RID: 3773
		public static readonly OpCode Ble_Un_S = new OpCode(34748159, 51314945);

		/// <summary>Transfers control to a target instruction (short form) if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EBE RID: 3774
		public static readonly OpCode Blt_Un_S = new OpCode(34748415, 51314945);

		/// <summary>Unconditionally transfers control to a target instruction.</summary>
		// Token: 0x04000EBF RID: 3775
		public static readonly OpCode Br = new OpCode(1194239, 1281);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is false, a null reference (Nothing in Visual Basic), or zero.</summary>
		// Token: 0x04000EC0 RID: 3776
		public static readonly OpCode Brfalse = new OpCode(51526143, 50332929);

		/// <summary>Transfers control to a target instruction if <paramref name="value" /> is true, not null, or non-zero.</summary>
		// Token: 0x04000EC1 RID: 3777
		public static readonly OpCode Brtrue = new OpCode(51526399, 50332929);

		/// <summary>Transfers control to a target instruction if two values are equal.</summary>
		// Token: 0x04000EC2 RID: 3778
		public static readonly OpCode Beq = new OpCode(34749439, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than or equal to the second value.</summary>
		// Token: 0x04000EC3 RID: 3779
		public static readonly OpCode Bge = new OpCode(34749695, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value.</summary>
		// Token: 0x04000EC4 RID: 3780
		public static readonly OpCode Bgt = new OpCode(34749951, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value.</summary>
		// Token: 0x04000EC5 RID: 3781
		public static readonly OpCode Ble = new OpCode(34750207, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value.</summary>
		// Token: 0x04000EC6 RID: 3782
		public static readonly OpCode Blt = new OpCode(34750463, 50331905);

		/// <summary>Transfers control to a target instruction when two unsigned integer values or unordered float values are not equal.</summary>
		// Token: 0x04000EC7 RID: 3783
		public static readonly OpCode Bne_Un = new OpCode(34750719, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EC8 RID: 3784
		public static readonly OpCode Bge_Un = new OpCode(34750975, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is greater than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000EC9 RID: 3785
		public static readonly OpCode Bgt_Un = new OpCode(34751231, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than or equal to the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000ECA RID: 3786
		public static readonly OpCode Ble_Un = new OpCode(34751487, 50331905);

		/// <summary>Transfers control to a target instruction if the first value is less than the second value, when comparing unsigned integer values or unordered float values.</summary>
		// Token: 0x04000ECB RID: 3787
		public static readonly OpCode Blt_Un = new OpCode(34751743, 50331905);

		/// <summary>Implements a jump table.</summary>
		// Token: 0x04000ECC RID: 3788
		public static readonly OpCode Switch = new OpCode(51529215, 51053825);

		/// <summary>Loads a value of type int8 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ECD RID: 3789
		public static readonly OpCode Ldind_I1 = new OpCode(51726079, 84215041);

		/// <summary>Loads a value of type unsigned int8 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ECE RID: 3790
		public static readonly OpCode Ldind_U1 = new OpCode(51726335, 84215041);

		/// <summary>Loads a value of type int16 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ECF RID: 3791
		public static readonly OpCode Ldind_I2 = new OpCode(51726591, 84215041);

		/// <summary>Loads a value of type unsigned int16 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED0 RID: 3792
		public static readonly OpCode Ldind_U2 = new OpCode(51726847, 84215041);

		/// <summary>Loads a value of type int32 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED1 RID: 3793
		public static readonly OpCode Ldind_I4 = new OpCode(51727103, 84215041);

		/// <summary>Loads a value of type unsigned int32 as an int32 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED2 RID: 3794
		public static readonly OpCode Ldind_U4 = new OpCode(51727359, 84215041);

		/// <summary>Loads a value of type int64 as an int64 onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED3 RID: 3795
		public static readonly OpCode Ldind_I8 = new OpCode(51793151, 84215041);

		/// <summary>Loads a value of type native int as a native int onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED4 RID: 3796
		public static readonly OpCode Ldind_I = new OpCode(51727871, 84215041);

		/// <summary>Loads a value of type float32 as a type F (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED5 RID: 3797
		public static readonly OpCode Ldind_R4 = new OpCode(51859199, 84215041);

		/// <summary>Loads a value of type float64 as a type F (float) onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED6 RID: 3798
		public static readonly OpCode Ldind_R8 = new OpCode(51924991, 84215041);

		/// <summary>Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.</summary>
		// Token: 0x04000ED7 RID: 3799
		public static readonly OpCode Ldind_Ref = new OpCode(51990783, 84215041);

		/// <summary>Stores a object reference value at a supplied address.</summary>
		// Token: 0x04000ED8 RID: 3800
		public static readonly OpCode Stind_Ref = new OpCode(85086719, 84215041);

		/// <summary>Stores a value of type int8 at a supplied address.</summary>
		// Token: 0x04000ED9 RID: 3801
		public static readonly OpCode Stind_I1 = new OpCode(85086975, 84215041);

		/// <summary>Stores a value of type int16 at a supplied address.</summary>
		// Token: 0x04000EDA RID: 3802
		public static readonly OpCode Stind_I2 = new OpCode(85087231, 84215041);

		/// <summary>Stores a value of type int32 at a supplied address.</summary>
		// Token: 0x04000EDB RID: 3803
		public static readonly OpCode Stind_I4 = new OpCode(85087487, 84215041);

		/// <summary>Stores a value of type int64 at a supplied address.</summary>
		// Token: 0x04000EDC RID: 3804
		public static readonly OpCode Stind_I8 = new OpCode(101864959, 84215041);

		/// <summary>Stores a value of type float32 at a supplied address.</summary>
		// Token: 0x04000EDD RID: 3805
		public static readonly OpCode Stind_R4 = new OpCode(135419647, 84215041);

		/// <summary>Stores a value of type float64 at a supplied address.</summary>
		// Token: 0x04000EDE RID: 3806
		public static readonly OpCode Stind_R8 = new OpCode(152197119, 84215041);

		/// <summary>Adds two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000EDF RID: 3807
		public static readonly OpCode Add = new OpCode(34822399, 84215041);

		/// <summary>Subtracts one value from another and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000EE0 RID: 3808
		public static readonly OpCode Sub = new OpCode(34822655, 84215041);

		/// <summary>Multiplies two values and pushes the result on the evaluation stack.</summary>
		// Token: 0x04000EE1 RID: 3809
		public static readonly OpCode Mul = new OpCode(34822911, 84215041);

		/// <summary>Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.</summary>
		// Token: 0x04000EE2 RID: 3810
		public static readonly OpCode Div = new OpCode(34823167, 84215041);

		/// <summary>Divides two unsigned integer values and pushes the result (int32) onto the evaluation stack.</summary>
		// Token: 0x04000EE3 RID: 3811
		public static readonly OpCode Div_Un = new OpCode(34823423, 84215041);

		/// <summary>Divides two values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x04000EE4 RID: 3812
		public static readonly OpCode Rem = new OpCode(34823679, 84215041);

		/// <summary>Divides two unsigned values and pushes the remainder onto the evaluation stack.</summary>
		// Token: 0x04000EE5 RID: 3813
		public static readonly OpCode Rem_Un = new OpCode(34823935, 84215041);

		/// <summary>Computes the bitwise AND of two values and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000EE6 RID: 3814
		public static readonly OpCode And = new OpCode(34824191, 84215041);

		/// <summary>Compute the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000EE7 RID: 3815
		public static readonly OpCode Or = new OpCode(34824447, 84215041);

		/// <summary>Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04000EE8 RID: 3816
		public static readonly OpCode Xor = new OpCode(34824703, 84215041);

		/// <summary>Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04000EE9 RID: 3817
		public static readonly OpCode Shl = new OpCode(34824959, 84215041);

		/// <summary>Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04000EEA RID: 3818
		public static readonly OpCode Shr = new OpCode(34825215, 84215041);

		/// <summary>Shifts an unsigned integer value (in zeroes) to the right by a specified number of bits, pushing the result onto the evaluation stack.</summary>
		// Token: 0x04000EEB RID: 3819
		public static readonly OpCode Shr_Un = new OpCode(34825471, 84215041);

		/// <summary>Negates a value and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000EEC RID: 3820
		public static readonly OpCode Neg = new OpCode(18048511, 84215041);

		/// <summary>Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.</summary>
		// Token: 0x04000EED RID: 3821
		public static readonly OpCode Not = new OpCode(18048767, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int8, then extends (pads) it to int32.</summary>
		// Token: 0x04000EEE RID: 3822
		public static readonly OpCode Conv_I1 = new OpCode(18180095, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int16, then extends (pads) it to int32.</summary>
		// Token: 0x04000EEF RID: 3823
		public static readonly OpCode Conv_I2 = new OpCode(18180351, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int32.</summary>
		// Token: 0x04000EF0 RID: 3824
		public static readonly OpCode Conv_I4 = new OpCode(18180607, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to int64.</summary>
		// Token: 0x04000EF1 RID: 3825
		public static readonly OpCode Conv_I8 = new OpCode(18246399, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to float32.</summary>
		// Token: 0x04000EF2 RID: 3826
		public static readonly OpCode Conv_R4 = new OpCode(18312191, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to float64.</summary>
		// Token: 0x04000EF3 RID: 3827
		public static readonly OpCode Conv_R8 = new OpCode(18377983, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int32, and extends it to int32.</summary>
		// Token: 0x04000EF4 RID: 3828
		public static readonly OpCode Conv_U4 = new OpCode(18181631, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int64, and extends it to int64.</summary>
		// Token: 0x04000EF5 RID: 3829
		public static readonly OpCode Conv_U8 = new OpCode(18247423, 84215041);

		/// <summary>Calls a late-bound method on an object, pushing the return value onto the evaluation stack.</summary>
		// Token: 0x04000EF6 RID: 3830
		public static readonly OpCode Callvirt = new OpCode(438005759, 33817345);

		/// <summary>Copies the value type located at the address of an object (type &amp;, * or native int) to the address of the destination object (type &amp;, * or native int).</summary>
		// Token: 0x04000EF7 RID: 3831
		public static readonly OpCode Cpobj = new OpCode(85094655, 84738817);

		/// <summary>Copies the value type object pointed to by an address to the top of the evaluation stack.</summary>
		// Token: 0x04000EF8 RID: 3832
		public static readonly OpCode Ldobj = new OpCode(51606015, 84738817);

		/// <summary>Pushes a new object reference to a string literal stored in the metadata.</summary>
		// Token: 0x04000EF9 RID: 3833
		public static readonly OpCode Ldstr = new OpCode(1667839, 84542209);

		/// <summary>Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.</summary>
		// Token: 0x04000EFA RID: 3834
		public static readonly OpCode Newobj = new OpCode(437875711, 33817345);

		/// <summary>Attempts to cast an object passed by reference to the specified class.</summary>
		// Token: 0x04000EFB RID: 3835
		[ComVisible(true)]
		public static readonly OpCode Castclass = new OpCode(169440511, 84738817);

		/// <summary>Tests whether an object reference (type O) is an instance of a particular class.</summary>
		// Token: 0x04000EFC RID: 3836
		public static readonly OpCode Isinst = new OpCode(169178623, 84738817);

		/// <summary>Converts the unsigned integer value on top of the evaluation stack to float32.</summary>
		// Token: 0x04000EFD RID: 3837
		public static readonly OpCode Conv_R_Un = new OpCode(18380543, 84215041);

		/// <summary>Converts the boxed representation of a value type to its unboxed form.</summary>
		// Token: 0x04000EFE RID: 3838
		public static readonly OpCode Unbox = new OpCode(169179647, 84739329);

		/// <summary>Throws the exception object currently on the evaluation stack.</summary>
		// Token: 0x04000EFF RID: 3839
		public static readonly OpCode Throw = new OpCode(168983295, 134546177);

		/// <summary>Finds the value of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x04000F00 RID: 3840
		public static readonly OpCode Ldfld = new OpCode(169049087, 83952385);

		/// <summary>Finds the address of a field in the object whose reference is currently on the evaluation stack.</summary>
		// Token: 0x04000F01 RID: 3841
		public static readonly OpCode Ldflda = new OpCode(169180415, 83952385);

		/// <summary>Replaces the value stored in the field of an object reference or pointer with a new value.</summary>
		// Token: 0x04000F02 RID: 3842
		public static readonly OpCode Stfld = new OpCode(185761279, 83952385);

		/// <summary>Pushes the value of a static field onto the evaluation stack.</summary>
		// Token: 0x04000F03 RID: 3843
		public static readonly OpCode Ldsfld = new OpCode(1277695, 83952385);

		/// <summary>Pushes the address of a static field onto the evaluation stack.</summary>
		// Token: 0x04000F04 RID: 3844
		public static readonly OpCode Ldsflda = new OpCode(1409023, 83952385);

		/// <summary>Replaces the value of a static field with a value from the evaluation stack.</summary>
		// Token: 0x04000F05 RID: 3845
		public static readonly OpCode Stsfld = new OpCode(17989887, 83952385);

		/// <summary>Copies a value of a specified type from the evaluation stack into a supplied memory address.</summary>
		// Token: 0x04000F06 RID: 3846
		public static readonly OpCode Stobj = new OpCode(68321791, 84739329);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F07 RID: 3847
		public static readonly OpCode Conv_Ovf_I1_Un = new OpCode(18187007, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F08 RID: 3848
		public static readonly OpCode Conv_Ovf_I2_Un = new OpCode(18187263, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F09 RID: 3849
		public static readonly OpCode Conv_Ovf_I4_Un = new OpCode(18187519, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0A RID: 3850
		public static readonly OpCode Conv_Ovf_I8_Un = new OpCode(18253311, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0B RID: 3851
		public static readonly OpCode Conv_Ovf_U1_Un = new OpCode(18188031, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0C RID: 3852
		public static readonly OpCode Conv_Ovf_U2_Un = new OpCode(18188287, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0D RID: 3853
		public static readonly OpCode Conv_Ovf_U4_Un = new OpCode(18188543, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0E RID: 3854
		public static readonly OpCode Conv_Ovf_U8_Un = new OpCode(18254335, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to signed native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F0F RID: 3855
		public static readonly OpCode Conv_Ovf_I_Un = new OpCode(18189055, 84215041);

		/// <summary>Converts the unsigned value on top of the evaluation stack to unsigned native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F10 RID: 3856
		public static readonly OpCode Conv_Ovf_U_Un = new OpCode(18189311, 84215041);

		/// <summary>Converts a value type to an object reference (type O).</summary>
		// Token: 0x04000F11 RID: 3857
		public static readonly OpCode Box = new OpCode(18451711, 84739329);

		/// <summary>Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.</summary>
		// Token: 0x04000F12 RID: 3858
		public static readonly OpCode Newarr = new OpCode(52006399, 84738817);

		/// <summary>Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.</summary>
		// Token: 0x04000F13 RID: 3859
		public static readonly OpCode Ldlen = new OpCode(169185023, 84214529);

		/// <summary>Loads the address of the array element at a specified array index onto the top of the evaluation stack as type &amp; (managed pointer).</summary>
		// Token: 0x04000F14 RID: 3860
		public static readonly OpCode Ldelema = new OpCode(202739711, 84738817);

		/// <summary>Loads the element with type int8 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F15 RID: 3861
		public static readonly OpCode Ldelem_I1 = new OpCode(202739967, 84214529);

		/// <summary>Loads the element with type unsigned int8 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F16 RID: 3862
		public static readonly OpCode Ldelem_U1 = new OpCode(202740223, 84214529);

		/// <summary>Loads the element with type int16 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F17 RID: 3863
		public static readonly OpCode Ldelem_I2 = new OpCode(202740479, 84214529);

		/// <summary>Loads the element with type unsigned int16 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F18 RID: 3864
		public static readonly OpCode Ldelem_U2 = new OpCode(202740735, 84214529);

		/// <summary>Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F19 RID: 3865
		public static readonly OpCode Ldelem_I4 = new OpCode(202740991, 84214529);

		/// <summary>Loads the element with type unsigned int32 at a specified array index onto the top of the evaluation stack as an int32.</summary>
		// Token: 0x04000F1A RID: 3866
		public static readonly OpCode Ldelem_U4 = new OpCode(202741247, 84214529);

		/// <summary>Loads the element with type int64 at a specified array index onto the top of the evaluation stack as an int64.</summary>
		// Token: 0x04000F1B RID: 3867
		public static readonly OpCode Ldelem_I8 = new OpCode(202807039, 84214529);

		/// <summary>Loads the element with type native int at a specified array index onto the top of the evaluation stack as a native int.</summary>
		// Token: 0x04000F1C RID: 3868
		public static readonly OpCode Ldelem_I = new OpCode(202741759, 84214529);

		/// <summary>Loads the element with type float32 at a specified array index onto the top of the evaluation stack as type F (float).</summary>
		// Token: 0x04000F1D RID: 3869
		public static readonly OpCode Ldelem_R4 = new OpCode(202873087, 84214529);

		/// <summary>Loads the element with type float64 at a specified array index onto the top of the evaluation stack as type F (float).</summary>
		// Token: 0x04000F1E RID: 3870
		public static readonly OpCode Ldelem_R8 = new OpCode(202938879, 84214529);

		/// <summary>Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).</summary>
		// Token: 0x04000F1F RID: 3871
		public static readonly OpCode Ldelem_Ref = new OpCode(203004671, 84214529);

		/// <summary>Replaces the array element at a given index with the native int value on the evaluation stack.</summary>
		// Token: 0x04000F20 RID: 3872
		public static readonly OpCode Stelem_I = new OpCode(219323391, 84214529);

		/// <summary>Replaces the array element at a given index with the int8 value on the evaluation stack.</summary>
		// Token: 0x04000F21 RID: 3873
		public static readonly OpCode Stelem_I1 = new OpCode(219323647, 84214529);

		/// <summary>Replaces the array element at a given index with the int16 value on the evaluation stack.</summary>
		// Token: 0x04000F22 RID: 3874
		public static readonly OpCode Stelem_I2 = new OpCode(219323903, 84214529);

		/// <summary>Replaces the array element at a given index with the int32 value on the evaluation stack.</summary>
		// Token: 0x04000F23 RID: 3875
		public static readonly OpCode Stelem_I4 = new OpCode(219324159, 84214529);

		/// <summary>Replaces the array element at a given index with the int64 value on the evaluation stack.</summary>
		// Token: 0x04000F24 RID: 3876
		public static readonly OpCode Stelem_I8 = new OpCode(236101631, 84214529);

		/// <summary>Replaces the array element at a given index with the float32 value on the evaluation stack.</summary>
		// Token: 0x04000F25 RID: 3877
		public static readonly OpCode Stelem_R4 = new OpCode(252879103, 84214529);

		/// <summary>Replaces the array element at a given index with the float64 value on the evaluation stack.</summary>
		// Token: 0x04000F26 RID: 3878
		public static readonly OpCode Stelem_R8 = new OpCode(269656575, 84214529);

		/// <summary>Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.</summary>
		// Token: 0x04000F27 RID: 3879
		public static readonly OpCode Stelem_Ref = new OpCode(286434047, 84214529);

		/// <summary>Loads the element at a specified array index onto the top of the evaluation stack as the type specified in the instruction. </summary>
		// Token: 0x04000F28 RID: 3880
		public static readonly OpCode Ldelem = new OpCode(202613759, 84738817);

		/// <summary>Replaces the array element at a given index with the value on the evaluation stack, whose type is specified in the instruction.</summary>
		// Token: 0x04000F29 RID: 3881
		public static readonly OpCode Stelem = new OpCode(470983935, 84738817);

		/// <summary>Converts the boxed representation of a type specified in the instruction to its unboxed form. </summary>
		// Token: 0x04000F2A RID: 3882
		public static readonly OpCode Unbox_Any = new OpCode(169059839, 84738817);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F2B RID: 3883
		public static readonly OpCode Conv_Ovf_I1 = new OpCode(18199551, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int8 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F2C RID: 3884
		public static readonly OpCode Conv_Ovf_U1 = new OpCode(18199807, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int16 and extending it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F2D RID: 3885
		public static readonly OpCode Conv_Ovf_I2 = new OpCode(18200063, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int16 and extends it to int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F2E RID: 3886
		public static readonly OpCode Conv_Ovf_U2 = new OpCode(18200319, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F2F RID: 3887
		public static readonly OpCode Conv_Ovf_I4 = new OpCode(18200575, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int32, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F30 RID: 3888
		public static readonly OpCode Conv_Ovf_U4 = new OpCode(18200831, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F31 RID: 3889
		public static readonly OpCode Conv_Ovf_I8 = new OpCode(18266623, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned int64, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F32 RID: 3890
		public static readonly OpCode Conv_Ovf_U8 = new OpCode(18266879, 84215041);

		/// <summary>Retrieves the address (type &amp;) embedded in a typed reference.</summary>
		// Token: 0x04000F33 RID: 3891
		public static readonly OpCode Refanyval = new OpCode(18203391, 84739329);

		/// <summary>Throws <see cref="T:System.ArithmeticException" /> if value is not a finite number.</summary>
		// Token: 0x04000F34 RID: 3892
		public static readonly OpCode Ckfinite = new OpCode(18400255, 84215041);

		/// <summary>Pushes a typed reference to an instance of a specific type onto the evaluation stack.</summary>
		// Token: 0x04000F35 RID: 3893
		public static readonly OpCode Mkrefany = new OpCode(51627775, 84739329);

		/// <summary>Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.</summary>
		// Token: 0x04000F36 RID: 3894
		public static readonly OpCode Ldtoken = new OpCode(1429759, 84673793);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int16, and extends it to int32.</summary>
		// Token: 0x04000F37 RID: 3895
		public static readonly OpCode Conv_U2 = new OpCode(18207231, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned int8, and extends it to int32.</summary>
		// Token: 0x04000F38 RID: 3896
		public static readonly OpCode Conv_U1 = new OpCode(18207487, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to native int.</summary>
		// Token: 0x04000F39 RID: 3897
		public static readonly OpCode Conv_I = new OpCode(18207743, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to signed native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F3A RID: 3898
		public static readonly OpCode Conv_Ovf_I = new OpCode(18207999, 84215041);

		/// <summary>Converts the signed value on top of the evaluation stack to unsigned native int, throwing <see cref="T:System.OverflowException" /> on overflow.</summary>
		// Token: 0x04000F3B RID: 3899
		public static readonly OpCode Conv_Ovf_U = new OpCode(18208255, 84215041);

		/// <summary>Adds two integers, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F3C RID: 3900
		public static readonly OpCode Add_Ovf = new OpCode(34854655, 84215041);

		/// <summary>Adds two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F3D RID: 3901
		public static readonly OpCode Add_Ovf_Un = new OpCode(34854911, 84215041);

		/// <summary>Multiplies two integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F3E RID: 3902
		public static readonly OpCode Mul_Ovf = new OpCode(34855167, 84215041);

		/// <summary>Multiplies two unsigned integer values, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F3F RID: 3903
		public static readonly OpCode Mul_Ovf_Un = new OpCode(34855423, 84215041);

		/// <summary>Subtracts one integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F40 RID: 3904
		public static readonly OpCode Sub_Ovf = new OpCode(34855679, 84215041);

		/// <summary>Subtracts one unsigned integer value from another, performs an overflow check, and pushes the result onto the evaluation stack.</summary>
		// Token: 0x04000F41 RID: 3905
		public static readonly OpCode Sub_Ovf_Un = new OpCode(34855935, 84215041);

		/// <summary>Transfers control from the fault or finally clause of an exception block back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x04000F42 RID: 3906
		public static readonly OpCode Endfinally = new OpCode(1236223, 117769473);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a specific target instruction.</summary>
		// Token: 0x04000F43 RID: 3907
		public static readonly OpCode Leave = new OpCode(1236479, 1281);

		/// <summary>Exits a protected region of code, unconditionally transferring control to a target instruction (short form).</summary>
		// Token: 0x04000F44 RID: 3908
		public static readonly OpCode Leave_S = new OpCode(1236735, 984321);

		/// <summary>Stores a value of type native int at a supplied address.</summary>
		// Token: 0x04000F45 RID: 3909
		public static readonly OpCode Stind_I = new OpCode(85123071, 84215041);

		/// <summary>Converts the value on top of the evaluation stack to unsigned native int, and extends it to native int.</summary>
		// Token: 0x04000F46 RID: 3910
		public static readonly OpCode Conv_U = new OpCode(18211071, 84215041);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F47 RID: 3911
		public static readonly OpCode Prefix7 = new OpCode(1243391, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F48 RID: 3912
		public static readonly OpCode Prefix6 = new OpCode(1243647, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F49 RID: 3913
		public static readonly OpCode Prefix5 = new OpCode(1243903, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F4A RID: 3914
		public static readonly OpCode Prefix4 = new OpCode(1244159, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F4B RID: 3915
		public static readonly OpCode Prefix3 = new OpCode(1244415, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F4C RID: 3916
		public static readonly OpCode Prefix2 = new OpCode(1244671, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F4D RID: 3917
		public static readonly OpCode Prefix1 = new OpCode(1244927, 67437057);

		/// <summary>This is a reserved instruction.</summary>
		// Token: 0x04000F4E RID: 3918
		public static readonly OpCode Prefixref = new OpCode(1245183, 67437057);

		/// <summary>Returns an unmanaged pointer to the argument list of the current method.</summary>
		// Token: 0x04000F4F RID: 3919
		public static readonly OpCode Arglist = new OpCode(1376510, 84215042);

		/// <summary>Compares two values. If they are equal, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x04000F50 RID: 3920
		public static readonly OpCode Ceq = new OpCode(34931198, 84215042);

		/// <summary>Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x04000F51 RID: 3921
		public static readonly OpCode Cgt = new OpCode(34931454, 84215042);

		/// <summary>Compares two unsigned or unordered values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x04000F52 RID: 3922
		public static readonly OpCode Cgt_Un = new OpCode(34931710, 84215042);

		/// <summary>Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x04000F53 RID: 3923
		public static readonly OpCode Clt = new OpCode(34931966, 84215042);

		/// <summary>Compares the unsigned or unordered values <paramref name="value1" /> and <paramref name="value2" />. If <paramref name="value1" /> is less than <paramref name="value2" />, then the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
		// Token: 0x04000F54 RID: 3924
		public static readonly OpCode Clt_Un = new OpCode(34932222, 84215042);

		/// <summary>Pushes an unmanaged pointer (type native int) to the native code implementing a specific method onto the evaluation stack.</summary>
		// Token: 0x04000F55 RID: 3925
		public static readonly OpCode Ldftn = new OpCode(1378046, 84149506);

		/// <summary>Pushes an unmanaged pointer (type native int) to the native code implementing a particular virtual method associated with a specified object onto the evaluation stack.</summary>
		// Token: 0x04000F56 RID: 3926
		public static readonly OpCode Ldvirtftn = new OpCode(169150462, 84149506);

		/// <summary>Loads an argument (referenced by a specified index value) onto the stack.</summary>
		// Token: 0x04000F57 RID: 3927
		public static readonly OpCode Ldarg = new OpCode(1247742, 84804866);

		/// <summary>Load an argument address onto the evaluation stack.</summary>
		// Token: 0x04000F58 RID: 3928
		public static readonly OpCode Ldarga = new OpCode(1379070, 84804866);

		/// <summary>Stores the value on top of the evaluation stack in the argument slot at a specified index.</summary>
		// Token: 0x04000F59 RID: 3929
		public static readonly OpCode Starg = new OpCode(17959934, 84804866);

		/// <summary>Loads the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x04000F5A RID: 3930
		public static readonly OpCode Ldloc = new OpCode(1248510, 84804866);

		/// <summary>Loads the address of the local variable at a specific index onto the evaluation stack.</summary>
		// Token: 0x04000F5B RID: 3931
		public static readonly OpCode Ldloca = new OpCode(1379838, 84804866);

		/// <summary>Pops the current value from the top of the evaluation stack and stores it in a the local variable list at a specified index.</summary>
		// Token: 0x04000F5C RID: 3932
		public static readonly OpCode Stloc = new OpCode(17960702, 84804866);

		/// <summary>Allocates a certain number of bytes from the local dynamic memory pool and pushes the address (a transient pointer, type *) of the first allocated byte onto the evaluation stack.</summary>
		// Token: 0x04000F5D RID: 3933
		public static readonly OpCode Localloc = new OpCode(51711998, 84215042);

		/// <summary>Transfers control from the filter clause of an exception back to the Common Language Infrastructure (CLI) exception handler.</summary>
		// Token: 0x04000F5E RID: 3934
		public static readonly OpCode Endfilter = new OpCode(51515902, 117769474);

		/// <summary>Indicates that an address currently atop the evaluation stack might not be aligned to the natural size of the immediately following ldind, stind, ldfld, stfld, ldobj, stobj, initblk, or cpblk instruction.</summary>
		// Token: 0x04000F5F RID: 3935
		public static readonly OpCode Unaligned = new OpCode(1184510, 68158466);

		/// <summary>Specifies that an address currently atop the evaluation stack might be volatile, and the results of reading that location cannot be cached or that multiple stores to that location cannot be suppressed.</summary>
		// Token: 0x04000F60 RID: 3936
		public static readonly OpCode Volatile = new OpCode(1184766, 67437570);

		/// <summary>Performs a postfixed method call instruction such that the current method's stack frame is removed before the actual call instruction is executed.</summary>
		// Token: 0x04000F61 RID: 3937
		public static readonly OpCode Tailcall = new OpCode(1185022, 67437570);

		/// <summary>Initializes each field of the value type at a specified address to a null reference or a 0 of the appropriate primitive type.</summary>
		// Token: 0x04000F62 RID: 3938
		public static readonly OpCode Initobj = new OpCode(51516926, 84738818);

		/// <summary>Constrains the type on which a virtual method call is made.</summary>
		// Token: 0x04000F63 RID: 3939
		public static readonly OpCode Constrained = new OpCode(1185534, 67961858);

		/// <summary>Copies a specified number bytes from a source address to a destination address.</summary>
		// Token: 0x04000F64 RID: 3940
		public static readonly OpCode Cpblk = new OpCode(118626302, 84215042);

		/// <summary>Initializes a specified block of memory at a specific address to a given size and initial value.</summary>
		// Token: 0x04000F65 RID: 3941
		public static readonly OpCode Initblk = new OpCode(118626558, 84215042);

		/// <summary>Rethrows the current exception.</summary>
		// Token: 0x04000F66 RID: 3942
		public static readonly OpCode Rethrow = new OpCode(1186558, 134546178);

		/// <summary>Pushes the size, in bytes, of a supplied value type onto the evaluation stack.</summary>
		// Token: 0x04000F67 RID: 3943
		public static readonly OpCode Sizeof = new OpCode(1383678, 84739330);

		/// <summary>Retrieves the type token embedded in a typed reference.</summary>
		// Token: 0x04000F68 RID: 3944
		public static readonly OpCode Refanytype = new OpCode(18161150, 84215042);

		/// <summary>Specifies that the subsequent array address operation performs no type check at run time, and that it returns a managed pointer whose mutability is restricted.</summary>
		// Token: 0x04000F69 RID: 3945
		public static readonly OpCode Readonly = new OpCode(1187582, 67437570);
	}
}
