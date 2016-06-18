using System;
using System.Runtime.InteropServices;

namespace CocoaSpotify
{
    public enum sp_error : uint
    {
        Ok = 0,
        BadApiVersion = 1,
        ApiInitializationFailed = 2,
        TrackNotPlayable = 3,
        BadApplicationKey = 5,
        BadUsernameOrPassword = 6,
        UserBanned = 7,
        UnableToContactServer = 8,
        ClientTooOld = 9,
        OtherPermanent = 10,
        BadUserAgent = 11,
        MissingCallback = 12,
        InvalidIndata = 13,
        IndexOutOfRange = 14,
        UserNeedsPremium = 15,
        OtherTransient = 16,
        IsLoading = 17,
        NoStreamAvailable = 18,
        PermissionDenied = 19,
        InboxIsFull = 20,
        NoCache = 21,
        NoSuchUser = 22,
        NoCredentials = 23,
        NetworkDisabled = 24,
        InvalidDeviceId = 25,
        CantOpenTraceFile = 26,
        ApplicationBanned = 27,
        OfflineTooManyTracks = 31,
        OfflineDiskCache = 32,
        OfflineExpired = 33,
        OfflineNotAllowed = 34,
        OfflineLicenseLost = 35,
        OfflineLicenseError = 36,
        LastfmAuthError = 39,
        InvalidArgument = 40,
        SystemFailure = 41
    }

    //static class CFunctions
    //{
    //    // extern const char * sp_error_message (sp_error error);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_error_message (sp_error error);

    //    // extern sp_error sp_session_create (const sp_session_config *config, sp_session **sess);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_create (sp_session_config* config, sp_session** sess);

    //    // extern sp_error sp_session_release (sp_session *sess);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_release (sp_session* sess);

    //    // extern sp_error sp_session_login (sp_session *session, const char *username, const char *password, _Bool remember_me, const char *blob);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_login (sp_session* session, sbyte* username, sbyte* password, bool remember_me, sbyte* blob);

    //    // extern sp_error sp_session_relogin (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_relogin (sp_session* session);

    //    // extern int sp_session_remembered_user (sp_session *session, char *buffer, size_t buffer_size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_session_remembered_user (sp_session* session, sbyte* buffer, nuint buffer_size);

    //    // extern const char * sp_session_user_name (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_session_user_name (sp_session* session);

    //    // extern sp_error sp_session_forget_me (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_forget_me (sp_session* session);

    //    // extern sp_user * sp_session_user (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_user* sp_session_user (sp_session* session);

    //    // extern sp_error sp_session_logout (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_logout (sp_session* session);

    //    // extern sp_error sp_session_flush_caches (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_flush_caches (sp_session* session);

    //    // extern sp_connectionstate sp_session_connectionstate (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_connectionstate sp_session_connectionstate (sp_session* session);

    //    // extern void * sp_session_userdata (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe void* sp_session_userdata (sp_session* session);

    //    // extern sp_error sp_session_set_cache_size (sp_session *session, size_t size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_cache_size (sp_session* session, nuint size);

    //    // extern sp_error sp_session_process_events (sp_session *session, int *next_timeout);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_process_events (sp_session* session, int* next_timeout);

    //    // extern sp_error sp_session_player_load (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_player_load (sp_session* session, sp_track* track);

    //    // extern sp_error sp_session_player_seek (sp_session *session, int offset);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_player_seek (sp_session* session, int offset);

    //    // extern sp_error sp_session_player_play (sp_session *session, _Bool play);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_player_play (sp_session* session, bool play);

    //    // extern sp_error sp_session_player_unload (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_player_unload (sp_session* session);

    //    // extern sp_error sp_session_player_prefetch (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_player_prefetch (sp_session* session, sp_track* track);

    //    // extern sp_playlistcontainer * sp_session_playlistcontainer (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlistcontainer* sp_session_playlistcontainer (sp_session* session);

    //    // extern sp_playlist * sp_session_inbox_create (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_session_inbox_create (sp_session* session);

    //    // extern sp_playlist * sp_session_starred_create (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_session_starred_create (sp_session* session);

    //    // extern sp_playlist * sp_session_starred_for_user_create (sp_session *session, const char *canonical_username);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_session_starred_for_user_create (sp_session* session, sbyte* canonical_username);

    //    // extern sp_playlistcontainer * sp_session_publishedcontainer_for_user_create (sp_session *session, const char *canonical_username);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlistcontainer* sp_session_publishedcontainer_for_user_create (sp_session* session, sbyte* canonical_username);

    //    // extern sp_error sp_session_preferred_bitrate (sp_session *session, sp_bitrate bitrate);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_preferred_bitrate (sp_session* session, sp_bitrate bitrate);

    //    // extern sp_error sp_session_preferred_offline_bitrate (sp_session *session, sp_bitrate bitrate, _Bool allow_resync);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_preferred_offline_bitrate (sp_session* session, sp_bitrate bitrate, bool allow_resync);

    //    // extern _Bool sp_session_get_volume_normalization (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_session_get_volume_normalization (sp_session* session);

    //    // extern sp_error sp_session_set_volume_normalization (sp_session *session, _Bool on);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_volume_normalization (sp_session* session, bool on);

    //    // extern sp_error sp_session_set_private_session (sp_session *session, _Bool enabled);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_private_session (sp_session* session, bool enabled);

