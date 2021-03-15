function add(a, b) {
    return a + b;
}

function sub(a, b) {
    return a - b;
}

// this works
module.exports.add = add

// this also works : behind hte scene short hand exports = module.exports
exports.sub = sub

exports.multiply = function multiply(a, b) {
    return a * b;
}








