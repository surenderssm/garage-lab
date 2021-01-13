import http from 'k6/http';

export let options = { vus: 5, duration: '10s' }

export default function () {

    var options = {
        headers: { 'traceid': 'hello' },
    };
    http.get('http://localhost:5000/test/testlog', options);
    http.get('http://localhost:5000/test/TestLogException', options);
    http.get('http://localhost:5000/test/TestLogScope1', options);
    // http.get('http://localhost:5000/testmetric/LogMetric', options);
}