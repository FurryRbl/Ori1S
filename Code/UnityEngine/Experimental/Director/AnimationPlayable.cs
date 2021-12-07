using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Experimental.Director
{
	// Token: 0x020001C9 RID: 457
	public class AnimationPlayable : Playable
	{
		// Token: 0x06001B59 RID: 7001 RVA: 0x0001A03C File Offset: 0x0001823C
		public AnimationPlayable() : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			this.InstantiateEnginePlayable();
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x0001A058 File Offset: 0x00018258
		public AnimationPlayable(bool final) : base(false)
		{
			this.m_Ptr = IntPtr.Zero;
			if (final)
			{
				this.InstantiateEnginePlayable();
			}
		}

		// Token: 0x06001B5B RID: 7003
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InstantiateEnginePlayable();

		// Token: 0x06001B5C RID: 7004 RVA: 0x0001A078 File Offset: 0x00018278
		public virtual int AddInput(AnimationPlayable source)
		{
			Playable.Connect(source, this, -1, -1);
			Playable[] inputs = base.GetInputs();
			return inputs.Length - 1;
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x0001A09C File Offset: 0x0001829C
		public virtual bool SetInput(AnimationPlayable source, int index)
		{
			if (!base.CheckInputBounds(index))
			{
				return false;
			}
			Playable[] inputs = base.GetInputs();
			if (inputs[index] != null)
			{
				Playable.Disconnect(this, index);
			}
			return Playable.Connect(source, this, -1, index);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x0001A0DC File Offset: 0x000182DC
		public virtual bool SetInputs(IEnumerable<AnimationPlayable> sources)
		{
			Playable[] inputs = base.GetInputs();
			int num = inputs.Length;
			for (int i = 0; i < num; i++)
			{
				Playable.Disconnect(this, i);
			}
			bool flag = false;
			int num2 = 0;
			foreach (AnimationPlayable source in sources)
			{
				if (num2 < num)
				{
					flag |= Playable.Connect(source, this, -1, num2);
				}
				else
				{
					flag |= Playable.Connect(source, this, -1, -1);
				}
				base.SetInputWeight(num2, 1f);
				num2++;
			}
			for (int j = num2; j < num; j++)
			{
				base.SetInputWeight(j, 0f);
			}
			return flag;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x0001A1C4 File Offset: 0x000183C4
		public virtual bool RemoveInput(int index)
		{
			if (!base.CheckInputBounds(index))
			{
				return false;
			}
			Playable.Disconnect(this, index);
			return true;
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x0001A1DC File Offset: 0x000183DC
		public virtual bool RemoveInput(AnimationPlayable playable)
		{
			if (!Playable.CheckPlayableValidity(playable, "playable"))
			{
				return false;
			}
			Playable[] inputs = base.GetInputs();
			for (int i = 0; i < inputs.Length; i++)
			{
				if (inputs[i] == playable)
				{
					Playable.Disconnect(this, i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0001A230 File Offset: 0x00018430
		public virtual bool RemoveAllInputs()
		{
			Playable[] inputs = base.GetInputs();
			for (int i = 0; i < inputs.Length; i++)
			{
				this.RemoveInput(inputs[i] as AnimationPlayable);
			}
			return true;
		}
	}
}
