const express = require('express');
const app = express();

app.get("/", function (req, res) {
    res.send("hello-world")
});

app.get("/ping", function (req, res) {
    res.send("pong")
});

app.get("/echo/:id", function (req, res) {
    res.send("echo" + req.params.id);
});

app.get("/echo", function (req, res) {
    res.send("echo" + req.query.id);
});


app.listen(9000, function (req, res) {
    console.log('running..');
});

