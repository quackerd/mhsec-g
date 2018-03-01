using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;

namespace MHSEC_G
{
    public static class Offsets
    {
        //
        // Ver
        //
        public enum Version
        {
            JPN,
            USA
        };

        private static Version _VER;
        public static Version VER => _VER;

        //
        // Save
        //
        public const uint SAVE_FILE_SIZE_JPN = 483976;
        public const uint SAVE_FILE_SIZE_NA = 535736;

        //
        // Genes
        //
        private static List<uint> _GENE_ID;
        private static List<string> _GENE_NAME;
        public static List<uint> GENE_ID => _GENE_ID;
        public static List<string> GENE_NAME => _GENE_NAME;

        //
        // Armors
        //

        private static uint _OFFSETR_ARM_EQUIPPED;
        public static uint OFFSETR_ARM_EQUIPPED => _OFFSETR_ARM_EQUIPPED;

        private static uint _OFFSETA_ARM;
        public static uint OFFSETA_ARM => _OFFSETA_ARM;

        private static uint _SIZE_ARM;
        public static uint SIZE_ARM => _SIZE_ARM;

        private static uint _OFFSETR_ARM_ID;
        public static uint OFFSETR_ARM_ID => _OFFSETR_ARM_ID;

        private static uint _OFFSETR_ARM_LEVEL;
        public static uint OFFSETR_ARM_LEVEL => _OFFSETR_ARM_LEVEL;

        private static uint _OFFSETR_ARM_14h;
        public static uint OFFSETR_ARM_14h => _OFFSETR_ARM_14h;

        private static uint _OFFSETR_ARM_18h;
        public static uint OFFSETR_ARM_18h => _OFFSETR_ARM_18h;

        private static uint _OFFSETR_ARM_1C;
        public static uint OFFSETR_ARM_1C => _OFFSETR_ARM_1C;

        private static uint _OFFSETA_ARM_END;
        public static uint OFFSETA_ARM_END => _OFFSETA_ARM_END;

        private static uint _OFFSETR_ARM_TYPE;
        public static uint OFFSETR_ARM_TYPE => _OFFSETR_ARM_TYPE;


        //
        // Character
        //
        private static uint _OFFSETA_CHAR_NAME;
        public static uint OFFSETA_CHAR_NAME => _OFFSETA_CHAR_NAME;


        private static uint _LENGTH_CHAR_NAME;
        public static uint LENGTH_CHAR_NAME => _LENGTH_CHAR_NAME;

        private static uint _OFFSETA_CHAR_MONEY;
        public static uint OFFSETA_CHAR_MONEY => _OFFSETA_CHAR_MONEY;

        private static uint _OFFSETA_CHAR_EXP;
        public static uint OFFSETA_CHAR_EXP => _OFFSETA_CHAR_EXP;

        private static uint _OFFSETA_CHAR_LEVEL;
        public static uint OFFSETA_CHAR_LEVEL => _OFFSETA_CHAR_LEVEL;

        private static uint _LIMIT_LEVEL;
        public static uint LIMIT_LEVEL => _LIMIT_LEVEL;

        private static uint _LIMIT_MONEY;
        public static uint LIMIT_MONEY => _LIMIT_MONEY;

        private static uint _LIMIT_EXP;
        public static uint LIMIT_EXP => _LIMIT_EXP;


        //
        // Egg
        //
        private static uint _OFFSETA_EGG_START;
        public static uint OFFSETA_EGG_START => _OFFSETA_EGG_START;


        private static uint _OFFSETA_EGG_END;
        public static uint OFFSETA_EGG_END => _OFFSETA_EGG_END;

        private static uint _OFFSETR_EGG_GENE;
        public static uint OFFSETR_EGG_GENE => _OFFSETR_EGG_GENE;

        private static uint _SIZE_EGG_GENE;
        public static uint SIZE_EGG_GENE => _SIZE_EGG_GENE;

        private static uint _SIZE_EGG;
        public static uint SIZE_EGG => _SIZE_EGG;

        private static uint _OFFSETR_SPE;
        public static uint OFFSETR_SPE => _OFFSETR_SPE;

        private static uint _OFFSETR_WGT;
        public static uint OFFSETR_WGT => _OFFSETR_WGT;


        //
        // Egg fragments
        //
        private static uint _OFFSETA_EGG_FRAGMENTS;
        public static uint OFFSETA_EGG_FRAGMENTS => _OFFSETA_EGG_FRAGMENTS;


