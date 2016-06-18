using System;
using AppKit;
using Foundation;
using ObjCRuntime;

namespace CocoaSpotify
{
    // typedef void (^SPErrorableOperationCallback)(NSError *);
    delegate void SPErrorableOperationCallback(NSError arg0);

    interface ISPPlaylistableItem
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPPlaylistableItemTool
    {
        [Export("getProtocol")]
        ISPPlaylistableItem GetProtocol();
    }

    // @protocol SPPlaylistableItem <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPPlaylistableItem
    {
        // @required -(NSString *)name;
        [Abstract]
        [Export("name")]
        string Name { get; }

        // @required -(NSURL *)spotifyURL;
        [Abstract]
        [Export("spotifyURL")]
        NSUrl SpotifyURL { get; }
    }

    interface ISPSessionPlaybackProvider
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPSessionPlaybackProviderTool
    {
        [Export("getProtocol")]
        ISPSessionPlaybackProvider GetProtocol();
    }

    // @protocol SPSessionPlaybackProvider <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPSessionPlaybackProvider
    {
        // @required @property (getter = isPlaying, readwrite, nonatomic) BOOL playing;
        [Abstract]
        [Export("playing")]
        bool Playing { [Bind("isPlaying")] get; set; }

        //[Wrap("WeakPlaybackDelegate"), Abstract]
        //SPSessionPlaybackDelegate PlaybackDelegate { get; set; }

        //// @required @property (assign, readwrite, nonatomic) id<SPSessionPlaybackDelegate> playbackDelegate;
        //[Abstract]
        //[NullAllowed, Export("playbackDelegate", ArgumentSemantic.Assign)]
        //NSObject WeakPlaybackDelegate { get; set; }

        //[Wrap("WeakAudioDeliveryDelegate"), Abstract]
        //SPSessionAudioDeliveryDelegate AudioDeliveryDelegate { get; set; }

        // @required @property (assign, readwrite, nonatomic) id<SPSessionAudioDeliveryDelegate> audioDeliveryDelegate;
        //[Abstract]
        //[NullAllowed, Export("audioDeliveryDelegate", ArgumentSemantic.Assign)]
        //NSObject WeakAudioDeliveryDelegate { get; set; }

        // @required -(void)preloadTrackForPlayback:(SPTrack *)aTrack callback:(SPErrorableOperationCallback)block;
        [Abstract]
        [Export("preloadTrackForPlayback:callback:")]
        void PreloadTrackForPlayback(SPTrack aTrack, SPErrorableOperationCallback block);

        // @required -(void)playTrack:(SPTrack *)aTrack callback:(SPErrorableOperationCallback)block;
        [Abstract]
        [Export("playTrack:callback:")]
        void PlayTrack(SPTrack aTrack, SPErrorableOperationCallback block);

        // @required -(void)seekPlaybackToOffset:(NSTimeInterval)offset;
        [Abstract]
        [Export("seekPlaybackToOffset:")]
        void SeekPlaybackToOffset(double offset);

        // @required -(void)unloadPlayback;
        [Abstract]
        [Export("unloadPlayback")]
        void UnloadPlayback();
    }

    interface ISPAsyncLoading
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPAsyncLoadingTool
    {
        [Export("getProtocol")]
        ISPAsyncLoading GetProtocol();
    }

    delegate void OnWaitCompleted(NSArray items, NSArray others);

    // @protocol SPAsyncLoading <NSObject>
    [Protocol]
    [BaseType(typeof(NSObject))]
    interface SPAsyncLoading
    {
        // @required @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Abstract]
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // +(void)waitUntilLoaded:(id)itemOrItems timeout:(NSTimeInterval)timeout then:(void (^)(NSArray *, NSArray *))block;
        [Static]
        [Export("waitUntilLoaded:timeout:then:")]
        [Async(ResultTypeName = "SPAsyncResult")]
        void WaitUntilLoaded(NSObject itemOrItems, double timeout, OnWaitCompleted onCompleted);
    }

    interface ISPDelayableAsyncLoading : ISPAsyncLoading
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPDelayableAsyncLoadingTool
    {
        [Export("getProtocol")]
        ISPDelayableAsyncLoading GetProtocol();
    }

    // @protocol SPDelayableAsyncLoading <SPAsyncLoading, NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPDelayableAsyncLoading : SPAsyncLoading
    {
        // @required -(void)startLoading;
        [Abstract]
        [Export("startLoading")]
        void StartLoading();
    }

    // @interface SPErrorExtensions (NSError)
    [Category]
    [BaseType(typeof(NSError))]
    interface NSError_SPErrorExtensions
    {
        // +(NSError *)spotifyErrorWithDescription:(NSString *)msg code:(NSInteger)code;
        [Static]
        [Export("spotifyErrorWithDescription:code:")]
        NSError SpotifyErrorWithDescription(string msg, nint code);

        // +(NSError *)spotifyErrorWithCode:(sp_error)code;
        [Static]
        [Export("spotifyErrorWithCode:")]
        NSError SpotifyErrorWithCode(sp_error code);

        // +(NSError *)spotifyErrorWithDescription:(NSString *)msg;
        [Static]
        [Export("spotifyErrorWithDescription:")]
        NSError SpotifyErrorWithDescription(string msg);

        // +(NSError *)spotifyErrorWithCode:(NSInteger)code format:(NSString *)format, ...;
        [Static, Internal]
        [Export("spotifyErrorWithCode:format:", IsVariadic = true)]
        NSError SpotifyErrorWithCode(nint code, string format, IntPtr varArgs);

        // +(NSError *)spotifyErrorWithFormat:(NSString *)format, ...;
        [Static, Internal]
        [Export("spotifyErrorWithFormat:", IsVariadic = true)]
        NSError SpotifyErrorWithFormat(string format, IntPtr varArgs);
    }

    // @interface SPURLExtensions (NSURL)
    [Category]
    [BaseType(typeof(NSUrl))]
    interface NSURL_SPURLExtensions
    {
        // +(NSURL *)urlWithSpotifyLink:(sp_link *)link;
        [Static]
        [Export("urlWithSpotifyLink:")]
        unsafe NSUrl UrlWithSpotifyLink(sp_link link);

        // -(sp_link *)createSpotifyLink;
        [Export("createSpotifyLink")]
        unsafe sp_link CreateSpotifyLink();

        // -(sp_linktype)spotifyLinkType;
        [Export("spotifyLinkType")]
        sp_linktype SpotifyLinkType();

        // +(NSString *)urlDecodedStringForString:(NSString *)encodedString;
        [Static]
        [Export("urlDecodedStringForString:")]
        string UrlDecodedStringForString(string encodedString);

        // +(NSString *)urlEncodedStringForString:(NSString *)plainOldString;
        [Static]
        [Export("urlEncodedStringForString:")]
        string UrlEncodedStringForString(string plainOldString);
    }

    // @interface SPAlbum : NSObject <SPPlaylistableItem, SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPAlbum : ISPPlaylistableItem, ISPAsyncLoading
    {
        // +(SPAlbum *)albumWithAlbumStruct:(sp_album *)anAlbum inSession:(SPSession *)aSession;
        [Static]
        [Export("albumWithAlbumStruct:inSession:")]
        unsafe SPAlbum AlbumWithAlbumStruct(Action anAlbum, SPSession aSession);

        // +(void)albumWithAlbumURL:(NSURL *)aURL inSession:(SPSession *)aSession callback:(void (^)(SPAlbum *))block;
        [Static]
        [Export("albumWithAlbumURL:inSession:callback:")]
        void AlbumWithAlbumURL(NSUrl aURL, SPSession aSession, Action<SPAlbum> block);

        // -(id)initWithAlbumStruct:(sp_album *)anAlbum inSession:(SPSession *)aSession;
        [Export("initWithAlbumStruct:inSession:")]
        unsafe IntPtr Constructor(sp_album anAlbum, SPSession aSession);

        // @property (readonly, nonatomic) sp_album * album;
        [Export("album")]
        unsafe sp_album Album { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, nonatomic, strong) SPArtist * artist;
        [Export("artist", ArgumentSemantic.Strong)]
        SPArtist Artist { get; }

        // @property (readonly, nonatomic, strong) SPImage * cover;
        [Export("cover", ArgumentSemantic.Strong)]
        SPImage Cover { get; }

        // @property (readonly, nonatomic, strong) SPImage * smallCover;
        [Export("smallCover", ArgumentSemantic.Strong)]
        SPImage SmallCover { get; }

        // @property (readonly, nonatomic, strong) SPImage * largeCover;
        [Export("largeCover", ArgumentSemantic.Strong)]
        SPImage LargeCover { get; }

        // @property (readonly, nonatomic, strong) SPImage * largestAvailableCover;
        [Export("largestAvailableCover", ArgumentSemantic.Strong)]
        SPImage LargestAvailableCover { get; }

        // @property (readonly, nonatomic, strong) SPImage * smallestAvailableCover;
        [Export("smallestAvailableCover", ArgumentSemantic.Strong)]
        SPImage SmallestAvailableCover { get; }

        // @property (readonly, getter = isAvailable, nonatomic) BOOL available;
        [Export("available")]
        bool Available { [Bind("isAvailable")] get; }

        // @property (readonly, copy, nonatomic) NSString * name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, nonatomic) sp_albumtype type;
        [Export("type")]
        sp_albumtype Type { get; }

        // @property (readonly, nonatomic) NSUInteger year;
        [Export("year")]
        nuint Year { get; }
    }

