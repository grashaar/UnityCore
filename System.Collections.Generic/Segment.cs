﻿namespace System.Collections.Generic
{
    public readonly struct Segment<T> : ISegment<T>, IEquatable<Segment<T>>
    {
        private static T[] _empty { get; } = new T[0];

        // Do not replace the array allocation with Array.Empty. We don't want to have the overhead of
        // instantiating another generic type in addition to ListSegment<T> for new type parameters.
        public static Segment<T> Empty { get; } = new Segment<T>(_empty);

        public static bool operator ==(in Segment<T> a, in Segment<T> b)
            => a.Equals(b);

        public static bool operator !=(in Segment<T> a, in Segment<T> b)
            => !(a == b);

        public static implicit operator Segment<T>(T[] array)
            => array == null ? Empty : new Segment<T>(array);

        public static implicit operator Segment<T>(List<T> list)
            => list == null ? Empty : new Segment<T>(list);

        public Segment(IReadOnlyList<T> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.HasSource = true;
            this.Offset = 0;
            this.Count = source.Count;
        }

        public Segment(IReadOnlyList<T> source, int offset, int count)
        {
            // Validate arguments, check is minimal instructions with reduced branching for inlinable fast-path
            // Negative values discovered though conversion to high values when converted to unsigned
            // Failure should be rare and location determination and message is delegated to failure functions
            if (source == null || (uint)offset > (uint)source.Count || (uint)count > (uint)(source.Count - offset))
                throw ThrowHelper.GetSegmentCtorValidationFailedException(source, offset, count);

            this.source = source;
            this.HasSource = true;
            this.Offset = offset;
            this.Count = count;
        }

        private readonly IReadOnlyList<T> source;

        public bool HasSource { get; }

        public int Offset { get; }

        public int Count { get; }

        public T this[int index]
        {
            get
            {
                if ((uint)index >= (uint)this.Count)
                {
                    throw ThrowHelper.GetArgumentOutOfRange_IndexException();
                }

                return this.source[this.Offset + index];
            }
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = (int)2166136261;
                hash = (hash * 16777619) ^ this.Offset;
                hash = (hash * 16777619) ^ this.Count;

                // The array hash is expected to be an evenly-distributed mixture of bits,
                // so rather than adding the cost of another rotation we just xor it.
                hash ^= this.HasSource ? this.source.GetHashCode() : _empty.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => obj is Segment<T> other ? Equals(other) : false;

        public bool Equals(Segment<T> other)
        {
            if (!this.HasSource)
                return !other.HasSource || ReferenceEquals(other.source, _empty);

            return ReferenceEquals(this.source, other.source) &&
                   other.Offset == this.Offset &&
                   other.Count == this.Count;
        }

        public Segment<T> Slice(int index)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count)
            {
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();
            }

            return new Segment<T>(this.source, this.Offset + index, this.Count - index);
        }

        public Segment<T> Slice(int index, int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)index > (uint)this.Count || (uint)count > (uint)(this.Count - index))
            {
                throw ThrowHelper.GetArgumentOutOfRange_IndexException();
            }

            return new Segment<T>(this.source, this.Offset + index, count);
        }

        public Segment<T> Skip(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment<T>(this.source, this.Offset + count, this.Count - count);
        }

        public Segment<T> Take(int count)
        {
            if (!this.HasSource)
                return Empty;

            if ((uint)count > (uint)this.Count)
            {
                throw ThrowHelper.GetArgumentOutOfRange_CountException();
            }

            return new Segment<T>(this.source, this.Offset, count);
        }

        public Segment<T> TakeLast(int count)
            => Skip(this.Count - count);

        public Segment<T> SkipLast(int count)
            => Take(this.Count - count);

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

        public T[] ToArray()
        {
            if (this.HasSource || this.Count == 0)
                return _empty;

            var array = new T[this.Count];
            var count = this.Count + this.Offset;

            for (int i = this.Offset, j = 0; i < count; i++, j++)
            {
                array[j] = this.source[i];
            }

            return array;
        }

        public int IndexOf(T item)
        {
            var index = -1;

            if (!this.HasSource)
                return index;

            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            return index >= 0 ? index - this.Offset : -1;
        }

        public bool Contains(T item)
        {
            if (!this.HasSource)
                return false;

            var count = this.Count + this.Offset;

            for (var i = this.Offset; i < count; i++)
            {
                if (this.source[i].Equals(item))
                    return true;
            }

            return false;
        }

        public Enumerator GetEnumerator()
            => new Enumerator(this.HasSource ? this : Empty);

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public struct Enumerator : IEnumerator<T>
        {
            private readonly IReadOnlyList<T> source;
            private readonly int start;
            private readonly int end; // cache Offset + Count, since it's a little slow
            private int current;

            internal Enumerator(in Segment<T> segment)
            {
                this.source = segment.source;
                this.start = segment.Offset;
                this.end = segment.Offset + segment.Count;
                this.current = this.start - 1;
            }

            public bool MoveNext()
            {
                if (this.current < this.end)
                {
                    this.current++;
                    return (this.current < this.end);
                }

                return false;
            }

            public T Current
            {
                get
                {
                    if (this.current < this.start)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumNotStarted();

                    if (this.current >= this.end)
                        throw ThrowHelper.GetInvalidOperationException_InvalidOperation_EnumEnded();

                    return this.source[this.current];
                }
            }

            object IEnumerator.Current
                => this.Current;

            public void Reset()
            {
                this.current = this.start - 1;
            }

            public void Dispose()
            {
            }
        }
    }
}