    //    // extern _Bool sp_session_is_private_session (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_session_is_private_session (sp_session* session);

    //    // extern sp_error sp_session_set_scrobbling (sp_session *session, sp_social_provider provider, sp_scrobbling_state state);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_scrobbling (sp_session* session, sp_social_provider provider, sp_scrobbling_state state);

    //    // extern sp_error sp_session_is_scrobbling (sp_session *session, sp_social_provider provider, sp_scrobbling_state *state);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_is_scrobbling (sp_session* session, sp_social_provider provider, sp_scrobbling_state* state);

    //    // extern sp_error sp_session_is_scrobbling_possible (sp_session *session, sp_social_provider provider, _Bool *out);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_is_scrobbling_possible (sp_session* session, sp_social_provider provider, bool* @out);

    //    // extern sp_error sp_session_set_social_credentials (sp_session *session, sp_social_provider provider, const char *username, const char *password);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_social_credentials (sp_session* session, sp_social_provider provider, sbyte* username, sbyte* password);

    //    // extern sp_error sp_session_set_connection_type (sp_session *session, sp_connection_type type);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_connection_type (sp_session* session, sp_connection_type type);

    //    // extern sp_error sp_session_set_connection_rules (sp_session *session, sp_connection_rules rules);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_session_set_connection_rules (sp_session* session, sp_connection_rules rules);

    //    // extern int sp_offline_tracks_to_sync (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_offline_tracks_to_sync (sp_session* session);

    //    // extern int sp_offline_num_playlists (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_offline_num_playlists (sp_session* session);

    //    // extern _Bool sp_offline_sync_get_status (sp_session *session, sp_offline_sync_status *status);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_offline_sync_get_status (sp_session* session, sp_offline_sync_status* status);

    //    // extern int sp_offline_time_left (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_offline_time_left (sp_session* session);

    //    // extern int sp_session_user_country (sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_session_user_country (sp_session* session);

    //    // extern sp_link * sp_link_create_from_string (const char *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_string (sbyte* link);

    //    // extern sp_link * sp_link_create_from_track (sp_track *track, int offset);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_track (sp_track* track, int offset);

    //    // extern sp_link * sp_link_create_from_album (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_album (sp_album* album);

    //    // extern sp_link * sp_link_create_from_album_cover (sp_album *album, sp_image_size size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_album_cover (sp_album* album, sp_image_size size);

    //    // extern sp_link * sp_link_create_from_artist (sp_artist *artist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_artist (sp_artist* artist);

    //    // extern sp_link * sp_link_create_from_artist_portrait (sp_artist *artist, sp_image_size size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_artist_portrait (sp_artist* artist, sp_image_size size);

    //    // extern sp_link * sp_link_create_from_artistbrowse_portrait (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_artistbrowse_portrait (sp_artistbrowse* arb, int index);

    //    // extern sp_link * sp_link_create_from_search (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_search (sp_search* search);

    //    // extern sp_link * sp_link_create_from_playlist (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_playlist (sp_playlist* playlist);

    //    // extern sp_link * sp_link_create_from_user (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_user (sp_user* user);

    //    // extern sp_link * sp_link_create_from_image (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_link* sp_link_create_from_image (sp_image* image);

    //    // extern int sp_link_as_string (sp_link *link, char *buffer, int buffer_size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_link_as_string (sp_link* link, sbyte* buffer, int buffer_size);

    //    // extern sp_linktype sp_link_type (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_linktype sp_link_type (sp_link* link);

    //    // extern sp_track * sp_link_as_track (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_link_as_track (sp_link* link);

    //    // extern sp_track * sp_link_as_track_and_offset (sp_link *link, int *offset);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_link_as_track_and_offset (sp_link* link, int* offset);

    //    // extern sp_album * sp_link_as_album (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_link_as_album (sp_link* link);

    //    // extern sp_artist * sp_link_as_artist (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_link_as_artist (sp_link* link);

    //    // extern sp_user * sp_link_as_user (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_user* sp_link_as_user (sp_link* link);

    //    // extern sp_error sp_link_add_ref (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_link_add_ref (sp_link* link);

    //    // extern sp_error sp_link_release (sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_link_release (sp_link* link);

    //    // extern _Bool sp_track_is_loaded (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_track_is_loaded (sp_track* track);

    //    // extern sp_error sp_track_error (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_track_error (sp_track* track);

    //    // extern sp_track_offline_status sp_track_offline_get_status (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track_offline_status sp_track_offline_get_status (sp_track* track);

    //    // extern sp_track_availability sp_track_get_availability (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track_availability sp_track_get_availability (sp_session* session, sp_track* track);

    //    // extern _Bool sp_track_is_local (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_track_is_local (sp_session* session, sp_track* track);

    //    // extern _Bool sp_track_is_autolinked (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_track_is_autolinked (sp_session* session, sp_track* track);

    //    // extern sp_track * sp_track_get_playable (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_track_get_playable (sp_session* session, sp_track* track);

    //    // extern _Bool sp_track_is_placeholder (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_track_is_placeholder (sp_track* track);

    //    // extern _Bool sp_track_is_starred (sp_session *session, sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_track_is_starred (sp_session* session, sp_track* track);

    //    // extern sp_error sp_track_set_starred (sp_session *session, sp_track *const *tracks, int num_tracks, _Bool star);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_track_set_starred (sp_session* session, sp_track** tracks, int num_tracks, bool star);

