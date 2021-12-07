using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000349 RID: 841
public class SpringSeinAction : ActionMethod
{
	// Token: 0x06001805 RID: 6149 RVA: 0x000670B0 File Offset: 0x000652B0
	public override void Perform(IContext context)
	{
		Vector2 direction = MoonMath.Angle.VectorFromAngle(base.transform.eulerAngles.z + 90f);
		if (context is SpringContext)
		{
			SpringContext springContext = (SpringContext)context;
			GameObject lastObject = springContext.Spring.LastObject;
			if (lastObject == Characters.Sein.gameObject)
			{
				this.SpringSein(direction);
			}
			else
			{
				SpiritGrenade component = lastObject.GetComponent<SpiritGrenade>();
				if (component)
				{
					component.OnSpring(this.Height, direction);
				}
			}
		}
		else
		{
			this.SpringSein(Vector2.zero);
		}
	}

	// Token: 0x06001806 RID: 6150 RVA: 0x0006714C File Offset: 0x0006534C
	public void SpringSein(Vector2 direction)
	{
		SeinCharacter sein = Characters.Sein;
		if (sein.PlatformBehaviour.PlatformMovement.LocalSpeedY > 0f)
		{
			return;
		}
		float num = PhysicsHelper.CalculateSpeedFromHeight(this.Height, sein.PlatformBehaviour.Gravity.Settings.GravityStrength);
		if (Mathf.Abs(direction.x) < 0.1f)
		{
			sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = num;
		}
		else
		{
			sein.PlatformBehaviour.PlatformMovement.LocalSpeed = direction * num;
			sein.PlatformBehaviour.AirNoDeceleration.NoDeceleration = true;
		}
		if (sein.Abilities.DoubleJump)
		{
			sein.Abilities.DoubleJump.LockForDuration(0.3f);
		}
		sein.ResetAirLimits();
		if (this.ActiveJump && this.PassiveJump && !sein.Controller.IsSwimming)
		{
			sein.PlatformBehaviour.Visuals.Animation.Play((!Core.Input.Jump.Pressed) ? this.PassiveJump : this.ActiveJump, 10, new Func<bool>(this.ShouldJumpAnimationKeepPlaying));
			sein.PlatformBehaviour.Visuals.SpriteRotater.BeginTiltLeftRightInAir(1.5f);
		}
		if (sein.Controller.IsStomping)
		{
			sein.Abilities.Stomp.EndStomp();
			sein.PlatformBehaviour.PlatformMovement.LocalSpeedY = PhysicsHelper.CalculateSpeedFromHeight(this.Height * 1.5f, sein.PlatformBehaviour.Gravity.Settings.GravityStrength);
		}
		else
		{
			sein.PlatformBehaviour.JumpSustain.SetAmountOfSpeedToLose(sein.PlatformBehaviour.PlatformMovement.LocalSpeedY, this.StopDecelerationMultiplier);
		}
	}

	// Token: 0x06001807 RID: 6151 RVA: 0x0006732E File Offset: 0x0006552E
	public bool ShouldJumpAnimationKeepPlaying()
	{
		return Characters.Sein.PlatformBehaviour.PlatformMovement.IsInAir;
	}

	// Token: 0x040014BB RID: 5307
	public float Height;

	// Token: 0x040014BC RID: 5308
	public float StopDecelerationMultiplier = 0.2f;

	// Token: 0x040014BD RID: 5309
	public TextureAnimationWithTransitions ActiveJump;

	// Token: 0x040014BE RID: 5310
	public TextureAnimationWithTransitions PassiveJump;
}