        private static uint _OFFSETA_EGG_FRAGMENTS_END;
        public static uint OFFSETA_EGG_FRAGMENTS_END => _OFFSETA_EGG_FRAGMENTS_END;

        private static uint _SIZE_EGG_FRAGMENT;
        public static uint SIZE_EGG_FRAGMENT => _SIZE_EGG_FRAGMENT;

        private static uint _OFFSETR_EF_SPE;
        public static uint OFFSETR_EF_SPE => _OFFSETR_EF_SPE;

        private static uint _OFFSETR_EF_POS;
        public static uint OFFSETR_EF_POS => _OFFSETR_EF_POS;

        private static uint _OFFSETR_EF_NEW;
        public static uint OFFSETR_EF_NEW => _OFFSETR_EF_NEW;

        private static uint _OFFSETR_EF_RAR;
        public static uint OFFSETR_EF_RAR => _OFFSETR_EF_RAR;

        private static uint _OFFSETR_EF_COL;
        public static uint OFFSETR_EF_COL => _OFFSETR_EF_COL;

        private static uint _OFFSETR_EF_DLC;
        public static uint OFFSETR_EF_DLC => _OFFSETR_EF_DLC;

        private static uint _OFFSETR_EF_6H;
        public static uint OFFSETR_EF_6H => _OFFSETR_EF_6H;

        private static uint _OFFSETR_EF_7H;
        public static uint OFFSETR_EF_7H => _OFFSETR_EF_7H;


        //
        // Items
        //
        private static Dictionary<uint, uint> _OFFSET_ID_MAPPING;
        public static Dictionary<uint, uint> OFFSET_ID_MAPPING => _OFFSET_ID_MAPPING;
        private static Dictionary<uint, string> _OFFSET_NAME_MAPPING;
        public static Dictionary<uint, string> OFFSET_NAME_MAPPING => _OFFSET_NAME_MAPPING;

        private static uint _OFFSETA_ITEM_BOX;
        public static uint OFFSETA_ITEM_BOX => _OFFSETA_ITEM_BOX;


        private static uint _SIZE_ITEM;
        public static uint SIZE_ITEM => _SIZE_ITEM;

        private static uint _OFFSETR_ITEM_ID;
        public static uint OFFSETR_ITEM_ID => _OFFSETR_ITEM_ID;

        private static uint _OFFSETR_ITEM_COUNT;
        public static uint OFFSETR_ITEM_COUNT => _OFFSETR_ITEM_COUNT;

        private static uint _OFFSETA_ITEM_BOX_END;
        public static uint OFFSETA_ITEM_BOX_END => _OFFSETA_ITEM_BOX_END;

        private static uint _OFFSETA_FIRST_KEY_ITEM;
        public static uint OFFSETA_FIRST_KEY_ITEM => _OFFSETA_FIRST_KEY_ITEM;


        //
        // Monsters 
        //
        private static byte[] _MONSTER_NULL_TEMPLATE;
        public static byte[] MONSTER_NULL_TEMPLATE => _MONSTER_NULL_TEMPLATE;

        private static uint _OFFSETA_MONSTER_PARTY;
        public static uint OFFSETA_MONSTER_PARTY => _OFFSETA_MONSTER_PARTY;

        private static uint _OFFSETR_MONSTER_UID;
        public static uint OFFSETR_MONSTER_UID => _OFFSETR_MONSTER_UID ;

        private static uint _LIMIT_MONSTER_EXP;
        public static uint LIMIT_MONSTER_EXP => _LIMIT_MONSTER_EXP;


        private static uint _SIZE_MONSTER;
        public static uint SIZE_MONSTER => _SIZE_MONSTER;

        private static uint _OFFSETA_MONSTER;
        public static uint OFFSETA_MONSTER => _OFFSETA_MONSTER;

        private static uint _OFFSETR_MONSTER_GENE;
        public static uint OFFSETR_MONSTER_GENE => _OFFSETR_MONSTER_GENE;

        private static uint _SIZE_MONSTER_GENE;
        public static uint SIZE_MONSTER_GENE => _SIZE_MONSTER_GENE;

        private static uint _OFFSETR_MONSTER_EXP;
        public static uint OFFSETR_MONSTER_EXP => _OFFSETR_MONSTER_EXP;