    //    // extern int sp_track_num_artists (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_track_num_artists (sp_track* track);

    //    // extern sp_artist * sp_track_artist (sp_track *track, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_track_artist (sp_track* track, int index);

    //    // extern sp_album * sp_track_album (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_track_album (sp_track* track);

    //    // extern const char * sp_track_name (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_track_name (sp_track* track);

    //    // extern int sp_track_duration (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_track_duration (sp_track* track);

    //    // extern int sp_track_popularity (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_track_popularity (sp_track* track);

    //    // extern int sp_track_disc (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_track_disc (sp_track* track);

    //    // extern int sp_track_index (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_track_index (sp_track* track);

    //    // extern sp_track * sp_localtrack_create (const char *artist, const char *title, const char *album, int length);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_localtrack_create (sbyte* artist, sbyte* title, sbyte* album, int length);

    //    // extern sp_error sp_track_add_ref (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_track_add_ref (sp_track* track);

    //    // extern sp_error sp_track_release (sp_track *track);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_track_release (sp_track* track);

    //    // extern _Bool sp_album_is_loaded (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_album_is_loaded (sp_album* album);

    //    // extern _Bool sp_album_is_available (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_album_is_available (sp_album* album);

    //    // extern sp_artist * sp_album_artist (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_album_artist (sp_album* album);

    //    // extern const byte * sp_album_cover (sp_album *album, sp_image_size size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe byte* sp_album_cover (sp_album* album, sp_image_size size);

    //    // extern const char * sp_album_name (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_album_name (sp_album* album);

    //    // extern int sp_album_year (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_album_year (sp_album* album);

    //    // extern sp_albumtype sp_album_type (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_albumtype sp_album_type (sp_album* album);

    //    // extern sp_error sp_album_add_ref (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_album_add_ref (sp_album* album);

    //    // extern sp_error sp_album_release (sp_album *album);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_album_release (sp_album* album);

    //    // extern const char * sp_artist_name (sp_artist *artist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_artist_name (sp_artist* artist);

    //    // extern _Bool sp_artist_is_loaded (sp_artist *artist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_artist_is_loaded (sp_artist* artist);

    //    // extern const byte * sp_artist_portrait (sp_artist *artist, sp_image_size size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe byte* sp_artist_portrait (sp_artist* artist, sp_image_size size);

    //    // extern sp_error sp_artist_add_ref (sp_artist *artist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_artist_add_ref (sp_artist* artist);

    //    // extern sp_error sp_artist_release (sp_artist *artist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_artist_release (sp_artist* artist);

    //    // extern sp_albumbrowse * sp_albumbrowse_create (sp_session *session, sp_album *album, albumbrowse_complete_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_albumbrowse* sp_albumbrowse_create (sp_session* session, sp_album* album, albumbrowse_complete_cb* callback, void* userdata);

    //    // extern _Bool sp_albumbrowse_is_loaded (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_albumbrowse_is_loaded (sp_albumbrowse* alb);

    //    // extern sp_error sp_albumbrowse_error (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_albumbrowse_error (sp_albumbrowse* alb);

    //    // extern sp_album * sp_albumbrowse_album (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_albumbrowse_album (sp_albumbrowse* alb);

    //    // extern sp_artist * sp_albumbrowse_artist (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_albumbrowse_artist (sp_albumbrowse* alb);

    //    // extern int sp_albumbrowse_num_copyrights (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_albumbrowse_num_copyrights (sp_albumbrowse* alb);

    //    // extern const char * sp_albumbrowse_copyright (sp_albumbrowse *alb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_albumbrowse_copyright (sp_albumbrowse* alb, int index);

    //    // extern int sp_albumbrowse_num_tracks (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_albumbrowse_num_tracks (sp_albumbrowse* alb);

    //    // extern sp_track * sp_albumbrowse_track (sp_albumbrowse *alb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_albumbrowse_track (sp_albumbrowse* alb, int index);

    //    // extern const char * sp_albumbrowse_review (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_albumbrowse_review (sp_albumbrowse* alb);

    //    // extern int sp_albumbrowse_backend_request_duration (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_albumbrowse_backend_request_duration (sp_albumbrowse* alb);

    //    // extern sp_error sp_albumbrowse_add_ref (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_albumbrowse_add_ref (sp_albumbrowse* alb);

    //    // extern sp_error sp_albumbrowse_release (sp_albumbrowse *alb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_albumbrowse_release (sp_albumbrowse* alb);

    //    // extern sp_artistbrowse * sp_artistbrowse_create (sp_session *session, sp_artist *artist, sp_artistbrowse_type type, artistbrowse_complete_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artistbrowse* sp_artistbrowse_create (sp_session* session, sp_artist* artist, sp_artistbrowse_type type, artistbrowse_complete_cb* callback, void* userdata);

    //    // extern _Bool sp_artistbrowse_is_loaded (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_artistbrowse_is_loaded (sp_artistbrowse* arb);

    //    // extern sp_error sp_artistbrowse_error (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_artistbrowse_error (sp_artistbrowse* arb);

    //    // extern sp_artist * sp_artistbrowse_artist (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_artistbrowse_artist (sp_artistbrowse* arb);

    //    // extern int sp_artistbrowse_num_portraits (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_num_portraits (sp_artistbrowse* arb);

    //    // extern const byte * sp_artistbrowse_portrait (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe byte* sp_artistbrowse_portrait (sp_artistbrowse* arb, int index);

