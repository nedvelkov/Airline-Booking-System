function getValue(id) {
    return document.getElementById(id).value;
}

function setValue(id, value=null) {
    document.getElementById(id).value = value;
}