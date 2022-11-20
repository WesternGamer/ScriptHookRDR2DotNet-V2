//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using RDR2.NaturalMotion;
using System;

namespace RDR2
{
	public sealed class Ped : Entity
	{
		#region Fields
		Tasks _tasks;
		NaturalMotion.Euphoria _euphoria;
		WeaponCollection _weapons;
		#endregion

		public Ped(int handle) : base(handle)
		{
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is a player.
		/// </summary>
		public bool IsPlayer => PED.IS_PED_A_PLAYER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is dead or dying.
		/// </summary>
		public bool IsDeadOrDying => PED.IS_PED_DEAD_OR_DYING(Handle, true);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is aiming while in cover.
		/// </summary>
		public bool IsAimingFromCover => PED.IS_PED_AIMING_FROM_COVER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is going into cover.
		/// </summary>
		public bool IsGoingIntoCover => PED.IS_PED_GOING_INTO_COVER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is carrying something.
		/// </summary>
		public bool IsCarryingSomething => PED.IS_PED_CARRYING_SOMETHING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is ready to render.
		/// </summary>
		public bool IsReadToRender => PED.IS_PED_READY_TO_RENDER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is using an action mode.
		/// </summary>
		public bool IsUsingActionMode => PED.IS_PED_USING_ACTION_MODE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is using a scenario.
		/// </summary>
		public bool IsUsingScenario => PED.IS_PED_USING_ANY_SCENARIO(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in a scenario.
		/// <remarks>This is an alias for <see cref="IsUsingScenario"/></remarks>
		/// </summary>
		public bool IsInScenario => IsUsingScenario;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is a child.
		/// </summary>
		public bool IsChild => PED._IS_PED_CHILD(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is cowering.
		/// </summary>
		public bool IsCowering => PED._IS_PED_COWERING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is drunk.
		/// </summary>
		public bool IsDrunk => PED._IS_PED_DRUNK(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is intimidated.
		/// </summary>
		public bool IsIntimidated => PED._IS_PED_INTIMIDATED(Handle);

		/// <summary>
		/// Spawn an identical clone of this <see cref="Ped"/>.
		/// </summary>
		public void Clone()
		{
			PED.CLONE_PED(Handle, true, true, false);
		}

		/// <summary>
		/// Instantly kill this <see cref="Ped"/>.
		/// </summary>
		public void Kill()
		{
			Health = 0;
			if (!IsDead) {
				PED._FORCE_PED_DEATH(Handle, 0, 0);
			}
		}

		/// <summary>
		/// Resurrects this <see cref="Ped"/> from death.
		/// </summary>
		public void Resurrect()
		{
			PED.REVIVE_INJURED_PED(Handle);
			Health = MaxHealth;
		}

		/// <summary>
		/// Revives this <see cref="Ped"/> from death.
		/// <remarks>This is an alias for <see cref="Resurrect"/></remarks>
		/// </summary>
		public void Revive()
		{
			Resurrect();
		}


		#region Styling

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is human.
		/// </summary>
		public bool IsHuman => PED.IS_PED_HUMAN(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is handcuffed.
		/// </summary>
		public bool IsCuffed => TASK.IS_PED_CUFFED(Handle);

		/// <summary>
		/// Gets the gender of this <see cref="Ped"/>.
		/// </summary>
		public Gender Gender => PED.IS_PED_MALE(Handle) ? Gender.Male : Gender.Female;

		/// <summary>
		/// Sets the scale of this <see cref="Ped"/>.
		/// </summary>
		public float Scale
		{
			set => PED._SET_PED_SCALE(Handle, value);
		}

		/// <summary>
		/// Sets how much sweat should be rendered on this <see cref="Ped"/>.
		/// </summary>
		public float Sweat
		{
			set {
				if (value < 0)
				{
					value = 0;
				}
				if (value > 100)
				{
					value = 100;
				}

				PED.SET_PED_SWEAT(Handle, value);
			}
		}

		/// <summary>
		/// Sets how high up on this <see cref="Ped"/>s body water should be visible.
		/// </summary>
		/// <value>
		/// The height ranges from 0.0f to 1.99f, 0.0f being no water visible, 1.99f being covered in water.
		/// </value>
		public float WetnessHeight
		{
			set
			{
				if (value == 0.0f) {
					PED.CLEAR_PED_WETNESS(Handle);
				} else {
					PED.SET_PED_WETNESS_HEIGHT(Handle, value);
				}
			}
		}

		/// <summary>
		/// Clean this <see cref="Ped"/> from dirt, mud, snow, water, etc
		/// </summary>
		public void Clean()
		{
			PED.CLEAR_PED_BLOOD_DAMAGE(Handle);
			PED.CLEAR_PED_DAMAGE_DECAL_BY_ZONE(Handle, 10, "ALL");
			PED.CLEAR_PED_DECORATIONS(Handle);
			PED.CLEAR_PED_ENV_DIRT(Handle);
			PED.CLEAR_PED_WETNESS(Handle);
			PED._SET_PED_DIRT_CLEANED(Handle, 0.0f, -1, true, true);
		}

		/// <summary>
		/// Randomize this <see cref="Ped"/> outfit
		/// </summary>
		public void RandomizeOutfit()
		{
			PED._SET_RANDOM_OUTFIT_VARIATION(Handle, true);
		}

		/// <summary>
		/// Update variation on this <see cref="Ped"/>, needed after first creation, or when component or texture/overlay is changed.
		/// </summary>
		public void UpdateVariation()
		{
			PED._UPDATE_PED_VARIATION(Handle, false, true, true, true, false);
		}

		#endregion

		#region Configuration

		public override int MaxHealth
		{
			get => PED.GET_PED_MAX_HEALTH(Handle);
			set => PED.SET_PED_MAX_HEALTH(Handle, value);
		}

		public void SetPedPromptName(string name)
		{
			PED._SET_PED_PROMPT_NAME(Handle, MISC.VAR_STRING(10, "LITERAL_STRING", name));
		}
		
		public bool GetConfigFlag(ePedScriptConfigFlags flagID)
		{
			return PED.GET_PED_CONFIG_FLAG(Handle, (int)flagID, true);
		}

		public void SetConfigFlag(ePedScriptConfigFlags flagID, bool value)
		{
			PED.SET_PED_CONFIG_FLAG(Handle, (int)flagID, value);
		}

		public void SetResetFlag(ePedScriptResetFlags flagID)
		{
			PED.SET_PED_RESET_FLAG(Handle, (int)flagID, true);
		}

		public bool GetResetFlag(ePedScriptResetFlags flagID)
		{
			return PED.GET_PED_RESET_FLAG(Handle, (int)flagID);
		}

		public int GetBoneIndex(BoneID BoneID)
		{
			return PED.GET_PED_BONE_INDEX(Handle, (int)BoneID);
		}

		public Vector3 GetBoneCoord(BoneID BoneID)
		{
			return GetBoneCoord(BoneID, Vector3.Zero);
		}
		public Vector3 GetBoneCoord(BoneID BoneID, Vector3 Offset)
		{
			return PED.GET_PED_BONE_COORDS(Handle, (int)BoneID, Offset.X, Offset.Y, Offset.Z);
		}

		#endregion

		#region Tasks

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being arrested.
		/// </summary>
		public bool IsBeingArrested => TASK.IS_PED_BEING_ARRESTED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is exiting/leaving a scenario.
		/// </summary>
		public bool IsExitingScenario => TASK.IS_PED_EXITING_SCENARIO(Handle, true);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is getting up from the ground.
		/// </summary>
		public bool IsGettingUp => TASK.IS_PED_GETTING_UP(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is writhing.
		/// </summary>
		public bool IsWrithing => TASK.IS_PED_IN_WRITHE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is running.
		/// </summary>
		public bool IsRunning => TASK.IS_PED_RUNNING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is sprinting.
		/// </summary>
		public bool IsSprinting => TASK.IS_PED_SPRINTING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is standing still.
		/// </summary>
		public bool IsStandingStill => TASK.IS_PED_STILL(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is walking.
		/// </summary>
		public bool IsWalking => TASK.IS_PED_WALKING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is aiming in the air.
		/// </summary>
		public bool IsAimingInTheAir => TASK.GET_IS_PED_AIMING_IN_THE_AIR(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is arresting another <see cref="Ped"/>
		/// </summary>
		public bool IsArrestingAPed => TASK._IS_PED_ARRESTING_ANY_PED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is leading a horse.
		/// </summary>
		public bool IsLeadingHorse => TASK._IS_PED_LEADING_HORSE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is climbing.
		/// </summary>
		public bool IsClimbing => PED.IS_PED_CLIMBING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is diving.
		/// </summary>
		public bool IsDiving => PED.IS_PED_DIVING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is jumping.
		/// </summary>
		public bool IsJumping => PED.IS_PED_JUMPING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is prone.
		/// </summary>
		public bool IsProne => PED.IS_PED_PRONE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is sitting.
		/// </summary>
		public bool IsSitting => PED.IS_PED_SITTING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is stopped.
		/// </summary>
		public bool IsStopped => PED.IS_PED_STOPPED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is swimming.
		/// </summary>
		public bool IsSwimming => PED.IS_PED_SWIMMING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is swimming underwater.
		/// </summary>
		public bool IsSwimmingUnderwater => PED.IS_PED_SWIMMING_UNDER_WATER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is climbing a ladder.
		/// </summary>
		public bool IsClimbingLadder => PED._IS_PED_CLIMBING_LADDER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is vaulting over something.
		/// </summary>
		public bool IsVaulting => PED.IS_PED_VAULTING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is sliding.
		/// </summary>
		public bool IsSliding => PED._IS_PED_SLIDING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is opening a door.
		/// </summary>
		public bool IsOpeningDoor => PED.IS_PED_OPENING_DOOR(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is idle.
		/// <remarks>This is an alias for <see cref="IsStandingStill"/></remarks>
		/// </summary>
		public bool IsIdle => IsStandingStill;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is headtracking an <see cref="Entity"/>.
		/// </summary>
		public bool IsHeadTrackingEntity(Entity entity)
		{
			return PED.IS_PED_HEADTRACKING_ENTITY(Handle, entity.Handle);
		}

		/// <summary>
		/// Set a value indicating whether this <see cref="Ped"/> should keep scripted tasks.
		/// </summary>
		public bool AlwaysKeepTask
		{
			set => PED.SET_PED_KEEP_TASK(Handle, value);
		}

		/// <summary>
		/// Set a value indicating whether this <see cref="Ped"/> should block non temporary events.
		/// </summary>
		public bool BlockPermanentEvents
		{
			set => PED.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS(Handle, value);
		}

		public Tasks Task => _tasks ?? (_tasks = new Tasks(this));

		public int TaskSequenceProgress => TASK.GET_SEQUENCE_PROGRESS(Handle);

		#endregion

		#region Euphoria & Ragdoll

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is ragdolling.
		/// </summary>
		public bool IsRagdoll => PED.IS_PED_RAGDOLL(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is falling.
		/// </summary>
		public bool IsFalling => PED.IS_PED_FALLING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is falling over.
		/// </summary>
		public bool IsFallingOver => PED.IS_PED_FALLING_OVER(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is ragdolling.
		/// </summary>
		public bool CanRagdoll
		{
			get => PED.CAN_PED_RAGDOLL(Handle);
			set => PED.SET_PED_CAN_RAGDOLL(Handle, value);
		}

		/// <summary>
		/// Set this <see cref="Ped"/> to stop ragdolling.
		/// </summary>
		public void StopRagdoll()
		{
			if (IsRagdoll) {
				// Incorrect native name
				PED._SET_PED_TO_DISABLE_RAGDOLL(Handle, false);
			}
		}

		public Euphoria Euphoria => _euphoria ?? (_euphoria = new Euphoria(this));

		#endregion

		#region Weapon Interaction

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being lassoed.
		/// </summary>
		public bool IsLassoed => PED.IS_PED_LASSOED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is shooting.
		/// </summary>
		public bool IsShooting => PED.IS_PED_SHOOTING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is reloading.
		/// </summary>
		public bool IsReloading => PED.IS_PED_RELOADING(Handle);

		/// <summary>
		/// Gets or sets how accurate this <see cref="Ped"/>s shooting ability is.
		///  The higher the value of this property is, the more likely it is that this <see cref="Ped"/> will shoot at exactly where they are aiming at.
		/// </summary>
		/// <value>
		/// The accuracy from 0 to 100, 0 being very inaccurate, which means this <see cref="Ped"/> cannot shoot at exactly where they are aiming at,
		///  100 being perfectly accurate.
		/// </value>
		public int Accuracy
		{
			get => PED.GET_PED_ACCURACY(Handle);
			set => PED.SET_PED_ACCURACY(Handle, value);
		}

		/// <summary>
		/// Sets the rate this <see cref="Ped"/> will shoot at.
		/// </summary>
		/// <value>
		/// The shoot rate from 0 to 1000. 100 is the default value.
		/// </value>
		public int ShootRate
		{
			set => PED.SET_PED_SHOOT_RATE(Handle, value);
		}

		public WeaponCollection Weapons => _weapons ??= new WeaponCollection(this);

		
		public void GiveWeapon(eWeapon weapon, int ammoCount, eWeaponAttachPoint attachPoint, bool bForceInHand = false, bool bForceInHolster = false, bool bAllowMultipleCopies = false)
		{
			WEAPON.GIVE_WEAPON_TO_PED(Handle, (uint)weapon, ammoCount, bForceInHand, bForceInHolster, (int)attachPoint, bAllowMultipleCopies, 0.0f, 0.0f, (uint)eAddItemReason.ADD_REASON_DEFAULT, true, 0.0f, false);
		}

		#endregion

		#region Vehicles & Transport

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in a boat.
		/// </summary>
		public bool IsInBoat => PED.IS_PED_IN_ANY_BOAT(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in a train.
		/// </summary>
		public bool IsInTrain => PED.IS_PED_IN_ANY_TRAIN(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in a vehicle.
		/// </summary>
		public bool IsInVehicle => PED.IS_PED_IN_ANY_VEHICLE(Handle, false);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is jacking another <see cref="Ped"/>.
		/// </summary>
		public bool IsJacking => PED.IS_PED_JACKING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is on foot.
		/// </summary>
		public bool IsOnFoot => PED.IS_PED_ON_FOOT(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is on a horse.
		/// </summary>
		public bool IsOnHorse => PED.IS_PED_ON_MOUNT(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is on a mount.
		/// <remarks>This is an alias for <see cref="IsOnHorse"/></remarks>
		/// </summary>
		public bool IsOnMount => IsOnHorse;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is sitting in a vehicle.
		/// </summary>
		public bool IsSittingInVehicle => PED.IS_PED_SITTING_IN_ANY_VEHICLE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is getting into a vehicle.
		/// </summary>
		public bool IsEnteringVehicle => PED.IS_PED_GETTING_INTO_A_VEHICLE(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is getting into a vehicle.
		/// <remarks>This is an alias for <see cref="IsEnteringVehicle"/></remarks>
		/// </summary>
		public bool IsGettingIntoAVehicle => IsEnteringVehicle;

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is entering transport.
		/// </summary>
		public bool IsEnteringTransport => PED.IS_PED_ENTERING_ANY_TRANSPORT(Handle);

		/// <summary>
		/// Gets the mount that this <see cref="Ped"/> is currently on.
		/// </summary>
		public Ped CurrentMount => (Ped)Ped.FromHandle(PED.GET_MOUNT(Handle));

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in a specific vehicle.
		/// </summary>
		public bool IsInThisVehicle(Vehicle vehicle)
		{
			return PED.IS_PED_IN_VEHICLE(Handle, vehicle.Handle, false);
		}

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is sitting in a specific vehicle.
		/// </summary>
		public bool IsSittingInThisVehicle(Vehicle vehicle)
		{
			return PED.IS_PED_SITTING_IN_VEHICLE(Handle, vehicle.Handle);
		}

		public void SetIntoVehicle(Vehicle vehicle, eVehicleSeat seat)
		{
			PED.SET_PED_INTO_VEHICLE(Handle, vehicle.Handle, (int)seat);
		}

		public Vehicle LastVehicle => (Vehicle)FromHandle(PED.GET_VEHICLE_PED_IS_IN(Handle, true));

		public Vehicle CurrentVehicle => (Vehicle)FromHandle(PED.GET_VEHICLE_PED_IS_IN(Handle, false));

		public float DrivingSpeed
		{
			set => TASK.SET_DRIVE_TASK_CRUISE_SPEED(Handle, value);
		}

		public float MaxDrivingSpeed
		{
			set => TASK.SET_DRIVE_TASK_MAX_CRUISE_SPEED(Handle, value);
		}

		#endregion

		#region Combat

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being hogtied.
		/// </summary>
		public bool IsBeingHogtied => PED.IS_PED_BEING_HOGTIED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being jacked.
		/// </summary>
		public bool IsBeingJacked => PED.IS_PED_BEING_JACKED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being stealth killed.
		/// </summary>
		public bool IsBeingStealthKilled => PED.IS_PED_BEING_STEALTH_KILLED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is being stunned.
		/// </summary>
		public bool IsBeingStunned => PED.IS_PED_BEING_STUNNED(Handle, 0);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is fleeing.
		/// </summary>
		public bool IsFleeing => PED.IS_PED_FLEEING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is hogtied.
		/// </summary>
		public bool IsHogtied => PED.IS_PED_HOGTIED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is hogtying.
		/// </summary>
		public bool IsHogtying => PED.IS_PED_HOGTYING(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is in combat.
		/// </summary>
		public bool IsInCombat => PED.IS_PED_IN_COMBAT(Handle, 0);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is melee combat.
		/// </summary>
		public bool IsInMeleeCombat => PED.IS_PED_IN_MELEE_COMBAT(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is responding to a threat.
		/// </summary>
		public bool IsRespondingToThreat => PED.IS_PED_RESPONDING_TO_THREAT(Handle);

		public bool IsPriorityTargetForEnemies
		{
			set => ENTITY.SET_ENTITY_IS_TARGET_PRIORITY(Handle, value, 0);
		}

		public bool IsInCover()
		{
			return IsInCover(false);
		}
		public bool IsInCover(bool expectUseWeapon)
		{
			return PED.IS_PED_IN_COVER(Handle, expectUseWeapon, false);
		}

		public bool IsInCoverFacingLeft => PED.IS_PED_IN_COVER_FACING_LEFT(Handle);

		public bool CanBeTargetted
		{
			set => PED.SET_PED_CAN_BE_TARGETTED(Handle, value);
		}

		public Ped GetMeleeTarget()
		{
			return (Ped)FromHandle(PED.GET_MELEE_TARGET_FOR_PED(Handle));
		}

		public bool IsInCombatAgainst(Ped target)
		{
			return PED.IS_PED_IN_COMBAT(Handle, target.Handle);
		}

		#endregion

		#region Damaging

		/*public bool CanWrithe
		{
			get => !GetConfigFlag(281);
			set => SetConfigFlag(281, !value);
		}*/

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is incapacitated.
		/// </summary>
		public bool IsIncapacitated => PED.IS_PED_INCAPACITATED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is injured.
		/// </summary>
		public bool IsInjured => PED.IS_PED_INJURED(Handle);

		/// <summary>
		/// Gets a value indicating whether this <see cref="Ped"/> is fatally injured.
		/// </summary>
		public bool IsFatallyInjured => PED.IS_PED_FATALLY_INJURED(Handle);

		/// <summary>
		/// Set this <see cref="Ped"/> is drop his weapons on death.
		/// </summary>
		public bool DropsWeaponsOnDeath
		{
			set => WEAPON.SET_PED_DROPS_WEAPONS_WHEN_DEAD(Handle, value);
		}

		public void ApplyDamage(int damageAmount)
		{
			PED.APPLY_DAMAGE_TO_PED(Handle, damageAmount, 0, 0, 0);
		}

		public unsafe Vector3 GetLastWeaponImpactCoords()
		{
			Vector3 outCoords;
			if (WEAPON.GET_PED_LAST_WEAPON_IMPACT_COORD(Handle, &outCoords)) {
				return outCoords;
			}
			return Vector3.Zero;
		}

		#endregion

		#region Relationship

		public Relationship GetRelationshipWithPed(Ped ped)
		{
			return (Relationship)PED.GET_RELATIONSHIP_BETWEEN_PEDS(Handle, ped.Handle);
		}

		public uint RelationshipGroup
		{
			get => PED.GET_PED_RELATIONSHIP_GROUP_HASH(Handle);
			set => PED.SET_PED_RELATIONSHIP_GROUP_HASH(Handle, value);
		}

		#endregion

		#region Group

		public bool IsInGroup => PED.IS_PED_IN_GROUP(Handle);

		public void LeaveGroup()
		{
			PED.REMOVE_PED_FROM_GROUP(Handle);
		}

		/*public bool NeverLeavesGroup
		{
			set => PED.SET_PED_NEVER_LEAVES_GROUP(Handle, value);
		}*/

		public RelationshipGroup CurrentPedGroup => IsInGroup ? PED.GET_PED_RELATIONSHIP_GROUP_HASH(Handle) : 0;

		#endregion

		#region Speech & Animation

		public bool CanPlayGestures
		{
			set => PED.SET_PED_CAN_PLAY_GESTURE_ANIMS(Handle, (ulong)(value == true ? 1 : 0), 0);
		}

		public string Voice
		{
			set => AUDIO.SET_AMBIENT_VOICE_NAME(Handle, value);
		}

		#endregion

		#region Cores
		public int HealthCore
		{
			get => GetCoreValue(PedCore.Health);
			set => SetCoreValue(PedCore.Health, value);
		}

		public int HealthCoreRank
		{
			get => GetCoreRank(PedCore.Health);
			set => SetCoreRank(PedCore.Health, value);
		}

		public int StaminaCore
		{
			get => GetCoreValue(PedCore.Stamina);
			set => SetCoreValue(PedCore.Stamina, value);
		}

		public int StaminaCoreRank
		{
			get => GetCoreRank(PedCore.Stamina);
			set => SetCoreRank(PedCore.Stamina, value);
		}

		public int DeadEyeCore
		{
			get => GetCoreValue(PedCore.DeadEye);
			set => SetCoreValue(PedCore.DeadEye, value);
		}

		public int DeadEyeRank
		{
			get => GetCoreRank(PedCore.DeadEye);
			set => SetCoreRank(PedCore.DeadEye, value);
		}

		public int GetCoreValue(PedCore core)
		{
			return ATTRIBUTE._GET_ATTRIBUTE_CORE_VALUE(Handle, (int)core);
		}

		public void SetCoreValue(PedCore core, int value)
		{
			ATTRIBUTE._SET_ATTRIBUTE_CORE_VALUE(Handle, (int)core, value);
		}

		public int GetCoreRank(PedCore core)
		{
			return ATTRIBUTE.GET_ATTRIBUTE_RANK(Handle, (int)core);
		}

		public void SetCoreRank(PedCore core, int level)
		{
			ATTRIBUTE.SET_ATTRIBUTE_POINTS(Handle, (int)core, GetExperienceByRank(level));
		}

		private static int GetExperienceByRank(int level)
		{
			switch (level)
			{
				case -1:
					return -1;
				case 0:
					return 0;

				case 1:
					return 50;

				case 2:
					return 100;

				case 3:
					return 200;

				case 4:
					return 350;

				case 5:
					return 550;

				case 6:
					return 800;

				case 7:
					return 1100;

				default:
					return 0;
			}
		}
		#endregion
	}

	public enum PedCore
	{
		Health = 0,
		Stamina,
		DeadEye
	}
}