    // @interface SPArtist : NSObject <SPPlaylistableItem, SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPArtist : ISPPlaylistableItem, ISPAsyncLoading
    {
        // +(SPArtist *)artistWithArtistStruct:(sp_artist *)anArtist inSession:(SPSession *)aSession;
        [Static]
        [Export("artistWithArtistStruct:inSession:")]
        unsafe SPArtist ArtistWithArtistStruct(sp_artist anArtist, SPSession aSession);

        // +(void)artistWithArtistURL:(NSURL *)aURL inSession:(SPSession *)aSession callback:(void (^)(SPArtist *))block;
        [Static]
        [Export("artistWithArtistURL:inSession:callback:")]
        void ArtistWithArtistURL(NSUrl aURL, SPSession aSession, Action<SPArtist> block);

        // -(id)initWithArtistStruct:(sp_artist *)anArtist inSession:(SPSession *)aSession;
        [Export("initWithArtistStruct:inSession:")]
        unsafe IntPtr Constructor(sp_artist anArtist, SPSession aSession);

        // @property (readonly, nonatomic) sp_artist * artist;
        [Export("artist")]
        unsafe sp_artist Artist { get; }

        // @property (readonly, copy, nonatomic) NSString * name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }
    }

    // @interface SPImage : NSObject <SPAsyncLoading, SPDelayableAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPImage : ISPAsyncLoading, ISPDelayableAsyncLoading
    {
        // +(SPImage *)imageWithImageId:(const byte *)imageId inSession:(SPSession *)aSession;
        //[Static]
        //[Export("imageWithImageId:inSession:")]
        //unsafe SPImage ImageWithImageId(byte* imageId, SPSession aSession);

        // +(void)imageWithImageURL:(NSURL *)imageURL inSession:(SPSession *)aSession callback:(void (^)(SPImage *))block;
        [Static]
        [Export("imageWithImageURL:inSession:callback:")]
        void ImageWithImageURL(NSUrl imageURL, SPSession aSession, Action<SPImage> block);

        // -(id)initWithImageStruct:(sp_image *)anImage imageId:(const byte *)anId inSession:(SPSession *)aSession;
        //[Export("initWithImageStruct:imageId:inSession:")]
        //unsafe IntPtr Constructor(sp_image anImage, byte* anId, SPSession aSession);

        // -(void)startLoading;
        [Export("startLoading")]
        void StartLoading();

        // @property (readonly, nonatomic, strong) NSImage * image;
        [Export("image", ArgumentSemantic.Strong)]
        NSImage Image { get; }

        // @property (readonly, nonatomic) const byte * imageId;
        //[Export("imageId")]
        //unsafe byte[] ImageId { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, assign, nonatomic) SPSession * session;
        [Export("session", ArgumentSemantic.Assign)]
        SPSession Session { get; }

        // @property (readonly, nonatomic) sp_image * spImage;
        [Export("spImage")]
        unsafe sp_image SpImage { get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }
    }

    // @interface SPUser : NSObject <SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPUser : ISPAsyncLoading
    {
        // +(SPUser *)userWithUserStruct:(sp_user *)spUser inSession:(SPSession *)aSession;
        [Static]
        [Export("userWithUserStruct:inSession:")]
        unsafe SPUser UserWithUserStruct(sp_user spUser, SPSession aSession);

        // +(void)userWithURL:(NSURL *)userUrl inSession:(SPSession *)aSession callback:(void (^)(SPUser *))block;
        [Static]
        [Export("userWithURL:inSession:callback:")]
        void UserWithURL(NSUrl userUrl, SPSession aSession, Action<SPUser> block);

        // -(id)initWithUserStruct:(sp_user *)aUser inSession:(SPSession *)aSession;
        [Export("initWithUserStruct:inSession:")]
        unsafe IntPtr Constructor(sp_user aUser, SPSession aSession);

        // @property (readonly, copy, nonatomic) NSString * canonicalName;
        [Export("canonicalName")]
        string CanonicalName { get; }

        // @property (readonly, copy, nonatomic) NSString * displayName;
        [Export("displayName")]
        string DisplayName { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, nonatomic) sp_user * user;
        [Export("user")]
        unsafe sp_user User { get; }
    }

    // @interface SPSession : NSObject <SPSessionPlaybackProvider, SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPSession : ISPSessionPlaybackProvider, ISPAsyncLoading
    {
        // +(void)dispatchToLibSpotifyThread:(dispatch_block_t)block;
        [Static]
        [Export("dispatchToLibSpotifyThread:")]
        void DispatchToLibSpotifyThread(Action block);

        // +(void)dispatchToLibSpotifyThread:(dispatch_block_t)block waitUntilDone:(BOOL)wait;
        [Static]
        [Export("dispatchToLibSpotifyThread:waitUntilDone:")]
        void DispatchToLibSpotifyThread(Action block, bool wait);

        // +(CFRunLoopRef)libSpotifyRunloop;
        //[Static]
        //[Export("libSpotifyRunloop")]
        //[Verify(MethodToProperty)]
        //unsafe CFRunLoopRef* LibSpotifyRunloop { get; }

        // +(BOOL)spotifyClientInstalled;
        [Static]
        [Export("spotifyClientInstalled")]
        bool SpotifyClientInstalled { get; }

        // +(BOOL)launchSpotifyClientIfInstalled;
        [Static]
        [Export("launchSpotifyClientIfInstalled")]
        bool LaunchSpotifyClientIfInstalled();

        // +(SPSession *)sharedSession;
        [Static]
        [Export("sharedSession")]
        SPSession SharedSession { get; }

        // +(BOOL)initializeSharedSessionWithApplicationKey:(NSData *)appKey userAgent:(NSString *)userAgent loadingPolicy:(SPAsyncLoadingPolicy)policy error:(NSError **)error;
        [Static]
        [Export("initializeSharedSessionWithApplicationKey:userAgent:loadingPolicy:error:")]
        bool InitializeSharedSessionWithApplicationKey(NSData appKey, string userAgent, SPAsyncLoadingPolicy policy, out NSError error);

        // +(NSString *)libSpotifyBuildId;
        [Static]
        [Export("libSpotifyBuildId")]
        string LibSpotifyBuildId { get; }

        // -(id)initWithApplicationKey:(NSData *)appKey userAgent:(NSString *)userAgent loadingPolicy:(SPAsyncLoadingPolicy)policy error:(NSError **)error;
        [Export("initWithApplicationKey:userAgent:loadingPolicy:error:")]
        IntPtr Constructor(NSData appKey, string userAgent, SPAsyncLoadingPolicy policy, out NSError error);

        // -(void)attemptLoginWithUserName:(NSString *)userName password:(NSString *)password;
        [Export("attemptLoginWithUserName:password:")]
        void AttemptLogin(string userName, string password);

        // -(void)attemptLoginWithUserName:(NSString *)userName existingCredential:(NSString *)credential;
        [Export("attemptLoginWithUserName:existingCredential:")]
        void AttemptLoginWithCredential(string userName, string credential);

        // -(void)fetchLoginUserName:(void (^)(NSString *))block;
        [Export("fetchLoginUserName:")]
        void FetchLoginUserName(Action<NSString> block);

        // -(void)flushCaches:(void (^)())completionBlock;
        [Export("flushCaches:")]
        void FlushCaches(Action completionBlock);

        // -(void)logout:(void (^)())completionBlock;
        [Export("logout:")]
        void Logout(Action completionBlock);

        // @property (readonly, nonatomic) sp_connectionstate connectionState;
        [Export("connectionState")]
        sp_connectionstate ConnectionState { get; }

        // -(void)setMaximumCacheSizeMB:(size_t)maximumCacheSizeMB;
        [Export("setMaximumCacheSizeMB:")]
        void SetMaximumCacheSizeMB(nuint maximumCacheSizeMB);

        // -(void)setPreferredBitrate:(sp_bitrate)bitrate;
        [Export("setPreferredBitrate:")]
        void SetPreferredBitrate(sp_bitrate bitrate);

        [Wrap("WeakDelegate")]
        SPSessionDelegate Delegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPSessionDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic) sp_session * session;
        [Export("session")]
        unsafe sp_session Session { get; }

