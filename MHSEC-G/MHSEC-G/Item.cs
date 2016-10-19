using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Item : INotifyPropertyChanged
    {
        private static readonly uint OFFSETA_ITEM_BOX = 0x10;
        private static readonly uint SIZE_ITEM = 0x8;
        private static readonly uint OFFSETR_ITEM_ID = 0x0;
        private static readonly uint OFFSETR_ITEM_COUNT = 0x2;
        private static readonly uint OFFSETA_ITEM_BOX_END = 0x2EE7;

        private static readonly string ID_MAPPING_FILE_NAME = "C:\\Users\\hyper\\Desktop\\idmap.txt";
        private static readonly Dictionary<uint, uint> OFFSET_ID_MAPPING = new Dictionary<uint, uint>();
        private static readonly Dictionary<uint, string> OFFSET_NAME_MAPPING = new Dictionary<uint, string>();

        private readonly uint _offset;
        private readonly Model _model;

        public string name
        {
            get {
                string name = "Unknown";
                if (OFFSET_NAME_MAPPING.ContainsKey(_offset))
                {
                     name = OFFSET_NAME_MAPPING[_offset];
                }
                return name;
            }
        }


        public uint count
        {
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_ITEM_COUNT); }
            set
            {
                if (value <= 999)
                {
                    Model.write_uint16_le(_model.save_file, _offset + OFFSETR_ITEM_COUNT, value);
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
            get { return Model.byte_to_uint16_le(_model.save_file, _offset + OFFSETR_ITEM_ID); }
        }

        public Item(uint offset, Model model)
        {
            _offset = offset;
            _model = model;
        }

        public static List<Item> read_all_items(Model model)
        {
            byte[] buffer = model.save_file;
            List<Item> ret = new List<Item>();
            for (uint offset = OFFSETA_ITEM_BOX; offset < OFFSETA_ITEM_BOX_END; offset += SIZE_ITEM)
            {
                string name = "Unknown";
                uint item_id = Model.byte_to_uint16_le(buffer, offset + OFFSETR_ITEM_ID);

                if (item_id == 0)
                {
                    // if not obtained yet
                    if (!OFFSET_ID_MAPPING.ContainsKey(offset))
                    {
                        // well we dont know the id to this offset either. just ignore!
                        // continue
                        continue;
                    }

                }
                else
                {
                    // already obtained
                    // validate
                    if (OFFSET_ID_MAPPING.ContainsKey(offset))
                    {
                        if (item_id != OFFSET_ID_MAPPING[offset])
                        {
                            throw new SystemException("Item offset and ID do not correspond.");
                        }
                    }
                }
                // valid offset
                ret.Add(new Item(offset, model));
            }
            return ret;
        }

        public static void read_item_mappings()
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(ID_MAPPING_FILE_NAME);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length == 0)
                    continue;

                string[] eachline = line.Split('\t');
                if (eachline.Length != 3)
                    throw new SystemException("Item mapping file is corrupted.");

                OFFSET_ID_MAPPING.Add(UInt32.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber), UInt32.Parse(eachline[1], System.Globalization.NumberStyles.HexNumber));
                OFFSET_NAME_MAPPING.Add(UInt32.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber), eachline[2]);
            }

            file.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
