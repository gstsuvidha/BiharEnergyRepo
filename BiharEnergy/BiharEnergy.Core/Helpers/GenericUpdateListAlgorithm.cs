using System.Collections.Generic;

namespace BiharEnergy.Core.Helpers
{
    public static class GenericUpdateListAlgorithm<T> where T : class
    {
        public delegate bool TMatchCondition(T oldItem, T newItem);

        public delegate void TModify(T oldItem, T newItem);

        public static void UpdateList(ICollection<T> oldItems, ICollection<T> newItems, TMatchCondition ifMatched,TModify modify)
        {

            var itemsToRemove = new List<T>();
            //Items To remove and modify
            foreach (var oldItem in oldItems)
            {
                var remove = true;
                foreach (var newItem in newItems)
                {
                    if (ifMatched(oldItem,newItem))
                    {            
                        modify(oldItem,newItem);

                        remove = false;
                        break;
                    }
                }
                if (remove)
                {
                    itemsToRemove.Add(oldItem);
                }
            }

            var itemToAdd = new List<T>();
            //NewItemsToAdd
            foreach (var newItem in newItems)
            {
                var add = true;
                foreach (var oldItem in oldItems)
                {
                    if (ifMatched(oldItem,newItem))
                    {
                        add = false;
                        break;
                    }

                }
                if (add)
                {
                    itemToAdd.Add(newItem);
                }
            }
            foreach (var item in itemsToRemove)
            {
                oldItems.Remove(item);
            }

            foreach (var item in itemToAdd)
            {
                oldItems.Add(item);
            }

        }

    }
}