        // @property (readonly, copy, nonatomic) NSString * userAgent;
        [Export("userAgent")]
        string UserAgent { get; }

        // @property (readonly, nonatomic) SPAsyncLoadingPolicy loadingPolicy;
        [Export("loadingPolicy")]
        SPAsyncLoadingPolicy LoadingPolicy { get; }

        // @property (getter = isPrivateSession, readwrite, nonatomic) BOOL privateSession;
        [Export("privateSession")]
        bool PrivateSession { [Bind("isPrivateSession")] get; set; }

        // -(void)setScrobblingState:(sp_scrobbling_state)state forService:(sp_social_provider)service callback:(SPErrorableOperationCallback)block;
        [Export("setScrobblingState:forService:callback:")]
        void SetScrobblingState(sp_scrobbling_state state, sp_social_provider service, SPErrorableOperationCallback block);

        // -(void)setScrobblingUserName:(NSString *)userName password:(NSString *)password forService:(sp_social_provider)service callback:(SPErrorableOperationCallback)block;
        [Export("setScrobblingUserName:password:forService:callback:")]
        void SetScrobblingUserName(string userName, string password, sp_social_provider service, SPErrorableOperationCallback block);

        // -(void)fetchScrobblingStateForService:(sp_social_provider)service callback:(void (^)(sp_scrobbling_state, NSError *))block;
        [Export("fetchScrobblingStateForService:callback:")]
        void FetchScrobblingStateForService(sp_social_provider service, Action<sp_scrobbling_state, NSError> block);

        // -(void)fetchScrobblingAllowedForService:(sp_social_provider)service callback:(void (^)(BOOL, NSError *))block;
        [Export("fetchScrobblingAllowedForService:callback:")]
        void FetchScrobblingAllowedForService(sp_social_provider service, Action<bool, NSError> block);

        // @property (readonly, getter = isOfflineSyncing, nonatomic) BOOL offlineSyncing;
        [Export("offlineSyncing")]
        bool OfflineSyncing { [Bind("isOfflineSyncing")] get; }

        // @property (readonly, nonatomic) NSUInteger offlineTracksRemaining;
        [Export("offlineTracksRemaining")]
        nuint OfflineTracksRemaining { get; }

        // @property (readonly, nonatomic) NSUInteger offlinePlaylistsRemaining;
        [Export("offlinePlaylistsRemaining")]
        nuint OfflinePlaylistsRemaining { get; }

        // @property (readonly, copy, nonatomic) NSDictionary * offlineStatistics;
        [Export("offlineStatistics", ArgumentSemantic.Copy)]
        NSDictionary OfflineStatistics { get; }

        // -(void)fetchOfflineKeyTimeRemaining:(void (^)(NSTimeInterval))block;
        [Export("fetchOfflineKeyTimeRemaining:")]
        void FetchOfflineKeyTimeRemaining(Action<double> block);

        // @property (readonly, nonatomic, strong) NSError * offlineSyncError;
        [Export("offlineSyncError", ArgumentSemantic.Strong)]
        NSError OfflineSyncError { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, nonatomic, strong) SPPlaylist * inboxPlaylist;
        [Export("inboxPlaylist", ArgumentSemantic.Strong)]
        SPPlaylist InboxPlaylist { get; }

        // @property (readonly, nonatomic, strong) NSLocale * locale;
        [Export("locale", ArgumentSemantic.Strong)]
        NSLocale Locale { get; }

        // @property (readonly, nonatomic, strong) SPPlaylist * starredPlaylist;
        [Export("starredPlaylist", ArgumentSemantic.Strong)]
        SPPlaylist StarredPlaylist { get; }

        // @property (readonly, nonatomic, strong) SPUser * user;
        [Export("user", ArgumentSemantic.Strong)]
        SPUser User { get; }

        // @property (readonly, nonatomic, strong) SPPlaylistContainer * userPlaylists;
        [Export("userPlaylists", ArgumentSemantic.Strong)]
        SPPlaylistContainer UserPlaylists { get; }

        // -(SPPostTracksToInboxOperation *)postTracks:(NSArray *)tracks toInboxOfUser:(NSString *)targetUserName withMessage:(NSString *)aFriendlyMessage callback:(SPErrorableOperationCallback)block;
        [Export("postTracks:toInboxOfUser:withMessage:callback:")]
        SPPostTracksToInboxOperation PostTracks(SPTrack[] tracks, string targetUserName, string aFriendlyMessage, SPErrorableOperationCallback block);

        // -(void)albumForURL:(NSURL *)url callback:(void (^)(SPAlbum *))block;
        [Export("albumForURL:callback:")]
        void AlbumForURL(NSUrl url, Action<SPAlbum> block);

        // -(void)artistForURL:(NSURL *)url callback:(void (^)(SPArtist *))block;
        [Export("artistForURL:callback:")]
        void ArtistForURL(NSUrl url, Action<SPArtist> block);

        // -(void)imageForURL:(NSURL *)url callback:(void (^)(SPImage *))block;
        [Export("imageForURL:callback:")]
        void ImageForURL(NSUrl url, Action<SPImage> block);

        // -(void)playlistForURL:(NSURL *)url callback:(void (^)(SPPlaylist *))block;
        [Export("playlistForURL:callback:")]
        void PlaylistForURL(NSUrl url, Action<SPPlaylist> block);

        // -(void)searchForURL:(NSURL *)url callback:(void (^)(SPSearch *))block;
        [Export("searchForURL:callback:")]
        void SearchForURL(NSUrl url, Action<SPSearch> block);

        // -(void)trackForURL:(NSURL *)url callback:(void (^)(SPTrack *))block;
        [Export("trackForURL:callback:")]
        void TrackForURL(NSUrl url, Action<SPTrack> block);

        // -(void)userForURL:(NSURL *)url callback:(void (^)(SPUser *))block;
        [Export("userForURL:callback:")]
        void UserForURL(NSUrl url, Action<SPUser> block);

        // -(id)objectRepresentationForSpotifyURL:(NSURL *)aSpotifyUrlOfSomeKind linkType:(sp_linktype *)linkType;
        [Export("objectRepresentationForSpotifyURL:linkType:")]
        unsafe NSObject ObjectRepresentationForSpotifyURL(NSUrl aSpotifyUrlOfSomeKind, sp_linktype linkType);

        // -(void)objectRepresentationForSpotifyURL:(NSURL *)aSpotifyUrlOfSomeKind callback:(void (^)(sp_linktype, id))block;
        [Export("objectRepresentationForSpotifyURL:callback:")]
        void ObjectRepresentationForSpotifyURL(NSUrl aSpotifyUrlOfSomeKind, Action<sp_linktype, NSObject> block);

        // -(SPPlaylist *)playlistForPlaylistStruct:(sp_playlist *)playlist;
        [Export("playlistForPlaylistStruct:")]
        unsafe SPPlaylist PlaylistForPlaylistStruct(sp_playlist playlist);

        // -(SPPlaylistFolder *)playlistFolderForFolderId:(sp_uint64)playlistId inContainer:(SPPlaylistContainer *)aContainer;
        [Export("playlistFolderForFolderId:inContainer:")]
        SPPlaylistFolder PlaylistFolderForFolderId(ulong playlistId, SPPlaylistContainer aContainer);

        // -(SPUnknownPlaylist *)unknownPlaylistForPlaylistStruct:(sp_playlist *)playlist;
        [Export("unknownPlaylistForPlaylistStruct:")]
        unsafe SPUnknownPlaylist UnknownPlaylistForPlaylistStruct(sp_playlist playlist);

        // -(SPTrack *)trackForTrackStruct:(sp_track *)track;
        [Export("trackForTrackStruct:")]
        unsafe SPTrack TrackForTrackStruct(sp_track track);

        // -(SPUser *)userForUserStruct:(sp_user *)user;
        [Export("userForUserStruct:")]
        unsafe SPUser UserForUserStruct(sp_user user);

        // @property (getter = isUsingVolumeNormalization, readwrite, nonatomic) BOOL usingVolumeNormalization;
        [Export("usingVolumeNormalization")]
        bool UsingVolumeNormalization { [Bind("isUsingVolumeNormalization")] get; set; }

