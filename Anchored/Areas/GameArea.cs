﻿using Anchored.Assets;
using Anchored.Assets.Maps;
using Anchored.Util;
using Anchored.World;
using Anchored.World.Components;
using Anchored.World.Types;
using Arch.Assets.Maps;
using Arch.World;
using Arch.World.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Anchored.Areas
{
	public class GameArea
	{
		protected EntityWorld world;
		protected Entity cameraEntity;

		public Camera Camera => cameraEntity.GetComponent<Camera>();
		
		public GameArea(EntityWorld world)
		{
			this.world = world;
			cameraEntity = SetupCamera(320, 180, 160, 90);
			Camera.Main = Camera;
		}

		public virtual void Load(SpriteBatch sb)
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Update()
		{
			world.Update();
		}

		public virtual void Draw(SpriteBatch sb)
		{
			world.Draw(sb, Camera.Main.GetPerfectViewMatrix());
		}

		public virtual void DrawUI(SpriteBatch sb)
		{
		}

		protected Entity SetupCamera(int width, int height, int originX = 0, int originY = 0)
		{
			var camEntity = world.AddEntity("Camera");
			var camera = camEntity.AddComponent(new Camera(width, height));
			camera.Origin = new Vector2(originX, originY);
			return camEntity;
		}

		protected Entity SetupLevel(AnchoredMap map, bool loadColliders = true, bool loadEntities = true)
		{
			Entity tileMapEntity = world.AddEntity("TileMap");
			new LevelType(map, loadColliders, loadEntities).Create(tileMapEntity);
			return tileMapEntity;
		}
	}
}
