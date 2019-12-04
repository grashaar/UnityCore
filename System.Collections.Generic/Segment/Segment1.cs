namespace System.Collections.Generic
{
    public readonly struct Segment1<T> : ISegment<T>, IEquatable<Segment1<T>>
    {
        public static bool operator ==(in Segment1<T> a, in Segment1<T> b)
            => a.Equals(b);

        public static bool operator !=(in Segment1<T> a, in Segment1<T> b)
            => !(a == b);

        public static implicit operator Segment1<T>(T item)
            => new Segment1<T>(item);

        public static implicit operator Segment<T>(in Segment1<T> segment)
            => new Segment<T>(segment);

        public Segment1(T item)
        {
            this.item = item;
        }

        private readonly T item;

        public T this[int index]
        {
            get
            {
                if (index != 0)
                    throw new IndexOutOfRangeException(nameof(index));

                return this.item;
            }
        }

        public int Count
            => 1;

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash ^= this.item == null ? 0 : this.item.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is Segment1<T> other ? Equals(other) : false;

        public bool Equals(Segment1<T> other)
        {
            if (this.item == null && other.item == null)
                return true;

            if (this.item == null || other.item == null)
                return false;

            return this.item.Equals(other.item);
        }

        /// <summary>
        /// Take a slice of 1 item
        /// </summary>
        /// <param name="index">Must be 0</param>
        /// <returns></returns>
        public Segment1<T> Slice(int index)
        {
            if (index != 0)
            {
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();
            }

            return new Segment1<T>(this.item);
        }

        /// <summary>
        /// Take a slice of 1 item
        /// </summary>
        /// <param name="index">Must be 0</param>
        /// <param name="count">Must be 1</param>
        /// <returns></returns>
        public Segment1<T> Slice(int index, int count)
        {
            if (index != 0 || count != 1)
            {
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();
            }

            return new Segment1<T>(this.item);
        }

        /// <summary>
        /// Skip 0 item
        /// </summary>
        /// <param name="count">Must be 0</param>
        /// <returns></returns>
        public Segment1<T> Skip(int count)
        {
            if (count != 0)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment1<T>(this.item);
        }

        /// <summary>
        /// Take 1 item
        /// </summary>
        /// <param name="count">Must be 1</param>
        /// <returns></returns>
        public Segment1<T> Take(int count)
        {
            if (count != 1)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment1<T>(this.item);
        }

        /// <summary>
        /// Take 1 item from the last position
        /// </summary>
        /// <param name="count">Must be 1</param>
        /// <returns></returns>
        public Segment1<T> TakeLast(int count)
        {
            if (count != 1)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment1<T>(this.item);
        }

        /// <summary>
        /// Skip 0 item from the last position
        /// </summary>
        /// <param name="count">Must be 0</param>
        /// <returns></returns>
        public Segment1<T> SkipLast(int count)
        {
            if (count != 0)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment1<T>(this.item);
        }

        ISegment<T> ISegment<T>.Slice(int index)
            => Slice(index);

        ISegment<T> ISegment<T>.Slice(int index, int count)
            => Slice(index, count);

        ISegment<T> ISegment<T>.Skip(int count)
            => Skip(count);

        ISegment<T> ISegment<T>.Take(int count)
            => Take(count);

        ISegment<T> ISegment<T>.TakeLast(int count)
            => TakeLast(count);

        ISegment<T> ISegment<T>.SkipLast(int count)
            => SkipLast(count);

        public Enumerator GetEnumerator()
            => new Enumerator(this);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            private readonly T item;
            private int current;

            public Enumerator(in Segment1<T> array)
            {
                this.item = array.item;
                this.current = -1;
            }

            public bool MoveNext()
            {
                if (this.current < 0)
                {
                    this.current = 0;
                    return true;
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current > 0)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.item;
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = -1;
            }

            public void Dispose()
            {
            }
        }
    }
}