        // @property (getter = isPlaying, readwrite, nonatomic) BOOL playing;
        [Export("playing")]
        bool Playing { [Bind("isPlaying")] get; set; }

        [Wrap("WeakPlaybackDelegate")]
        SPSessionPlaybackDelegate PlaybackDelegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPSessionPlaybackDelegate> playbackDelegate;
        [NullAllowed, Export("playbackDelegate", ArgumentSemantic.Assign)]
        NSObject WeakPlaybackDelegate { get; set; }

        [Wrap("WeakAudioDeliveryDelegate")]
        SPSessionAudioDeliveryDelegate AudioDeliveryDelegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPSessionAudioDeliveryDelegate> audioDeliveryDelegate;
        [NullAllowed, Export("audioDeliveryDelegate", ArgumentSemantic.Assign)]
        NSObject WeakAudioDeliveryDelegate { get; set; }

        // -(void)preloadTrackForPlayback:(SPTrack *)aTrack callback:(SPErrorableOperationCallback)block;
        [Export("preloadTrackForPlayback:callback:")]
        void PreloadTrackForPlayback(SPTrack aTrack, SPErrorableOperationCallback block);

        // -(void)playTrack:(SPTrack *)aTrack callback:(SPErrorableOperationCallback)block;
        [Export("playTrack:callback:")]
        void PlayTrack(SPTrack aTrack, SPErrorableOperationCallback block);

        // -(void)seekPlaybackToOffset:(NSTimeInterval)offset;
        [Export("seekPlaybackToOffset:")]
        void SeekPlaybackToOffset(double offset);

