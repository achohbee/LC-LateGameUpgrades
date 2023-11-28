﻿using BepInEx.Configuration;

namespace MoreShipUpgrades.Misc
{
    public class PluginConfig
    {
        readonly ConfigFile configFile;
        
        // enabled disabled
        public bool ADVANCED_TELE_ENABLED { get; set; }
        public bool WEAK_TELE_ENABLED { get; set; }
        public bool BEEKEEPER_ENABLED { get; set; }
        public bool BIGGER_LUNGS_ENABLED { get; set; }
        public bool BACK_MUSCLES_ENABLED { get; set; }
        public bool LIGHT_FOOTED_ENABLED { get; set; }
        public bool NIGHT_VISION_ENABLED { get; set; }
        public bool RUNNING_SHOES_ENABLED { get; set; }
        public bool BETTER_SCANNER_ENABLED { get; set; }
        public bool STRONG_LEGS_ENABLED { get; set; }
        public bool DISCOMBOBULATOR_ENABLED { get; set; }
        public bool MALWARE_BROADCASTER_ENABLED { get; set; }

        // prices
        public int ADVANCED_TELE_PRICE { get; set; }
        public int WEAK_TELE_PRICE { get; set; }
        public int BEEKEEPER_PRICE { get; set; }
        public int BIGGER_LUNGS_PRICE { get; set; }
        public int BACK_MUSCLES_PRICE { get; set; }
        public int LIGHT_FOOTED_PRICE { get; set; }
        public int NIGHT_VISION_PRICE { get; set; }
        public int RUNNING_SHOES_PRICE { get; set; }
        public int BETTER_SCANNER_PRICE { get; set; }
        public int STRONG_LEGS_PRICE { get; set; }
        public int DISCOMBOBULATOR_PRICE { get; set; }
        public int MALWARE_BROADCASTER_PRICE { get; set; }

        // attributes
        public bool KEEP_ITEMS_ON_TELE { get; set; }
        public float SPRINT_TIME_INCREASE { get; set; }
        public float MOVEMENT_SPEED { get; set; }
        public float JUMP_FORCE { get; set; }
        public bool DESTROY_TRAP { get; set; }
        public float DISARM_TIME { get; set; }
        public bool EXPLODE_TRAP { get; set; }
        public bool REQUIRE_LINE_OF_SIGHT { get; set; }
        public float CARRY_WEIGHT_REDUCTION { get; set; }
        public float NODE_DISTANCE_INCREASE { get; set; }
        public float SHIP_AND_ENTRANCE_DISTANCE_INCREASE { get; set; }
        public float NOISE_REDUCTION { get; set; }
        public float DISCOMBOBULATOR_COOLDOWN { get; set; }
        public float ADV_CHANCE_TO_BREAK { get; set; }
        public float CHANCE_TO_BREAK { get; set; }
        public float BEEKEEPER_DAMAGE_MULTIPLIER { get; set; }
        public float NIGHT_VIS_DRAIN_SPEED { get; set; }
        public float NIGHT_VIS_REGEN_SPEED { get; set; }
        public float DISCOMBOBULATOR_RADIUS { get; set; }
        public float DISCOMBOBULATOR_STUN_DURATION { get; set; }

        public PluginConfig(ConfigFile cfg)
        {
            configFile = cfg;
        }

        private T ConfigEntry<T>(string key, T defaultVal, string description)
        {
            return configFile.Bind(Metadata.NAME, key, defaultVal, description).Value;
        }

