using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.GameContent.Generation;
using Terraria.World.Generation;
using Terraria.World;


namespace LivingWorldMod {

    public class LivingWorldModWorld : ModWorld {

        public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight) {

            int dungeonGen = tasks.FindIndex(genpass => genpass.Name.Equals("Jungle"));
            tasks[dungeonGen] = new PassLegacy("Living Dungeon", delegate (GenerationProgress progress) {
                WorldGen.TileRunner(Main.spawnTileX, Main.spawnTileY + 12, 6, Main.rand.Next(1, 3), TileID.Dirt, true, 0f, 0f, true, true);
            });

        }

        private void dungeonGen() {

            




        }

    }

}