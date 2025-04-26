using Terraria;
using Terraria.ModLoader;
using ReDaggers.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.DataStructures;

namespace ReDaggers.Global
{
    public class GlobalDaggers : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AltFunctionUse(Item item, Player player) {
            return true; 
        }

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.TungstenShortsword ||
                   item.type == ItemID.CopperShortsword ||
                   item.type == ItemID.TinShortsword ||
                   item.type == ItemID.SilverShortsword ||
                   item.type == ItemID.GoldShortsword ||
                   item.type == ItemID.PlatinumShortsword ||
                   item.type == ItemID.LeadShortsword ||
                   item.type == ItemID.Gladius ||
                   item.type == ItemID.Ruler;
        }

        public override bool CanShoot(Item item, Player player)
        {
            if (player.altFunctionUse == 2)
            {
                bool hasActiveDagger = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    if (Main.projectile[i].active && Main.projectile[i].owner == player.whoAmI && 
                        Main.projectile[i].type == ModContent.ProjectileType<ThrownDagger>())
                    {
                        hasActiveDagger = true;
                        break;
                    }
                }

                if (!hasActiveDagger)
                {
                    Vector2 shootDir = Main.MouseWorld - player.MountedCenter;
                    shootDir.Normalize();
                    Projectile.NewProjectile(player.GetSource_ItemUse(item), player.MountedCenter, 
                        shootDir * 12f, ModContent.ProjectileType<ThrownDagger>(), 
                        item.damage, item.knockBack, player.whoAmI);
                }
                return false;
            }

            Vector2 direction = Main.MouseWorld - player.MountedCenter;
            direction.Normalize();
            player.direction = Main.MouseWorld.X < player.MountedCenter.X ? -1 : 1;
            player.itemRotation = (Main.MouseWorld - player.MountedCenter).ToRotation() + 
                (player.direction == -1 ? -MathHelper.Pi : 0f);
            
            Projectile.NewProjectile(player.GetSource_ItemUse(item), player.MountedCenter, 
                Vector2.Zero, ModContent.ProjectileType<DaggerSlash>(), 
                item.damage, item.knockBack, player.whoAmI);
            return false;
        }
    }
}
