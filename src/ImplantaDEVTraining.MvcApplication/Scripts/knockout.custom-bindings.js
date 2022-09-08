ko.bindingHandlers.datePicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        // Register change callbacks to update the model
        // if the control changes.
        ko.utils.registerEventHandler(element, "change", function () {
            var value = valueAccessor();
            value(moment(element.value).format());
        });
    },
    // Update the control whenever the view model changes
    update: function (element, valueAccessor, allBindingsAccessor, viewModel) {

        const primeiroAnoValido = 1901;

        var value = valueAccessor();

        if (!value || !value()) {
            element.value = null;
            return;
        }

        if (value().substring(0, 5) === '/Date') {
            element.value = moment(value()).format("YYYY-MM-DD");
            return;
        }

        if (parseInt(value().substring(0, 4)) > primeiroAnoValido) {
            element.value = moment(value()).format("YYYY-MM-DD");
        }
    }
};