using SpotifyWeb;

namespace BackGraphQL.Web.Types;
[GraphQLDescription("A curated collection of tracks designed for a specific activity or mood.")]
public class Playlist
{
    
    [GraphQLDescription("The I for the playlist")]
    [ID]
    public string Id { get; }
    [GraphQLDescription("The name of the playlist")]
    public string Name { get; set; }
    [GraphQLDescription("Describes the playlist")]
    public string? Description { get; set; }
    
    private List<Track>? _tracks;

    public async Task<List<Track>> Tracks(SpotifyService spotifyService)
    {
        if (_tracks != null) {
            return _tracks;
        } else {
            var response = await spotifyService.GetPlaylistsTracksAsync(this.Id);
            return response.Items.Select(item => new Track(item.Track)).ToList();
        }
    }

    public Playlist(string id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Playlist(PlaylistSimplified obj)
    {
        Id = obj.Id;
        Name = obj.Name;
        Description = obj.Description;
    }

    public Playlist(SpotifyWeb.Playlist obj)
    {
        var paginatedTracks = obj.Tracks.Items;
        var trackObjects = paginatedTracks.Select(item => new Track(item.Track)).ToList();
        Id = obj.Id;
        Name = obj.Name;
        Description = obj.Description;
        _tracks = trackObjects;
    }
    
}