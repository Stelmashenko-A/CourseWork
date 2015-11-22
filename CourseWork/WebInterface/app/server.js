var http = require('http');

var server = http.createServer(function (req, res) {
    var url = 'http://localhost:12008/auth' + req.url;
    //Lets configure and request
    var http = require('http');
    http.post = require('http-post');


    http.post(url, function (res) {
        res.on('data', function (chunk) {
            res.end('Hello World!\n');
        });
    });


    res.writeHead(200, { 'Content-Type': 'text/plain' });
    req;


});
server.listen(1400, 'localhost');