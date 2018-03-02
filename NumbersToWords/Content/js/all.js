// @koala-prepend "vendor/_pickaday.js";

(function () {
    'use strict';

    function formatDate(date) {
        if (!date) {
            return '';
        }
        var d = new Date(date),
            month = '' + (d.getMonth() + 1),
            day = '' + d.getDate(),
            year = d.getFullYear();

        if (month.length < 2) month = '0' + month;
        if (day.length < 2) day = '0' + day;

        return [year, month, day].join('/');
    }

    (function setupForm() {
        var dateElement = document.getElementById('datepicker');

        var minDate = new Date(dateElement.min);
        var maxDate = new Date(dateElement.max);

        var options = {
            defaultDate: formatDate(new Date()),
            field: dateElement,
            minDate: minDate,
            maxDate: maxDate,
            yearRange: [minDate.getFullYear(), maxDate.getFullYear()],
            toString: formatDate
        };

        var picker = new Pikaday(options);
        picker.setDate(''); // to show placeholder
    }());
}());