        private static uint _OFFSETR_MONSTER_HIV;
        public static uint OFFSETR_MONSTER_HIV => _OFFSETR_MONSTER_HIV;

        private static uint _OFFSETR_MONSTER_AIV;
        public static uint OFFSETR_MONSTER_AIV => _OFFSETR_MONSTER_AIV;

        private static uint _OFFSETR_MONSTER_DIV;
        public static uint OFFSETR_MONSTER_DIV => _OFFSETR_MONSTER_DIV;

        private static uint _OFFSETR_MONSTER_HPU;
        public static uint OFFSETR_MONSTER_HPU => _OFFSETR_MONSTER_HPU;

        private static uint _OFFSETR_MONSTER_APU;
        public static uint OFFSETR_MONSTER_APU => _OFFSETR_MONSTER_APU;

        private static uint _OFFSETR_MONSTER_DPU;
        public static uint OFFSETR_MONSTER_DPU => _OFFSETR_MONSTER_DPU;

        private static uint _OFFSETR_MONSTER_SKILL;
        public static uint OFFSETR_MONSTER_SKILL => _OFFSETR_MONSTER_SKILL;

        private static uint _OFFSETR_MONSTER_LEVEL;
        public static uint OFFSETR_MONSTER_LEVEL => _OFFSETR_MONSTER_LEVEL;

        private static uint _LIMIT_MONSTER_LEVEL;
        public static uint LIMIT_MONSTER_LEVEL => _LIMIT_MONSTER_LEVEL;

        private static uint _OFFSETR_MONSTER_NAME;
        public static uint OFFSETR_MONSTER_NAME => _OFFSETR_MONSTER_NAME;

        private static uint _LIMIT_MONSTER_NAME;
        public static uint LIMIT_MONSTER_NAME => _LIMIT_MONSTER_NAME;

        private static uint _OFFSETR_MONSTER_SPE;
        public static uint OFFSETR_MONSTER_SPE => _OFFSETR_MONSTER_SPE;

        private static uint _OFFSETA_MONSTE_END;
        public static uint OFFSETA_MONSTE_END => _OFFSETA_MONSTE_END;

        private static uint _OFFSETR_MONSTER_ATK;
        public static uint OFFSETR_MONSTER_ATK => _OFFSETR_MONSTER_ATK;

        private static uint _OFFSETR_MONSTER_HP;
        public static uint OFFSETR_MONSTER_HP => _OFFSETR_MONSTER_HP;

        private static uint _OFFSETR_MONSTER_DEF;
        public static uint OFFSETR_MONSTER_DEF => _OFFSETR_MONSTER_DEF;


        //
        // Talismans
        //
        private static uint _OFFSETA_TALI;
        public static uint OFFSETA_TALI => _OFFSETA_TALI;


        private static uint _OFFSETA_TALI_END;
        public static uint OFFSETA_TALI_END => _OFFSETA_TALI_END;

        private static uint _SIZE_TALI;
        public static uint SIZE_TALI => _SIZE_TALI;

        private static uint _OFFSETR_TALI_RARITY;
        public static uint OFFSETR_TALI_RARITY => _OFFSETR_TALI_RARITY;

        private static uint _OFFSETR_TALI_NEW;
        public static uint OFFSETR_TALI_NEW => _OFFSETR_TALI_NEW;

        private static uint _OFFSETR_TALI_SKILL1;
        public static uint OFFSETR_TALI_SKILL1 => _OFFSETR_TALI_SKILL1;

        private static uint _OFFSETR_TALI_SKILL2;
        public static uint OFFSETR_TALI_SKILL2 => _OFFSETR_TALI_SKILL2;

        private static uint _OFFSETR_TALI_ID;
        public static uint OFFSETR_TALI_ID => _OFFSETR_TALI_ID;

        private static uint _OFFSETR_TALI_EQUIPPED;
        public static uint OFFSETR_TALI_EQUIPPED => _OFFSETR_TALI_EQUIPPED;


        //
        // Weapons
        //
        private static uint _OFFSETA_WEAPON_START;
        public static uint OFFSETA_WEAPON_START => _OFFSETA_WEAPON_START;

        private static uint _OFFSETR_WEAPON_EQUIPPED;
        public static uint OFFSETR_WEAPON_EQUIPPED => _OFFSETR_WEAPON_EQUIPPED;

