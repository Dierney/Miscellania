﻿
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Buffs
{
	public class CursedMemory : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cursed Memory");
			Description.SetDefault("Cuts maximum HP in half");

			DisplayName.AddTranslation(GameCulture.Russian, "Проклятые воспоминания");
			Description.AddTranslation(GameCulture.Russian, "Уменьшают максиальное здоровье наполовину");

			DisplayName.AddTranslation(GameCulture.Chinese, "被诅咒的记忆");
			Description.AddTranslation(GameCulture.Chinese, "最大生命值减半");

			canBeCleared = false;
			longerExpertDebuff = true;
			Main.debuff[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.statLifeMax2 = Math.Max(player.statLifeMax / 2, 100);
		}
	}
}
