﻿using Anchored.World;
using Anchored.World.Components;
using Microsoft.Xna.Framework;

namespace Anchored.Physics
{
	public class Hit
	{
		public Entity Other => Collider.Entity;
		public Collider Collider;
		public Vector2 Pushout;
	}
}