    //    // extern int sp_artistbrowse_num_tracks (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_num_tracks (sp_artistbrowse* arb);

    //    // extern sp_track * sp_artistbrowse_track (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_artistbrowse_track (sp_artistbrowse* arb, int index);

    //    // extern int sp_artistbrowse_num_tophit_tracks (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_num_tophit_tracks (sp_artistbrowse* arb);

    //    // extern sp_track * sp_artistbrowse_tophit_track (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_artistbrowse_tophit_track (sp_artistbrowse* arb, int index);

    //    // extern int sp_artistbrowse_num_albums (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_num_albums (sp_artistbrowse* arb);

    //    // extern sp_album * sp_artistbrowse_album (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_artistbrowse_album (sp_artistbrowse* arb, int index);

    //    // extern int sp_artistbrowse_num_similar_artists (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_num_similar_artists (sp_artistbrowse* arb);

    //    // extern sp_artist * sp_artistbrowse_similar_artist (sp_artistbrowse *arb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_artistbrowse_similar_artist (sp_artistbrowse* arb, int index);

    //    // extern const char * sp_artistbrowse_biography (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_artistbrowse_biography (sp_artistbrowse* arb);

    //    // extern int sp_artistbrowse_backend_request_duration (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_artistbrowse_backend_request_duration (sp_artistbrowse* arb);

    //    // extern sp_error sp_artistbrowse_add_ref (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_artistbrowse_add_ref (sp_artistbrowse* arb);

    //    // extern sp_error sp_artistbrowse_release (sp_artistbrowse *arb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_artistbrowse_release (sp_artistbrowse* arb);

    //    // extern sp_image * sp_image_create (sp_session *session, const byte *image_id);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_image* sp_image_create (sp_session* session, byte[] image_id);

    //    // extern sp_image * sp_image_create_from_link (sp_session *session, sp_link *l);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_image* sp_image_create_from_link (sp_session* session, sp_link* l);

    //    // extern sp_error sp_image_add_load_callback (sp_image *image, image_loaded_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_image_add_load_callback (sp_image* image, image_loaded_cb* callback, void* userdata);

    //    // extern sp_error sp_image_remove_load_callback (sp_image *image, image_loaded_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_image_remove_load_callback (sp_image* image, image_loaded_cb* callback, void* userdata);

    //    // extern _Bool sp_image_is_loaded (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_image_is_loaded (sp_image* image);

    //    // extern sp_error sp_image_error (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_image_error (sp_image* image);

    //    // extern sp_imageformat sp_image_format (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_imageformat sp_image_format (sp_image* image);

    //    // extern const void * sp_image_data (sp_image *image, size_t *data_size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe void* sp_image_data (sp_image* image, nuint* data_size);

    //    // extern const byte * sp_image_image_id (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe byte* sp_image_image_id (sp_image* image);

    //    // extern sp_error sp_image_add_ref (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_image_add_ref (sp_image* image);

    //    // extern sp_error sp_image_release (sp_image *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_image_release (sp_image* image);

    //    // extern sp_search * sp_search_create (sp_session *session, const char *query, int track_offset, int track_count, int album_offset, int album_count, int artist_offset, int artist_count, int playlist_offset, int playlist_count, sp_search_type search_type, search_complete_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_search* sp_search_create (sp_session* session, sbyte* query, int track_offset, int track_count, int album_offset, int album_count, int artist_offset, int artist_count, int playlist_offset, int playlist_count, sp_search_type search_type, search_complete_cb* callback, void* userdata);

    //    // extern _Bool sp_search_is_loaded (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_search_is_loaded (sp_search* search);

    //    // extern sp_error sp_search_error (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_search_error (sp_search* search);

    //    // extern int sp_search_num_tracks (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_num_tracks (sp_search* search);

    //    // extern sp_track * sp_search_track (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_search_track (sp_search* search, int index);

    //    // extern int sp_search_num_albums (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_num_albums (sp_search* search);

    //    // extern sp_album * sp_search_album (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_search_album (sp_search* search, int index);

    //    // extern int sp_search_num_playlists (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_num_playlists (sp_search* search);

    //    // extern const char * sp_search_playlist_name (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_search_playlist_name (sp_search* search, int index);

    //    // extern const char * sp_search_playlist_uri (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_search_playlist_uri (sp_search* search, int index);

    //    // extern const char * sp_search_playlist_image_uri (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_search_playlist_image_uri (sp_search* search, int index);

    //    // extern int sp_search_num_artists (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_num_artists (sp_search* search);

    //    // extern sp_artist * sp_search_artist (sp_search *search, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_search_artist (sp_search* search, int index);

    //    // extern const char * sp_search_query (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_search_query (sp_search* search);

    //    // extern const char * sp_search_did_you_mean (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_search_did_you_mean (sp_search* search);

    //    // extern int sp_search_total_tracks (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_total_tracks (sp_search* search);

    //    // extern int sp_search_total_albums (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_total_albums (sp_search* search);

    //    // extern int sp_search_total_artists (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_total_artists (sp_search* search);

    //    // extern int sp_search_total_playlists (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_search_total_playlists (sp_search* search);

    //    // extern sp_error sp_search_add_ref (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_search_add_ref (sp_search* search);

    //    // extern sp_error sp_search_release (sp_search *search);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_search_release (sp_search* search);

