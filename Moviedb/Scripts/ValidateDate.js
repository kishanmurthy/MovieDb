$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || window.Globalize.parseDate(value, "dd.MM.yyyy") !== null;
    }
});