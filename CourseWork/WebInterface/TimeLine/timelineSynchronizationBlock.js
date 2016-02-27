twittyApp.factory('TimelineSynchronizationBlock', function ($cookies, TweetViewBuilder) {
    function TimelineSynchronizationBlock() {
        this.allIsLoaded = false;
        this.loading = false;
        this.firstTime = true;
        this.scrollMarker = 100;
        this.loadedUnshownPages = 0;
        this.timeLineHiader = $cookies.get("lastReadedTweetId");
    }

    return TimelineSynchronizationBlock;
});