    //    // extern _Bool sp_playlist_is_loaded (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_is_loaded (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_add_callbacks (sp_playlist *playlist, sp_playlist_callbacks *callbacks, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_add_callbacks (sp_playlist* playlist, sp_playlist_callbacks* callbacks, void* userdata);

    //    // extern sp_error sp_playlist_remove_callbacks (sp_playlist *playlist, sp_playlist_callbacks *callbacks, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_remove_callbacks (sp_playlist* playlist, sp_playlist_callbacks* callbacks, void* userdata);

    //    // extern int sp_playlist_num_tracks (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlist_num_tracks (sp_playlist* playlist);

    //    // extern sp_track * sp_playlist_track (sp_playlist *playlist, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_playlist_track (sp_playlist* playlist, int index);

    //    // extern int sp_playlist_track_create_time (sp_playlist *playlist, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlist_track_create_time (sp_playlist* playlist, int index);

    //    // extern sp_user * sp_playlist_track_creator (sp_playlist *playlist, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_user* sp_playlist_track_creator (sp_playlist* playlist, int index);

    //    // extern _Bool sp_playlist_track_seen (sp_playlist *playlist, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_track_seen (sp_playlist* playlist, int index);

    //    // extern sp_error sp_playlist_track_set_seen (sp_playlist *playlist, int index, _Bool seen);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_track_set_seen (sp_playlist* playlist, int index, bool seen);

    //    // extern const char * sp_playlist_track_message (sp_playlist *playlist, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_playlist_track_message (sp_playlist* playlist, int index);

    //    // extern const char * sp_playlist_name (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_playlist_name (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_rename (sp_playlist *playlist, const char *new_name);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_rename (sp_playlist* playlist, sbyte* new_name);

    //    // extern sp_user * sp_playlist_owner (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_user* sp_playlist_owner (sp_playlist* playlist);

    //    // extern _Bool sp_playlist_is_collaborative (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_is_collaborative (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_set_collaborative (sp_playlist *playlist, _Bool collaborative);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_set_collaborative (sp_playlist* playlist, bool collaborative);

    //    // extern sp_error sp_playlist_set_autolink_tracks (sp_playlist *playlist, _Bool link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_set_autolink_tracks (sp_playlist* playlist, bool link);

    //    // extern const char * sp_playlist_get_description (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_playlist_get_description (sp_playlist* playlist);

    //    // extern _Bool sp_playlist_get_image (sp_playlist *playlist, byte *image);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_get_image (sp_playlist* playlist, byte[] image);

    //    // extern _Bool sp_playlist_has_pending_changes (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_has_pending_changes (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_add_tracks (sp_playlist *playlist, sp_track *const *tracks, int num_tracks, int position, sp_session *session);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_add_tracks (sp_playlist* playlist, sp_track** tracks, int num_tracks, int position, sp_session* session);

    //    // extern sp_error sp_playlist_remove_tracks (sp_playlist *playlist, const int *tracks, int num_tracks);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_remove_tracks (sp_playlist* playlist, int* tracks, int num_tracks);

    //    // extern sp_error sp_playlist_reorder_tracks (sp_playlist *playlist, const int *tracks, int num_tracks, int new_position);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_reorder_tracks (sp_playlist* playlist, int* tracks, int num_tracks, int new_position);

    //    // extern unsigned int sp_playlist_num_subscribers (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe uint sp_playlist_num_subscribers (sp_playlist* playlist);

    //    // extern sp_subscribers * sp_playlist_subscribers (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_subscribers* sp_playlist_subscribers (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_subscribers_free (sp_subscribers *subscribers);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_subscribers_free (sp_subscribers* subscribers);

    //    // extern sp_error sp_playlist_update_subscribers (sp_session *session, sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_update_subscribers (sp_session* session, sp_playlist* playlist);

    //    // extern _Bool sp_playlist_is_in_ram (sp_session *session, sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlist_is_in_ram (sp_session* session, sp_playlist* playlist);

    //    // extern sp_error sp_playlist_set_in_ram (sp_session *session, sp_playlist *playlist, _Bool in_ram);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_set_in_ram (sp_session* session, sp_playlist* playlist, bool in_ram);

    //    // extern sp_playlist * sp_playlist_create (sp_session *session, sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_playlist_create (sp_session* session, sp_link* link);

    //    // extern sp_error sp_playlist_set_offline_mode (sp_session *session, sp_playlist *playlist, _Bool offline);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_set_offline_mode (sp_session* session, sp_playlist* playlist, bool offline);

    //    // extern sp_playlist_offline_status sp_playlist_get_offline_status (sp_session *session, sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist_offline_status sp_playlist_get_offline_status (sp_session* session, sp_playlist* playlist);

    //    // extern int sp_playlist_get_offline_download_completed (sp_session *session, sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlist_get_offline_download_completed (sp_session* session, sp_playlist* playlist);

    //    // extern sp_error sp_playlist_add_ref (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_add_ref (sp_playlist* playlist);

    //    // extern sp_error sp_playlist_release (sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlist_release (sp_playlist* playlist);

    //    // extern sp_error sp_playlistcontainer_add_callbacks (sp_playlistcontainer *pc, sp_playlistcontainer_callbacks *callbacks, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_add_callbacks (sp_playlistcontainer* pc, sp_playlistcontainer_callbacks* callbacks, void* userdata);

