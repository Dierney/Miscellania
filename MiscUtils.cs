﻿
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace GoldensMisc
{
	public class MiscUtils
	{
		static readonly int[] vanillaPowerups = new int[]
		{
				ItemID.Heart,
				ItemID.CandyApple,
				ItemID.CandyCane,
				ItemID.Star,
				ItemID.SoulCake,
				ItemID.SugarPlum,
				ItemID.NebulaPickup1,
				ItemID.NebulaPickup2,
				ItemID.NebulaPickup3
		};

		//Thank you Iriazul for this!
		public static bool DoesBeamCollide(Rectangle targetHitbox, Vector2 beamStart, float beamAngle, float beamWidth)
		{
			float length = GetBeamLength(beamStart, beamAngle);
			var endPoint = beamStart + beamAngle.ToRotationVector2() * length;
			float temp = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), beamStart, endPoint, beamWidth, ref temp);
		}

		public static float GetBeamLength(Vector2 beamStart, float beamAngle, float maxLength = 10000f)
		{
			var startTile = beamStart.ToTileCoordinates();
			var endTile = (beamStart + beamAngle.ToRotationVector2() * maxLength).ToTileCoordinates();
			Tuple<int, int> collisionTile;
			if(!Collision.TupleHitLine(startTile.X, startTile.Y, endTile.X, endTile.Y,
									   0, 0, new List<Tuple<int, int>>(), out collisionTile))
			{
				return maxLength;
			}
			var beamEnd = new Vector2(collisionTile.Item1 * 16 + 8, collisionTile.Item2 * 16 + 8);
			return (beamStart - beamEnd).Length();
		}

		public static Zone GetZoneInLocation(int x, int y)
		{
			int[] countedTiles = CountBiomeTiles(x, y);

			var currentZone = (Zone)0;
			if(countedTiles[0] >= 100)
				currentZone |= Zone.Holy;
			if(countedTiles[1] >= 200)
				currentZone |= Zone.Corrupt;
			if(countedTiles[2] >= 200)
				currentZone |= Zone.Crimson;
			if(countedTiles[3] >= 300)
				currentZone |= Zone.Snow;
			if(countedTiles[4] >= 80)
				currentZone |= Zone.Jungle;
			if(countedTiles[5] >= 200)
				currentZone |= Zone.Shroom;
			if(countedTiles[6] >= 250 && y > Main.worldSurface && Main.wallDungeon[Main.tile[x, y].wall])
				currentZone |= Zone.Dungeon;

			return currentZone;
		}

		//don't do this too often kids
		//that's one laggy boi
		//0=holy, 1=corrupt, 2=crimson, 3=snow, 4=jungle, 5=shroom, 6=dungeon
		static int[] CountBiomeTiles(int x, int y)
		{
			int[] results = new int[7];

			int startX = Math.Max(0, x - Main.zoneX / 2);
			int startY = Math.Max(0, y - Main.zoneY / 2);
			int endX = Math.Min(Main.maxTilesX, x + Main.zoneX / 2);
			int endY = Math.Min(Main.maxTilesY, y + Main.zoneY / 2);
			for(int i = startX; i < endX; i++)
			{
				for(int j = startY; j < endY; j++)
				{
					var tile = Main.tile[i, j];
					if(tile == null || !tile.active())
						continue;
					switch(tile.type)
					{
						case 109:
						case 110:
						case 113:
						case 117:
						case 116:
						case 403:
						case 402:
							results[0]++;
							break;
						case 23:
						case 24:
						case 25:
						case 32:
						case 112:
						case 400:
						case 398:
							results[1]++;
							break;
						case 27:
							results[1] -= 5;
							results[2] -= 5;
							break;
						case 199:
						case 203:
						case 401:
						case 399:
						case 234:
						case 352:
							results[2]++;
							break;
						case 147:
						case 148:
						case 161:
						case 162:
							results[3]++;
							break;
						case 164:
							results[3]++;
							results[0]++;
							break;
						case 163:
							results[1]++;
							results[3]++;
							break;
						case 200:
							results[2]++;
							results[3]++;
							break;
						case 60:
						case 61:
						case 62:
						case 74:
						case 226:
							results[4]++;
							break;
						case 70:
						case 71:
						case 72:
							results[5]++;
							break;
						case 41:
						case 43:
						case 44:
							results[6]++;
							break;
					}
				}
			}
			return results;
		}

		public static bool IsActuallyAnItem(Item item)
		{
			return item.stack != 0 && item.type != 0 && !vanillaPowerups.Contains(item.type);
		}

		public static class UI
		{
			public static readonly Color defaultUIBlue = new Color(73, 94, 171);
			public static readonly Color defaultUIBlueMouseOver = new Color(63, 82, 151) * 0.7f;

			public static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				Main.PlaySound(SoundID.MenuTick);
				((UIPanel)evt.Target).BackgroundColor = defaultUIBlue;
			}

			public static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				((UIPanel)evt.Target).BackgroundColor = defaultUIBlueMouseOver;
			}

			public static void CustomFadedMouseOver(Color customColor, UIMouseEvent evt, UIElement listeningElement)
			{
				Main.PlaySound(SoundID.MenuTick);
				((UIPanel)evt.Target).BackgroundColor = customColor;
			}

			public static void CustomFadedMouseOut(Color customColor, UIMouseEvent evt, UIElement listeningElement)
			{
				((UIPanel)evt.Target).BackgroundColor = customColor;
			}
		}
	}

	//only zones that are relevant to fishing are here
	[Flags]
	public enum Zone
	{
		None = 0,
		Corrupt = 1,
		Crimson = 2,
		Holy = 4,
		Snow = 8,
		Jungle = 16,
		Shroom = 32,
		Dungeon = 64
	}

	//this was supposed to use Extension methods but tML had trouble compiling it
	public static class ChestExtensions
	{
		public static bool RemoveItem(Chest shop, int type)
		{
			bool found = false;
			for(int i = 0; i < shop.item.Length; i++)
			{
				if(shop.item[i].type == type)
				{
					found = true;
				}
				if(found)
				{
					if(i < shop.item.Length - 1)
					{
						shop.item[i] = shop.item[i + 1];
					}
					else
					{
						shop.item[i] = new Item();
					}
				}
			}
			return found;
		}

		public static bool HasItem(Chest shop, int type)
		{
			for(int i = 0; i < shop.item.Length; i++)
			{
				if(shop.item[i].type == type)
				{
					return true;
				}
			}
			return false;
		}

		public static Item PutItem(Chest chest, Item item, bool smartStack = false)
		{
			if(!MiscUtils.IsActuallyAnItem(item))
				return item;
			bool hasEmptySpace = false;
			bool hasSameItem = false;
			for(int index = 0; index < chest.item.Length; ++index)
			{
				if(chest.item[index].type > 0 && chest.item[index].stack > 0)
				{
					if(item.IsTheSameAs(chest.item[index]))
					{
						int num = chest.item[index].maxStack - chest.item[index].stack;
						if(num > 0)
						{
							if(num > item.stack)
								num = item.stack;
							item.stack -= num;
							chest.item[index].stack += num;
							if(item.stack <= 0)
							{
								item.SetDefaults(0, true);
								return item;
							}
						}
						hasSameItem = true;
					}
				}
				else
					hasEmptySpace = true;
			}
			if((!smartStack || hasSameItem) && hasEmptySpace && item.stack > 0)
			{
				for(int index = 0; index < chest.item.Length; ++index)
				{
					if(chest.item[index].type == 0 || chest.item[index].stack == 0)
					{
						chest.item[index] = item.Clone();
						item.SetDefaults(0, true);
						return item;
					}
				}
			}
			return item;
		}
	}
}
