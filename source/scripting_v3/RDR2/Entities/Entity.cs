//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using System;

namespace RDR2
{
	public abstract class Entity : PoolObject, ISpatial
	{
		internal Entity(int handle) : base(handle)
		{
		}

		/// <summary>
		/// Creates a new instance of an <see cref="Entity"/> from the given handle.
		/// </summary>
		/// <param name="handle">The entity handle.</param>
		/// <returns>Returns a <see cref="Ped"/> if this handle corresponds to a Ped.
		/// Returns a <see cref="Vehicle"/> if this handle corresponds to a Vehicle.
		/// Returns a <see cref="Prop"/> if this handle corresponds to a Prop.
		/// Returns <c>null</c> if no <see cref="Entity"/> exists this the specified <paramref name="handle"/></returns>
		public static Entity FromHandle(int handle)
		{
			switch ((eEntityType)ENTITY.GET_ENTITY_TYPE(handle)) {
				case eEntityType.ET_PED:
				return new Ped(handle);
				case eEntityType.ET_VEHICLE:
				return new Vehicle(handle);
				case eEntityType.ET_OBJECT:
				return new Prop(handle);
			}
			return null;
		}



		public bool IsDead => ENTITY.IS_ENTITY_DEAD(Handle);
		public bool IsAlive => !IsDead;

		#region Styling

		public Model Model => new Model(ENTITY.GET_ENTITY_MODEL(Handle));

		public int Alpha
		{
			get => ENTITY.GET_ENTITY_ALPHA(Handle);
			set => ENTITY.SET_ENTITY_ALPHA(Handle, value, false);
		}

		#endregion

		#region Configuration
		public eEntityType EntityType => (eEntityType)ENTITY.GET_ENTITY_TYPE(Handle);

		public int LodDistance
		{
			get => ENTITY.GET_ENTITY_LOD_DIST(Handle);
			set => ENTITY.SET_ENTITY_LOD_DIST(Handle, value);
		}

		public bool IsPersistent
		{
			get => ENTITY.IS_ENTITY_A_MISSION_ENTITY(Handle);
			set => ENTITY.SET_ENTITY_AS_MISSION_ENTITY(Handle, value, value);
		}

		#endregion

		#region Health

		public int Health
		{
			get => ENTITY.GET_ENTITY_HEALTH(Handle);
			set => ENTITY.SET_ENTITY_HEALTH(Handle, value, 0);
		}

		public virtual int MaxHealth
		{
			get => ENTITY.GET_ENTITY_MAX_HEALTH(Handle, false);
			set => ENTITY.SET_ENTITY_MAX_HEALTH(Handle, value);
		}

		#endregion

		#region Positioning

		public virtual Vector3 Position
		{
			get => ENTITY.GET_ENTITY_COORDS(Handle, true, true);
			set => ENTITY.SET_ENTITY_COORDS(Handle, value.X, value.Y, value.Z, true, true, true, true); // SET_ENTITY_COORDS_NO_OFFSET
		}

		public virtual Vector3 Rotation
		{
			get => ENTITY.GET_ENTITY_ROTATION(Handle, 2);
			set => ENTITY.SET_ENTITY_ROTATION(Handle, value.X, value.Y, value.Z, 2, true);
		}

		public float Heading
		{
			get => ENTITY.GET_ENTITY_HEADING(Handle);
			set => ENTITY.SET_ENTITY_HEADING(Handle, value);
		}

		public float HeightAboveGround => ENTITY.GET_ENTITY_HEIGHT_ABOVE_GROUND(Handle);

		public Quaternion Quaternion
		{
			/*get
			{
				float x;
				float y;
				float z;
				float w;
				unsafe
				{
					ENTITY.GET_ENTITY_QUATERNION(Handle, &x, &y, &z, &w);
				}

				return new Quaternion(x, y, z, w);
			}*/ // waiting for GET_ENTITY_QUATERNOIN
			set => ENTITY.SET_ENTITY_QUATERNION(Handle, value.X, value.Y, value.Z, value.W);
		}

		public Vector3 UpVector
		{
			get => Vector3.Cross(RightVector, Forward);
		}

		public Vector3 RightVector
		{
			get
			{
				const double D2R = 0.01745329251994329576923690768489;
				double num1 = System.Math.Cos(Rotation.Y * D2R);
				double x = num1 * System.Math.Cos(-Rotation.Z * D2R);
				double y = num1 * System.Math.Sin(Rotation.Z * D2R);
				double z = System.Math.Sin(-Rotation.Y * D2R);
				return new Vector3((float)x, (float)y, (float)z);
			}
		}

