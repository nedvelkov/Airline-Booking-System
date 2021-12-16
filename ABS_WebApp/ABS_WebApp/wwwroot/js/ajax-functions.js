function createAirline() {
    $('form').submit(function () {
        event.preventDefault();
        let arilineModel = getAirline();

        $.ajax({
            type: "POST",
            url: "https://localhost:5001/api/airline",
            contentType: 'application/json',
            data: JSON.stringify(arilineModel)
        })
            .done(function (data) {
                clearAirlineForm();
                $('#result').text(data);
            })
            .fail(function (error) {
                $('#result').text(error.responseText);
            })
            .always(function () {
                setResult();
            });
    });
}

function createFlight() {
    $('form').submit(function () {
        event.preventDefault();
        let getFlightModel = getFlight();
        let airlineName = getValue("AirlineName");
        let urlFlightApi = "https://localhost:5001/api/airline/" + airlineName + "/flight";
        
        $.ajax({
            type: "POST",
            url: urlFlightApi,
            contentType: 'application/json',
            data: JSON.stringify(getFlightModel)
        })
            .done(function (data) {
                clearFlightForm();
                $('#result').text(data);
            })
            .fail(function (error) {
                $('#result').text(error.responseText);
            })
            .always(function () {
                setResult();
            });
    });
}

function createFlightSection() {
    $('form').submit(function () {
        event.preventDefault();
        let getFlightModel = getSection();
        let airlineName = getValue("AirlineName");
        let flightId = getValue("Id");
        let urlApi = "https://localhost:5001/api/airline/" + airlineName + "/flight/" + flightId + "/section";
        
        $.ajax({
            type: "POST",
            url: urlApi,
            contentType: 'application/json',
            data: JSON.stringify(getFlightModel)
        })
            .done(function (data) {
                clearSectionForm();
                $('#result').text(data);
            })
            .fail(function (error) {
                $('#result').text(error.responseText);
            })
            .always(function () {
                setResult();
            });
    });
}

function setResult() {
    let element = $('#result');
    let text = element.text();
    if (text.includes('successfully')) {
        element.removeClass('text-danger')
            .addClass('text-success');
    } else {
        element.removeClass('text-success')
            .addClass('text-danger');
    }
}