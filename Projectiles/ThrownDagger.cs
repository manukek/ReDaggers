using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.ID;

namespace ReDaggers.Projectiles
{
    public class ThrownDagger : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 1;
        }

        public override string Texture => $"Terraria/Images/Item_{Main.player[Projectile.owner].HeldItem.type}";

        public override void SetDefaults()
        {
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.scale = 0.8f;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 200f;
                Projectile.netUpdate = true;
            }

            if (Projectile.ai[0] == 0f)
            {
                if (Vector2.Distance(Projectile.Center, player.MountedCenter) >= Projectile.localAI[0])
                {
                    Projectile.ai[0] = 1f;
                    Projectile.netUpdate = true;
                }
            }

            if (Projectile.ai[0] == 1f)
            {
                Vector2 returnDir = player.MountedCenter - Projectile.Center;
                returnDir.Normalize();
                Projectile.velocity = returnDir * 12f;
                
                if (Vector2.Distance(Projectile.Center, player.MountedCenter) < 20f)
                {
                    Projectile.Kill();
                    return;
                }
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 10; i++)
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Iron);
            Projectile.Kill();
            return false;
        }
    }
}