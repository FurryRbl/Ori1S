using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020000DC RID: 220
	[RequiredByNativeCode]
	public class Playable : IDisposable
	{
		// Token: 0x06000E1D RID: 3613 RVA: 0x00011E5C File Offset: 0x0001005C
		public Playable()
		{
			this.m_Ptr = IntPtr.Zero;
			this.m_UniqueId = this.GenerateUniqueId();
			this.InstantiateEnginePlayable();
		}

		// Token: 0x06000E1E RID: 3614 RVA: 0x00011E84 File Offset: 0x00010084
		internal Playable(bool callCPPConstructor)
		{
			this.m_Ptr = IntPtr.Zero;
			this.m_UniqueId = this.GenerateUniqueId();
			if (callCPPConstructor)
			{
				this.InstantiateEnginePlayable();
			}
		}

		// Token: 0x06000E1F RID: 3615 RVA: 0x00011EB0 File Offset: 0x000100B0
		private void Dispose(bool disposing)
		{
			this.ReleaseEnginePlayable();
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06000E20 RID: 3616
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetUniqueIDInternal();

		// Token: 0x06000E21 RID: 3617 RVA: 0x00011EC4 File Offset: 0x000100C4
		public static bool Connect(Playable source, Playable target)
		{
			return Playable.Connect(source, target, -1, -1);
		}

		// Token: 0x06000E22 RID: 3618 RVA: 0x00011ED0 File Offset: 0x000100D0
		public static bool Connect(Playable source, Playable target, int sourceOutputPort, int targetInputPort)
		{
			return (Playable.CheckPlayableValidity(source, "source") || Playable.CheckPlayableValidity(target, "target")) && (!(source != null) || source.CheckInputBounds(sourceOutputPort, true)) && target.CheckInputBounds(targetInputPort, true) && Playable.ConnectInternal(source, target, sourceOutputPort, targetInputPort);
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x00011F34 File Offset: 0x00010134
		public static void Disconnect(Playable target, int inputPort)
		{
			if (!Playable.CheckPlayableValidity(target, "target"))
			{
				return;
			}
			if (!target.CheckInputBounds(inputPort))
			{
				return;
			}
			Playable.DisconnectInternal(target, inputPort);
		}

		// Token: 0x06000E24 RID: 3620
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void ReleaseEnginePlayable();

		// Token: 0x06000E25 RID: 3621
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InstantiateEnginePlayable();

		// Token: 0x06000E26 RID: 3622
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int GenerateUniqueId();

		// Token: 0x06000E27 RID: 3623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool SetInputWeightInternal(int inputIndex, float weight);

		// Token: 0x06000E28 RID: 3624
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern float GetInputWeightInternal(int inputIndex);

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000E29 RID: 3625
		// (set) Token: 0x06000E2A RID: 3626
		public extern double time { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000E2B RID: 3627
		// (set) Token: 0x06000E2C RID: 3628
		public extern PlayState state { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000E2D RID: 3629
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool ConnectInternal(Playable source, Playable target, int sourceOutputPort, int targetInputPort);

		// Token: 0x06000E2E RID: 3630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void DisconnectInternal(Playable target, int inputPort);

		// Token: 0x06000E2F RID: 3631
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Playable GetInput(int inputPort);

		// Token: 0x06000E30 RID: 3632
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Playable[] GetInputs();

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000E31 RID: 3633
		public extern int inputCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000E32 RID: 3634
		public extern int outputCount { [WrapperlessIcall] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06000E33 RID: 3635
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void ClearInputs();

		// Token: 0x06000E34 RID: 3636
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Playable GetOutput(int outputPort);

		// Token: 0x06000E35 RID: 3637
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Playable[] GetOutputs();

		// Token: 0x06000E36 RID: 3638
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetInputsInternal(object list);

		// Token: 0x06000E37 RID: 3639
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetOutputsInternal(object list);

		// Token: 0x06000E38 RID: 3640 RVA: 0x00011F5C File Offset: 0x0001015C
		~Playable()
		{
			this.Dispose(false);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x00011F98 File Offset: 0x00010198
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00011FA8 File Offset: 0x000101A8
		public override bool Equals(object p)
		{
			return Playable.CompareIntPtr(this, p as Playable);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00011FB8 File Offset: 0x000101B8
		public override int GetHashCode()
		{
			return this.m_UniqueId;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x00011FC0 File Offset: 0x000101C0
		internal static bool CompareIntPtr(Playable lhs, Playable rhs)
		{
			bool flag = lhs == null || !Playable.IsNativePlayableAlive(lhs);
			bool flag2 = rhs == null || !Playable.IsNativePlayableAlive(rhs);
			if (flag2 && flag)
			{
				return true;
			}
			if (flag2)
			{
				return !Playable.IsNativePlayableAlive(lhs);
			}
			if (flag)
			{
				return !Playable.IsNativePlayableAlive(rhs);
			}
			return lhs.GetUniqueIDInternal() == rhs.GetUniqueIDInternal();
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00012030 File Offset: 0x00010230
		internal static bool IsNativePlayableAlive(Playable p)
		{
			return p.m_Ptr != IntPtr.Zero;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00012044 File Offset: 0x00010244
		internal static bool CheckPlayableValidity(Playable playable, string name)
		{
			if (playable == null)
			{
				throw new NullReferenceException("Playable " + name + "is null");
			}
			return true;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001206C File Offset: 0x0001026C
		internal bool CheckInputBounds(int inputIndex)
		{
			return this.CheckInputBounds(inputIndex, false);
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00012078 File Offset: 0x00010278
		internal bool CheckInputBounds(int inputIndex, bool acceptAny)
		{
			if (inputIndex == -1 && acceptAny)
			{
				return true;
			}
			if (inputIndex < 0)
			{
				throw new IndexOutOfRangeException("Index must be greater than 0");
			}
			Playable[] inputs = this.GetInputs();
			if (inputs.Length <= inputIndex)
			{
				throw new IndexOutOfRangeException(string.Concat(new object[]
				{
					"inputIndex ",
					inputIndex,
					" is greater than the number of available inputs (",
					inputs.Length,
					")."
				}));
			}
			return true;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000120F4 File Offset: 0x000102F4
		public float GetInputWeight(int inputIndex)
		{
			if (this.CheckInputBounds(inputIndex))
			{
				return this.GetInputWeightInternal(inputIndex);
			}
			return -1f;
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00012110 File Offset: 0x00010310
		public bool SetInputWeight(int inputIndex, float weight)
		{
			return this.CheckInputBounds(inputIndex) && this.SetInputWeightInternal(inputIndex, weight);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00012128 File Offset: 0x00010328
		public void GetInputs(List<Playable> inputList)
		{
			inputList.Clear();
			this.GetInputsInternal(inputList);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00012138 File Offset: 0x00010338
		public void GetOutputs(List<Playable> outputList)
		{
			outputList.Clear();
			this.GetOutputsInternal(outputList);
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00012148 File Offset: 0x00010348
		public virtual void PrepareFrame(FrameData info)
		{
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0001214C File Offset: 0x0001034C
		public virtual void ProcessFrame(FrameData info, object playerData)
		{
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x00012150 File Offset: 0x00010350
		public virtual void OnSetTime(float localTime)
		{
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x00012154 File Offset: 0x00010354
		public virtual void OnSetPlayState(PlayState newState)
		{
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00012158 File Offset: 0x00010358
		public static bool operator ==(Playable x, Playable y)
		{
			return Playable.CompareIntPtr(x, y);
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00012164 File Offset: 0x00010364
		public static bool operator !=(Playable x, Playable y)
		{
			return !Playable.CompareIntPtr(x, y);
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00012170 File Offset: 0x00010370
		public static implicit operator bool(Playable exists)
		{
			return !Playable.CompareIntPtr(exists, null);
		}

		// Token: 0x04000284 RID: 644
		internal IntPtr m_Ptr;

		// Token: 0x04000285 RID: 645
		internal int m_UniqueId;
	}
}
