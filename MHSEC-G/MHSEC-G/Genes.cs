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
    public class Genes : INotifyPropertyChanged
    {
        public static readonly List<uint> GENE_ID = new List<uint>();
        public static readonly List<string> GENE_NAME = new List<string>();
        public List<string> gene_name => GENE_NAME;

        private readonly uint _size;
        private readonly Model _model;
        private readonly uint _offset;
        public Genes(Model model, uint offset, uint size)
        {
            this._size = size;
            this._offset = offset;
            this._model = model;
        }

        // 
        // Genes
        //

        private uint extract_gene(uint gene_idx)
        {
            if (gene_idx >= 9)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MON_GENE_IDX_OVERFLOW, "Invalid gene index: " + gene_idx);
            }
            return Model.byte_to_uint16_le(_model.save_file,
                _offset + gene_idx * _size);
        }

        private void set_gene_str(uint gene_idx, string val)
        {
            uint parsed;
            if (Model.parse_hex_string(val, out parsed))
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
                Model.write_uint16_le(_model.save_file, _offset + gene_idx * _size,
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
            int idx = GENE_ID.IndexOf(gene_id);
            if (idx == -1)
            {
                // unknown
                return GENE_NAME.Count - 1;
            }
            return idx;
        }

        private void set_gene_idx(uint gene_idx, int val)
        {
            if (val != -1 && val != GENE_NAME.Count - 1)
            {
                set_gene(gene_idx, GENE_ID.ElementAt(val));
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

        public static void read_gene_mapping()
        {
            string line;
            StringReader file = new StringReader(Properties.Resources.gene);

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

                GENE_ID.Add(uint.Parse(eachline[0], System.Globalization.NumberStyles.HexNumber));
                GENE_NAME.Add(eachline[1]);
            }
            GENE_NAME.Add("Custom");

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
