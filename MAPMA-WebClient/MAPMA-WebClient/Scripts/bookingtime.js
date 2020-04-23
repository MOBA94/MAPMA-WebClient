/// <reference path="jquery-3.4.1.min.js" />

function populate(selector) {
    var select = $(selector);
    var hours, minutes, ampm;
    
    for (var i = 960; i <= 1330; i += 30) {
        hours = Math.floor(i / 60);
        minutes = i % 60;
        if (minutes < 10) {
            minutes = '0' + minutes; // adding leading zero to minutes portion
        }
        //add the value to dropdownlist
        select.append($('<option></option>')
            .attr('value', hours)
            .text(hours + ':' + minutes));
    }
}
//Calling the function on pageload

populate('#timeDropdownlist');