using System;
using System.Collections.Generic;
using System.Linq;

public abstract class MusicAlbum
{
    public string Title { get; set; }
    public string Artist { get; set; }
    public int ReleaseYear { get; set; }
    public int TrackCount { get; set; }

    protected MusicAlbum(string title, string artist, int releaseYear, int trackCount)
    {
        Title = title;
        Artist = artist;
        ReleaseYear = releaseYear;
        TrackCount = trackCount;
    }
}

public sealed class RockAlbum : MusicAlbum
{
    public RockAlbum(string title, string artist, int releaseYear, int trackCount)
        : base(title, artist, releaseYear, trackCount) { }
}

public sealed class PopAlbum : MusicAlbum
{
    public PopAlbum(string title, string artist, int releaseYear, int trackCount)
        : base(title, artist, releaseYear, trackCount) { }
}

public class MusicLibrary
{
    private MusicAlbum[] albums;

    public MusicLibrary(MusicAlbum[] albums)
    {
        this.albums = albums;
    }

    public MusicAlbum GetNewestAlbum()
    {
        return albums.OrderByDescending(album => album.ReleaseYear).FirstOrDefault();
    }

    public List<MusicAlbum> GetAlbumsByArtist(string artist)
    {
        return albums.Where(album => album.Artist.Equals(artist, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}

public class Program
{
    public static void Main()
    {
        MusicAlbum[] albums = new MusicAlbum[]
        {
            new RockAlbum("Rock Album 1", "Artist A", 2020, 10),
            new PopAlbum("Pop Album 1", "Artist B", 2023, 11),
            new RockAlbum("Rock Album 2", "Artist A", 2025, 2),
            new PopAlbum("Pop Album 2", "Artist B", 2019, 9),
            new PopAlbum("Pop Album 3", "Artist C", 2022, 10)
        };

        MusicLibrary library = new MusicLibrary(albums);

        MusicAlbum newestAlbum = library.GetNewestAlbum();
        Console.WriteLine($"Самый новый альбом: {newestAlbum.Title} (Год выпуска: {newestAlbum.ReleaseYear})");

        Console.Write("Введите имя исполнителя для поиска альбомов: ");
        string artist = Console.ReadLine();
        List<MusicAlbum> artistAlbums = library.GetAlbumsByArtist(artist);

        Console.WriteLine($"Альбомы исполнителя {artist}:");
        foreach (var album in artistAlbums)
        {
            Console.WriteLine($"- {album.Title} (Год выпуска: {album.ReleaseYear})");
        }
    }
}