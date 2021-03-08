﻿using Anchored.Debug.Console;
using Anchored.Debug.Info;
using Anchored.World;
using ImGuiNET;
using Microsoft.Xna.Framework;
using System;
using System.Numerics;

namespace Anchored.Debug
{
	public static class DebugManager
	{
		private static System.Numerics.Vector2 size;
		private static System.Numerics.Vector3 bgcol = new System.Numerics.Vector3();

		public static bool Cheats = false; // not done
		public static bool Entities = false; // not done
		public static bool RunInfo = false; // not done
		public static bool Console = true; // not done
		public static bool ItemEditor = false; // not done
		public static bool LevelEditor = false; // not done
		public static bool LayerDebug = false; // not done
		public static bool LocaleEditor = false; // not done
		public static bool Rooms = false; // not done
		public static bool Settings = false; // not done
		public static bool SaveEditor = false; // not done
		public static bool LootTableEditor = false; // not done
		public static bool DebugView = false;

		private static void DrawSettings()
		{
			if (!Settings)
				return;

			if (!ImGui.Begin("Settings"))
			{
				ImGui.End();
				return;
			}

			if (ImGui.ColorPicker3("Background Colour", ref bgcol))
			{
				Game1.BackgroundColor = new Color(bgcol.X, bgcol.Y, bgcol.Z, 255);
			}

			ImGui.End();
		}

		public static void Draw(EntityWorld world)
		{
			DrawSettings();
			//RunStatistics.DrawDebug();
			Anchored.Debug.Info.DebugView.Draw();

			ImGui.SetNextWindowPos(new System.Numerics.Vector2(Game1.WINDOW_WIDTH - size.X - 10, Game1.WINDOW_HEIGHT - size.Y - 10));
			ImGui.Begin("Windows", ImGuiWindowFlags.NoCollapse |
								   ImGuiWindowFlags.AlwaysAutoResize |
								   ImGuiWindowFlags.NoTitleBar);

			if (ImGui.Button("Hide All"))
			{
				Cheats = DebugView = Entities = RunInfo = ItemEditor = LevelEditor = LayerDebug = LocaleEditor = Rooms = Settings = SaveEditor = LootTableEditor = false;
			}

			ImGui.SameLine();

			if (ImGui.Button("Show All"))
			{
				Cheats = DebugView = Entities = RunInfo = ItemEditor = LevelEditor = LayerDebug = LocaleEditor = Rooms = Settings = SaveEditor = LootTableEditor = true;
			}

			ImGui.BeginMainMenuBar();
			{
				if (ImGui.BeginMenu("General"))
				{
					ImGui.MenuItem("Settings", "", ref Settings);
					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Editors"))
				{
					ImGui.MenuItem("Level Editor", "", ref LevelEditor);
					ImGui.MenuItem("Item Editor", "", ref ItemEditor);
					ImGui.MenuItem("Locale Editor", "", ref LocaleEditor);
					ImGui.MenuItem("Loot Table Editor", "", ref LootTableEditor);
					ImGui.MenuItem("Save Editor", "", ref SaveEditor);
					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Tools"))
				{
					ImGui.MenuItem("Console", "", ref Console);
					ImGui.MenuItem("Cheats", "", ref Cheats);
					ImGui.MenuItem("Rooms", "", ref Rooms);
					ImGui.MenuItem("Layer Debug", "", ref LayerDebug);
					ImGui.EndMenu();
				}

				if (ImGui.BeginMenu("Info"))
				{
					ImGui.MenuItem("Entities", "", ref Entities);
					ImGui.MenuItem("Run Info", "", ref RunInfo);
					ImGui.MenuItem("Debug View", "", ref DebugView);
					ImGui.EndMenu();
				}
			}
			ImGui.EndMainMenuBar();

			size = ImGui.GetWindowSize();
			ImGui.End();
		}
	}
}
