$(document).ready(function () {
    $('#airportLabel').on("click", function () {
        $('#airports').toggle();
    });

    $('#airlineLabel').on("click", function () {
       $('#airlineLabel').children('i').removeClass('.arrow-right').addClass('.arrow-down');
       $('#airlines').toggle();
    });

    $('.airlineTitle').click(function () {
        $(this).next('.flights').toggle();
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

    $('#result:contains("successfully")').removeClass('text-danger')
        .addClass('text-success');

    $('input').focusin(function () {
        $('#result').empty();
    })

});

