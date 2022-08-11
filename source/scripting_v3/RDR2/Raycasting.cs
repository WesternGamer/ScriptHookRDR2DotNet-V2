using System;
using RDR2.Native;
using RDR2.Math;

namespace RDR2
{
	public static class Raycasting
	{
		public static RaycastResult Raycast(Vector3 source, Vector3 dest, IntersectOptions options, Entity entityToIgnore = null)
		{
			return new RaycastResult(SHAPETEST.START_EXPENSIVE_SYNCHRONOUS_SHAPE_TEST_LOS_PROBE(source.X, source.Y, source.Z,
				dest.X, dest.Y, dest.Z, (int)options, entityToIgnore == null ? 0 : entityToIgnore.Handle, 7));
		}

		public static RaycastResult Raycast(Vector3 source, Vector3 direction, float maxDist, IntersectOptions options, Entity entityToIgnore = null)
		{
			var target = source + direction * maxDist;
			return new RaycastResult(SHAPETEST.START_EXPENSIVE_SYNCHRONOUS_SHAPE_TEST_LOS_PROBE(source.X, source.Y, source.Z,
				target.X, target.Y, target.Z, (int)options, entityToIgnore == null ? 0 : entityToIgnore.Handle, 7));
		}

		public static RaycastResult CenterScreenRaycast(float maxDist, IntersectOptions options, Entity entityToIgnore = null)
		{
			var source = GameplayCamera.Position;
			var rotation = (float)(System.Math.PI / 180.0) * GameplayCamera.Rotation;
			var forward = Vector3.Normalize(new Vector3(
				(float)-System.Math.Sin(rotation.Z) * (float)System.Math.Abs(System.Math.Cos(rotation.X)),
				(float)System.Math.Cos(rotation.Z) * (float)System.Math.Abs(System.Math.Cos(rotation.X)),
				(float)System.Math.Sin(rotation.X)));
			var target = source + forward * maxDist;
			return Raycast(source, target, options, entityToIgnore);
		}
	}

	public struct RaycastResult
	{
		public Entity HitEntity { get; }
		public Vector3 HitPosition { get; }
		public Vector3 SurfaceNormal { get; }
		public bool DidHit { get; }
		public bool DidHitEntity { get; }
		public int Result { get; }

		public RaycastResult(int handle)
		{
			bool didHit = false;
			Vector3 hitCoords = Vector3.Zero;
			Vector3 surfaceNormal = Vector3.Zero;
			int entityHit = 0;

			unsafe { Result = SHAPETEST.GET_SHAPE_TEST_RESULT(handle, &didHit, &hitCoords, &surfaceNormal, &entityHit); }

			HitPosition = hitCoords;
			HitEntity = entityHit == 0 || HitPosition == default ? null : Entity.FromHandle(entityHit);
			DidHitEntity = HitEntity != null && HitPosition != default && HitEntity.EntityType != 0;
			DidHit = didHit;
			SurfaceNormal = surfaceNormal;
		}
	}

	[Flags]
	public enum IntersectOptions
	{
		Everything = -1,
		Map = 1,
		MissionEntities,
		Peds1 = 12,
		Objects = 16,
		Unk1 = 32,
		Unk2 = 64,
		Unk3 = 128,
		Vegetation = 256,
		Unk4 = 512
	}
}
