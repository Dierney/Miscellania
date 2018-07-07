﻿
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class InertStone : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.MagicStones;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'Has the potential to store magical energy'");

			DisplayName.AddTranslation(GameCulture.Russian, "Инертный камень");
			Tooltip.AddTranslation(GameCulture.Russian, "'Имеет потенциал для хранения магической энергии'");

			DisplayName.AddTranslation(GameCulture.Chinese, "无效的石头");
			Tooltip.AddTranslation(GameCulture.Chinese, "'它似乎可以储存魔法能量'");
		}
		
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.rare = 3;
			item.value = Item.buyPrice(0, 50);
		}
		
		//This is necessary to prevent Life and Mana Stones from dissapearing when using Quick Heal/Mana
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
	}
}
