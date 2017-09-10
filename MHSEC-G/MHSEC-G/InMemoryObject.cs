using System;
using System.ComponentModel;
using MHSEC_G.Annotations;

namespace MHSEC_G
{
    public class InMemoryObject : INotifyPropertyChanged
    {
        protected readonly uint _obj_offset;
        protected readonly byte[] _data;
        protected readonly uint _obj_size;

        public InMemoryObject(byte[] data, uint objOffset, uint objSize)
        {
            _obj_offset = objOffset;
            _data = data;
            _obj_size = objSize;
        }

        public virtual byte[] toByteArray()
        {
            byte[] ret = new byte[_obj_size];
            Array.Copy(_data, _obj_offset, ret, 0, _obj_size);
            return ret;
        }

        public virtual void setByteArray(byte[] data)
        {
            if (data.Length != _obj_size)
            {
                BugCheck.bug_check(BugCheck.ErrorCode.MODEL_INVALID_FILE_SIZE, "Invalid data size in setByteArray.");
            }
            else
            {
                Array.Copy(data, 0, _data, _obj_offset, _obj_size);
                OnPropertyChanged(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
