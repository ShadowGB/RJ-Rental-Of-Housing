namespace RJRentalOfHousing.Domain.Apartments
{
    public record PictureSize
    {
        public int Height { get; internal set; }
        public int Width { get; internal set; }

        public PictureSize(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "图片宽度必须大于0");
            if (height <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), "图片长度必须大于0");
            Width = width;
            Height = height;
        }

        internal PictureSize() { }
    }
}
