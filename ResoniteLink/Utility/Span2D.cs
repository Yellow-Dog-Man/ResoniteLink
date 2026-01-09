using System;
using System.Collections.Generic;
using System.Text;

namespace ResoniteLink
{
    public ref struct Span2D<T>
    {
        public readonly int width;
        public readonly int height;

        Span<T> _innerSpan;

        public Span2D(int width, int height, Span<T> innerSpan)
        {
            var elements = width * height;

            if (innerSpan.Length < elements)
                throw new ArgumentException("Inner span is too short");

            this.width = width;
            this.height = height;

            _innerSpan = innerSpan;
        }

        public ref T this[int x, int y] => ref _innerSpan[x + y * width];
    }
}