		public Vector3 Forward
		{
			get => ENTITY.GET_ENTITY_FORWARD_VECTOR(Handle);
		}

		public Vector3 GetOffsetInWorldCoords(Vector3 offset)
		{
			return ENTITY.GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS(Handle, offset.X, offset.Y, offset.Z);
		}

		public Vector3 GetOffsetFromWorldCoords(Vector3 worldCoords)
		{
			return ENTITY.GET_OFFSET_FROM_ENTITY_GIVEN_WORLD_COORDS(Handle, worldCoords.X, worldCoords.Y, worldCoords.Z);
		}


		public Vector3 Velocity
		{
			get => ENTITY.GET_ENTITY_VELOCITY(Handle, -1);
			set => ENTITY.SET_ENTITY_VELOCITY(Handle, value.X, value.Y, value.Z);
		}

		#endregion

		#region Proofs

		public bool IsBulletProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 1) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 1, value);
		}

		public bool IsFlameProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 2) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 2, value);
		}

		public bool IsExplosionProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 4) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 4, value);
		}

		public bool IsCollisionProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 8) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 8, value);
		}

		public bool IsMeleeProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 16) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 16, value);
		}

		public bool IsSteamProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 32) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 32, value);
		}

		public bool IsSmokeProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 64) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 64, value);
		}

		public bool IsHeadshotProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 128) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 128, value);
		}

		public bool IsProjectileProof
		{
			get => (ENTITY._GET_ENTITY_PROOFS(Handle) & 256) != 0;
			set => ENTITY.SET_ENTITY_PROOFS(Handle, 256, value);
		}

		#endregion

		#region Invincibility

		public bool IsInvincible
		{
			set => ENTITY.SET_ENTITY_INVINCIBLE(Handle, value);
		}

		public bool IsOnlyDamagedByPlayer
		{
			set => ENTITY.SET_ENTITY_ONLY_DAMAGED_BY_PLAYER(Handle, value);
		}

		#endregion

		#region Status Effects

		public bool IsVisible
		{
			get => ENTITY.IS_ENTITY_VISIBLE(Handle);
			set => ENTITY.SET_ENTITY_VISIBLE(Handle, value);
		}

		public bool IsOccluded
		{
			get => ENTITY.IS_ENTITY_OCCLUDED(Handle);
		}

		public bool IsOnFire => FIRE.IS_ENTITY_ON_FIRE(Handle);

		public bool IsOnScreen
		{
			get => ENTITY.IS_ENTITY_ON_SCREEN(Handle);
		}

		public bool IsUpright => ENTITY.IS_ENTITY_UPRIGHT(Handle, 30.0f);

		public bool IsUpsideDown => ENTITY.IS_ENTITY_UPSIDEDOWN(Handle);

		public bool IsInAir => ENTITY.IS_ENTITY_IN_AIR(Handle, 0);

		public bool IsInWater => ENTITY.IS_ENTITY_IN_WATER(Handle);

		#endregion

		#region Collision

		/*public bool HasCollision
		{
			get
			{
				var address = RDR2DN.NativeMemory.GetEntityAddress(Handle);
				if (address == IntPtr.Zero)
				{
					return false;
				}

				return RDR2DN.NativeMemory.IsBitSet(address + 0x29, 1);
			}
			set => ENTITY.SET_ENTITY_COLLISION(Handle, value, false);
		}*/ // collision address unknown

		public bool HasCollidedWithAnything => ENTITY.HAS_ENTITY_COLLIDED_WITH_ANYTHING(Handle);

		public void SetNoCollision(Entity entity, bool toggle)
		{
			ENTITY.SET_ENTITY_NO_COLLISION_ENTITY(Handle, entity.Handle, !toggle);
		}

		public bool IsInArea(Vector3 minBounds, Vector3 maxBounds)
		{
			return ENTITY.IS_ENTITY_IN_AREA(Handle, minBounds.X, minBounds.Y, minBounds.Z, maxBounds.X, maxBounds.Y, maxBounds.Z, true, true, 0);
		}
		public bool IsInArea(Vector3 pos1, Vector3 pos2, float angle)
		{
			return IsInAngledArea(pos1, pos2, angle);
		}
		public bool IsInAngledArea(Vector3 origin, Vector3 edge, float angle)
		{
			return ENTITY.IS_ENTITY_IN_ANGLED_AREA(Handle, origin.X, origin.Y, origin.Z, edge.X, edge.Y, edge.Z, angle, false, true, 0);
		}

		public bool IsInRangeOf(Vector3 position, float distance)
		{
			return Vector3.Subtract(Position, position).Length() < distance;
		}

		public bool IsNearEntity(Entity entity, Vector3 distance)
		{
			return ENTITY.IS_ENTITY_AT_ENTITY(Handle, entity.Handle, distance.X, distance.Y, distance.Z, false, true, 0);
		}

		public bool IsTouching(Model model)
		{
			return ENTITY.IS_ENTITY_TOUCHING_MODEL(Handle, (uint)model.Hash);
		}
		public bool IsTouching(Entity entity)
		{
			return ENTITY.IS_ENTITY_TOUCHING_ENTITY(Handle, entity.Handle);
		}

		#endregion

		#region Blips
		
		public Blip AddBlip(BlipType blipType)
		{
			return new Blip(MAP.BLIP_ADD_FOR_ENTITY((uint)blipType, Handle));
		}
		
		public Blip GetBlip => new Blip(MAP.GET_BLIP_FROM_ENTITY(Handle));

		#endregion

		#region Attaching

		public void Detach()
		{
			ENTITY.DETACH_ENTITY(Handle, true, true);
		}
		public void AttachTo(Entity entity, int boneIndex)
		{
			AttachTo(entity, boneIndex, Vector3.Zero, Vector3.Zero);
		}
		public void AttachTo(Entity entity, int boneIndex, Vector3 position, Vector3 rotation)
		{
			ENTITY.ATTACH_ENTITY_TO_ENTITY(Handle, entity.Handle, boneIndex, position.X, position.Y, position.Z, rotation.X, rotation.Y, rotation.Z, false, false, false, false, 2, true, false, false);
		}

		public bool IsAttached()
		{
			return ENTITY.IS_ENTITY_ATTACHED(Handle);
		}
		public bool IsAttachedTo(Entity entity)
		{
			return ENTITY.IS_ENTITY_ATTACHED_TO_ENTITY(Handle, entity.Handle);
		}

		public Entity GetEntityAttachedTo()
		{
			return Entity.FromHandle(ENTITY.GET_ENTITY_ATTACHED_TO(Handle));
		}

		#endregion

		#region Forces

		public void ApplyForce(Vector3 direction)
		{
			ApplyForce(direction, Vector3.Zero, 1);
		}
		public void ApplyForce(Vector3 direction, Vector3 rotation)
		{
			ApplyForce(direction, rotation, 1);
		}
		public void ApplyForce(Vector3 direction, Vector3 rotation, int forceType)
		{
			ENTITY.APPLY_FORCE_TO_ENTITY(Handle, (int)forceType, direction.X, direction.Y, direction.Z, rotation.X, rotation.Y, rotation.Z, 0, false, true, true, false, true);
		}
		public void ApplyForceRelative(Vector3 direction)
		{
			ApplyForceRelative(direction, Vector3.Zero, 1);
		}
		public void ApplyForceRelative(Vector3 direction, Vector3 rotation)
		{
			ApplyForceRelative(direction, Rotation, 1);
		}
		public void ApplyForceRelative(Vector3 direction, Vector3 rotation, int forceType)
		{
			ENTITY.APPLY_FORCE_TO_ENTITY(Handle, (int)forceType, direction.X, direction.Y, direction.Z, rotation.X, rotation.Y, rotation.Z, 0, true, true, true, false, true);
		}

		#endregion

		public override void Delete()
		{
			int handle = Handle;

			// Request ownership of entity if we do not have it
			if (!ENTITY.DOES_ENTITY_BELONG_TO_THIS_SCRIPT(handle, true) || !ENTITY._DOES_THREAD_OWN_THIS_ENTITY(handle)) {
				ENTITY.SET_ENTITY_AS_MISSION_ENTITY(handle, true, true);
			}
			
			unsafe { ENTITY.DELETE_ENTITY(&handle); }
		}

		public override bool Exists()
		{
			return ENTITY.DOES_ENTITY_EXIST(Handle);
		}
		public static bool Exists(Entity entity)
		{
			return entity != null && entity.Exists();
		}

		public bool Equals(Entity obj)
		{
			return !(obj is null) && Handle == obj.Handle;
		}
		public override bool Equals(object obj)
		{
			return !(obj is null) && obj.GetType() == GetType() && Equals((Entity)obj);
		}

		public static bool operator ==(Entity left, Entity right)
		{
			return left is null ? right is null : left.Equals(right);
		}
		public static bool operator !=(Entity left, Entity right)
		{
			return !(left == right);
		}

		public override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
	}
}
