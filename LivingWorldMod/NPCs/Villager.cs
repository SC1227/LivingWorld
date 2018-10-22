using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace LivingWorldMod.NPCs {

    [AutoloadHead]
    public class Villager : ModNPC {

//        private TownRoomManager townRoomManager = new TownRoomManager();

        public override bool Autoload(ref string name) {
            name = "Villager";
            return mod.Properties.Autoload;
        }

        public override void SetDefaults() {
            npc.GivenName = "Villager";
            npc.townNPC = true; //This defines if the npc is a town Npc or not
            npc.friendly = true;  //this defines if the npc can hur you or not()
            npc.width = 18; //the npc sprite width
            npc.height = 46;  //the npc sprite height
            npc.aiStyle = 7; //this is the npc ai style, 7 is Pasive Ai
            npc.defense = 25;  //the npc defense
            npc.lifeMax = 250; // the npc life
            npc.HitSound = SoundID.NPCHit1;  //the npc sound when is hit
            npc.DeathSound = SoundID.NPCDeath1;  //the npc sound when he dies
            npc.knockBackResist = 0.5f;  //the npc knockback resistance
            Main.npcFrameCount[npc.type] = 25; //this defines how many frames the npc sprite sheet has
            NPCID.Sets.ExtraFramesCount[npc.type] = 9;
            NPCID.Sets.AttackFrameCount[npc.type] = 4;
            NPCID.Sets.DangerDetectRange[npc.type] = 150; //this defines the npc danger detect range
            NPCID.Sets.AttackType[npc.type] = 3; //this is the attack type,  0 (throwing), 1 (shooting), or 2 (magic). 3 (melee)
            NPCID.Sets.AttackTime[npc.type] = 30; //this defines the npc attack speed
            NPCID.Sets.AttackAverageChance[npc.type] = 10;//this defines the npc atack chance
            NPCID.Sets.HatOffsetY[npc.type] = 4; //this defines the party hat position
            animationType = NPCID.Guide;  //this copy the guide animation
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money) {

            if (NPC.downedBoss1) { //so after the EoC is killed
                return true;
            }
            return false;
            // this make that he will spawn when a house is available
        }

        //Allows you to define special conditions required for this town NPC's house
        public override bool CheckConditions(int left, int right, int top, int bottom) {   
            return true;  //so when a house is available the npc will  spawn
        }

        public override string TownNPCName() {                                       //NPC names
            switch (WorldGen.genRand.Next(0, 6)) {
                case 0:
                    return "Rick"; break;
                case 1:
                    return "Denis"; break;
                case 2:
                    return "Heisenberg"; break;
                case 3:
                    return "Jack"; break;
                case 4:
                    return "Blue Magic"; break;
                case 5:
                    return "Blue"; break;
                default:
                    return "Walter"; break;
            }
        }

        //Allows you to set the text for the buttons that appear on this town NPC's chat window.
        public override void SetChatButtons(ref string button, ref string button2) {

            button = "Buy Potions";   //this defines the buy button name
            button2 = "Another Button";
        }


        //Allows you to make something happen whenever a button is clicked on this town NPC's chat window. 
        //The firstButton parameter tells whether the first button or second button (button and button2 from SetChatButtons) was clicked.
        //Set the shop parameter to true to open this NPC's shop.
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop) {
 
            if (firstButton) {
                openShop = true;   //so when you click on buy button opens the shop
            }
        }


        //Allows you to add items to this town NPC's shop. Add an item by setting the defaults of shop.item[nextSlot] 
        //then incrementing nextSlot.
        public override void SetupShop(Chest shop, ref int nextSlot) {
            if (NPC.downedSlimeKing) {  //this make so when the king slime is killed the town npc will sell this
                shop.item[nextSlot].SetDefaults(ItemID.RecallPotion);  //an example of how to add a vanilla terraria item
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
                nextSlot++;
            }

            //this make so when Skeletron is killed the town npc will sell this
            if (NPC.downedBoss3) {
                shop.item[nextSlot].SetDefaults(ItemID.BookofSkulls);
                nextSlot++;
                shop.item[nextSlot].SetDefaults(ItemID.ClothierVoodooDoll);
                nextSlot++;
            }

            shop.item[nextSlot].SetDefaults(ItemID.IronskinPotion);
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SwissArmyKnife"));  //this is an example of how to add a modded item
            nextSlot++;
 
        }

        public override string GetChat()       //Allows you to give this town NPC a chat message when a player talks to it.
        {
            int wizardNPC = NPC.FindFirstNPC(NPCID.Wizard);   //this make so when this npc is close to Wizard
            if (wizardNPC >= 0 && Main.rand.Next(4) == 0)    //has 1 in 3 chance to show this message
            {
                return "Yes " + Main.npc[wizardNPC].GivenName + " is a wizard.";
            }
            int guideNPC = NPC.FindFirstNPC(NPCID.Guide); //this make so when this npc is close to the Guide
            if (guideNPC >= 0 && Main.rand.Next(4) == 0) //has 1 in 3 chance to show this message
            {
                return "Sure you can ask " + Main.npc[guideNPC].GivenName + 
                    " how to make Ironskin potion or you can buy it from me..hehehe.";
            }
            switch (Main.rand.Next(4))    //this are the messages when you talk to the npc
            {
                case 0:
                    return "You wanna buy something?";
                case 1:
                    return "What you want?";
                case 2:
                    return "I like this house.";
                case 3:
                    return "<I'm blue dabu di dabu dai>....OH HELLO THERE..";
                default:
                    return "Go kill Skeletron.";
 
            }
        }
        //  Allows you to determine the damage and knockback of this town NPC attack
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 40;  //npc damage
            knockback = 2f;   //npc knockback
        }
        //Allows you to determine the cooldown between each of this town NPC's attack. 
        //The cooldown will be a number greater than or equal to the first parameter, and less then the sum of the two parameters.
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)  
        {
            cooldown = 5;
            randExtraCooldown = 10;
        }

        //------------------------------------This is an example of how to make the npc use a sward attack-------------------------------
        //Allows you to customize how this town NPC's weapon is drawn when this NPC is swinging it (this NPC must have an attack type of 3).
        //Item is the Texture2D instance of the item to be drawn (use Main.itemTexture[id of item]), 
        //itemSize is the width and height of the item's hitbox
        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            scale = 1f;
            item = Main.itemTexture[mod.ItemType("CustomSword")]; //this defines the item that this npc will use
            itemSize = 56;
        }
        //  Allows you to determine the width and height of the item this town NPC swings when it attacks, 
        //which controls the range of this NPC's swung weapon.
        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight) 
        {
            itemWidth = 56;
            itemHeight = 56;
        }

    }
}
