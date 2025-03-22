using System;

public interface IImageFilter
{
    void ApplyFilter(string filterName);
}

public interface IVideoFilter
{
    void ApplyFilter(string filterName);
}

public class MediaProcessor : IImageFilter, IVideoFilter
{
    void IImageFilter.ApplyFilter(string filterName)
    {
        Console.WriteLine($"Applying image filter: {filterName}");
    }

    void IVideoFilter.ApplyFilter(string filterName)
    {
        Console.WriteLine($"Applying video filter: {filterName}");
    }
}

public class Program
{
    public static void Main()
    {
        MediaProcessor mediaProcessor = new MediaProcessor();

        IImageFilter imageFilter = mediaProcessor;
        IVideoFilter videoFilter = mediaProcessor;

        imageFilter.ApplyFilter("Brightness");

        videoFilter.ApplyFilter("Contrast");
    }
}