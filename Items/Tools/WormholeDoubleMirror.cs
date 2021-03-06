﻿
using System;
using GoldensMisc.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Tools
{
	public class WormholeDoubleMirror : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.WormholeMirror;
		}
		
		public override void SetDefaults()
		{
			item.useTurn = true;
			item.width = 24;
			item.height = 24;
			item.useStyle = 4;
			item.useTime = 90;
			item.UseSound = SoundID.Item6;
			item.useAnimation = 90;
			item.rare = 5;
			item.value = Item.sellPrice(0, 10);
		}

		public override bool AltFunctionUse(Player player)
		{
			return true;
		}

		public override bool CanUseItem(Player player)
		{
			if(player.altFunctionUse != 2)
			{
				item.UseSound = SoundID.Item6;
				if(UIWormhole.Visible)
					return false;
			}
			else
			{
				item.UseSound = new LegacySoundStyle(SoundID.MenuOpen, 0);
			}
			return true;
		}

		public override bool UseItem(Player player)
		{
			if(player.altFunctionUse == 2)
			{
				if(UIWormhole.Visible)
					UIWormhole.Close();
				else
					UIWormhole.Open(item);
				return true;
			}

			if (Main.rand.Next(2) == 0)
				Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.White, 1.1f);
			if (player.itemAnimation == item.useAnimation / 2)
			{
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, 15, (float) (player.velocity.X * 0.5), (float) (player.velocity.Y * 0.5), 150, Color.White, 1.5f);
				player.grappling[0] = -1;
				player.grapCount = 0;
				for (int index = 0; index < 1000; ++index)
				{
					if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
						Main.projectile[index].Kill();
				}
				player.Spawn();
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.White, 1.5f);
			}
			return false;
		}

		public override bool CanRightClick()
		{
			//return Main.netMode != NetmodeID.SinglePlayer && Main.LocalPlayer.team != 0;
			return true;
		}

		public override void RightClick(Player player)
		{
			Main.PlaySound(SoundID.MenuOpen);
			if(UIWormhole.Visible)
				UIWormhole.Close();
			else
				UIWormhole.Open(item);
			item.stack++;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw(Main.itemTexture[ItemID.MagicMirror], position, frame, Color.White * alpha, 0f, origin, scale, 0, 0f);
		}
		
		public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
		{
			spriteBatch.Draw(Main.itemTexture[item.type], item.Center - Main.screenPosition, null, lightColor, rotation, item.Size / 2, scale, 0, 0f);
			float alpha = (float)Math.Sin((float)DateTime.Now.TimeOfDay.TotalMilliseconds / 500f) / 2f + 0.5f;
			spriteBatch.Draw(Main.itemTexture[ItemID.MagicMirror], item.Center - Main.screenPosition, null, lightColor * alpha, rotation, item.Size / 2, scale, 0, 0f);
			return false;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.MagicMirror);
			recipe.AddIngredient(mod.ItemType<WormholeMirror>());
			recipe.AddTile(TileID.TinkerersWorkbench);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