        private static uint _OFFSETA_WEAPON_END;
        public static uint OFFSETA_WEAPON_END => _OFFSETA_WEAPON_END;

        private static uint _SIZE_WEAPON;
        public static uint SIZE_WEAPON => _SIZE_WEAPON;

        private static uint _OFFSETR_CLASS;
        public static uint OFFSETR_CLASS => _OFFSETR_CLASS;

        private static uint _OFFSETR_ID;
        public static uint OFFSETR_ID => _OFFSETR_ID;

        private static uint _OFFSETR_LEVEL;
        public static uint OFFSETR_LEVEL => _OFFSETR_LEVEL;


        public static void init(byte[] save)
        {
            if (save.Length == SAVE_FILE_SIZE_JPN)
            {
                _VER = Version.JPN;
       
                //
                //Armors
                //
                _OFFSETA_ARM = 0x55F0;
                _SIZE_ARM = 0x24;
                _OFFSETR_ARM_ID = 0x2;
                _OFFSETR_ARM_LEVEL = 0x4;
                _OFFSETR_ARM_14h = 0x14;
                _OFFSETR_ARM_18h = 0x18;
                _OFFSETR_ARM_1C = 0x1c;
                _OFFSETA_ARM_END = 0x720E;
                _OFFSETR_ARM_TYPE = 0x0;
                _OFFSETR_ARM_EQUIPPED = 0x11;

                //
                //Character
                //
                _OFFSETA_CHAR_NAME = 0x9DA0;
                _LENGTH_CHAR_NAME = 6;
                _OFFSETA_CHAR_MONEY = 0x5B404;
                _OFFSETA_CHAR_EXP = 0x9E68;
                _OFFSETA_CHAR_LEVEL = 0x9E64;
                _LIMIT_LEVEL = 99;
                _LIMIT_MONEY = 9999999;
                _LIMIT_EXP = 25165822;

                //
                //Egg
                //
                _OFFSETA_EGG_START = 0x53EC0;
                _OFFSETA_EGG_END = 0x54597;
                _OFFSETR_EGG_GENE = 0x30;
                _SIZE_EGG_GENE = 0x2;
                _SIZE_EGG = 0x92;
                _OFFSETR_SPE = 0x0;
                _OFFSETR_WGT = 0x2E;

                //
                //Eggfragments
                //
                _OFFSETA_EGG_FRAGMENTS = 0x9790;
                _OFFSETA_EGG_FRAGMENTS_END = 0x9C3F;
                _SIZE_EGG_FRAGMENT = 0xC;
                _OFFSETR_EF_SPE = 0x0;
                _OFFSETR_EF_POS = 0x1;
                _OFFSETR_EF_NEW = 0x2;
                _OFFSETR_EF_RAR = 0x3;
                _OFFSETR_EF_COL = 0x4;
                _OFFSETR_EF_DLC = 0x5;
                _OFFSETR_EF_6H = 0x6;
                _OFFSETR_EF_7H = 0x7;

                //
                //Items
                //
                _OFFSETA_ITEM_BOX = 0x10;
                _SIZE_ITEM = 0x8;
                _OFFSETR_ITEM_ID = 0x0;
                _OFFSETR_ITEM_COUNT = 0x2;
                _OFFSETA_ITEM_BOX_END = 0x2EE7;
                _OFFSETA_FIRST_KEY_ITEM = 0x17B0;

                //
                //Monsters
                //
                _OFFSETR_MONSTER_UID = 0x28;
                _OFFSETA_MONSTER_PARTY = 0x47870;
                _MONSTER_NULL_TEMPLATE = Properties.Resources.monster_null_template_JPN;

                _LIMIT_MONSTER_EXP = 0xFFFFFF;
                _SIZE_MONSTER = 0x478;
                _OFFSETA_MONSTER = 0xA150;
                _OFFSETR_MONSTER_GENE = 0x424;
                _SIZE_MONSTER_GENE = 0x4;
                _OFFSETR_MONSTER_EXP = 0xE0;
                _OFFSETR_MONSTER_HIV = 0xD8;
                _OFFSETR_MONSTER_AIV = 0xD9;
                _OFFSETR_MONSTER_DIV = 0xDA;
                _OFFSETR_MONSTER_HPU = 0xD4;
                _OFFSETR_MONSTER_APU = 0xD5;
                _OFFSETR_MONSTER_DPU = 0xD6;
                _OFFSETR_MONSTER_SKILL = 0x38;
                _OFFSETR_MONSTER_LEVEL = 0x5C;
                _LIMIT_MONSTER_LEVEL = 99;
                _OFFSETR_MONSTER_NAME = 0;
                _LIMIT_MONSTER_NAME = 10;
                _OFFSETR_MONSTER_SPE = 0x30;
                _OFFSETA_MONSTE_END = 0x4786F;
                _OFFSETR_MONSTER_ATK = 0x48;
                _OFFSETR_MONSTER_HP = 0x46;
                _OFFSETR_MONSTER_DEF = 0x4A;

                //
                //Talismans
                //
                _OFFSETA_TALI = 0x7210;
                _OFFSETA_TALI_END = 0x978F;
                _SIZE_TALI = 0x30;
                _OFFSETR_TALI_RARITY = 0x24;
                _OFFSETR_TALI_NEW = 0x12;
                _OFFSETR_TALI_SKILL1 = 0x28;
                _OFFSETR_TALI_SKILL2 = 0x2A;
                _OFFSETR_TALI_ID = 0x2;
                _OFFSETR_TALI_EQUIPPED = 0x11;

                //
                //Weapons
                //
                _OFFSETA_WEAPON_START = 0x39D0;
                _OFFSETA_WEAPON_END = 0x55EF;
                _OFFSETR_WEAPON_EQUIPPED = 0x11;
                _SIZE_WEAPON = 0x24;
                _OFFSETR_CLASS = 0x0;
                _OFFSETR_ID = 0x2;
                _OFFSETR_LEVEL = 0x4;

                read_item_mappings(Properties.Resources.idmap);
                read_gene_mapping(Properties.Resources.gene_JPN);
            }
            else
            {
                _VER = Version.USA;
                //
                // Armors ok
                //
                _OFFSETA_ARM = 0x5694;
                _SIZE_ARM = 0x24;
                _OFFSETR_ARM_ID = 0x2;
                _OFFSETR_ARM_LEVEL = 0x4;
                _OFFSETR_ARM_14h = 0x14;
                _OFFSETR_ARM_18h = 0x18;
                _OFFSETR_ARM_1C = 0x1c;
                _OFFSETA_ARM_END = 0x72B3;
                _OFFSETR_ARM_TYPE = 0x0;
                _OFFSETR_ARM_EQUIPPED = 0x11;

                //
                // Character ok
                //
                _OFFSETA_CHAR_NAME = 0x9E44;
                _LENGTH_CHAR_NAME = 12;
                _OFFSETA_CHAR_MONEY = 0x5E394;
                _OFFSETA_CHAR_EXP = 0x9F14;
                _OFFSETA_CHAR_LEVEL = 0x9F10;
                _LIMIT_LEVEL = 99;
                _LIMIT_MONEY = 9999999;
                _LIMIT_EXP = 25165822;

                //
                // Egg ok
                //
                _OFFSETA_EGG_START = 0x56E50;
                _OFFSETA_EGG_END = 0x57527;
                _OFFSETR_EGG_GENE = 0x30;
                _SIZE_EGG_GENE = 0x2;
                _SIZE_EGG = 0x92;
                _OFFSETR_SPE = 0x0;
                _OFFSETR_WGT = 0x2E;

                //
                // Eggfragments ok
                //
                _OFFSETA_EGG_FRAGMENTS = 0x9834;
                _OFFSETA_EGG_FRAGMENTS_END = 0x9CE3;
                _SIZE_EGG_FRAGMENT = 0xC;
                _OFFSETR_EF_SPE = 0x0;
                _OFFSETR_EF_POS = 0x1;
                _OFFSETR_EF_NEW = 0x2;
                _OFFSETR_EF_RAR = 0x3;
                _OFFSETR_EF_COL = 0x4;
                _OFFSETR_EF_DLC = 0x5;
                _OFFSETR_EF_6H = 0x6;
                _OFFSETR_EF_7H = 0x7;

                //
                // Items ok
                //
                _OFFSETA_ITEM_BOX = 0x10;
                _SIZE_ITEM = 0x8;
                _OFFSETR_ITEM_ID = 0x0;
                _OFFSETR_ITEM_COUNT = 0x2;
                _OFFSETA_ITEM_BOX_END = 0x2EE7;
                _OFFSETA_FIRST_KEY_ITEM = 0x17B0;

                //
                // Monsters ok
                //
                _MONSTER_NULL_TEMPLATE = Properties.Resources.monster_null_template_NA;

                _OFFSETA_MONSTER_PARTY = 0x49B7C;
                _OFFSETR_MONSTER_UID = 0x50;
                _LIMIT_MONSTER_EXP = 0xFFFFFF;
                _SIZE_MONSTER = 0x4A0;
                _OFFSETA_MONSTER = 0xA1FC;
                _LIMIT_MONSTER_LEVEL = 99;
                _OFFSETR_MONSTER_NAME = 0;
                _LIMIT_MONSTER_NAME = 20;
                _OFFSETA_MONSTE_END = 0x49B7B;
                _SIZE_MONSTER_GENE = 0x4;

                _OFFSETR_MONSTER_GENE = 0x424 + 0x28;
                _OFFSETR_MONSTER_EXP = 0xE0 + 0x28;
                _OFFSETR_MONSTER_HIV = 0xD8 + 0x28;
                _OFFSETR_MONSTER_AIV = 0xD9 + 0x28;
                _OFFSETR_MONSTER_DIV = 0xDA + 0x28;
                _OFFSETR_MONSTER_HPU = 0xD4 + 0x28;
                _OFFSETR_MONSTER_APU = 0xD5 + 0x28;
                _OFFSETR_MONSTER_DPU = 0xD6 + 0x28;
                _OFFSETR_MONSTER_SKILL = 0x38 + 0x28;
                _OFFSETR_MONSTER_LEVEL = 0x5C + 0x28;
                _OFFSETR_MONSTER_SPE = 0x30 + 0x28;
                _OFFSETR_MONSTER_ATK = 0x48 + 0x28;
                _OFFSETR_MONSTER_HP = 0x46 + 0x28;
                _OFFSETR_MONSTER_DEF = 0x4A + 0x28;

                //
                // Talismans ok
                //
                _OFFSETA_TALI = 0x72B4;
                _OFFSETA_TALI_END = 0x9833;
                _SIZE_TALI = 0x30;
                _OFFSETR_TALI_RARITY = 0x24;
                _OFFSETR_TALI_NEW = 0x12;
                _OFFSETR_TALI_SKILL1 = 0x28;
                _OFFSETR_TALI_SKILL2 = 0x2A;
                _OFFSETR_TALI_ID = 0x2;
                _OFFSETR_TALI_EQUIPPED = 0x11;

                //
                // Weapons ok
                //
                _OFFSETA_WEAPON_START = 0x3A74;
                _OFFSETA_WEAPON_END = 0x5693;
                _OFFSETR_WEAPON_EQUIPPED = 0x11;
                _SIZE_WEAPON = 0x24;
                _OFFSETR_CLASS = 0x0;
                _OFFSETR_ID = 0x2;
                _OFFSETR_LEVEL = 0x4;

                read_item_mappings(Properties.Resources.idmap);
                read_gene_mapping(Properties.Resources.gene);
            }
        }