    //    // extern sp_error sp_playlistcontainer_remove_callbacks (sp_playlistcontainer *pc, sp_playlistcontainer_callbacks *callbacks, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_remove_callbacks (sp_playlistcontainer* pc, sp_playlistcontainer_callbacks* callbacks, void* userdata);

    //    // extern int sp_playlistcontainer_num_playlists (sp_playlistcontainer *pc);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlistcontainer_num_playlists (sp_playlistcontainer* pc);

    //    // extern _Bool sp_playlistcontainer_is_loaded (sp_playlistcontainer *pc);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_playlistcontainer_is_loaded (sp_playlistcontainer* pc);

    //    // extern sp_playlist * sp_playlistcontainer_playlist (sp_playlistcontainer *pc, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_playlistcontainer_playlist (sp_playlistcontainer* pc, int index);

    //    // extern sp_playlist_type sp_playlistcontainer_playlist_type (sp_playlistcontainer *pc, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist_type sp_playlistcontainer_playlist_type (sp_playlistcontainer* pc, int index);

    //    // extern sp_error sp_playlistcontainer_playlist_folder_name (sp_playlistcontainer *pc, int index, char *buffer, int buffer_size);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_playlist_folder_name (sp_playlistcontainer* pc, int index, sbyte* buffer, int buffer_size);

    //    // extern sp_uint64 sp_playlistcontainer_playlist_folder_id (sp_playlistcontainer *pc, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe ulong sp_playlistcontainer_playlist_folder_id (sp_playlistcontainer* pc, int index);

    //    // extern sp_playlist * sp_playlistcontainer_add_new_playlist (sp_playlistcontainer *pc, const char *name);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_playlistcontainer_add_new_playlist (sp_playlistcontainer* pc, sbyte* name);

    //    // extern sp_playlist * sp_playlistcontainer_add_playlist (sp_playlistcontainer *pc, sp_link *link);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_playlist* sp_playlistcontainer_add_playlist (sp_playlistcontainer* pc, sp_link* link);

    //    // extern sp_error sp_playlistcontainer_remove_playlist (sp_playlistcontainer *pc, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_remove_playlist (sp_playlistcontainer* pc, int index);

    //    // extern sp_error sp_playlistcontainer_move_playlist (sp_playlistcontainer *pc, int index, int new_position, _Bool dry_run);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_move_playlist (sp_playlistcontainer* pc, int index, int new_position, bool dry_run);

    //    // extern sp_error sp_playlistcontainer_add_folder (sp_playlistcontainer *pc, int index, const char *name);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_add_folder (sp_playlistcontainer* pc, int index, sbyte* name);

    //    // extern sp_user * sp_playlistcontainer_owner (sp_playlistcontainer *pc);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_user* sp_playlistcontainer_owner (sp_playlistcontainer* pc);

    //    // extern sp_error sp_playlistcontainer_add_ref (sp_playlistcontainer *pc);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_add_ref (sp_playlistcontainer* pc);

    //    // extern sp_error sp_playlistcontainer_release (sp_playlistcontainer *pc);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_playlistcontainer_release (sp_playlistcontainer* pc);

    //    // extern int sp_playlistcontainer_get_unseen_tracks (sp_playlistcontainer *pc, sp_playlist *playlist, sp_track **tracks, int num_tracks);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlistcontainer_get_unseen_tracks (sp_playlistcontainer* pc, sp_playlist* playlist, sp_track** tracks, int num_tracks);

    //    // extern int sp_playlistcontainer_clear_unseen_tracks (sp_playlistcontainer *pc, sp_playlist *playlist);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_playlistcontainer_clear_unseen_tracks (sp_playlistcontainer* pc, sp_playlist* playlist);

    //    // extern const char * sp_user_canonical_name (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_user_canonical_name (sp_user* user);

    //    // extern const char * sp_user_display_name (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_user_display_name (sp_user* user);

    //    // extern _Bool sp_user_is_loaded (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_user_is_loaded (sp_user* user);

    //    // extern sp_error sp_user_add_ref (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_user_add_ref (sp_user* user);

    //    // extern sp_error sp_user_release (sp_user *user);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_user_release (sp_user* user);

    //    // extern sp_toplistbrowse * sp_toplistbrowse_create (sp_session *session, sp_toplisttype type, sp_toplistregion region, const char *username, toplistbrowse_complete_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_toplistbrowse* sp_toplistbrowse_create (sp_session* session, sp_toplisttype type, sp_toplistregion region, sbyte* username, toplistbrowse_complete_cb* callback, void* userdata);

    //    // extern _Bool sp_toplistbrowse_is_loaded (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe bool sp_toplistbrowse_is_loaded (sp_toplistbrowse* tlb);

    //    // extern sp_error sp_toplistbrowse_error (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_toplistbrowse_error (sp_toplistbrowse* tlb);

    //    // extern sp_error sp_toplistbrowse_add_ref (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_toplistbrowse_add_ref (sp_toplistbrowse* tlb);

    //    // extern sp_error sp_toplistbrowse_release (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_toplistbrowse_release (sp_toplistbrowse* tlb);

    //    // extern int sp_toplistbrowse_num_artists (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_toplistbrowse_num_artists (sp_toplistbrowse* tlb);

    //    // extern sp_artist * sp_toplistbrowse_artist (sp_toplistbrowse *tlb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_artist* sp_toplistbrowse_artist (sp_toplistbrowse* tlb, int index);

    //    // extern int sp_toplistbrowse_num_albums (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_toplistbrowse_num_albums (sp_toplistbrowse* tlb);

