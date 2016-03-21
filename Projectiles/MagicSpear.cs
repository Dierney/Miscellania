﻿
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class MagicSpear : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.name = "Magic Spear";
			projectile.scale = 1.3f;
			projectile.width = 14;
			projectile.height = 14;
			projectile.aiStyle = 1;
			projectile.friendly = true;
			projectile.penetrate = 2;
			projectile.magic = true;
			projectile.ignoreWater = true;
			aiType = ProjectileID.JavelinFriendly;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int type = Main.rand.Next(2) == 0 ? mod.ProjectileType("MagicSpearMini") : mod.ProjectileType("MagicSpearMiniAlt");
			
			switch(Main.rand.Next(4))
			{
				case 0: //Shoot right
					Projectile.NewProjectile(target.position.X - 64, target.position.Y, 3f, 0f, type, projectile.damage / 3, 0.5f, projectile.owner, 0, 1);
					return;
				case 1: //Shoot down
					Projectile.NewProjectile(target.position.X, target.position.Y - 64, 0f, 3f, type, projectile.damage / 3, 0.5f, projectile.owner, 0, 1);
					return;
				case 2: //Shoot right
					Projectile.NewProjectile(target.position.X + 64, target.position.Y, -3f, 0f, type, projectile.damage / 3, 0.5f, projectile.owner, 0, 1);
					return;
				case 3: //Shoot up
					Projectile.NewProjectile(target.position.X, target.position.Y + 64, 0f, -3f, type, projectile.damage / 3, 0.5f, projectile.owner, 0, 1);
					return;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(2, projectile.position, 10);
			for (int i = 0; i < 4; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 59, projectile.velocity.X, projectile.velocity.Y);
				Main.dust[dust].scale = 1.4f;
			}
		}
	}
}
