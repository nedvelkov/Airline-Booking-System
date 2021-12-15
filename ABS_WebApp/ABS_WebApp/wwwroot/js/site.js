$(document).ready(function () {
    $('#airportLabel').on("click", function () {
        let airports = $('#airports');
        airports.toggle();

        changeArrow($(this), airports);
    });

    $('#airlineLabel').on("click", function () {
        let airlines = $('#airlines');
        airlines.toggle();
        let arrow = $(this).children('i');

        changeArrow($(this), airlines);
    });

    $('.airlineTitle').click(function () {
        let flights = $(this).next('.flights');
        flights.toggle();
        changeArrow($(this), flights);
    })

    $('.flightTitle').click(function () {
        $(this).next('.sections').toggle();
    })

    $('.flightSectionTitile').click(function () {
        let seats = $(this).next('.seats');
        seats.toggle();
        let allSeats = $('.seats').is(function () {
            return $(this).is(':visible');
        });
        if (allSeats) {
            $('#container').removeClass('col-md-6 offset-md-3');
        } else {
            $('#container').addClass('col-md-6 offset-md-3');
        }
    })

    $('input').focusin(function () {
        $('#result').empty();
    })

    //TODO: Delete after code review
    $('#result:contains("successfully")').removeClass('text-danger')
        .addClass('text-success');

    function changeArrow(clickItem, element) {

        let arrow = clickItem.children('i');

        if (element.is(":visible")) {
            arrow.removeClass('arrow-right');
            arrow.addClass('arrow-down');
        } else {
            arrow.removeClass('arrow-down');
            arrow.addClass('arrow-right');
        }
    }
});

