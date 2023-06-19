using System.Collections.Generic;

public class PriorityQueueCustom<T>
{
    private List<T> _items = new List<T>();
    private Comparer<T> _comparer = Comparer<T>.Default;

    public int Count => _items.Count;

    public void Enqueue(T item)
    {
        _items.Add(item);
        int childIndex = _items.Count - 1;

        while (childIndex > 0)
        {
            int parentIndex = (childIndex - 1) / 2;

            if (_comparer.Compare(_items[childIndex], _items[parentIndex]) >= 0)
                break;

            Swap(childIndex, parentIndex);
            childIndex = parentIndex;
        }
    }

    public T Dequeue()
    {
        int lastIndex = _items.Count - 1;
        T firstItem = _items[0];
        _items[0] = _items[lastIndex];
        _items.RemoveAt(lastIndex);

        lastIndex--;

        int parentIndex = 0;

        while (true)
        {
            int childIndex = parentIndex * 2 + 1;

            if (childIndex > lastIndex)
                break;

            int rightChildIndex = childIndex + 1;

            if (rightChildIndex <= lastIndex && _comparer.Compare(_items[rightChildIndex], _items[childIndex]) < 0)
                childIndex = rightChildIndex;

            if (_comparer.Compare(_items[parentIndex], _items[childIndex]) <= 0)
                break;

            Swap(parentIndex, childIndex);
            parentIndex = childIndex;
        }

        return firstItem;
    }

    public T Peek()
    {
        return _items[0];
    }

    private void Swap(int index1, int index2)
    {
        T temp = _items[index1];
        _items[index1] = _items[index2];
        _items[index2] = temp;
    }
}