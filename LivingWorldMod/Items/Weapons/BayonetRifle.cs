using Microsoft.Xna.Framework;
using LivingWorldMod.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LivingWorldMod.Items.Weapons {

    public class BayonetRifle : ModItem {

        public override void SetStaticDefaults() {
            DisplayName.SetDefault("Bayonet Rifle");
            Tooltip.SetDefault("Can stab and shoot");
        }

        public override void SetDefaults() {
            item.damage = 32;
            item.useStyle = 5;
            item.useAnimation = 24;
            item.useTime = 24;
            item.shootSpeed = 3f;
            item.knockBack = 6f;
            item.width = 32;
            item.height = 32;
            item.scale = 1f;
            item.rare = 5;
            item.value = 10000;

            item.melee = true;
            item.noMelee = true; // Important because the spear is actually a projectile instead of an item. This prevents the melee hitbox of this item.
            item.noUseGraphic = true; // Important, it's kind of wired if people see two spears at one time. This prevents the melee animation of this item.
            item.autoReuse = true; // Most spears don't autoReuse, but it's possible when used in conjunction with CanUseItem()

            item.UseSound = SoundID.Item1;
            item.shoot = mod.ProjectileType<BayonetRifleSpearProjectile>();
        }

        public override void AddRecipes() {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.PlatinumPickaxe, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool AltFunctionUse(Player player) {
            return true;
        }

        public override bool CanUseItem(Player player) {
            if (player.altFunctionUse == 2) {    //2 is right click
                item.melee = false;
                item.ranged = true;
                item.useTime = 20;
                item.useAnimation = 20;
                item.damage = 52;
                item.UseSound = SoundID.Item1;
                item.shoot = 10;
                item.shootSpeed = 10f;
                item.useAmmo = AmmoID.Bullet;
                item.noUseGraphic = false;
            } else {
                item.ranged = false;
                item.melee = true;
                item.useTime = 24;
                item.useAnimation = 24;
                item.damage = 32;
                item.UseSound = SoundID.Item1;
                item.shoot = mod.ProjectileType<BayonetRifleSpearProjectile>();
                item.shootSpeed = 3.7f;
                item.useAmmo = 0;
                item.noUseGraphic = true;

            }
            return base.CanUseItem(player) && player.ownedProjectileCounts[item.shoot] < 1;
        }
    }
}
