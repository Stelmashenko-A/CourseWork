twittyApp.factory('TweetViewBuilder', function () {
function TweetViewBuilder() {
    this.pattern1 = "<div class=\"well tweet-well\">"+
                    "<div class=\"row\">"+
                        "<div class=\"twitt\">"+
                            "<div class=\"twit-text\">"+
                                "<span>";
    this.pattern2 = "</span>"+
                            "</div>"+
                            "<div class=\"row\">"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-bullhorn\"></i></p>"+
                                "</div>"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-retweet\"></i></p>"+
                                "</div>"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-star\"></i></p>"+
                                "</div>"+
                            "</div>"+
                        "</div>"+
                    "</div>"+
                "</div>";
}
    TweetViewBuilder.buildHtml = function (text) {
        var res ="<div class=\"well tweet-well\">"+
                    "<div class=\"row\">"+
                        "<div class=\"twitt\">"+
                            "<div class=\"twit-text\">"+
                                "<span>" + text +  "</span>"+
                            "</div>"+
                            "<div class=\"row\">"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-bullhorn\"></i></p>"+
                                "</div>"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-retweet\"></i></p>"+
                                "</div>"+
                                "<div class=\"col-xs-1 tweet-btm\">"+
                                    "<p><i class=\"glyphicon glyphicon-star\"></i></p>"+
                                "</div>"+
                            "</div>"+
                        "</div>"+
                    "</div>"+
                "</div>";
    
        return res;
    };   
    return TweetViewBuilder;
});