        // -(void)unloadPlayback;
        [Export("unloadPlayback")]
        void UnloadPlayback();
    }

    interface ISPSessionDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPSessionDelegateTool
    {
        [Export("getProtocol")]
        ISPSessionDelegate GetProtocol();
    }

    // @protocol SPSessionDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPSessionDelegate
    {
        // @optional -(void)sessionDidLoginSuccessfully:(SPSession *)aSession;
        [Export("sessionDidLoginSuccessfully:")]
        void SessionDidLoginSuccessfully(SPSession aSession);

        // @optional -(void)session:(SPSession *)aSession didGenerateLoginCredentials:(NSString *)credential forUserName:(NSString *)userName;
        [Export("session:didGenerateLoginCredentials:forUserName:")]
        void Session(SPSession aSession, string credential, string userName);

        // @optional -(void)session:(SPSession *)aSession didFailToLoginWithError:(NSError *)error;
        [Export("session:didFailToLoginWithError:")]
        void Session(SPSession aSession, NSError error);

        // @optional -(void)sessionDidLogOut:(SPSession *)aSession;
        [Export("sessionDidLogOut:")]
        void SessionDidLogOut(SPSession aSession);

        // @optional -(void)session:(SPSession *)aSession recievedMessageForUser:(NSString *)aMessage;
        [Export("session:recievedMessageForUser:")]
        void Session(SPSession aSession, string aMessage);

        // @optional -(void)sessionDidChangeMetadata:(SPSession *)aSession;
        [Export("sessionDidChangeMetadata:")]
        void SessionDidChangeMetadata(SPSession aSession);

        // @optional -(void)session:(SPSession *)aSession didEncounterNetworkError:(NSError *)error;
        [Export("session:didEncounterNetworkError:")]
        void SessionDidEncounterNetworkError(SPSession aSession, NSError error);

        // @optional -(void)session:(SPSession *)aSession didEncounterScrobblingError:(NSError *)error;
        [Export("session:didEncounterScrobblingError:")]
        void SessionDidEncounterScrobblingError(SPSession aSession, NSError error);

        // @optional -(void)session:(SPSession *)aSession didLogMessage:(NSString *)aMessage;
        [Export("session:didLogMessage:")]
        void SessionDidLogMessage(SPSession aSession, string aMessage);
    }

    interface ISPSessionPlaybackDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPSessionPlaybackDelegateTool
    {
        [Export("getProtocol")]
        ISPSessionPlaybackDelegate GetProtocol();
    }

    // @protocol SPSessionPlaybackDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPSessionPlaybackDelegate
    {
        // @optional -(void)sessionDidLosePlayToken:(id<SPSessionPlaybackProvider>)aSession;
        [Export("sessionDidLosePlayToken:")]
        void SessionDidLosePlayToken(SPSessionPlaybackProvider aSession);

        // @optional -(void)sessionDidEndPlayback:(id<SPSessionPlaybackProvider>)aSession;
        [Export("sessionDidEndPlayback:")]
        void SessionDidEndPlayback(SPSessionPlaybackProvider aSession);

        // @optional -(void)session:(id<SPSessionPlaybackProvider>)aSession didEncounterStreamingError:(NSError *)error;
        [Export("session:didEncounterStreamingError:")]
        void Session(SPSessionPlaybackProvider aSession, NSError error);

        // @optional -(NSInteger)session:(id<SPSessionPlaybackProvider>)aSession shouldDeliverAudioFrames:(const void *)audioFrames ofCount:(NSInteger)frameCount format:(const sp_audioformat *)audioFormat;
        //[Export("session:shouldDeliverAudioFrames:ofCount:format:")]
        //unsafe nint Session(SPSessionPlaybackProvider aSession, byte[] audioFrames, nint frameCount, sp_audioformat audioFormat);
    }

    interface ISPSessionAudioDeliveryDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPSessionAudioDeliveryDelegateTool
    {
        [Export("getProtocol")]
        ISPSessionAudioDeliveryDelegate GetProtocol();
    }

    // @protocol SPSessionAudioDeliveryDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPSessionAudioDeliveryDelegate
    {
        // @required -(NSInteger)session:(id<SPSessionPlaybackProvider>)aSession shouldDeliverAudioFrames:(const void *)audioFrames ofCount:(NSInteger)frameCount streamDescription:(AudioStreamBasicDescription)audioDescription;
        //[Abstract]
        //[Export("session:shouldDeliverAudioFrames:ofCount:streamDescription:")]
        //unsafe nint ShouldDeliverAudioFrames(SPSessionPlaybackProvider aSession, void* audioFrames, nint frameCount, AudioStreamBasicDescription audioDescription);
    }

    // @interface SPPlaylist : NSObject <SPPlaylistableItem, SPAsyncLoading, SPDelayableAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPPlaylist : ISPPlaylistableItem, ISPAsyncLoading, ISPDelayableAsyncLoading
    {
        // +(SPPlaylist *)playlistWithPlaylistStruct:(sp_playlist *)pl inSession:(SPSession *)aSession;
        [Static]
        [Export("playlistWithPlaylistStruct:inSession:")]
        unsafe SPPlaylist PlaylistWithPlaylistStruct(sp_playlist pl, SPSession aSession);

        // +(void)playlistWithPlaylistURL:(NSURL *)playlistURL inSession:(SPSession *)aSession callback:(void (^)(SPPlaylist *))block;
        [Static]
        [Export("playlistWithPlaylistURL:inSession:callback:")]
        void PlaylistWithPlaylistURL(NSUrl playlistURL, SPSession aSession, Action<SPPlaylist> block);

        // -(id)initWithPlaylistStruct:(sp_playlist *)pl inSession:(SPSession *)aSession;
        [Export("initWithPlaylistStruct:inSession:")]
        unsafe IntPtr Constructor(sp_playlist pl, SPSession aSession);

        [Wrap("WeakDelegate")]
        SPPlaylistDelegate Delegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPPlaylistDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic) BOOL hasPendingChanges;
        [Export("hasPendingChanges")]
        bool HasPendingChanges { get; }

        // @property (getter = isCollaborative, readwrite, nonatomic) BOOL collaborative;
        [Export("collaborative")]
        bool Collaborative { [Bind("isCollaborative")] get; set; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (getter = isMarkedForOfflinePlayback, readwrite, nonatomic) BOOL markedForOfflinePlayback;
        [Export("markedForOfflinePlayback")]
        bool MarkedForOfflinePlayback { [Bind("isMarkedForOfflinePlayback")] get; set; }

        // @property (readonly, getter = isUpdating, nonatomic) BOOL updating;
        [Export("updating")]
        bool Updating { [Bind("isUpdating")] get; }

        // @property (readonly, nonatomic) float offlineDownloadProgress;
        [Export("offlineDownloadProgress")]
        float OfflineDownloadProgress { get; }

        // @property (readonly, nonatomic) sp_playlist_offline_status offlineStatus;
        [Export("offlineStatus")]
        sp_playlist_offline_status OfflineStatus { get; }

        // @property (readonly, nonatomic, strong) SPUser * owner;
        [Export("owner", ArgumentSemantic.Strong)]
        SPUser Owner { get; }

        // @property (readonly, nonatomic) sp_playlist * playlist;
        [Export("playlist")]
        unsafe sp_playlist Playlist { get; }

        // @property (readonly, assign, nonatomic) SPSession * session;
        [Export("session", ArgumentSemantic.Assign)]
        SPSession Session { get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, nonatomic, strong) NSArray * subscribers;
        [Export("subscribers", ArgumentSemantic.Strong)]
        NSObject[] Subscribers { get; }

        // @property (readonly, nonatomic, strong) SPImage * image;
        [Export("image", ArgumentSemantic.Strong)]
        SPImage Image { get; }

        // @property (readwrite, copy, nonatomic) NSString * name;
        [Export("name")]
        string Name { get; set; }

        // @property (readonly, copy, nonatomic) NSString * playlistDescription;
        [Export("playlistDescription")]
        string PlaylistDescription { get; }

        // @property (readonly, copy, atomic) NSArray * items;
        [Export("items", ArgumentSemantic.Copy)]
        ISPPlaylistableItem[] Items { get; }

        // -(void)moveItemsAtIndexes:(NSIndexSet *)indexes toIndex:(NSUInteger)newLocation callback:(SPErrorableOperationCallback)block;
        [Export("moveItemsAtIndexes:toIndex:callback:")]
        void MoveItemsAtIndexes(NSIndexSet indexes, nuint newLocation, SPErrorableOperationCallback block);

        // -(void)addItem:(SPTrack *)item atIndex:(NSUInteger)index callback:(SPErrorableOperationCallback)block;
        [Export("addItem:atIndex:callback:")]
        void AddItem(SPTrack item, nuint index, SPErrorableOperationCallback block);

        // -(void)addItems:(NSArray *)items atIndex:(NSUInteger)index callback:(SPErrorableOperationCallback)block;
        [Export("addItems:atIndex:callback:")]
        void AddItems(ISPPlaylistableItem[] items, nuint index, SPErrorableOperationCallback block);

        // -(void)removeItemAtIndex:(NSUInteger)index callback:(SPErrorableOperationCallback)block;
        [Export("removeItemAtIndex:callback:")]
        void RemoveItemAtIndex(nuint index, SPErrorableOperationCallback block);
    }

    interface ISPPlaylistDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPPlaylistDelegateTool
    {
        [Export("getProtocol")]
        ISPPlaylistDelegate GetProtocol();
    }

    // @protocol SPPlaylistDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPPlaylistDelegate
    {
        // @optional -(void)itemsInPlaylistDidUpdateMetadata:(SPPlaylist *)aPlaylist;
        [Export("itemsInPlaylistDidUpdateMetadata:")]
        void ItemsInPlaylistDidUpdateMetadata(SPPlaylist aPlaylist);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist willRemoveItems:(NSArray *)items atIndexes:(NSIndexSet *)outgoingIndexes;
        [Export("playlist:willRemoveItems:atIndexes:")]
        void PlaylistWillRemoveItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet outgoingIndexes);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist didRemoveItems:(NSArray *)items atIndexes:(NSIndexSet *)theseIndexesArentValidAnymore;
        [Export("playlist:didRemoveItems:atIndexes:")]
        void PlaylistDidRemoveItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet theseIndexesArentValidAnymore);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist willAddItems:(NSArray *)items atIndexes:(NSIndexSet *)theseIndexesArentYetValid;
        [Export("playlist:willAddItems:atIndexes:")]
        void PlaylistWillAddItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet theseIndexesArentYetValid);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist didAddItems:(NSArray *)items atIndexes:(NSIndexSet *)newIndexes;
        [Export("playlist:didAddItems:atIndexes:")]
        void PlaylistDidAddItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet newIndexes);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist willMoveItems:(NSArray *)items atIndexes:(NSIndexSet *)oldIndexes toIndexes:(NSIndexSet *)newIndexes;
        [Export("playlist:willMoveItems:atIndexes:toIndexes:")]
        void PlaylistWillMoveItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet oldIndexes, NSIndexSet newIndexes);

        // @optional -(void)playlist:(SPPlaylist *)aPlaylist didMoveItems:(NSArray *)items atIndexes:(NSIndexSet *)oldIndexes toIndexes:(NSIndexSet *)newIndexes;
        [Export("playlist:didMoveItems:atIndexes:toIndexes:")]
        void PlaylistDidMoveItems(SPPlaylist aPlaylist, ISPPlaylistableItem[] items, NSIndexSet oldIndexes, NSIndexSet newIndexes);

        // @optional -(void)playlistWillChangeItems:(SPPlaylist *)aPlaylist;
        [Export("playlistWillChangeItems:")]
        void PlaylistWillChangeItems(SPPlaylist aPlaylist);

        // @optional -(void)playlistDidChangeItems:(SPPlaylist *)aPlaylist;
        [Export("playlistDidChangeItems:")]
        void PlaylistDidChangeItems(SPPlaylist aPlaylist);
    }

    // @interface SPPlaylistFolder : NSObject
    [BaseType(typeof(NSObject))]
    interface SPPlaylistFolder
    {
        // @property (readonly, nonatomic) sp_uint64 folderId;
        [Export("folderId")]
        ulong FolderId { get; }

        // @property (readonly, copy, nonatomic) NSString * name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, assign, nonatomic) SPPlaylistContainer * parentContainer;
        [Export("parentContainer", ArgumentSemantic.Assign)]
        SPPlaylistContainer ParentContainer { get; }

        // @property (readonly, assign, nonatomic) SPPlaylistFolder * parentFolder;
        [Export("parentFolder", ArgumentSemantic.Assign)]
        SPPlaylistFolder ParentFolder { get; }

        // -(NSArray *)parentFolders;
        [Export("parentFolders")]
        SPPlaylistFolder[] ParentFolders { get; }

        // @property (readonly, nonatomic, strong) NSArray * playlists;
        [Export("playlists", ArgumentSemantic.Strong)]
        SPPlaylist[] Playlists { get; }

        // @property (readonly, assign, nonatomic) SPSession * session;
        [Export("session", ArgumentSemantic.Assign)]
        SPSession Session { get; }
    }

    // @interface SPPlaylistItem : NSObject
    [BaseType(typeof(NSObject))]
    interface SPPlaylistItem
    {
        // @property (readonly, nonatomic, unsafe_unretained) Class itemClass;
        [Export("itemClass", ArgumentSemantic.Assign)]
        Class ItemClass { get; }

        // @property (readonly, nonatomic) NSURL * itemURL;
        [Export("itemURL")]
        NSUrl ItemURL { get; }

        // @property (readonly, nonatomic) sp_linktype itemURLType;
        [Export("itemURLType")]
        sp_linktype ItemURLType { get; }

        // @property (readonly, nonatomic, strong) id<SPPlaylistableItem,SPAsyncLoading> item;
        [Export("item", ArgumentSemantic.Strong)]
        ISPPlaylistableItem Item { get; }

        // @property (readonly, nonatomic, strong) SPUser * creator;
        [Export("creator", ArgumentSemantic.Strong)]
        SPUser Creator { get; }

        // @property (readonly, copy, nonatomic) NSDate * dateAdded;
        [Export("dateAdded", ArgumentSemantic.Copy)]
        NSDate DateAdded { get; }

        // @property (readonly, copy, nonatomic) NSString * message;
        [Export("message")]
        string Message { get; }

        // @property (getter = isUnread, readwrite, nonatomic) BOOL unread;
        [Export("unread")]
        bool Unread { [Bind("isUnread")] get; set; }
    }

    // @interface SPTrack : NSObject <SPPlaylistableItem, SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPTrack : ISPPlaylistableItem, ISPAsyncLoading
    {
        // +(SPTrack *)trackForTrackStruct:(sp_track *)spTrack inSession:(SPSession *)aSession;
        [Static]
        [Export("trackForTrackStruct:inSession:")]
        unsafe SPTrack TrackForTrackStruct(sp_track spTrack, SPSession aSession);

        // +(void)trackForTrackURL:(NSURL *)trackURL inSession:(SPSession *)aSession callback:(void (^)(SPTrack *))block;
        [Static]
        [Export("trackForTrackURL:inSession:callback:")]
        void TrackForTrackURL(NSUrl trackURL, SPSession aSession, Action<SPTrack> block);

        // -(id)initWithTrackStruct:(sp_track *)tr inSession:(SPSession *)aSession;
        [Export("initWithTrackStruct:inSession:")]
        unsafe IntPtr Constructor(sp_track tr, SPSession aSession);

        // -(SPTrack *)playableTrack;
        [Export("playableTrack")]
        SPTrack PlayableTrack { get; }

        // @property (readonly, nonatomic) sp_track_availability availability;
        [Export("availability")]
        sp_track_availability Availability { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, getter = isLocal, nonatomic) BOOL local;
        [Export("local")]
        bool Local { [Bind("isLocal")] get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, nonatomic) sp_track * track;
        [Export("track")]
        unsafe sp_track Track { get; }

        // @property (readwrite, nonatomic) BOOL starred;
        [Export("starred")]
        bool Starred { get; set; }

        // @property (readonly, nonatomic) sp_track_offline_status offlineStatus;
        [Export("offlineStatus")]
        sp_track_offline_status OfflineStatus { get; }

        // @property (readonly, assign, nonatomic) SPSession * session;
        [Export("session", ArgumentSemantic.Assign)]
        SPSession Session { get; }

        // @property (readonly, nonatomic, strong) SPAlbum * album;
        [Export("album", ArgumentSemantic.Strong)]
        SPAlbum Album { get; }

        // @property (readonly, nonatomic, strong) NSArray * artists;
        [Export("artists", ArgumentSemantic.Strong)]
        SPArtist[] Artists { get; }

        // @property (readonly, copy, nonatomic) NSString * consolidatedArtists;
        [Export("consolidatedArtists")]
        string ConsolidatedArtists { get; }

        // @property (readonly, nonatomic) NSUInteger discNumber;
        [Export("discNumber")]
        nuint DiscNumber { get; }

        // @property (readonly, nonatomic) NSTimeInterval duration;
        [Export("duration")]
        double Duration { get; }

        // @property (readonly, copy, nonatomic) NSString * name;
        [Export("name")]
        string Name { get; }

        // @property (readonly, nonatomic) NSUInteger popularity;
        [Export("popularity")]
        nuint Popularity { get; }

        // @property (readonly, nonatomic) NSUInteger trackNumber;
        [Export("trackNumber")]
        nuint TrackNumber { get; }
    }

    // @interface SPPlaylistContainer : NSObject <SPAsyncLoading, SPDelayableAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPPlaylistContainer : ISPAsyncLoading, ISPDelayableAsyncLoading
    {
        // @property (readonly, assign, nonatomic) sp_playlistcontainer * container;
        [Export("container", ArgumentSemantic.Assign)]
        unsafe sp_playlistcontainer Container { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, nonatomic, strong) SPUser * owner;
        [Export("owner", ArgumentSemantic.Strong)]
        SPUser Owner { get; }

        // @property (readonly, nonatomic, strong) NSArray * playlists;
        [Export("playlists", ArgumentSemantic.Strong)]
        SPPlaylist[] Playlists { get; }

        // -(NSArray *)flattenedPlaylists;
        [Export("flattenedPlaylists")]
        SPPlaylist[] FlattenedPlaylists { get; }

        // @property (readonly, assign, nonatomic) SPSession * session;
        [Export("session", ArgumentSemantic.Assign)]
        SPSession Session { get; }

        // -(void)createFolderWithName:(NSString *)name callback:(void (^)(SPPlaylistFolder *, NSError *))block;
        [Export("createFolderWithName:callback:")]
        void CreateFolderWithName(string name, Action<SPPlaylistFolder, NSError> block);

        // -(void)createPlaylistWithName:(NSString *)name callback:(void (^)(SPPlaylist *))block;
        [Export("createPlaylistWithName:callback:")]
        void CreatePlaylistWithName(string name, Action<SPPlaylist> block);

        // -(void)removeItem:(id)playlistOrFolder callback:(SPErrorableOperationCallback)block;
        [Export("removeItem:callback:")]
        void RemoveItem(NSObject playlistOrFolder, SPErrorableOperationCallback block);

        // -(void)moveItem:(id)playlistOrFolder toIndex:(NSUInteger)newIndex ofNewParent:(SPPlaylistFolder *)aParentFolderOrNil callback:(SPErrorableOperationCallback)block;
        [Export("moveItem:toIndex:ofNewParent:callback:")]
        void MoveItem(NSObject playlistOrFolder, nuint newIndex, SPPlaylistFolder aParentFolderOrNil, SPErrorableOperationCallback block);

        // -(void)subscribeToPlaylist:(SPPlaylist *)playlist callback:(SPErrorableOperationCallback)block;
        [Export("subscribeToPlaylist:callback:")]
        void SubscribeToPlaylist(SPPlaylist playlist, SPErrorableOperationCallback block);
    }

    // @interface SPSearch : NSObject <SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPSearch : ISPAsyncLoading
    {
        // +(SPSearch *)searchWithURL:(NSURL *)searchURL inSession:(SPSession *)aSession;
        [Static]
        [Export("searchWithURL:inSession:")]
        SPSearch SearchWithURL(NSUrl searchURL, SPSession aSession);

        // +(SPSearch *)searchWithSearchQuery:(NSString *)searchQuery inSession:(SPSession *)aSession;
        [Static]
        [Export("searchWithSearchQuery:inSession:")]
        SPSearch SearchWithSearchQuery(string searchQuery, SPSession aSession);

        // +(SPSearch *)liveSearchWithSearchQuery:(NSString *)searchQuery inSession:(SPSession *)aSession;
        [Static]
        [Export("liveSearchWithSearchQuery:inSession:")]
        SPSearch LiveSearchWithSearchQuery(string searchQuery, SPSession aSession);

        // -(id)initWithURL:(NSURL *)searchURL inSession:(SPSession *)aSession;
        [Export("initWithURL:inSession:")]
        IntPtr Constructor(NSUrl searchURL, SPSession aSession);

        // -(id)initWithURL:(NSURL *)searchURL pageSize:(NSInteger)size inSession:(SPSession *)aSession;
        [Export("initWithURL:pageSize:inSession:")]
        IntPtr Constructor(NSUrl searchURL, nint size, SPSession aSession);

        // -(id)initWithSearchQuery:(NSString *)searchString inSession:(SPSession *)aSession;
        [Export("initWithSearchQuery:inSession:")]
        IntPtr Constructor(string searchString, SPSession aSession);

        // -(id)initWithSearchQuery:(NSString *)searchString inSession:(SPSession *)aSession type:(sp_search_type)type;
        [Export("initWithSearchQuery:inSession:type:")]
        IntPtr Constructor(string searchString, SPSession aSession, sp_search_type type);

        // -(id)initWithSearchQuery:(NSString *)searchString pageSize:(NSInteger)size inSession:(SPSession *)aSession;
        [Export("initWithSearchQuery:pageSize:inSession:")]
        IntPtr Constructor(string searchString, nint size, SPSession aSession);

        // -(id)initWithSearchQuery:(NSString *)searchString pageSize:(NSInteger)size inSession:(SPSession *)aSession type:(sp_search_type)type;
        [Export("initWithSearchQuery:pageSize:inSession:type:")]
        IntPtr Constructor(string searchString, nint size, SPSession aSession, sp_search_type type);

        // -(BOOL)addAlbumPage;
        [Export("addAlbumPage")]
        bool AddAlbumPage();

        // -(BOOL)addArtistPage;
        [Export("addArtistPage")]
        bool AddArtistPage();

        // -(BOOL)addTrackPage;
        [Export("addTrackPage")]
        bool AddTrackPage();

        // -(BOOL)addPlaylistPage;
        [Export("addPlaylistPage")]
        bool AddPlaylistPage();

        // -(BOOL)addPageForArtists:(BOOL)searchArtist albums:(BOOL)searchAlbum tracks:(BOOL)searchTrack playlists:(BOOL)searchPlaylist;
        [Export("addPageForArtists:albums:tracks:playlists:")]
        bool AddPageForArtists(bool searchArtist, bool searchAlbum, bool searchTrack, bool searchPlaylist);

        // @property (readonly, copy, nonatomic) NSString * suggestedSearchQuery;
        [Export("suggestedSearchQuery")]
        string SuggestedSearchQuery { get; }

        // @property (readonly, nonatomic) BOOL hasExhaustedAlbumResults;
        [Export("hasExhaustedAlbumResults")]
        bool HasExhaustedAlbumResults { get; }

        // @property (readonly, nonatomic) BOOL hasExhaustedArtistResults;
        [Export("hasExhaustedArtistResults")]
        bool HasExhaustedArtistResults { get; }

        // @property (readonly, nonatomic) BOOL hasExhaustedTrackResults;
        [Export("hasExhaustedTrackResults")]
        bool HasExhaustedTrackResults { get; }

        // @property (readonly, nonatomic) BOOL hasExhaustedPlaylistResults;
        [Export("hasExhaustedPlaylistResults")]
        bool HasExhaustedPlaylistResults { get; }

        // @property (readonly, nonatomic, strong) NSArray * albums;
        [Export("albums", ArgumentSemantic.Strong)]
        SPAlbum[] Albums { get; }

        // @property (readonly, nonatomic, strong) NSArray * artists;
        [Export("artists", ArgumentSemantic.Strong)]
        SPArtist[] Artists { get; }

        // @property (readonly, nonatomic, strong) NSArray * tracks;
        [Export("tracks", ArgumentSemantic.Strong)]
        SPTrack[] Tracks { get; }

        // @property (readonly, nonatomic, strong) NSArray * playlists;
        [Export("playlists", ArgumentSemantic.Strong)]
        SPPlaylist[] Playlists { get; }

        // @property (readonly, copy, nonatomic) NSError * searchError;
        [Export("searchError", ArgumentSemantic.Copy)]
        NSError SearchError { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, copy, nonatomic) NSString * searchQuery;
        [Export("searchQuery")]
        string SearchQuery { get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, copy, nonatomic) NSURL * spotifyURL;
        [Export("spotifyURL", ArgumentSemantic.Copy)]
        NSUrl SpotifyURL { get; }

        // @property (readonly, nonatomic) sp_search_type searchType;
        [Export("searchType")]
        sp_search_type SearchType { get; }
    }

    // @interface SPPostTracksToInboxOperation : NSObject
    [BaseType(typeof(NSObject))]
    interface SPPostTracksToInboxOperation
    {
        // +(SPPostTracksToInboxOperation *)sendTracks:(NSArray *)tracksToSend toUser:(NSString *)user message:(NSString *)aFriendlyGreeting inSession:(SPSession *)aSession callback:(SPErrorableOperationCallback)block;
        [Static]
        [Export("sendTracks:toUser:message:inSession:callback:")]
        SPPostTracksToInboxOperation SendTracks(SPTrack[] tracksToSend, string user, string aFriendlyGreeting, SPSession aSession, SPErrorableOperationCallback block);

        // -(id)initBySendingTracks:(NSArray *)tracksToSend toUser:(NSString *)user message:(NSString *)aFriendlyGreeting inSession:(SPSession *)aSession callback:(SPErrorableOperationCallback)block;
        [Export("initBySendingTracks:toUser:message:inSession:callback:")]
        IntPtr Constructor(SPTrack[] tracksToSend, string user, string aFriendlyGreeting, SPSession aSession, SPErrorableOperationCallback block);

        // @property (readonly, copy, nonatomic) NSString * destinationUser;
        [Export("destinationUser")]
        string DestinationUser { get; }

        // @property (readonly, assign, nonatomic) sp_inbox * inboxOperation;
        [Export("inboxOperation", ArgumentSemantic.Assign)]
        unsafe sp_inbox InboxOperation { get; }

        // @property (readonly, copy, nonatomic) NSString * message;
        [Export("message")]
        string Message { get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, copy, nonatomic) NSArray * tracks;
        [Export("tracks", ArgumentSemantic.Copy)]
        SPTrack[] Tracks { get; }
    }

    // @interface SPArtistBrowse : NSObject <SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPArtistBrowse : ISPAsyncLoading
    {
        // +(SPArtistBrowse *)browseArtist:(SPArtist *)anArtist inSession:(SPSession *)aSession type:(sp_artistbrowse_type)browseMode;
        [Static]
        [Export("browseArtist:inSession:type:")]
        SPArtistBrowse BrowseArtist(SPArtist anArtist, SPSession aSession, sp_artistbrowse_type browseMode);

        // +(void)browseArtistAtURL:(NSURL *)artistURL inSession:(SPSession *)aSession type:(sp_artistbrowse_type)browseMode callback:(void (^)(SPArtistBrowse *))block;
        [Static]
        [Export("browseArtistAtURL:inSession:type:callback:")]
        void BrowseArtistAtURL(NSUrl artistURL, SPSession aSession, sp_artistbrowse_type browseMode, Action<SPArtistBrowse> block);

        // -(id)initWithArtist:(SPArtist *)anArtist inSession:(SPSession *)aSession type:(sp_artistbrowse_type)browseMode;
        [Export("initWithArtist:inSession:type:")]
        IntPtr Constructor(SPArtist anArtist, SPSession aSession, sp_artistbrowse_type browseMode);

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, copy, nonatomic) NSError * loadError;
        [Export("loadError", ArgumentSemantic.Copy)]
        NSError LoadError { get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, nonatomic, strong) NSArray * albums;
        [Export("albums", ArgumentSemantic.Strong)]
        SPAlbum[] Albums { get; }

        // @property (readonly, nonatomic, strong) SPArtist * artist;
        [Export("artist", ArgumentSemantic.Strong)]
        SPArtist Artist { get; }

        // @property (readonly, copy, nonatomic) NSString * biography;
        [Export("biography")]
        string Biography { get; }

        // @property (readonly, nonatomic, strong) SPImage * firstPortrait;
        [Export("firstPortrait", ArgumentSemantic.Strong)]
        SPImage FirstPortrait { get; }

        // @property (readonly, nonatomic, strong) NSArray * portraits;
        [Export("portraits", ArgumentSemantic.Strong)]
        SPImage[] Portraits { get; }

        // @property (readonly, nonatomic, strong) NSArray * relatedArtists;
        [Export("relatedArtists", ArgumentSemantic.Strong)]
        SPArtist[] RelatedArtists { get; }

        // @property (readonly, nonatomic, strong) NSArray * tracks;
        [Export("tracks", ArgumentSemantic.Strong)]
        SPTrack[] Tracks { get; }

        // @property (readonly, nonatomic, strong) NSArray * topTracks;
        [Export("topTracks", ArgumentSemantic.Strong)]
        SPTrack[] TopTracks { get; }
    }

    // @interface SPAlbumBrowse : NSObject <SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPAlbumBrowse : ISPAsyncLoading
    {
        // +(SPAlbumBrowse *)browseAlbum:(SPAlbum *)anAlbum inSession:(SPSession *)aSession;
        [Static]
        [Export("browseAlbum:inSession:")]
        SPAlbumBrowse BrowseAlbum(SPAlbum anAlbum, SPSession aSession);

        // +(void)browseAlbumAtURL:(NSURL *)albumURL inSession:(SPSession *)aSession callback:(void (^)(SPAlbumBrowse *))block;
        [Static]
        [Export("browseAlbumAtURL:inSession:callback:")]
        void BrowseAlbumAtURL(NSUrl albumURL, SPSession aSession, Action<SPAlbumBrowse> block);

        // -(id)initWithAlbum:(SPAlbum *)anAlbum inSession:(SPSession *)aSession;
        [Export("initWithAlbum:inSession:")]
        IntPtr Constructor(SPAlbum anAlbum, SPSession aSession);

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, copy, nonatomic) NSError * loadError;
        [Export("loadError", ArgumentSemantic.Copy)]
        NSError LoadError { get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, nonatomic, strong) SPAlbum * album;
        [Export("album", ArgumentSemantic.Strong)]
        SPAlbum Album { get; }

        // @property (readonly, nonatomic, strong) SPArtist * artist;
        [Export("artist", ArgumentSemantic.Strong)]
        SPArtist Artist { get; }

        // @property (readonly, nonatomic, strong) NSArray * copyrights;
        [Export("copyrights", ArgumentSemantic.Strong)]
        string[] Copyrights { get; }

        // @property (readonly, copy, nonatomic) NSString * review;
        [Export("review")]
        string Review { get; }

        // @property (readonly, nonatomic, strong) NSArray * tracks;
        [Export("tracks", ArgumentSemantic.Strong)]
        SPTrack[] Tracks { get; }
    }

    // @interface SPToplist : NSObject <SPAsyncLoading>
    [BaseType(typeof(NSObject))]
    interface SPToplist : ISPAsyncLoading
    {
        // +(SPToplist *)globalToplistInSession:(SPSession *)aSession;
        [Static]
        [Export("globalToplistInSession:")]
        SPToplist GlobalToplistInSession(SPSession aSession);

        // +(SPToplist *)toplistForLocale:(NSLocale *)toplistLocale inSession:(SPSession *)aSession;
        [Static]
        [Export("toplistForLocale:inSession:")]
        SPToplist ToplistForLocale(NSLocale toplistLocale, SPSession aSession);

        // +(SPToplist *)toplistForUserWithName:(NSString *)user inSession:(SPSession *)aSession;
        [Static]
        [Export("toplistForUserWithName:inSession:")]
        SPToplist ToplistForUserWithName(string user, SPSession aSession);

        // +(SPToplist *)toplistForCurrentUserInSession:(SPSession *)aSession;
        [Static]
        [Export("toplistForCurrentUserInSession:")]
        SPToplist ToplistForCurrentUserInSession(SPSession aSession);

        // -(id)initLocaleToplistWithLocale:(NSLocale *)toplistLocale inSession:(SPSession *)aSession;
        [Export("initLocaleToplistWithLocale:inSession:")]
        IntPtr Constructor(NSLocale toplistLocale, SPSession aSession);

        // -(id)initUserToplistWithUsername:(NSString *)user inSession:(SPSession *)aSession;
        [Export("initUserToplistWithUsername:inSession:")]
        IntPtr Constructor(string user, SPSession aSession);

        // @property (readonly, nonatomic, strong) NSArray * albums;
        [Export("albums", ArgumentSemantic.Strong)]
        SPAlbum[] Albums { get; }

        // @property (readonly, nonatomic, strong) NSArray * artists;
        [Export("artists", ArgumentSemantic.Strong)]
        SPArtist[] Artists { get; }

        // @property (readonly, getter = isLoaded, nonatomic) BOOL loaded;
        [Export("loaded")]
        bool Loaded { [Bind("isLoaded")] get; }

        // @property (readonly, copy, nonatomic) NSError * loadError;
        [Export("loadError", ArgumentSemantic.Copy)]
        NSError LoadError { get; }

        // @property (readonly, nonatomic, strong) NSLocale * locale;
        [Export("locale", ArgumentSemantic.Strong)]
        NSLocale Locale { get; }

        // @property (readonly, nonatomic, strong) NSArray * tracks;
        [Export("tracks", ArgumentSemantic.Strong)]
        SPTrack[] Tracks { get; }

        // @property (readonly, nonatomic, strong) SPSession * session;
        [Export("session", ArgumentSemantic.Strong)]
        SPSession Session { get; }

        // @property (readonly, copy, nonatomic) NSString * username;
        [Export("username")]
        string Username { get; }
    }

    // @interface SPUnknownPlaylist : SPPlaylist
    [BaseType(typeof(SPPlaylist))]
    interface SPUnknownPlaylist
    {
    }

    // @interface SPCircularBuffer : NSObject
    [BaseType(typeof(NSObject))]
    interface SPCircularBuffer
    {
        // -(id)initWithMaximumLength:(NSUInteger)size;
        [Export("initWithMaximumLength:")]
        IntPtr Constructor(nuint size);

        // -(void)clear;
        [Export("clear")]
        void Clear();

        //// -(NSUInteger)attemptAppendData:(const void *)data ofLength:(NSUInteger)dataLength;
        //[Export("attemptAppendData:ofLength:")]
        //unsafe nuint AttemptAppendData(void* data, nuint dataLength);

        //// -(NSUInteger)attemptAppendData:(const void *)data ofLength:(NSUInteger)dataLength chunkSize:(NSUInteger)chunkSize;
        //[Export("attemptAppendData:ofLength:chunkSize:")]
        //unsafe nuint AttemptAppendData(void* data, nuint dataLength, nuint chunkSize);

        // -(NSUInteger)readDataOfLength:(NSUInteger)desiredLength intoAllocatedBuffer:(void **)outBuffer;
        //[Export("readDataOfLength:intoAllocatedBuffer:")]
        //unsafe nuint ReadDataOfLength(nuint desiredLength, void** outBuffer);

        // @property (readonly) NSUInteger length;
        [Export("length")]
        nuint Length { get; }

        // @property (readonly, nonatomic) NSUInteger maximumLength;
        [Export("maximumLength")]
        nuint MaximumLength { get; }
    }

    interface ISPCoreAudioControllerDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPCoreAudioControllerDelegateTool
    {
        [Export("getProtocol")]
        ISPCoreAudioControllerDelegate GetProtocol();
    }

    // @protocol SPCoreAudioControllerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPCoreAudioControllerDelegate
    {
        // @required -(void)coreAudioController:(SPCoreAudioController *)controller didOutputAudioOfDuration:(NSTimeInterval)audioDuration;
        [Abstract]
        [Export("coreAudioController:didOutputAudioOfDuration:")]
        void DidOutputAudioOfDuration(SPCoreAudioController controller, double audioDuration);
    }

    // @interface SPCoreAudioController : NSObject <SPSessionAudioDeliveryDelegate>
    [BaseType(typeof(NSObject))]
    interface SPCoreAudioController : ISPSessionAudioDeliveryDelegate
    {
        // -(void)clearAudioBuffers;
        [Export("clearAudioBuffers")]
        void ClearAudioBuffers();

        //// -(BOOL)connectOutputBus:(UInt32)sourceOutputBusNumber ofNode:(AUNode)sourceNode toInputBus:(UInt32)destinationInputBusNumber ofNode:(AUNode)destinationNode inGraph:(AUGraph)graph error:(NSError **)error;
        //[Export("connectOutputBus:ofNode:toInputBus:ofNode:inGraph:error:")]
        //unsafe bool ConnectOutputBus(uint sourceOutputBusNumber, int sourceNode, uint destinationInputBusNumber, int destinationNode, AUGraph graph, out NSError error);

        //// -(void)disposeOfCustomNodesInGraph:(AUGraph)graph;
        //[Export("disposeOfCustomNodesInGraph:")]
        //unsafe void DisposeOfCustomNodesInGraph(AUGraph graph);

        // @property (readwrite, nonatomic) double volume;
        [Export("volume")]
        double Volume { get; set; }

        // @property (readwrite, nonatomic) BOOL audioOutputEnabled;
        [Export("audioOutputEnabled")]
        bool AudioOutputEnabled { get; set; }

        [Wrap("WeakDelegate")]
        SPCoreAudioControllerDelegate Delegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPCoreAudioControllerDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }
    }

    interface ISPPlaybackManagerDelegate
    {

    }

    [BaseType(typeof(NSObject))]
    interface SPPlaybackManagerDelegateTool
    {
        [Export("getProtocol")]
        ISPPlaybackManagerDelegate GetProtocol();
    }

    // @protocol SPPlaybackManagerDelegate <NSObject>
    [Protocol, Model]
    [BaseType(typeof(NSObject))]
    interface SPPlaybackManagerDelegate
    {
        // @required -(void)playbackManagerWillStartPlayingAudio:(SPPlaybackManager *)aPlaybackManager;
        [Abstract]
        [Export("playbackManagerWillStartPlayingAudio:")]
        void PlaybackManagerWillStartPlayingAudio(SPPlaybackManager aPlaybackManager);
    }

    // @interface SPPlaybackManager : NSObject <SPSessionPlaybackDelegate, SPCoreAudioControllerDelegate>
    [BaseType(typeof(NSObject))]
    interface SPPlaybackManager : ISPSessionPlaybackDelegate, ISPCoreAudioControllerDelegate
    {
        // -(id)initWithPlaybackSession:(SPSession *)aSession;
        [Export("initWithPlaybackSession:")]
        IntPtr Constructor(SPSession aSession);

        // -(id)initWithAudioController:(SPCoreAudioController *)aController playbackSession:(SPSession *)aSession;
        [Export("initWithAudioController:playbackSession:")]
        IntPtr Constructor(SPCoreAudioController aController, SPSession aSession);

        // @property (readonly, nonatomic, strong) SPTrack * currentTrack;
        [Export("currentTrack", ArgumentSemantic.Strong)]
        SPTrack CurrentTrack { get; }

        [Wrap("WeakDelegate")]
        SPPlaybackManagerDelegate Delegate { get; set; }

        // @property (assign, readwrite, nonatomic) id<SPPlaybackManagerDelegate> delegate;
        [NullAllowed, Export("delegate", ArgumentSemantic.Assign)]
        NSObject WeakDelegate { get; set; }

        // @property (readonly, nonatomic, strong) SPSession * playbackSession;
        [Export("playbackSession", ArgumentSemantic.Strong)]
        SPSession PlaybackSession { get; }

        // @property (readwrite) BOOL isPlaying;
        [Export("isPlaying")]
        bool IsPlaying { get; set; }

        // -(void)playTrack:(SPTrack *)aTrack callback:(SPErrorableOperationCallback)block;
        [Export("playTrack:callback:")]
        void PlayTrack(SPTrack aTrack, SPErrorableOperationCallback block);

        // -(void)seekToTrackPosition:(NSTimeInterval)newPosition;
        [Export("seekToTrackPosition:")]
        void SeekToTrackPosition(double newPosition);

        // @property (readonly) NSTimeInterval trackPosition;
        [Export("trackPosition")]
        double TrackPosition { get; }

        // @property (readwrite) double volume;
        [Export("volume")]
        double Volume { get; set; }
    }

}