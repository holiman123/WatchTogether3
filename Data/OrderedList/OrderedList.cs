using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WatchTogether3.Data.OrderedList;

public class OrderedList<T> : List<T> where T : OrderableItem
{
    private void ReorderItems(int startIndex = 0, int? endIndex = null)
    {
        endIndex ??= Count - 1;
        for (int i = startIndex; i <= endIndex; i++)
        {
            this[i].Order = i;
        }
    }


    public void MoveItem(int oldIndex, int newIndex)
    {
        if (oldIndex == newIndex)
            return;
        if (oldIndex < 0 || oldIndex >= Count)
            throw new ArgumentOutOfRangeException(nameof(oldIndex));
        if (newIndex < 0 || newIndex >= Count)
            throw new ArgumentOutOfRangeException(nameof(newIndex));

        T item = this[oldIndex];
        base.RemoveAt(oldIndex);
        base.Insert(newIndex, item);
        
        if (oldIndex < newIndex)
            ReorderItems(oldIndex, newIndex);
        else
            ReorderItems(newIndex, oldIndex);
    }

    public void ExchangeItems(int indexA, int indexB)
    {
        if (indexA < 0 || indexA >= Count)
            throw new ArgumentOutOfRangeException(nameof(indexA));
        if (indexB < 0 || indexB >= Count)
            throw new ArgumentOutOfRangeException(nameof(indexB));
        if (indexA == indexB)
            return;

        T temp = this[indexA];
        this[indexA] = this[indexB];
        this[indexB] = temp;

        this[indexA].Order = indexA;
        this[indexB].Order = indexB;
    }


    /// <inheritdoc cref="List{T}.Add(T)"/>
    public new void Add(T item)
    {
        item.Order = Count;
        base.Add(item);
    }

    /// <inheritdoc cref="List{T}.AddRange(IEnumerable{T})"/>
    public new void AddRange(IEnumerable<T> collection)
    {
        int previousCount = Count;
        base.AddRange(collection);
        ReorderItems(previousCount);
    }


    /// <inheritdoc cref="List{T}.Insert(int, T)"/>
    public new void Insert(int index, T item)
    {
        base.Insert(index, item);
        ReorderItems(index);
    }

    /// <inheritdoc cref="List{T}.InsertRange(int, IEnumerable{T})"/>
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        base.InsertRange(index, collection);
        ReorderItems(index);
    }


    /// <inheritdoc cref="List{T}.Remove(T)"/>
    public new bool Remove(T item)
    {
        int index = IndexOf(item);
        if (index == -1)
            return false;

        base.RemoveAt(index);
        ReorderItems(index);
        return true;
    }

    /// <inheritdoc cref="List{T}.RemoveAt(int)"/>
    public new void RemoveAt(int index)
    {
        base.RemoveAt(index);
        ReorderItems(index);
    }

    /// <inheritdoc cref="List{T}.RemoveAll(Predicate{T})"/>
    public new int RemoveAll(Predicate<T> match)
    {
        int removed = base.RemoveAll(match);
        if (removed != 0)
            ReorderItems();
        return removed;
    }

    /// <inheritdoc cref="List{T}.RemoveRange(int, int)"/>
    public new void RemoveRange(int index, int count)
    {
        base.RemoveRange(index, count);
        ReorderItems(index);
    }

    
    /// <inheritdoc cref="List{T}.Sort()"/>
    public new void Sort()
    {
        base.Sort();
        ReorderItems();
        base.Sort();
    }

    /// <inheritdoc cref="List{T}.Sort(Comparison{T})"/>
    public new void Sort(Comparison<T> comparison)
    {
        base.Sort(comparison);
        ReorderItems();
    }

    /// <inheritdoc cref="List{T}.Sort(IComparer{T}?)"/>
    public new void Sort(IComparer<T>? comparer)
    {
        base.Sort(comparer);
        ReorderItems();
    }

    /// <inheritdoc cref="List{T}.Sort(int, int, IComparer{T}?)"/>
    public new void Sort(int index, int count, IComparer<T>? comparer)
    {
        base.Sort(index, count, comparer);
        ReorderItems(index, index + count);
    }


    /// <inheritdoc cref="List{T}.Reverse()"/>
    public new void Reverse()
    {
        base.Reverse();
        ReorderItems();
    }

}
