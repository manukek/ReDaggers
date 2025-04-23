using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
namespace ReDaggers.Projectiles
{
    public class DaggerSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ownerHitCheck = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 30;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = Main.MouseWorld - player.MountedCenter;
            direction.Normalize();
            float offsetDistance = 40f;
            Projectile.Center = player.MountedCenter + direction * offsetDistance;

            float rot = direction.ToRotation();
            if (rot > MathHelper.PiOver2 || rot < -MathHelper.PiOver2)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = rot + MathHelper.Pi;
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = rot;
            }
            Projectile.direction = Projectile.spriteDirection;

            Projectile.frameCounter++;
            if (Projectile.frameCounter >= 4)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;
                if (Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            if (player.itemAnimation <= 1 || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }
        }
    }
}
