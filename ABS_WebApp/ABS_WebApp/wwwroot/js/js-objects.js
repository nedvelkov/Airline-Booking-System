function getAirline() {
    return {
        "name": getValue("Name")
    }
}

function getAirport() {
    return {
        "name": getValue("Name")
    }
}

function getFlight() {
    return {
        "id": getValue("Id"),
        "airlinename":getValue("AirlineName"),
        "origin": getValue("Origin"),
        "destination": getValue("Destination"),
        "date": getValue("Date")
    }
}

function getSection() {
    return {
        "id": getValue("Id"),
        "airlinename": getValue("AirlineName"),
        "rows": getValue("Rows"),
        "columns": getValue("Columns"),
        "seatclass": getValue("SeatClass")
    }
}