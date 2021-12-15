function clearAirlineForm() {
    return {
        "name": setValue("Name")
    }
}

function clearAirportForm() {
    return {
        "name": setValue("Name")
    }
}

function clearFlightForm() {
    return {
        "id": setValue("Id"),
        "airlinename": setValue("AirlineName"),
        "origin": setValue("Origin", ""),
        "destination": setValue("Destination"),
        "date": setValue("Date")
    }
}
function clearSectionForm() {
    return {
        "id": setValue("Id"),
        "airlinename": setValue("AirlineName"),
        "rows": setValue("Rows"),
        "columns": setValue("Columns"),
        "seatclass": setValue("SeatClass")
    }
}