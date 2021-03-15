// console.log("Hello World");
// var a = 10;
// var b = 10;
// console.log(a + b);
// module : components

var http = require('http')

http.createServer(function (req, res) {

    res.writeHead(200, { 'Content-Type': 'text/html' });
    res.write("Welcome to the api");
    res.end();

}).listen(8080);