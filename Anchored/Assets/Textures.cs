﻿using Anchored.Streams;
using Anchored.Util;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Anchored.Assets
{
	public class Textures
	{
		private static Dictionary<string, TextureRegion> textures = new Dictionary<string, TextureRegion>();
		public static TextureRegion NULL;

		internal static void Load()
		{
			var textureDir = FileHandle.FromRoot("txrs\\");

			if (textureDir.Exists())
				LoadTextures(textureDir);
		}

		public static Texture2D FastLoad(string path)
		{
			var fileStream = new FileStream(path, FileMode.Open);
			var texture = Texture2D.FromStream(Game1.GraphicsDevice, fileStream);
			fileStream.Dispose();
			return texture;
		}

		private static void LoadTextures(FileHandle handle)
		{
			foreach (var h in handle.ListFileHandles())
				LoadTexture(h);

			foreach (var h in handle.ListDirectoryHandles())
				LoadTextures(h);
		}

		private static void LoadTexture(FileHandle handle)
		{
			var region = new TextureRegion();

			string folder = handle.FullPath;
			folder = folder.Remove(handle.FullPath.Length - handle.Name.Length, handle.Name.Length);
			folder = folder.Remove(0, (AssetManager.Root + "\\txrs").Length);

			string id = folder + handle.NameWithoutExtension;

			if (AssetManager.LoadOriginalFiles)
			{
				var fileStream = new FileStream(handle.FullPath, FileMode.Open);
				region.Texture = Texture2D.FromStream(Game1.GraphicsDevice, fileStream);
				fileStream.Dispose();
			}
			else
			{
				region.Texture = AssetManager.Content.Load<Texture2D>($"txrs\\{id}");
			}

			region.Source = region.Texture.Bounds;
			textures[id] = region;
		}

		internal static void Destroy()
		{
			foreach (var region in textures.Values)
				region.Texture.Dispose();

			textures.Clear();
		}

		public static TextureRegion Get(string id)
		{
			if (textures.TryGetValue(id, out var region))
				return new TextureRegion(region.Texture, region.Source);

			return Textures.NULL;
		}
	}
}
