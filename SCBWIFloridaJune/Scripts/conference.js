$(document).ready(function() {
    $('#intensivebox').hide();

    $('#Type').change(function () {
        $('#intensivebox').hide('fast');

        if ($('#Type').val() == "Early Member Price" || $('#Type').val() == "Late Member Price") {
            $('#intensivebox').show('fast');
        }
    });

    $('#form').submit(function () {
        var error = false;

        $('#errorbox').html("");

        if ($('#Type').val() == "") {
            error = true;
            $('#errorbox').append("<span>You must select a registration type!</span><br />");
        }
        
        if ($('#Track').val() == "") {
            error = true;
            $('#errorbox').append("<span>You must select a track!</span><br />");
        }
        
        if ($('#SaturdayLunch').val() == "") {
            error = true;
            $('#errorbox').append("<span>You must select a lunch on saturday!</span><br />");
        }
        
        if ($('#Intensive').val() != "" && $('#FridayLunch').val() == "") {
            error = true;
            $('#errorbox').append("<span>If you choose to attend an intensive, you must select a lunch!</span><br />");
        }

        if (error == true) {
            $('#errorbox').show('fast');
            return false;
        }

        return true;
    });
});