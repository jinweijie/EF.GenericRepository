using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EF.GenericRepository.Entity
{
    // An System.Collections.ObjectModel.ObservableCollection that raises
    // individual item removal notifications on clear and prevents adding duplicates.
    [Serializable]
    public class FixupCollection<T> : ObservableCollection<T>
    {
        protected override void ClearItems()
        {
            new List<T>(this).ForEach(t => Remove(t));
        }

        protected override void InsertItem(int index, T item)
        {
            if (!this.Contains(item))
            {
                base.InsertItem(index, item);
            }
        }
    }
}