        private static void read_item_mappings(string src)
        {
            _OFFSET_ID_MAPPING = new Dictionary<uint, uint>();
            _OFFSET_NAME_MAPPING = new Dictionary<uint, string>();

            string line;
            StringReader file = new StringReader(src);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;

                string[] eachline = line.Split('\t');
                if (eachline.Length != 3)
                {

                    BugCheck.bug_check(BugCheck.ErrorCode.ITEM_MAPPING_CORRUPTED, "Invalid mapping file line:\n" + line);
                }

                _OFFSET_ID_MAPPING.Add(uint.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber), uint.Parse(eachline[1], System.Globalization.NumberStyles.HexNumber));
                _OFFSET_NAME_MAPPING.Add(uint.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber), eachline[2]);
            }

            file.Close();
        }

        public static void read_gene_mapping(string src)
        {
            _GENE_NAME = new List<string>();
            _GENE_ID = new List<uint>();

            string line;
            StringReader file = new StringReader(src);

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;

                string[] eachline = line.Split('\t');
                if (eachline.Length != 2)
                {
                    BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_MAPPING_CORRUPTED,
                        "Invalid gene mapping file line:\n" + line);
                }

                _GENE_ID.Add(uint.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber));
                _GENE_NAME.Add(eachline[1]);
            }
            _GENE_NAME.Add("Custom");

            file.Close();
        }
    }
}
