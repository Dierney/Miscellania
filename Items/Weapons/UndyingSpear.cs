﻿
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class UndyingSpear : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.SpearofJustice;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spear of Undying Justice");
			Tooltip.SetDefault("Rapidly throws piercing spears");
			DisplayName.AddTranslation(GameCulture.Russian, "Копье бессмертного правосудия");
			Tooltip.AddTranslation(GameCulture.Russian, "Быстро метает пронзающие копья");
		}
		
		public override void SetDefaults()
		{
			item.value = Item.sellPrice(0, 7);
			item.useStyle = 1;
			item.useAnimation = 18;
			item.useTime = 18;
			item.autoReuse = true;
			item.rare = 8;
			item.width = 48;
			item.height = 48;
			item.UseSound = SoundID.Item8;
			item.damage = 55;
			item.knockBack = 5;
			item.mana = 12;
			item.shoot = mod.ProjectileType(GetType().Name);
			item.shootSpeed = 16f;
			item.noMelee = true; //So that the swing itself doesn't do damage; this weapon is projectile-only
			item.noUseGraphic = true; //No swing animation
			item.magic = true;
			item.crit = 10;
			item.glowMask = MiscGlowMasks.UndyingSpear;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(mod.ItemType<MagicSpear>());
			recipe.AddIngredient(ItemID.SpectreBar, 15);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}