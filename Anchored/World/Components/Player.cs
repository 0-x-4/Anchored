﻿using Anchored.Assets;
using Anchored.World.Components.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Anchored.World.Components
{
	public class Player : Component, IUpdatable
	{
		private Mover mover;

		public const float ACCELERATION = 120f;
		public const float MAX_SPEED = 250f;

		public Player()
		{
		}

		public Player(Mover mover)
		{
			this.mover = mover;
		}

		public void Update()
		{
			int mx = 0;
			int my = 0;

			if (Input.IsDown(Keys.W))
				my -= 1;

			if (Input.IsDown(Keys.S))
				my += 1;

			if (Input.IsDown(Keys.A))
				mx -= 1;

			if (Input.IsDown(Keys.D))
				mx += 1;

			float angle = MathF.Atan2(my, mx);

			if (mx != 0)
				mover.Velocity.X = MathF.Cos(angle) * ACCELERATION;

			if (my != 0)
				mover.Velocity.Y = MathF.Sin(angle) * ACCELERATION;

			mover.Velocity.X = MathHelper.Clamp(mover.Velocity.X, -MAX_SPEED, MAX_SPEED);
			mover.Velocity.Y = MathHelper.Clamp(mover.Velocity.Y, -MAX_SPEED, MAX_SPEED);
		}
	}
}
