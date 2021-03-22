﻿using System;
using Anchored.Debug.Console;
using Anchored.Streams;
using Anchored.World;
using Microsoft.Xna.Framework;

namespace Anchored.Assets.Prefabs
{
    public class Prefab
    {
        public PrefabData[] Datas;

        public void Place(EntityWorld world, int x, int y, string name = "")
        {
            var pos = new Vector2(x * 16, y * 16);
            FileReader reader = new FileReader(null);

            foreach (var d in Datas)
            {
                // todo: check if type is level and if so skip (for future when i implement LevelType entities)
                
                var entityType = (EntityType)Activator.CreateInstance(d.Type);

                if (entityType == null)
                {
                    DebugConsole.Error("Tried to place prefab with a non-existent entity type!");
                    return;
                }
                
                reader.SetData(d.Data);
                entityType.Load(reader);

                var entity = world.AddEntity(name);
                entityType.Create(entity);
            }
        }
    }
}
