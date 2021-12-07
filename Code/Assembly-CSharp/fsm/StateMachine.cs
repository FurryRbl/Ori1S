using System;
using System.Collections.Generic;

namespace fsm
{
	// Token: 0x0200003D RID: 61
	public class StateMachine
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000B7A2 File Offset: 0x000099A2
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000B7AA File Offset: 0x000099AA
		public float CurrentStateTime { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000B7B3 File Offset: 0x000099B3
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000B7BB File Offset: 0x000099BB
		public IState CurrentState { get; set; }

		// Token: 0x060002B6 RID: 694 RVA: 0x0000B7C4 File Offset: 0x000099C4
		public void ChangeState(IState state)
		{
			if (this.CurrentState != null)
			{
				this.CurrentState.OnExit();
			}
			this.CurrentState = state;
			this.CurrentStateTime = 0f;
			if (this.CurrentState != null)
			{
				this.CurrentState.OnEnter();
			}
			if (this.OnStateChanged != null)
			{
				this.OnStateChanged();
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000B828 File Offset: 0x00009A28
		public void UpdateState(float dt)
		{
			if (this.CurrentState != null)
			{
				this.CurrentState.UpdateState();
			}
			this.CurrentStateTime += dt;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000B859 File Offset: 0x00009A59
		public StateConfigurator Configure(IState state)
		{
			return new StateConfigurator(this, state);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000B864 File Offset: 0x00009A64
		public TransitionManager FindTransitionManager(Type trigger)
		{
			TransitionManager transitionManager;
			if (this.m_triggerToTransitionManagers.TryGetValue(trigger, out transitionManager))
			{
				return transitionManager;
			}
			transitionManager = new TransitionManager();
			this.m_triggerToTransitionManagers.Add(trigger, transitionManager);
			return transitionManager;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B89C File Offset: 0x00009A9C
		public void Trigger(ITrigger trigger)
		{
			this.CurrentTrigger = trigger;
			TransitionManager transitionManager;
			if (this.m_triggerToTransitionManagers.TryGetValue(trigger.GetType(), out transitionManager))
			{
				transitionManager.Process(this);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000B8D0 File Offset: 0x00009AD0
		public void Trigger<T>() where T : ITrigger
		{
			this.CurrentTrigger = null;
			TransitionManager transitionManager;
			if (this.m_triggerToTransitionManagers.TryGetValue(typeof(T), out transitionManager))
			{
				transitionManager.Process(this);
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000B908 File Offset: 0x00009B08
		public TransitionManager GetTransistionManager<T>()
		{
			TransitionManager result;
			if (this.m_triggerToTransitionManagers.TryGetValue(typeof(T), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000B934 File Offset: 0x00009B34
		public void RegisterStates(params IState[] states)
		{
			foreach (IState item in states)
			{
				this.m_states.Add(item);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000B968 File Offset: 0x00009B68
		public void Serialize(Archive ar)
		{
			if (ar.Reading)
			{
				int num = ar.Serialize(0);
				if (num != -1)
				{
					IState state = this.IndexToState(num);
					this.ChangeState(state);
				}
				this.CurrentStateTime = ar.Serialize(this.CurrentStateTime);
			}
			else
			{
				int value = this.StateToIndex(this.CurrentState);
				ar.Serialize(value);
				ar.Serialize(this.CurrentStateTime);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		private int StateToIndex(IState state)
		{
			return this.m_states.FindIndex((IState a) => a == state);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000BA09 File Offset: 0x00009C09
		private IState IndexToState(int index)
		{
			return this.m_states[index];
		}

		// Token: 0x040001FD RID: 509
		public ITrigger CurrentTrigger;

		// Token: 0x040001FE RID: 510
		public Action OnStateChanged;

		// Token: 0x040001FF RID: 511
		private List<IState> m_states = new List<IState>(8);

		// Token: 0x04000200 RID: 512
		private Dictionary<Type, TransitionManager> m_triggerToTransitionManagers = new Dictionary<Type, TransitionManager>();
	}
}
