using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class Genes : InMemoryObject
    {
        public List<string> gene_name => Offsets.GENE_NAME;
        private readonly uint _gene_size;
        public Genes(byte[] model, uint objOffset, uint gene_size) : base(model, objOffset, 9 * gene_size)
        {
            _gene_size = gene_size;
        }

        private uint extract_gene(uint gene_idx)
        {
            if (gene_idx >= 9)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_IDX_OVERFLOW, "Invalid gene index: " + gene_idx);
            }
            return Helper.byte_to_uint16_le(_data, _obj_offset + gene_idx * _gene_size);
        }

        private void set_gene_str(uint gene_idx, string val)
        {
            uint parsed;
            if (Helper.parse_hex_string(val, out parsed))
            {
                set_gene(gene_idx, parsed);
            }
            else
            {
                MessageBox.Show("Malformed gene value - must be 0x0 to 0xFFFF.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void set_gene(uint gene_idx, uint val)
        {
            if (gene_idx >= 9)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_IDX_OVERFLOW, "Invalid gene index: " + gene_idx);
            }
            if (val <= 0xFFFF)
            {
                Helper.write_uint16_le(_data, _obj_offset + gene_idx * _gene_size,
                    val);
            }
            else
            {
                MessageBox.Show("Malformed gene value - must be 0x0 to 0xFFFF.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private int extract_gene_idx(uint gene_idx)
        {
            uint gene_id = extract_gene(gene_idx);
            int idx = Offsets.GENE_ID.IndexOf(gene_id);
            if (idx == -1)
            {
                // unknown
                return Offsets.GENE_NAME.Count - 1;
            }
            return idx;
        }

        private void set_gene_idx(uint gene_idx, int val)
        {
            if (val != -1 && val != Offsets.GENE_NAME.Count - 1)
            {
                set_gene(gene_idx, Offsets.GENE_ID.ElementAt(val));
            }
        }

        public int gene1_selected
        {
            get => extract_gene_idx(0);
            set
            {
                set_gene_idx(0, value);
                OnPropertyChanged(nameof(gene1));
                OnPropertyChanged(nameof(gene1_selected));
            }
        }

        public string gene1
        {
            get => extract_gene(0).ToString("X4");
            set
            {
                set_gene_str(0, value);
                OnPropertyChanged(nameof(gene1));
                OnPropertyChanged(nameof(gene1_selected));
            }
        }

        public int gene2_selected
        {
            get => extract_gene_idx(1);
            set
            {
                set_gene_idx(1, value);
                OnPropertyChanged(nameof(gene2));
                OnPropertyChanged(nameof(gene2_selected));
            }
        }

        public string gene2
        {
            get => extract_gene(1).ToString("X4");
            set
            {
                set_gene_str(1, value);
                OnPropertyChanged(nameof(gene2));
                OnPropertyChanged(nameof(gene2_selected));
            }
        }

        public int gene3_selected
        {
            get => extract_gene_idx(2);
            set
            {
                set_gene_idx(2, value);
                OnPropertyChanged(nameof(gene3));
                OnPropertyChanged(nameof(gene3_selected));
            }
        }


        public string gene3
        {
            get => extract_gene(2).ToString("X4");
            set
            {
                set_gene_str(2, value);
                OnPropertyChanged(nameof(gene3));
                OnPropertyChanged(nameof(gene3_selected));
            }
        }

        public int gene4_selected
        {
            get => extract_gene_idx(3);
            set
            {
                set_gene_idx(3, value);
                OnPropertyChanged(nameof(gene4));
                OnPropertyChanged(nameof(gene4_selected));
            }
        }


        public string gene4
        {
            get => extract_gene(3).ToString("X4");
            set
            {
                set_gene_str(3, value);
                OnPropertyChanged(nameof(gene4));
                OnPropertyChanged(nameof(gene4_selected));
            }
        }

        public int gene5_selected
        {
            get => extract_gene_idx(4);
            set
            {
                set_gene_idx(4, value);
                OnPropertyChanged(nameof(gene5));
                OnPropertyChanged(nameof(gene5_selected));
            }
        }


        public string gene5
        {
            get => extract_gene(4).ToString("X4");
            set
            {
                set_gene_str(4, value);
                OnPropertyChanged(nameof(gene5));
                OnPropertyChanged(nameof(gene5_selected));
            }
        }

        public int gene6_selected
        {
            get => extract_gene_idx(5);
            set
            {
                set_gene_idx(5, value);
                OnPropertyChanged(nameof(gene6));
                OnPropertyChanged(nameof(gene6_selected));
            }
        }


        public string gene6
        {
            get => extract_gene(5).ToString("X4");
            set
            {
                set_gene_str(5, value);
                OnPropertyChanged(nameof(gene6));
                OnPropertyChanged(nameof(gene6_selected));
            }
        }


        public int gene7_selected
        {
            get => extract_gene_idx(6);
            set
            {
                set_gene_idx(6, value);
                OnPropertyChanged(nameof(gene7));
                OnPropertyChanged(nameof(gene7_selected));
            }
        }


        public string gene7
        {
            get => extract_gene(6).ToString("X4");
            set
            {
                set_gene_str(6, value);
                OnPropertyChanged(nameof(gene7));
                OnPropertyChanged(nameof(gene7_selected));
            }
        }


        public int gene8_selected
        {
            get => extract_gene_idx(7);
            set
            {
                set_gene_idx(7, value);
                OnPropertyChanged(nameof(gene8));
                OnPropertyChanged(nameof(gene8_selected));
            }
        }


        public string gene8
        {
            get => extract_gene(7).ToString("X4");
            set
            {
                set_gene_str(7, value);
                OnPropertyChanged(nameof(gene8));
                OnPropertyChanged(nameof(gene8_selected));
            }
        }


        public int gene9_selected
        {
            get => extract_gene_idx(8);
            set
            {
                set_gene_idx(8, value);
                OnPropertyChanged(nameof(gene9));
                OnPropertyChanged(nameof(gene9_selected));
            }
        }

        public string gene9
        {
            get => extract_gene(8).ToString("X4");
            set
            {
                set_gene_str(8, value);
                OnPropertyChanged(nameof(gene9));
                OnPropertyChanged(nameof(gene9_selected));
            }
        }
    }
}
