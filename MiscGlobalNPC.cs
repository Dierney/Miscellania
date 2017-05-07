﻿
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using GoldensMisc.Items;
using GoldensMisc.Items.Consumable;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc
{
	public class MiscGlobalNPC : GlobalNPC
	{
		public override void SetupShop(int type, Chest shop, ref int nextSlot)
		{
			switch(type)
			{
				case NPCID.GoblinTinkerer:
					if(Config.Magnets && Main.hardMode && !Main.dayTime)
					{
						shop.item[nextSlot].SetDefaults(mod.ItemType<UniversalMagnet>());
						nextSlot++;
					}
					break;
				case NPCID.Demolitionist:
					if(Config.ReinforcedVest && Main.hardMode)
					{
						shop.item[nextSlot].SetDefaults(mod.ItemType<ReinforcedVest>());
						nextSlot++;
					}
					break;
				case NPCID.Wizard:
					if(Config.MagicStones && NPC.downedMechBossAny)
					{
						shop.item[nextSlot].SetDefaults(mod.ItemType<InertStone>());
						nextSlot++;
					}
					break;
			}
		}
		
		public override void NPCLoot(NPC npc)
		{
			switch(npc.type)
			{  
				case NPCID.AngryBones:
				case NPCID.AngryBonesBig:
				case NPCID.AngryBonesBigHelmet:
				case NPCID.AngryBonesBigMuscle:
				case NPCID.DarkCaster:
					if(Config.AncientMuramasa && Main.rand.Next(250) == 0)
					{
						Item.NewItem(npc.position, npc.Size, mod.ItemType<AncientMuramasa>(), prefixGiven: -1);
					}
					break;
				case NPCID.Demon:
					if(Config.DemonCrown && Main.hardMode && Main.rand.Next(100) == 0)
					{
						Item.NewItem(npc.position, npc.Size, mod.ItemType<DemonCrown>(), prefixGiven: -1);
					}
					break;
				case NPCID.VoodooDemon:
					if(Config.DemonCrown && Main.hardMode && Main.rand.Next(15) == 0)
					{
						Item.NewItem(npc.position, npc.Size, mod.ItemType<DemonCrown>(), prefixGiven: -1);
					}
					break;
			}
		}
	}
}