        public void InitBindings()
        {
            ADVANCED_TELE_ENABLED = ConfigEntry("Enable Advanced Portable Teleporter", true, "");
            ADVANCED_TELE_PRICE = ConfigEntry("Price of Advanced Portable Teleporter", 1750, "");
            ADV_CHANCE_TO_BREAK = ConfigEntry("Chance to break on use", 0.1f, "value should be 0.00 - 1.00");


            WEAK_TELE_ENABLED = ConfigEntry("Enable Portable Teleporter", true, "");
            WEAK_TELE_PRICE = ConfigEntry("Price of Portable Teleporter", 300, "");
            CHANCE_TO_BREAK = ConfigEntry("Chance to break on use", 0.9f, "value should be 0.00 - 1.00");

            KEEP_ITEMS_ON_TELE = ConfigEntry("Keep Items When Using Portable Teleporters", true, "If set to false you will drop your items like when using the vanilla TP.");

            BEEKEEPER_ENABLED = ConfigEntry("Enable Beekeeper Upgrade", true, "Take no damage from bees");
            BEEKEEPER_PRICE = ConfigEntry("Price of Beekeeper Upgrade", 450, "");
            BEEKEEPER_DAMAGE_MULTIPLIER = ConfigEntry("Multiplied to incoming damage (rounded to int)", 0.25f, "Incoming damage from bees is 10.");

            BIGGER_LUNGS_ENABLED = ConfigEntry("Enable Bigger Lungs Upgrade", true, "More Stamina");
            BIGGER_LUNGS_PRICE = ConfigEntry("Price of Bigger Lungs Upgrade", 700, "");
            SPRINT_TIME_INCREASE = ConfigEntry("SprintTime value", 17, "Vanilla value is 11");

            RUNNING_SHOES_ENABLED = ConfigEntry("Enable Running Shoes Upgrade", true, "Run Faster");
            RUNNING_SHOES_PRICE = ConfigEntry("Price of Running Shoes Upgrade", 1000, "");
            MOVEMENT_SPEED = ConfigEntry("Movement Speed Value", 6, "Vanilla value is 4.6");

            STRONG_LEGS_ENABLED = ConfigEntry("Enable Strong Legs Upgrade", true, "Jump Higher");
            STRONG_LEGS_PRICE = ConfigEntry("Price of Strong Legs Upgrade", 300, "");
            JUMP_FORCE = ConfigEntry("Jump Force", 16, "Vanilla value is 13");

            MALWARE_BROADCASTER_ENABLED = ConfigEntry("Enable Malware Broadcaster Upgrade", true, "Explode Map Hazards");
            MALWARE_BROADCASTER_PRICE = ConfigEntry("Price of Malware Broadcaster Upgrade", 650, "");
            DESTROY_TRAP = ConfigEntry("Destroy Trap", true, "If false Malware Broadcaster will disable the trap for a long time instead of destroying.");
            DISARM_TIME = ConfigEntry("Disarm Time", 7, "If `Destroy Trap` is false this is the duration traps will be disabled.");
            EXPLODE_TRAP = ConfigEntry("Explode Trap", true, "Destroy Trap must be true! If this is true when destroying a trap it will also explode.");


            LIGHT_FOOTED_ENABLED = ConfigEntry("Enable Light Footed Upgrade", true, "Make less noise moving.");
            LIGHT_FOOTED_PRICE = ConfigEntry("Price of Light Footed Upgrade", 350, "");
            NOISE_REDUCTION = ConfigEntry("Noise Reduction", 7, "Distance units to subtract from footstep noise");


            NIGHT_VISION_ENABLED = ConfigEntry("Enable Night Vision Upgrade", true, "Toggleable night vision.");
            NIGHT_VISION_PRICE = ConfigEntry("Price of Night Vision Upgrade", 700, "");
            NIGHT_VIS_DRAIN_SPEED = ConfigEntry("Multiplier for night vis battery drain", 0.1f, "Multiplied by timedelta. A value of 0.1 will result in a 10 second battery life.");
            NIGHT_VIS_REGEN_SPEED = ConfigEntry("Multiplier for night vis battery regen", 0.05f, "Multiplied by timedelta.");


            DISCOMBOBULATOR_ENABLED = ConfigEntry("Enable Discombobulator Upgrade", true, "Stun enemies around the ship.");
            DISCOMBOBULATOR_PRICE = ConfigEntry("Price of Discombobulator Upgrade", 550, "");
            DISCOMBOBULATOR_COOLDOWN = ConfigEntry("Discombobulator Cooldown", 120f, "");
            DISCOMBOBULATOR_RADIUS  = ConfigEntry("Discombobulator Effect Radius", 40f, "");
            DISCOMBOBULATOR_STUN_DURATION  = ConfigEntry("Discombobulator Stun Duration", 7.5f, "");

            BETTER_SCANNER_ENABLED = ConfigEntry("Enable Better Scanner Upgrade", true, "Further scan distance, no LOS needed.");
            BETTER_SCANNER_PRICE = ConfigEntry("Price of Better Scanner Upgrade", 650, "");
            REQUIRE_LINE_OF_SIGHT = ConfigEntry("Require Line Of Sight", false, "Default mod value is false.");
            SHIP_AND_ENTRANCE_DISTANCE_INCREASE = ConfigEntry("Ship and Entrance node distance boost", 150, "How much further away you can scan the ship and entrance.");
            NODE_DISTANCE_INCREASE = ConfigEntry("Node distance boost", 20, "How much further away you can scan other nodes.");


            BACK_MUSCLES_ENABLED = ConfigEntry("Enable Back Muscles Upgrade", true, "Reduce carry weight");
            BACK_MUSCLES_PRICE = ConfigEntry("Price of Back Muscles Upgrade", 835, "");
            CARRY_WEIGHT_REDUCTION = ConfigEntry("Carry Weight Multiplier", 0.5f, "your carry weight is multiplied by this.");
        }
    }
}
