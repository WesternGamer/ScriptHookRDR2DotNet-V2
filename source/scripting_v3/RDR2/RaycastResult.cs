using System;
using RDR2.Math;
using RDR2.Native;

namespace RDR2
{
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