    //    // extern sp_album * sp_toplistbrowse_album (sp_toplistbrowse *tlb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_album* sp_toplistbrowse_album (sp_toplistbrowse* tlb, int index);

    //    // extern int sp_toplistbrowse_num_tracks (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_toplistbrowse_num_tracks (sp_toplistbrowse* tlb);

    //    // extern sp_track * sp_toplistbrowse_track (sp_toplistbrowse *tlb, int index);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_track* sp_toplistbrowse_track (sp_toplistbrowse* tlb, int index);

    //    // extern int sp_toplistbrowse_backend_request_duration (sp_toplistbrowse *tlb);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe int sp_toplistbrowse_backend_request_duration (sp_toplistbrowse* tlb);

    //    // extern sp_inbox * sp_inbox_post_tracks (sp_session *session, const char *user, sp_track *const *tracks, int num_tracks, const char *message, inboxpost_complete_cb *callback, void *userdata);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_inbox* sp_inbox_post_tracks (sp_session* session, sbyte* user, sp_track** tracks, int num_tracks, sbyte* message, inboxpost_complete_cb* callback, void* userdata);

    //    // extern sp_error sp_inbox_error (sp_inbox *inbox);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_inbox_error (sp_inbox* inbox);

    //    // extern sp_error sp_inbox_add_ref (sp_inbox *inbox);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_inbox_add_ref (sp_inbox* inbox);

    //    // extern sp_error sp_inbox_release (sp_inbox *inbox);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sp_error sp_inbox_release (sp_inbox* inbox);

    //    // extern const char * sp_build_id ();
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern unsafe sbyte* sp_build_id ();

    //    // extern void SPDispatchSyncIfNeeded (Action block);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern void SPDispatchSyncIfNeeded (Action block);

    //    // extern void SPDispatchAsync (Action block);
    //    [DllImport ("__Internal")]
    //    [Verify (PlatformInvoke)]
    //    static extern void SPDispatchAsync (Action block);
    //}

    public enum sp_connectionstate : uint
    {
        LoggedOut = 0,
        LoggedIn = 1,
        Disconnected = 2,
        Undefined = 3,
        Offline = 4
    }

    public enum sp_sampletype : uint
    {
        SpSampletypeInt16NativeEndian = 0
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct sp_audioformat
    {
        public sp_sampletype sample_type;

        public int sample_rate;

        public int channels;
    }

    public enum sp_bitrate : uint
    {
        SP_BITRATE_160k = 0,
        SP_BITRATE_320k = 1,
        SP_BITRATE_96k = 2
    }

    public enum sp_playlist_type : uint
    {
        Playlist = 0,
        StartFolder = 1,
        EndFolder = 2,
        Placeholder = 3
    }

    public enum sp_search_type : uint
    {
        Tandard = 0,
        Uggest = 1
    }

    public enum sp_playlist_offline_status : uint
    {
        No = 0,
        Yes = 1,
        Downloading = 2,
        Waiting = 3
    }

    public enum sp_track_availability : uint
    {
        Unavailable = 0,
        Available = 1,
        NotStreamable = 2,
        BannedByArtist = 3
    }

    public enum sp_track_offline_status : uint
    {
        No = 0,
        Waiting = 1,
        Downloading = 2,
        Done = 3,
        Error = 4,
        DoneExpired = 5,
        LimitExceeded = 6,
        DoneResync = 7
    }

