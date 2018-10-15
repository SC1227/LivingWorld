using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace LivingWorldMod.Items.Tools
{
	public class SwissArmyKnife : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Swiss Army Knife");
			Tooltip.SetDefault("Functions as a pickaxe, axe, and hammer");
		}
		public override void SetDefaults()
		{
			item.damage = 8;
			item.melee = true;
			item.width = 40;
			item.height = 40;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 6;
			item.value = 10000;
			item.rare = 1;
			item.UseSound = SoundID.Item1;
			item.autoReuse = true;
			
			item.pick = 60;
			item.axe = (60/5);
			item.hammer = 60;
		}

		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.PlatinumPickaxe, 1);
			recipe.AddIngredient(ItemID.PlatinumHammer, 1);
			recipe.AddIngredient(ItemID.PlatinumAxe, 1);
            recipe.AddIngredient(ItemID.DirtBlock, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
