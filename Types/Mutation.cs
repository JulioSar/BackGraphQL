using SpotifyWeb;

namespace BackGraphQL.Web.Types;

public class Mutation
{
    [GraphQLDescription("Add one or more items to a user's playlist.")]
    public async Task<AddItemsToPlaylistPayload> AddItemsToPlaylist(AddItemsToPlaylistInput input, 
        SpotifyService spotifyService)
    {

        try
        {
            //Adding the track to "Spoti"
            var snapshot_id = await spotifyService.AddTracksToPlaylistAsync(
                input.PlaylistId,
                null,
                string.Join(",", input.Uris)
            );
            
            //Retrieving the playlist to send to the client
            var response = await spotifyService.GetPlaylistAsync(input.PlaylistId);
            var playlist = new Playlist(response);

            return new AddItemsToPlaylistPayload(
                200,
                true,
                "Successfully added items to playlist",
                playlist
            );
        }
        catch (Exception e)
        {
            return new AddItemsToPlaylistPayload(500, false, e.Message, null);        }
    }
}