    public enum sp_image_size : uint
    {
        Normal = 0,
        Small = 1,
        Large = 2
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct sp_audio_buffer_stats
    {
        public int samples;

        public int stutter;
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct sp_subscribers
    {
        public uint count;

        public unsafe sbyte*[] subscribers;
    }

    public enum sp_connection_type : uint
    {
        Unknown = 0,
        None = 1,
        Mobile = 2,
        MobileRoaming = 3,
        Wifi = 4,
        Wired = 5
    }

    public enum sp_connection_rules : uint
    {
        Network = 1,
        NetworkIfRoaming = 2,
        AllowSyncOverMobile = 4,
        AllowSyncOverWifi = 8
    }

    public enum sp_artistbrowse_type : uint
    {
        Full,
        NoTracks,
        NoAlbums
    }

    public enum sp_social_provider : uint
    {
        Spotify,
        Facebook,
        Lastfm
    }

    public enum sp_scrobbling_state : uint
    {
        UseGlobalSetting = 0,
        LocalEnabled = 1,
        LocalDisabled = 2,
        GlobalEnabled = 3,
        GlobalDisabled = 4
    }

    [StructLayout (LayoutKind.Sequential)]
    public struct sp_offline_sync_status
    {
        public int queued_tracks;

        public ulong queued_bytes;

        public int done_tracks;

        public ulong done_bytes;

        public int copied_tracks;

        public ulong copied_bytes;

        public int willnotcopy_tracks;

        public int error_tracks;

        public bool syncing;
    }

    //[StructLayout (LayoutKind.Sequential)]
    //public struct sp_session_callbacks
    //{
    //    public unsafe Action<CocoaSpotify.sp_session*, sp_error>* logged_in;

    //    public unsafe Action<CocoaSpotify.sp_session*>* logged_out;

    //    public unsafe Action<CocoaSpotify.sp_session*>* metadata_updated;

    //    public unsafe Action<CocoaSpotify.sp_session*, sp_error>* connection_error;

    //    public unsafe Action<CocoaSpotify.sp_session*, sbyte*>* message_to_user;

    //    public unsafe Action<CocoaSpotify.sp_session*>* notify_main_thread;

    //    public unsafe Func<CocoaSpotify.sp_session*, CocoaSpotify.sp_audioformat*, void*, int, int>* music_delivery;

    //    public unsafe Action<CocoaSpotify.sp_session*>* play_token_lost;

    //    public unsafe Action<CocoaSpotify.sp_session*, sbyte*>* log_message;

    //    public unsafe Action<CocoaSpotify.sp_session*>* end_of_track;

    //    public unsafe Action<CocoaSpotify.sp_session*, sp_error>* streaming_error;

    //    public unsafe Action<CocoaSpotify.sp_session*>* userinfo_updated;

    //    public unsafe Action<CocoaSpotify.sp_session*>* start_playback;

    //    public unsafe Action<CocoaSpotify.sp_session*>* stop_playback;

    //    public unsafe Action<CocoaSpotify.sp_session*, CocoaSpotify.sp_audio_buffer_stats*>* get_audio_buffer_stats;

    //    public unsafe Action<CocoaSpotify.sp_session*>* offline_status_updated;

    //    public unsafe Action<CocoaSpotify.sp_session*, sp_error>* offline_error;

    //    public unsafe Action<CocoaSpotify.sp_session*, sbyte*>* credentials_blob_updated;

    //    public unsafe Action<CocoaSpotify.sp_session*>* connectionstate_updated;

    //    public unsafe Action<CocoaSpotify.sp_session*, sp_error>* scrobble_error;

    //    public unsafe Action<CocoaSpotify.sp_session*, bool>* private_session_mode_changed;
    //}

    [StructLayout (LayoutKind.Sequential)]
    public struct sp_session_config
    {
        public int api_version;

        public unsafe sbyte* cache_location;

        public unsafe sbyte* settings_location;

        public unsafe void* application_key;

        public nuint application_key_size;

        public unsafe sbyte* user_agent;

        //public unsafe sp_session_callbacks* callbacks;

        public unsafe void* userdata;

        public bool compress_playlists;

        public bool dont_save_metadata_for_playlists;

        public bool initially_unload_playlists;

        public unsafe sbyte* device_id;

        public unsafe sbyte* proxy;

        public unsafe sbyte* proxy_username;

        public unsafe sbyte* proxy_password;

        public unsafe sbyte* tracefile;
    }


[StructLayout(LayoutKind.Sequential)]
public struct sp_session
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_track
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_album
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_artist
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_artistbrowse
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_albumbrowse
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_toplistbrowse
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_search
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_link
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_image
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_user
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_playlist
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_playlistcontainer
{
    
}

[StructLayout(LayoutKind.Sequential)]
public struct sp_inbox
{
    
}


    public enum sp_linktype : uint
    {
        Invalid = 0,
        Track = 1,
        Album = 2,
        Artist = 3,
        Search = 4,
        Playlist = 5,
        Profile = 6,
        Starred = 7,
        Localtrack = 8,
        Image = 9
    }

    public enum sp_albumtype : uint
    {
        Album = 0,
        Single = 1,
        Compilation = 2,
        Unknown = 3
    }

    public enum sp_imageformat
    {
        Unknown = -1,
        Jpeg = 0
    }

    //[StructLayout (LayoutKind.Sequential)]
    //public struct sp_playlist_callbacks
    //{
    //    public unsafe Action<CocoaSpotify.sp_playlist*, CocoaSpotify.sp_track**, int, int, void*>* tracks_added;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, int*, int, void*>* tracks_removed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, int*, int, int, void*>* tracks_moved;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, void*>* playlist_renamed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, void*>* playlist_state_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, bool, void*>* playlist_update_in_progress;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, void*>* playlist_metadata_updated;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, int, CocoaSpotify.sp_user*, int, void*>* track_created_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, int, bool, void*>* track_seen_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, sbyte*, void*>* description_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, byte*, void*>* image_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, int, sbyte*, void*>* track_message_changed;

    //    public unsafe Action<CocoaSpotify.sp_playlist*, void*>* subscribers_changed;
    //}

    //[StructLayout (LayoutKind.Sequential)]
    //public struct sp_playlistcontainer_callbacks
    //{
    //    public unsafe Action<CocoaSpotify.sp_playlistcontainer*, CocoaSpotify.sp_playlist*, int, void*>* playlist_added;

    //    public unsafe Action<CocoaSpotify.sp_playlistcontainer*, CocoaSpotify.sp_playlist*, int, void*>* playlist_removed;

    //    public unsafe Action<CocoaSpotify.sp_playlistcontainer*, CocoaSpotify.sp_playlist*, int, int, void*>* playlist_moved;

    //    public unsafe Action<CocoaSpotify.sp_playlistcontainer*, void*>* container_loaded;
    //}

    public enum sp_relation_type : uint
    {
        Unknown = 0,
        None = 1,
        Unidirectional = 2,
        Bidirectional = 3
    }

    public enum sp_toplisttype : uint
    {
        Artists = 0,
        Albums = 1,
        Tracks = 2
    }

    public enum sp_toplistregion : uint
    {
        Everywhere = 0,
        User = 1
    }

    public enum SPAsyncLoadingPolicy : uint
    {
        Immediate = 0,
        Manual
    }
}
