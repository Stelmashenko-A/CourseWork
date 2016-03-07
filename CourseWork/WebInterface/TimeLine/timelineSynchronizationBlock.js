twittyApp.factory('TimelineSynchronizationBlock', function ($cookies, TweetViewBuilder) {
    function TimelineSynchronizationBlock() {
        this.allIsLoaded = false;
        this.loading = false;
        this.firstTime = true;
        this.scrollMarker = 100;
        this.loadedUnshownPages = 0;
        this.timeLineHiader = $cookies.get("lastReadedTweetId");
        this.sendedToServer = 0;
        this.tweetCounter = 0;
        this.maxShownId = 0;
        
        this.lastUpdate = new Date();
        this.notUpdatedItems = 0; 
    }
    

    return TimelineSynchronizationBlock;
});