﻿
using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace GoldensMisc.Commands
{
	public class WorldGenCommand : ModCommand
	{
		public override CommandType Type => CommandType.World;
		
		public override string Command => "miscWorldGen";
		
		public override string Usage => "/miscWorldGen <forge|hellforge>";
		
		public override string Description => Language.GetTextValue("Mods.GoldensMisc.Command.WorldGen.Description");
		
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if(args.Length != 1)
			{
				throw new UsageException(Language.GetTextValue("Mods.GoldensMisc.CommandUsage") + " " + Usage);
			}
			if(args[0].Equals("hellforge", StringComparison.OrdinalIgnoreCase))
			{
				if(!Config.AncientForges)
				{
					Main.NewText(Language.GetTextValue("Mods.GoldensMisc.Command.WorldGen.HellforgeDisabled"));
					return;
				}
				new Task(() => mod.GetModWorld<MiscWorld>().AddHellforges()).Start();
			}
			if(args[0].Equals("forge", StringComparison.OrdinalIgnoreCase))
			{
				if(!Config.AncientForges)
				{
					Main.NewText(Language.GetTextValue("Mods.GoldensMisc.Command.WorldGen.ForgeDisabled"));
					return;
				}
				new Task(() => mod.GetModWorld<MiscWorld>().AddForges()).Start();
			}
			else
			{
				throw new UsageException(Language.GetTextValue("Mods.GoldensMisc.CommandUsage") + " " + Usage);
			}
			return;
		}
	}
}
