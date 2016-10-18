using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Model
    {
        public const uint SAVE_FILE_SIZE = 483976;

        private readonly byte[] save_file;

        private readonly Character _character;
        public Character character
        {
            get { return _character; }
        }

        private uint cur_monster_selection;
        private readonly List<Monster> monsters;

        private readonly List<Item> items;

        private uint cur_egg_fragment_selection;
        private readonly List<EggFragment> egg_fragments;
 
        public Model(byte[] save)
        {
            if (save == null || save.Length != SAVE_FILE_SIZE)
            {
                throw new SystemException("Invalid save file size.");
            }
            save_file = save;
            _character = new Character(save_file);

            cur_monster_selection = 0;
            monsters = null;
            items = null;
            cur_egg_fragment_selection = 0;
            egg_fragments = null;
        }


        public static uint byte_to_uint(byte b)
        {
            return (uint) (b) & 0xFF;
        }

        public static uint byte_to_uint16_le(byte[] arr, uint offset)
        {
            if (arr.Length < offset + 2)
                throw new SystemException("Buffer overflowed - Offset " + offset);
            return byte_to_uint(arr[offset]) | (byte_to_uint(arr[offset + 1]) << 8);
        }

        public static void write_uint16_le(byte[] arr, uint offset, uint val)
        {
            if (arr.Length < offset + 2)
                throw new SystemException("Buffer overflowed - Offset " + offset);
            arr[offset] = (byte) (val & 0xFF);
            arr[offset + 1] = (byte) ((val >> 8) & 0xFF);
        }

        public static uint byte_to_uint32_le(byte[] arr, uint offset)
        {
            if (arr.Length < offset + 4)
                throw new SystemException("Buffer overflowed - Offset " + offset);
            return byte_to_uint(arr[offset]) | (byte_to_uint(arr[offset + 1]) << 8) |
                   (byte_to_uint(arr[offset + 2]) << 16) | (byte_to_uint(arr[offset + 3]) << 24);
        }

        public static void write_uint32_le(byte[] arr, uint offset, uint val)
        {
            if (arr.Length < offset + 4)
                throw new SystemException("Buffer overflowed - Offset " + offset);
            arr[offset] = (byte) (val & 0xFF);
            arr[offset + 1] = (byte) ((val >> 8) & 0xFF);
            arr[offset + 2] = (byte) ((val >> 16) & 0xFF);
            arr[offset + 3] = (byte) ((val >> 24) & 0xFF);
        }

        public static string read_unicode_string(byte[] arr, uint offset, uint max_char)
        {
            StringBuilder name = new StringBuilder();
            uint read_char = 0;
            for (uint i = offset; i < arr.Length; i += 2)
            {
                if (read_char < max_char)
                {
                    uint each_char = byte_to_uint16_le(arr, i);
                    if (each_char == 0)
                        break;
                    name.Append(Convert.ToChar(each_char));
                    read_char++;
                }
                else
                {
                    break;
                }
            }
            return name.ToString();
        }
    }
}
