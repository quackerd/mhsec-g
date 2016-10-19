using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class ViewModel : INotifyPropertyChanged
    {

        private readonly Model _model;

        public Model model
        {
            get { return _model;}
        }

        private readonly Character _character;

        public Character character
        {
            get { return _character; }
        }

        private Monster _cur_monster_selection;
        private readonly ObservableCollection<Monster> _monsters;

        public Monster cur_monster
        {
            get { return _cur_monster_selection; }
            set { _cur_monster_selection = value; OnPropertyChanged(nameof(cur_monster)); }
        }
        public ObservableCollection<Monster> monsters
        {
            get
            {
                return _monsters;
            }
        }


        private readonly List<Item> _items;

        public List<Item> items
        {
            get { return _items; }
        }

        private readonly ObservableCollection<EggFragment> _egg_fragments;

        public ObservableCollection<EggFragment>  egg_fragments
        {
            get { return _egg_fragments; }
        }

        public ViewModel(byte[] save)
        {
            if (save == null || save.Length != Model.SAVE_FILE_SIZE)
            {
                throw new SystemException("Invalid save file size.");
            }

            _model = new Model(save);
            _character = new Character(_model);
            _items = Item.read_all_items(_model);
            _monsters = new ObservableCollection<Monster>(Monster.read_all_monsters(_model));
            _cur_monster_selection = _monsters.ElementAt(0);
            _egg_fragments = EggFragment.read_all_egg_fragments(_model);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
