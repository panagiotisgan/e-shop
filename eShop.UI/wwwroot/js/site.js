// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function enableSubmit() {
    //παιρνω την τιμή του robot κάθε φορά που το κλικάρω και ανάλογα αν είναι ή όχι κλικαρισμένο 
    // θα βάλω απο κάτω enable ή disable to submit buttom
    var isChecked = $('#robot').is(":checked");
    if (isChecked == true) {
        $('.btn-primary').prop('disabled', false);
    }
    else if (isChecked == false) {
        $('.btn-primary').prop('disabled', true);
    }
}

