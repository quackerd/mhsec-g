using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Item : InMemoryObject
    {
        public uint offset => _obj_offset;
        public string name
        {
            get {
                string name = _obj_offset >= Offsets.OFFSETA_FIRST_KEY_ITEM ? "Key Item [" + id.ToString("X4") + "]" : "Unknown";
                if (Offsets.OFFSET_NAME_MAPPING.ContainsKey(_obj_offset))
                {
                     name = Offsets.OFFSET_NAME_MAPPING[_obj_offset];
                }
                return name;
            }
        }


        public uint count
        {
            get { return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ITEM_COUNT); }
            set
            {
                if (value <= 999)
                {
                    Helper.write_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ITEM_COUNT, value);
                }
                else
                {
                    MessageBox.Show("Quantity must be less than 999.","Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                OnPropertyChanged(nameof(count));
            }
        }

        public uint id
        {
            get { return Helper.byte_to_uint16_le(_data, _obj_offset + Offsets.OFFSETR_ITEM_ID); }
        }

        public Item(byte[] model, uint objOffset) : base(model, objOffset, Offsets.SIZE_ITEM)
        {
        }

        public static List<Item> read_all_items(byte[] model)
        {
            List<Item> ret = new List<Item>();
            for (uint offset = Offsets.OFFSETA_ITEM_BOX;
                offset < Offsets.OFFSETA_ITEM_BOX_END;
                offset += Offsets.SIZE_ITEM)
            {
                uint item_id = Helper.byte_to_uint16_le(model, offset + Offsets.OFFSETR_ITEM_ID);
                if (item_id == 0)
                {
                    // if not obtained yet
                    if (!Offsets.OFFSET_ID_MAPPING.ContainsKey(offset))
                    {
                        // well we dont know the id to this offset either. just ignore!
                        // continue
                        continue;
                    }
                    // if we know the id to the offset, just set it
                    Helper.write_uint16_le(model, offset + Offsets.OFFSETR_ITEM_ID, Offsets.OFFSET_ID_MAPPING[offset]);
                }
                else
                {
                    // already obtained
                    // validate
                    if (Offsets.OFFSET_ID_MAPPING.ContainsKey(offset))
                    {
                        if (item_id != Offsets.OFFSET_ID_MAPPING[offset])
                        {
                            // correct to the correct id
                            // don't bug_check since too many people's files are broken
                            // TODO: BugCheck.bug_check(BugCheck.ErrorCode.ITEM_NO_CORRESPONDENCE, "Item offset " + offset.ToString("X") + " and item ID " + item_id.ToString("X") + " do not correspond.");
                            Helper.write_uint16_le(model, offset + Offsets.OFFSETR_ITEM_ID, Offsets.OFFSET_ID_MAPPING[offset]);
                        }
                    }
                }
                // valid offset
                ret.Add(new Item(model, offset));
            }
            return ret;
        }
    }
}
