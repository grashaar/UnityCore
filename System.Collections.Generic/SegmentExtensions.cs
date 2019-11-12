namespace System.Collections.Generic
{
    /// <summary>
    /// Provides extension methods for <see cref="Segment{T}"/>.
    /// </summary>
    public static class SegmentExtensions
    {
        /// <summary>
        /// Creates a segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>A new segment.</returns>
        public static Segment<T> AsSegment<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        public static Segment<T> AsSegment<T>(this T[] source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates a new segment by skipping a number of elements and then taking a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</param>
        /// <param name="count">The length of the new segment. Must be in the range <c>[0, <paramref name="source"/>.Count - <paramref name="offset"/>]</c>.</param>
        /// <returns>The new segment.</returns>
        public static Segment<T> Slice<T>(this IReadOnlyList<T> source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        public static Segment<T> Slice<T>(this T[] source, int offset, int count)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, count);

        /// <summary>
        /// Creates an segment referencing this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the segment begins. Defaults to <c>0</c> (the beginning of the source). Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> AsSegment<T>(this IReadOnlyList<T> source, int offset = 0)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        public static Segment<T> AsSegment<T>(this T[] source, int offset = 0)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates a new segment by skipping a number of elements from this source.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="offset">The offset in this source where the new segment begins. Must be in the range <c>[0, <paramref name="source"/>.Count]</c>.</returns>
        public static Segment<T> Slice<T>(this IReadOnlyList<T> source, int offset)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Count - offset);

        public static Segment<T> Slice<T>(this T[] source, int offset)
            => source == null ? Segment<T>.Empty : new Segment<T>(source, offset, source.Length - offset);

        /// <summary>
        /// Creates an <see cref="SegmentReader{T}"/> over this segment.
        /// </summary>
        /// <typeparam name="T">The type of elements contained in the source.</typeparam>
        /// <param name="segment">The segment.</param>
        /// <returns>A new <see cref="SegmentReader{T}"/>.</returns>
        public static SegmentReader<Segment<T>, T> CreateSegmentReader<T>(this Segment<T> segment)
            => new SegmentReader<Segment<T>, T>(segment);
    }
}
