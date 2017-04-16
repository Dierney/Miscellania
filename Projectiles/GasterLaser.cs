﻿
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class GasterLaser : ModProjectile
	{
		const float BeamWidth = 26f;
		float BeamLength
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		Vector2 endPoint;
		
		public override void SetDefaults()
		{
			ProjectileID.Sets.MinionShot[projectile.type] = true;
			projectile.netImportant = true;
			projectile.name = "Skull Laser";
			projectile.width = 1;
			projectile.height = 1;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 2;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
			projectile.scale = 1.5f;
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return MiscUtils.DoesBeamCollide(targetHitbox, projHitbox.Center(), projectile.rotation, BeamWidth);
		}
		
		public override void AI()
		{
			BeamLength = MiscUtils.GetBeamLength(projectile.Center, projectile.rotation);
			endPoint = projectile.Center + projectile.rotation.ToRotationVector2() * BeamLength;
			int dustAmount = Main.rand.Next(1, 4);
			for(int i = 0; i < dustAmount; i++)
			{
				int dust = Dust.NewDust(endPoint - new Vector2(10, 10), 20, 20, DustID.Smoke, Main.rand.NextFloat(), Main.rand.NextFloat());
				Main.dust[dust].noGravity = true;
			}
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(projectile.Center, endPoint, BeamWidth, new Utils.PerLinePoint(DelegateMethods.CastLight));
		}
		
		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			var unit = projectile.velocity;
			Utils.PlotTileLine(projectile.Center, endPoint, BeamWidth, new Utils.PerLinePoint(DelegateMethods.CutTiles));
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var unit = endPoint - projectile.Center;
			unit.Normalize();
//			DrawLaser(spriteBatch, Main.projectileTexture[projectile.type], startPoint, unit, 5, -1.57f, projectile.scale);
//			return false;
//		}
//
//		/// <summary>
//		/// The core function of drawing a laser
//		/// </summary>
//		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, Color color = default(Color))
//		{
			var position = projectile.Center;
			float r = unit.ToRotation() - MathHelper.PiOver2;
			const int transDist = 80;
			const int step = 8;

			#region Draw laser body
			for (float i = transDist; i <= BeamLength; i += step)
			{
				position = projectile.Center + i * unit;
				spriteBatch.Draw(Main.projectileTexture[projectile.type], position - Main.screenPosition,
				                 new Rectangle(0, 24, 38, 30), i < transDist ? Color.Transparent : Color.White, r,
				                 new Vector2(38 / 2, 30 / 2), projectile.scale, SpriteEffects.None, 0);
			}
			#endregion

			#region Draw laser tail
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center + unit * (transDist - step) - Main.screenPosition,
			                 new Rectangle(0, 0, 38, 22), Color.White, r, new Vector2(38 / 2, 22 / 2), projectile.scale, SpriteEffects.None, 0);
			#endregion

			#region Draw laser head
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center + unit * (BeamLength + step) - Main.screenPosition,
			                 new Rectangle(0, 56, 38, 22), Color.White, r, new Vector2(38 / 2, 22 / 2), projectile.scale, SpriteEffects.None, 0);
			#endregion
			return false;
		}
	}
}
