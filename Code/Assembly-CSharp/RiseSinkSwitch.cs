using System;
using UnityEngine;

// Token: 0x020009E0 RID: 2528
[RequireComponent(typeof(ListOfCollidedObjects))]
public class RiseSinkSwitch : SaveSerialize, ISuspendable
{
	// Token: 0x060036FE RID: 14078 RVA: 0x000E6DB5 File Offset: 0x000E4FB5
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_listOfCollidedObjects = base.GetComponent<ListOfCollidedObjects>();
	}

	// Token: 0x060036FF RID: 14079 RVA: 0x000E6DCF File Offset: 0x000E4FCF
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003700 RID: 14080 RVA: 0x000E6DDD File Offset: 0x000E4FDD
	private void Start()
	{
		this.m_startPosition = this.TransformToAffect.localPosition;
	}

	// Token: 0x06003701 RID: 14081 RVA: 0x000E6DF0 File Offset: 0x000E4FF0
	public void ChangeState(RiseSinkSwitch.RiseSinkState state)
	{
		switch (state)
		{
		}
		this.State = state;
		switch (state)
		{
		case RiseSinkSwitch.RiseSinkState.Up:
			if (!this.TriggerOnce)
			{
				this.m_active = true;
			}
			break;
		}
	}

	// Token: 0x06003702 RID: 14082 RVA: 0x000E6E78 File Offset: 0x000E5078
	private float GetCurrentlyAppliedMass()
	{
		float num = 0f;
		foreach (GameObject gameObject in this.m_listOfCollidedObjects.CollisionObjects)
		{
			Rigidbody component = gameObject.GetComponent<Rigidbody>();
			if (component)
			{
				num += component.mass;
			}
		}
		return num;
	}

	// Token: 0x06003703 RID: 14083 RVA: 0x000E6EF8 File Offset: 0x000E50F8
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float fixedDeltaTime = Time.fixedDeltaTime;
		Vector3 down = Vector3.down;
		float currentlyAppliedMass = this.GetCurrentlyAppliedMass();
		switch (this.State)
		{
		case RiseSinkSwitch.RiseSinkState.Rise:
		{
			float num = fixedDeltaTime * this.RiseSpeed;
			Vector3 vector = this.m_startPosition - this.TransformToAffect.localPosition;
			if (vector.magnitude > num)
			{
				vector.Normalize();
				vector *= num;
				this.TransformToAffect.localPosition += vector;
			}
			else
			{
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Up);
				this.TransformToAffect.localPosition = this.m_startPosition;
			}
			if (currentlyAppliedMass >= this.MinMassToAffect)
			{
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Sink);
			}
			break;
		}
		case RiseSinkSwitch.RiseSinkState.Sink:
			if (Mathf.Abs(this.m_startPosition.y - this.TransformToAffect.localPosition.y) >= this.SinkDistance)
			{
				if (this.m_active && this.OnPressedAction)
				{
					this.OnPressedAction.Perform(null);
					this.m_active = false;
					this.m_wasTriggered = true;
				}
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Down);
			}
			if (currentlyAppliedMass >= this.MinMassToAffect)
			{
				float d = fixedDeltaTime * this.SinkSpeed;
				this.TransformToAffect.localPosition += down * d;
			}
			else
			{
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Rise);
			}
			break;
		case RiseSinkSwitch.RiseSinkState.Up:
			if (currentlyAppliedMass >= this.MinMassToAffect)
			{
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Sink);
			}
			break;
		case RiseSinkSwitch.RiseSinkState.Down:
			if (currentlyAppliedMass < this.MinMassToAffect)
			{
				this.ChangeState(RiseSinkSwitch.RiseSinkState.Rise);
			}
			break;
		}
	}

	// Token: 0x06003704 RID: 14084 RVA: 0x000E70B8 File Offset: 0x000E52B8
	public override void Serialize(Archive ar)
	{
		this.ChangeState((RiseSinkSwitch.RiseSinkState)ar.Serialize((int)this.State));
		this.TransformToAffect.localPosition = ar.Serialize(this.TransformToAffect.localPosition);
		ar.Serialize(ref this.m_wasTriggered);
		ar.Serialize(ref this.m_active);
	}

	// Token: 0x17000881 RID: 2177
	// (get) Token: 0x06003705 RID: 14085 RVA: 0x000E710B File Offset: 0x000E530B
	// (set) Token: 0x06003706 RID: 14086 RVA: 0x000E7113 File Offset: 0x000E5313
	public bool IsSuspended { get; set; }

	// Token: 0x040031F0 RID: 12784
	public float SinkSpeed = 1f;

	// Token: 0x040031F1 RID: 12785
	public float RiseSpeed = 1f;

	// Token: 0x040031F2 RID: 12786
	public float SinkDistance = 1f;

	// Token: 0x040031F3 RID: 12787
	public float MinMassToAffect = 1f;

	// Token: 0x040031F4 RID: 12788
	public ActionMethod OnPressedAction;

	// Token: 0x040031F5 RID: 12789
	public Transform TransformToAffect;

	// Token: 0x040031F6 RID: 12790
	public bool TriggerOnce;

	// Token: 0x040031F7 RID: 12791
	private bool m_wasTriggered;

	// Token: 0x040031F8 RID: 12792
	private Vector3 m_startPosition;

	// Token: 0x040031F9 RID: 12793
	private ListOfCollidedObjects m_listOfCollidedObjects;

	// Token: 0x040031FA RID: 12794
	private bool m_active = true;

	// Token: 0x040031FB RID: 12795
	public RiseSinkSwitch.RiseSinkState State = RiseSinkSwitch.RiseSinkState.Up;

	// Token: 0x020009E1 RID: 2529
	public enum RiseSinkState
	{
		// Token: 0x040031FE RID: 12798
		Rise,
		// Token: 0x040031FF RID: 12799
		Sink,
		// Token: 0x04003200 RID: 12800
		Up,
		// Token: 0x04003201 RID: 12801
		Down
	}
}
